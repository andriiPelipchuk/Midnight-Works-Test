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
        }
    }
}