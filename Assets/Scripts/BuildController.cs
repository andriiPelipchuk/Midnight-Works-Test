using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class BuildController : MonoBehaviour
    {
        public LayerMask groundLayer;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                BuildingObject();
            }
        }

        private void BuildingObject()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                if (BuildingUI.Instance.uiOpen) return;

                var cell = hit.collider.GetComponent<GridCell>();
                BuildingUI.Instance.ShowBuildUI(cell);
            }
        }
    }
}