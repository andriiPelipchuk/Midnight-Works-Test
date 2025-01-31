using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public static class SaveSystem
    {

        private static string _savePath = Application.persistentDataPath+"/save.json";

        public static void SaveData(SaveData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(_savePath, json);
        }

        public static SaveData LoadData()
        {
            if (File.Exists(_savePath))
            {
                string json = File.ReadAllText(_savePath);
                return JsonUtility.FromJson<SaveData>(json);
            }
            return null;
        }
   
        public static void DeleteSave()
        {
            if (File.Exists(_savePath))
            {
                File.Delete(_savePath);
                Debug.Log("Save file deleted!");
            }
        }

        public static bool PresenceSave()
        {
            if (File.Exists(_savePath)) return true;
            else return false;
        } 
    }

    [System.Serializable]
    public class SaveData
    {
        public int money;
        public BuildingSaveData[] buildings;

        public SaveData(int money, BuildingSaveData[] buildings)
        {
            this.money = money;
            this.buildings = buildings;
        }
    }

    [System.Serializable]
    public class BuildingSaveData
    {
        public int buildingID;
        public int buildingLvl;
        public bool isActive;

        public BuildingSaveData(int id, bool active, int buildingLvl)
        {
            buildingID = id;
            isActive = active;
        }
    }
}