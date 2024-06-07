using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace CamLib.Editor
{
    public class BuildIncrementor : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public int callbackOrder => 1;
        

        public void OnPreprocessBuild(BuildReport report)
        {
            string bundleVersion = PlayerSettings.bundleVersion;

            Version version = ParseVersion(bundleVersion);
            version = version.IncrementBuild();
            
            PlayerSettings.bundleVersion = version.ToString(3);
            
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
            
            Debug.Log($"{bundleVersion} to {PlayerSettings.bundleVersion}");
            
        }
        
        public void OnPostprocessBuild(BuildReport report)
        {
            /*Debug.Log("try making zip");
            string path = report.summary.outputPath;*/
        }

        


        private string IncrementBuildNumber(string buildNumber)
        {
            int.TryParse(buildNumber, out int outputBuildNumber);

            return (outputBuildNumber + 1).ToString();
        }
        
        private static Version ParseVersion(string input)
        {
            try {
                Version ver = Version.Parse(input);
                //Debug.LogFormat("Converted '{0} to {1}.", input, ver);
                return ver;
            }
            catch (ArgumentNullException) {
                Debug.LogErrorFormat("Error: String to be parsed is null.");
            }
            catch (ArgumentOutOfRangeException) {
                Debug.LogErrorFormat("Error: Negative value in '{0}'.", input);
            }
            catch (ArgumentException) {
                Debug.LogErrorFormat("Error: Bad number of components in '{0}'.", 
                    input);
            }
            catch (FormatException) {
                Debug.LogErrorFormat("Error: Non-integer value in '{0}'.", input);
            }
            catch (OverflowException) {   
                Debug.LogErrorFormat("Error: Number out of range in '{0}'.", input);                  
            }

            throw new Exception("");
        }
    }
    
    public static class VersionExtension {

    public static Version IncrementRevision(this Version version) {
        return AddVersion(version, 0, 0, 0, 1);
    }
    public static Version IncrementBuild(this Version version) {
        return IncrementBuild(version, true);
    }
    public static Version IncrementBuild(this Version version, bool resetLowerNumbers) {
        return AddVersion(version, 0, 0, 1, resetLowerNumbers ? -version.Revision : 0);
    }
    public static Version IncrementMinor(this Version version) {
        return IncrementMinor(version, true);
    }
    public static Version IncrementMinor(this Version version, bool resetLowerNumbers) {
        return AddVersion(version, 0, 1, resetLowerNumbers ? -version.Build : 0, resetLowerNumbers ? -version.Revision : 0);
    }
    public static Version IncrementMajor(this Version version) {
        return IncrementMajor(version, true);
    }
    public static Version IncrementMajor(this Version version, bool resetLowerNumbers) {
        return AddVersion(version, 1, resetLowerNumbers ? -version.Minor : 0, resetLowerNumbers ? -version.Build : 0, resetLowerNumbers ? -version.Revision : 0);
    }

    public static Version AddVersion(this Version version, string addVersion) {
        return AddVersion(version, new Version(addVersion));
    }
    public static Version AddVersion(this Version version, Version addVersion) {
        return AddVersion(version, addVersion.Major, addVersion.Minor, addVersion.Build, addVersion.Revision);
    }
    public static Version AddVersion(this Version version, int major, int minor, int build, int revision) {
        return SetVersion(version, version.Major + major, version.Minor + minor, version.Build + build, version.Revision + revision);
    }
    public static Version SetVersion(this Version version, int major, int minor, int build, int revision) {
        return new Version(major, minor, build, revision); 
    }

    /*
    //one day we may even be able to do something like this...
    //https://github.com/dotnet/csharplang/issues/192
    public static Version operator +(Version version, int revision) {
        return AddVersion(version, 0, 0, 0, revision);
    }
    public static Version operator ++(Version version) {
        return IncrementVersion(version);
    }   
    */

}
}
