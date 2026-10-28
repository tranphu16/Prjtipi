using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class VegitoCCBuild
{
	public static void BuildWindowsRelease()
	{
		BuildWindows("PC", BuildOptions.None);
	}

	public static void BuildWindowsDiagnostic()
	{
		BuildWindows("PC-Diagnostic", BuildOptions.Development);
	}

	private static void BuildWindows(string folder, BuildOptions options)
	{
		string[] scenes = EditorBuildSettings.scenes
			.Where(scene => scene.enabled)
			.Select(scene => scene.path)
			.ToArray();
		if (scenes.Length == 0)
		{
			throw new InvalidOperationException("No enabled scenes in EditorBuildSettings.");
		}

		string output = Path.Combine(Directory.GetCurrentDirectory(), "Build", "VegitoCCFix", folder, "TiPi Ngoc Rong.exe");
		Directory.CreateDirectory(Path.GetDirectoryName(output));
		BuildReport report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
		{
			scenes = scenes,
			locationPathName = output,
			target = BuildTarget.StandaloneWindows64,
			options = options
		});
		BuildSummary summary = report.summary;
		Debug.Log(string.Format(
			"[VegitoCC.Build] output={0} result={1} errors={2} warnings={3} size={4} time={5}",
			output,
			summary.result,
			summary.totalErrors,
			summary.totalWarnings,
			summary.totalSize,
			summary.totalTime));
		if (summary.result != BuildResult.Succeeded)
		{
			throw new Exception("Vegito CC build failed: " + summary.result);
		}
	}
}
