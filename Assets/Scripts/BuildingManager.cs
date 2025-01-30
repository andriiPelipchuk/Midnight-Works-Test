using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BuildingManager : MonoBehaviour
    {
        public static BuildingManager Instance;
        public Building[] buildings;

        private void Awake()
        {
            Instance = this;
            buildings = FindObjectsByType<Building>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            for (int i = 0; i < buildings.Length; i++)
            {
                if (buildings[i].isActive)
                {
                    buildings[i].gameObject.SetActive(true);
                }
            }
        }
    }
}