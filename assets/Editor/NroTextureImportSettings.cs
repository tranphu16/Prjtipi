using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Keeps the original NRO raster assets pixel-accurate on every build target.
/// The policy is intentionally limited to Assets/Resources/res so unrelated
/// Unity UI, fonts, plug-ins and third-party artwork retain their own settings.
/// </summary>
public sealed class NroTextureImportSettings : AssetPostprocessor
{
    private const string ResourceRoot = "Assets/Resources/res/";
    private const int LosslessMaxDimension = 512;
    private const int MaxSourceDimension = 4096;

    private void OnPreprocessTexture()
    {
        if (!assetPath.StartsWith(ResourceRoot, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        TextureImporter importer = (TextureImporter)assetImporter;
        int sourceWidth;
        int sourceHeight;
        importer.GetSourceTextureWidthAndHeight(out sourceWidth, out sourceHeight);

        int sourceMax = Mathf.Max(sourceWidth, sourceHeight);
        int requiredMaxSize = Mathf.Clamp(Mathf.NextPowerOfTwo(sourceMax), 32, MaxSourceDimension);
        bool useLosslessCompression = sourceMax <= LosslessMaxDimension;

        importer.filterMode = FilterMode.Point;
        importer.mipmapEnabled = false;
        importer.anisoLevel = 0;
        importer.npotScale = TextureImporterNPOTScale.None;
        importer.wrapMode = TextureWrapMode.Clamp;
        importer.maxTextureSize = requiredMaxSize;
        importer.textureCompression = useLosslessCompression
            ? TextureImporterCompression.Uncompressed
            : TextureImporterCompression.CompressedHQ;
        importer.crunchedCompression = false;

        ApplyPlatform(importer, "Standalone", requiredMaxSize, useLosslessCompression);
        ApplyPlatform(importer, "Android", requiredMaxSize, useLosslessCompression);
        ApplyPlatform(importer, "iPhone", requiredMaxSize, useLosslessCompression);
    }

    private static void ApplyPlatform(
        TextureImporter importer,
        string platform,
        int maxTextureSize,
        bool useLosslessCompression)
    {
        TextureImporterPlatformSettings settings = importer.GetPlatformTextureSettings(platform);
        settings.name = platform;
        settings.overridden = true;
        settings.maxTextureSize = maxTextureSize;
        settings.resizeAlgorithm = TextureResizeAlgorithm.Mitchell;
        settings.format = TextureImporterFormat.Automatic;
        settings.textureCompression = useLosslessCompression
            ? TextureImporterCompression.Uncompressed
            : TextureImporterCompression.CompressedHQ;
        settings.compressionQuality = 100;
        settings.crunchedCompression = false;
        importer.SetPlatformTextureSettings(settings);
    }

    [MenuItem("TiPi/Graphics/Apply NRO Sharp Texture Settings")]
    public static void ApplyAll()
    {
        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { ResourceRoot.TrimEnd('/') });
        int imported = 0;

        try
        {
            for (int index = 0; index < guids.Length; index++)
            {
                string guid = guids[index];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (!path.StartsWith(ResourceRoot, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                EditorUtility.DisplayProgressBar(
                    "TiPi NRO texture settings",
                    path,
                    (float)index / guids.Length);
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
                imported++;
            }
        }
        finally
        {
            EditorUtility.ClearProgressBar();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        Debug.Log("[TiPi Graphics] Applied sharp texture settings to " + imported + " NRO textures.");
    }
}
