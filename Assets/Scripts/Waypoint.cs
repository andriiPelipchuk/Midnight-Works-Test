using UnityEngine;

namespace Assets.Scripts
{
    public class Waypoint : MonoBehaviour
    {
        public Waypoint[] connectedWaypoints; 
        public bool stopHere = false;
        public float stopDuration = 1;
        public GridCell сell;

        private int _productCost;
        private Building _building;

        private void Start()
        {
            if(сell != null)
                _building = сell.gameObject.GetComponentInChildren<Building>(true);

        }
        private void Update()
        {
            if (сell != null)
                _productCost = _building.productCost;
        }

        public int GetProductCost()
        {
            return _productCost;
        }

    }
}