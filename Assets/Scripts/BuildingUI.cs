using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class BuildingUI : MonoBehaviour
    {
        public static BuildingUI Instance;

        public bool uiOpen = false;
        public GameObject buildPanel;
        public Image preview;
        public TextMeshProUGUI buildingNameText; 
        public TextMeshProUGUI costText; 
        private GridCell _currentCell;
        private Building _currentBuilding;

        private void Awake()
        {
            Instance = this;
        }

        public void ShowBuildUI(GridCell cell)
        {
            _currentCell = cell;

            _currentBuilding = cell.building.GetComponent<Building>();

            if (_currentBuilding.buildingLvl == 2 || GameManager.Instance.money < _currentCell.cost)
                return;

            uiOpen = true;
            preview.sprite = cell.preview;

            if (!_currentBuilding.isActive)
            {
                buildingNameText.text = cell.building.name;
                costText.text = "Cost: " + cell.cost;
            }
            else
            {
                buildingNameText.text = "Lvl 2: " + cell.building.name;
                costText.text = cell.cost.ToString();
            }

            buildPanel.SetActive(true);
        }

        public void HideBuildUI()
        {
            buildPanel.SetActive(false);
            uiOpen = false;
        }

        public void Build()
        {
            if (GameManager.Instance.money >= _currentCell.cost)
            {
                if(_currentBuilding.buildingLvl >= 2)
                {
                    HideBuildUI();
                    return;
                }

                if (_currentBuilding.isActive)
                {
                    _currentBuilding.SetBuildingLevel();
                    GameManager.Instance.RemoveMoney(_currentCell.cost);
                }
                else
                {
                    GameManager.Instance.RemoveMoney(_currentCell.cost);
                    _currentCell.building.SetData(1, true);
                    _currentCell.cost *= 2;
                }

                HideBuildUI();
            }
            else
            {
                Debug.Log("Not enough money!");
                HideBuildUI();
            }
        }
    }
}