using System.Linq;
using UnityEngine;

namespace Assets.Scripts 
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public Waypoint exitWaypoint;
        public Waypoint cashRegister;

        public int money = 250;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            LoadGame();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveGame();
            }
        }

        public void SaveGame()
        {
            var buildings = BuildingManager.Instance.buildings;
            var buildingsSaveData = new BuildingSaveData[buildings.Length];

            for (int i = 0; i < buildings.Length; i++)
            {
                var building = buildings[i];
                buildingsSaveData[i] = new BuildingSaveData(building.idBuilding, building.isActive, building.buildingLvl);
            }

            SaveSystem.SaveData(new SaveData(money, buildingsSaveData));
            Debug.Log("Game Saved!");
        }

        public void LoadGame()
        {
            SaveData data = SaveSystem.LoadData();
            if (data != null)
            {
                var buildings = BuildingManager.Instance.buildings;
                var buildingsSaveData = data.buildings;

                for (int i = 0; i < buildingsSaveData.Length; i++)
                {
                    var buildingSave = buildingsSaveData[i];
                    var building = buildings.First(x => x.idBuilding == buildingSave.buildingID);
                    building.SetData(buildingSave.buildingLvl + 1, buildingSave.isActive);
                }
                money = data.money;
                Debug.Log("Game Loaded!");
            }
            else
            {
                Debug.Log("No save data found!");
            }
        }
        public void AddMoney(int productCost)
        {
            money += productCost;
        }
        public void RemoveMoney(int buildPrice)
        {
            money -= buildPrice;
        }
    }
}

