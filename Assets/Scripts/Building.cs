using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Building : MonoBehaviour
    {
        public static List<Building> ActiveBuildings = new List<Building>();

        public int buildingLvl = 1;
        public int idBuilding;
        public bool isActive;
        public int productCost;

        private void OnEnable()
        {
            if (!ActiveBuildings.Contains(this))
            {
                ActiveBuildings.Add(this);
                isActive = true;
            }
        }

        public void SetData(int lvl, bool isActive)
        {
            buildingLvl = lvl;
            this.isActive = isActive;
            gameObject.SetActive(isActive);
        }

        public void SetBuildingLevel()
        {
            buildingLvl++;
        }
    }
}