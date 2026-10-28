using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// Keeps the Vegito CC small-image set on the server/RMS pipeline for the
/// session's real zoom. The bundled Resources copy only contains x4 and must
/// not silently shadow an x1/x2/x3 server image.
/// </summary>
public static class VegitoCCImagePipeline
{
	private const string CacheMigrationMarker = "vegito_cc_zoom_cache_v1";

	private static readonly int[] IconIds =
	{
		26152, 26153, 26155, 26156, 26157, 26158, 26160, 26161, 26162, 26163,
		26164, 26165, 26166, 26168, 26169, 26170, 26171, 26172, 26173, 26174,
		26176, 26185, 26186, 26187, 26188, 26189, 26190, 26192, 26193, 26194,
		26195, 26196, 26197, 26198, 26200, 26217, 26218, 26219, 26220, 26221,
		26222, 26224, 26262, 26263, 26264, 26265, 26266, 26267, 26268, 26269,
		26270, 26271, 26272, 26273, 26274, 26275, 26276, 26277, 26278, 26279,
		26280, 26281, 26282, 26283, 26284, 26285, 26286, 26287, 26288, 26289,
		26290
	};

	private static readonly HashSet<string> Logged = new HashSet<string>();
	private static bool cachePrepared;

	public static bool IsAffectedIcon(int id)
	{
		return Array.BinarySearch(IconIds, id) >= 0;
	}

	public static Image DecodePng(sbyte[] png)
	{
		if (png == null || png.Length == 0)
		{
			return null;
		}
		Image image = new Image();
		image.texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
		if (!image.texture.LoadImage(ArrayCast.cast(png), false))
		{
			return null;
		}
		image.w = image.texture.width;
		image.h = image.texture.height;
		Image.setTextureQuality(image.texture);
		return image;
	}

	/// <summary>
	/// Invalidates only legacy Vegito icon caches once. Existing files are
	/// copied before removal. No account/session/general resource data is read.
	/// </summary>
	public static void PrepareScopedCache()
	{
		if (cachePrepared)
		{
			return;
		}
		cachePrepared = true;

		string root = Application.persistentDataPath;
		string marker = Path.Combine(root, CacheMigrationMarker);
		if (File.Exists(marker))
		{
			return;
		}

		string backup = Path.Combine(root, "VegitoCCCacheBackup_pre_zoom_fix");
		int backedUp = 0;
		for (int zoom = 1; zoom <= 4; zoom++)
		{
			for (int index = 0; index < IconIds.Length; index++)
			{
				string name = zoom + "Small" + IconIds[index];
				string source = Path.Combine(root, name);
				if (!File.Exists(source))
				{
					continue;
				}

				Directory.CreateDirectory(backup);
				File.Copy(source, Path.Combine(backup, name), true);
				File.Delete(source);
				backedUp++;
			}
		}

		File.WriteAllText(marker, "Vegito CC zoom-aware cache migration v1");
		LogOnce("cache", -1, "zoom=" + mGraphics.zoomLevel + " invalidated=" + backedUp
			+ " backup=" + (backedUp == 0 ? "none-needed" : backup));
	}

	public static void LogHandshake(string channel)
	{
		LogOnce("handshake-" + channel, -1,
			"endpoint=" + GameMidlet.IP + ":" + GameMidlet.PORT
			+ " zoom=" + mGraphics.zoomLevel
			+ " logical=" + GameCanvas.w + "x" + GameCanvas.h
			+ " screen=" + Screen.width + "x" + Screen.height);
	}

	public static void LogSource(int id, string source, Image image, sbyte[] png)
	{
		if (!IsAffectedIcon(id))
		{
			return;
		}
		string texture = image == null || image.texture == null
			? "null"
			: image.getRealImageWidth() + "x" + image.getRealImageHeight()
				+ " filter=" + image.texture.filterMode
				+ " mipmaps=" + image.texture.mipmapCount;
		string hash = png == null ? "n/a" : Sha256(png);
		LogOnce("source-" + source, id,
			"zoom=" + mGraphics.zoomLevel + " source=" + source
			+ " bytes=" + (png == null ? 0 : png.Length)
			+ " sha256=" + hash + " texture=" + texture);
	}

	public static void LogDraw(int id, Image image, int logicalWidth, int logicalHeight)
	{
		if (!IsAffectedIcon(id) || image == null || image.texture == null)
		{
			return;
		}
		int destinationWidth = logicalWidth * mGraphics.zoomLevel;
		int destinationHeight = logicalHeight * mGraphics.zoomLevel;
		float scaleX = (float)destinationWidth / image.texture.width;
		float scaleY = (float)destinationHeight / image.texture.height;
		LogOnce("draw", id,
			"zoom=" + mGraphics.zoomLevel
			+ " texture=" + image.texture.width + "x" + image.texture.height
			+ " regionLogical=" + logicalWidth + "x" + logicalHeight
			+ " destination=" + destinationWidth + "x" + destinationHeight
			+ " finalScale=" + scaleX.ToString("0.###") + "x" + scaleY.ToString("0.###")
			+ " filter=" + image.texture.filterMode);
	}

	private static string Sha256(sbyte[] data)
	{
		using (SHA256 algorithm = SHA256.Create())
		{
			byte[] hash = algorithm.ComputeHash(ArrayCast.cast(data));
			return BitConverter.ToString(hash).Replace("-", string.Empty);
		}
	}

	private static void LogOnce(string kind, int id, string detail)
	{
		if (!Debug.isDebugBuild)
		{
			return;
		}
		string key = kind + ":" + mGraphics.zoomLevel + ":" + id;
		if (Logged.Add(key))
		{
			Debug.Log("[VegitoCC.Image] " + kind + (id >= 0 ? " id=" + id : string.Empty) + " " + detail);
		}
	}
}
