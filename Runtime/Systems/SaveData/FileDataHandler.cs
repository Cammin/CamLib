using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CamLib
{
    /// <summary>
    /// Given a directory and file name, can load, save, and delete data.
    /// Each profile is a directory, containing a save file, and a backup file.
    /// </summary>
    internal class FileDataHandler<T> where T : GameData
    {
        private string rootDir;
        private string fileName;
        private bool encrypt;
        
        private const string encryptionCodeWord = "word";
        private const string backupExtension = ".backup";

        public FileDataHandler(string rootDir, string fileName, bool encrypt) 
        {
            this.rootDir = rootDir;
            this.fileName = fileName;
            this.encrypt = encrypt;
        }

        public T Load(string profileId, bool allowRestoreFromBackup = true) 
        {
            if (profileId == null) 
            {
                return null;
            }

            string fullPath = Path.Combine(rootDir, profileId, fileName);
            T loadedData = null;
            
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning($"Tried loading but file does not exist at {fullPath}");
                return null;
            }
            
            try 
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (encrypt) 
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<T>(dataToLoad);
            }
            catch (Exception e) 
            {
                if (allowRestoreFromBackup) 
                {
                    Debug.LogWarning("Failed to load data file. Attempting to roll back.\n" + e);
                    bool rollbackSuccess = AttemptRollback(fullPath);
                    if (rollbackSuccess)
                    {
                        //Try to load this profile again. But don't allow restoring from backup again, to prevent infinite recursion
                        loadedData = Load(profileId, false);
                    }
                }
                else 
                {
                    Debug.LogError($"Error occured when trying to load file at path: {fullPath} and backup did not work.\n{e}");
                }
            }

            Debug.Log($"Loaded save data for {profileId}");
            return loadedData;
        }
        
        public void Save(T data, string profileId) 
        {
            // base case - if the profileId is null, return right away
            if (string.IsNullOrEmpty(profileId)) 
            {
                Debug.LogWarning("Tried saving but the profileId was null or empty.");
                return;
            }

            // use Path.Combine to account for different OS's having different path separators
            string filePath = Path.Combine(rootDir, profileId, fileName);
            string backupFilePath = filePath + backupExtension;
            string profileDir = Path.GetDirectoryName(filePath);
            try 
            {
                Directory.CreateDirectory(profileDir);

                string dataToWrite = JsonUtility.ToJson(data, true);

                if (encrypt) 
                {
                    dataToWrite = EncryptDecrypt(dataToWrite);
                }

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream)) 
                    {
                        writer.Write(dataToWrite);
                    }
                }
                Debug.Log($"Wrote save data \"{profileId}\"");

                // verify it can load to backup
                T verifiedGameData = Load(profileId);
                if (verifiedGameData != null) 
                {
                    File.Copy(filePath, backupFilePath, true);
                }
                else 
                {
                    throw new Exception("Save file could not be verified and backup could not be created.");
                }

            }
            catch (Exception e) 
            {
                Debug.LogError("Error occured when trying to save data to file: " + filePath + "\n" + e);
            }
        }

        /// <summary>
        /// Deletes a profile's directory, destroying both the save data and the backup
        /// </summary>
        public void Delete(string profileId) 
        {
            // base case - if the profileId is null, return right away
            if (profileId == null) 
            {
                return;
            }

            string profilePath = Path.Combine(rootDir, profileId, fileName);
            string profileDir = Path.GetDirectoryName(profilePath);
            
            try 
            {
                // ensure the data file exists at this path before deleting the directory
                if (File.Exists(profilePath)) 
                {
                    Directory.Delete(profileDir, true);
                }
                else 
                {
                    Debug.LogWarning("Tried to delete profile data, but data was not found at path: " + profilePath);
                }
            }
            catch (Exception e) 
            {
                Debug.LogError("Failed to delete profile data for profileId: " 
                               + profileId + " at path: " + profilePath + "\n" + e);
            }
        }

        /// <summary>
        /// Loads every single profile from inside the directory
        /// </summary>
        public Dictionary<string, T> LoadAllProfiles() 
        {
            Dictionary<string, T> profileDictionary = new Dictionary<string, T>();

            IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(rootDir).EnumerateDirectories();
            foreach (DirectoryInfo dirInfo in dirInfos) 
            {
                string profileId = dirInfo.Name;

                string profilePath = Path.Combine(rootDir, profileId, fileName);
                if (!File.Exists(profilePath))
                {
                    Debug.LogWarning("Skipping directory when loading all profiles because it does not contain data: "
                                     + profileId);
                    continue;
                }

                T profileData = Load(profileId);
                
                if (profileData != null) 
                {
                    profileDictionary.Add(profileId, profileData);
                }
                else 
                {
                    Debug.LogError("Tried to load profile but something went wrong. ProfileId: " + profileId);
                }
            }

            return profileDictionary;
        }
        
        public string FindMostRecentlyUpdatedProfileId()
        {
            var profiles = LoadAllProfiles();
            return FindMostRecentlyUpdatedProfileId(profiles);
        }
        
        public string FindMostRecentlyUpdatedProfileId(Dictionary<string, T> profiles)
        {
            string mostRecentProfileId = null;
            
            foreach (KeyValuePair<string, T> pair in profiles) 
            {
                string profileId = pair.Key;
                T gameData = pair.Value;

                if (gameData == null) 
                {
                    continue;
                }
                
                if (mostRecentProfileId == null) 
                {
                    mostRecentProfileId = profileId;
                    continue;
                }
                
                DateTime mostRecentDateTime = DateTime.FromBinary(profiles[mostRecentProfileId].LastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.LastUpdated);
                if (newDateTime > mostRecentDateTime) 
                {
                    mostRecentProfileId = profileId;
                }
            }
            return mostRecentProfileId;
        }

        /// <summary>
        /// Simple implementation of XOR encryption. Scrambles/unscrambles the chars
        /// </summary>
        private string EncryptDecrypt(string data) 
        {
            string modifiedData = "";
            for (int i = 0; i < data.Length; i++) 
            {
                modifiedData += (char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
            }
            return modifiedData;
        }

        /// <summary>
        /// Overwrite original file with backup file
        /// </summary>
        /// <returns>If the rollback is a success</returns>
        private bool AttemptRollback(string fullPath) 
        {
            bool success = false;
            string backupFilePath = fullPath + backupExtension;
            try 
            {
                if (File.Exists(backupFilePath))
                {
                    File.Copy(backupFilePath, fullPath, true);
                    success = true;
                    Debug.LogWarning($"Had to rollback to backup file at {backupFilePath}");
                }
                else 
                {
                    throw new Exception($"Tried to roll back, but no backup file exists at {backupFilePath}");
                }
            }
            catch (Exception e) 
            {
                Debug.LogError($"Error occured when trying to roll back to backup file at {backupFilePath}\n{e}");
            }

            return success;
        }
    }
}
