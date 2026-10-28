using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class AndroidBuildSetup
{
    public const string OFFICIAL_JDK_PATH = @"C:\OpenJDK";
    public const string OFFICIAL_SDK_PATH = @"C:\Program Files\Unity\Hub\Editor\2021.3.36f1\Editor\Data\PlaybackEngines\AndroidPlayer\SDK";
    public const string OFFICIAL_NDK_PATH = @"C:\Program Files\Unity\Hub\Editor\2021.3.36f1\Editor\Data\PlaybackEngines\AndroidPlayer\NDK";

    [MenuItem("TiPi/Configure Android Tools")]
    public static void ConfigureAndroidTools()
    {
        try
        {
            // Set environment variable
            Environment.SetEnvironmentVariable("JAVA_HOME", OFFICIAL_JDK_PATH);
            Environment.SetEnvironmentVariable("JDK_HOME", OFFICIAL_JDK_PATH);

            // Set EditorPrefs
            EditorPrefs.SetString("JdkPath", OFFICIAL_JDK_PATH);
            EditorPrefs.SetBool("JdkUseEmbedded", false);

            EditorPrefs.SetString("AndroidSdkRoot", OFFICIAL_SDK_PATH);
            EditorPrefs.SetBool("SdkUseEmbedded", true);

            EditorPrefs.SetString("AndroidNdkRoot", OFFICIAL_NDK_PATH);
            EditorPrefs.SetBool("NdkUseEmbedded", true);

            // Sync with Android Java Root
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asm.GetName().Name == "UnityEditor.Android.Extensions")
                {
                    var javaRootType = asm.GetType("UnityEditor.Android.AndroidJavaRoot");
                    if (javaRootType != null)
                    {
                        var getInstance = javaRootType.GetMethod("GetInstance", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                        var instance = getInstance?.Invoke(null, null);
                        if (instance != null)
                        {
                            var setUseEmbedded = javaRootType.GetMethod("set_UseEmbedded", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                            setUseEmbedded?.Invoke(instance, new object[] { false });

                            var setCustomDir = javaRootType.GetMethod("set_CustomDirectory", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                            setCustomDir?.Invoke(instance, new object[] { OFFICIAL_JDK_PATH });

                            var syncPref = javaRootType.GetMethod("SyncPreferences", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                            syncPref?.Invoke(instance, null);
                        }
                    }
                }
            }

            Debug.Log($"[TiPi Build] Đã cấu hình JDK thành công: {OFFICIAL_JDK_PATH}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[TiPi Build] Lỗi cấu hình: {ex}");
        }
    }

    [MenuItem("TiPi/Build TiPiNRO.apk")]
    public static void BuildAPK()
    {
        ConfigureAndroidTools();

        string buildDir = Path.Combine(Directory.GetCurrentDirectory(), "Build");
        if (!Directory.Exists(buildDir))
        {
            Directory.CreateDirectory(buildDir);
        }

        string apkPath = Path.Combine(buildDir, "TiPiNRO.apk");

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new string[] { "Assets/Scenes/SampleScene.unity" };
        buildPlayerOptions.locationPathName = apkPath;
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;

        Debug.Log($"[TiPi Build] Bắt đầu build APK vào: {apkPath}...");

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"[TiPi Build] BUILD THÀNH CÔNG! Dung lượng: {summary.totalSize} bytes. File: {apkPath}");
            if (!Application.isBatchMode)
            {
                EditorUtility.DisplayDialog("TiPi Build", $"Build thành công!\nFile: {apkPath}", "OK");
            }
        }
        else if (summary.result == BuildResult.Failed)
        {
            Debug.LogError($"[TiPi Build] BUILD THẤT BẠI! Số lỗi: {summary.totalErrors}");
            if (!Application.isBatchMode)
            {
                EditorUtility.DisplayDialog("TiPi Build", $"Build thất bại! Xem Console để biết chi tiết.", "OK");
            }
        }
    }

    [MenuItem("TiPi/Build TiPi PC (Windows)")]
    public static void BuildPC()
    {
        string buildDir = Path.Combine(Directory.GetCurrentDirectory(), "Build", "PC");
        if (!Directory.Exists(buildDir))
        {
            Directory.CreateDirectory(buildDir);
        }

        string exePath = Path.Combine(buildDir, "TiPi Ngoc Rong.exe");

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new string[] { "Assets/Scenes/SampleScene.unity" };
        buildPlayerOptions.locationPathName = exePath;
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;

        Debug.Log($"[TiPi Build] Bắt đầu build PC vào: {exePath}...");

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"[TiPi Build] BUILD PC THÀNH CÔNG! Dung lượng: {summary.totalSize} bytes. File: {exePath}");
            if (!Application.isBatchMode)
            {
                EditorUtility.DisplayDialog("TiPi Build", $"Build PC thành công!\nFile: {exePath}", "OK");
            }
        }
        else if (summary.result == BuildResult.Failed)
        {
            Debug.LogError($"[TiPi Build] BUILD PC THẤT BẠI! Số lỗi: {summary.totalErrors}");
            if (!Application.isBatchMode)
            {
                EditorUtility.DisplayDialog("TiPi Build", $"Build PC thất bại! Xem Console để biết chi tiết.", "OK");
            }
        }
    }
}
