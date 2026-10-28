using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class SharpnessBuildValidation
{
    private const string ReportFile = "_codex_reports/sharpness_build_report.txt";

    [MenuItem("TiPi/Graphics/Validate Sharpness And Build All")]
    public static void RunAll()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(ReportFile));

        List<string> report = new List<string>();
        report.Add("TiPi NRO sharpness validation - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        report.Add(ValidateTextureImports());

        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();
        if (scenes.Length == 0)
        {
            scenes = new[] { "Assets/Scenes/SampleScene.unity" };
        }

        string buildRoot = Path.Combine(Directory.GetCurrentDirectory(), "Build", "Sharpness");
        Directory.CreateDirectory(buildRoot);

        BuildAndRecord(
            report,
            "Windows PC",
            scenes,
            BuildTarget.StandaloneWindows64,
            Path.Combine(buildRoot, "PC", "TiPi Ngoc Rong.exe"));

        AndroidBuildSetup.ConfigureAndroidTools();
        BuildAndRecord(
            report,
            "Android APK",
            scenes,
            BuildTarget.Android,
            Path.Combine(buildRoot, "Android", "TiPiNRO.apk"));

        BuildAndRecord(
            report,
            "iOS Xcode export",
            scenes,
            BuildTarget.iOS,
            Path.Combine(buildRoot, "iOS-Xcode"));

        File.WriteAllLines(ReportFile, report.ToArray());
        AssetDatabase.Refresh();
        Debug.Log("[TiPi Graphics] Sharpness validation finished. Report: " + ReportFile);
    }

    private static string ValidateTextureImports()
    {
        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets/Resources/res" });
        int checkedTextures = 0;
        int invalidTextures = 0;
        int losslessTextures = 0;
        int compressedTextures = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer == null)
            {
                continue;
            }

            int width;
            int height;
            importer.GetSourceTextureWidthAndHeight(out width, out height);
            int sourceMax = Mathf.Max(width, height);
            bool expectedLossless = sourceMax <= 512;
            bool valid = importer.filterMode == FilterMode.Point
                && !importer.mipmapEnabled
                && importer.anisoLevel == 0
                && importer.npotScale == TextureImporterNPOTScale.None
                && importer.wrapMode == TextureWrapMode.Clamp
                && importer.maxTextureSize >= sourceMax
                && importer.textureCompression == (expectedLossless
                    ? TextureImporterCompression.Uncompressed
                    : TextureImporterCompression.CompressedHQ);

            checkedTextures++;
            if (!valid)
            {
                invalidTextures++;
            }
            if (expectedLossless)
            {
                losslessTextures++;
            }
            else
            {
                compressedTextures++;
            }
        }

        return string.Format(
            "Texture audit: checked={0}, invalid={1}, losslessSmall={2}, compressedHQLarge={3}",
            checkedTextures,
            invalidTextures,
            losslessTextures,
            compressedTextures);
    }

    private static void BuildAndRecord(
        List<string> report,
        string label,
        string[] scenes,
        BuildTarget target,
        string outputPath)
    {
        try
        {
            string parent = target == BuildTarget.iOS ? outputPath : Path.GetDirectoryName(outputPath);
            Directory.CreateDirectory(parent);

            BuildReport buildReport = BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes = scenes,
                locationPathName = outputPath,
                target = target,
                options = BuildOptions.None
            });
            BuildSummary summary = buildReport.summary;
            report.Add(string.Format(
                "{0}: result={1}, errors={2}, warnings={3}, size={4}, time={5}",
                label,
                summary.result,
                summary.totalErrors,
                summary.totalWarnings,
                summary.totalSize,
                summary.totalTime));
            File.WriteAllLines(ReportFile, report.ToArray());
        }
        catch (Exception exception)
        {
            report.Add(label + ": exception=" + exception);
            File.WriteAllLines(ReportFile, report.ToArray());
        }
    }
}
