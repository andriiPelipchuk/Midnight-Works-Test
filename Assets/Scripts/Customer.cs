using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Customer : MonoBehaviour
    {
        public enum CustomerState
        {
            GoingToProduct,
            GoingToCashRegister,
            GoingToExit
        }

        public CustomerState currentState;
        public GameObject shoppingCart;

        public float speed = 1;
        public float rotationSpeed = 5f;

        private Transform _target;
        private int _randomIndex;
        private Waypoint _currentWaypoint;
        private Waypoint _cashRegister; 
        private Waypoint _exitWaypoint; 
        private bool _isMoving = false;
        private Animator _animator; 

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _cashRegister = GameManager.Instance.cashRegister;
            _exitWaypoint = GameManager.Instance.exitWaypoint;
            ChooseTargetBuilding();
            FindStartWaypoint();
        }

        private void Update()
        {
            if (_isMoving && _currentWaypoint != null)
            {
                MoveToNextWaypoint();
            }
            if(currentState == CustomerState.GoingToExit)
            {
                shoppingCart.SetActive(false);
                _animator.SetLayerWeight(1, 0);
            }
        }

        private void NextEnumState()
        {
            CustomerState[] states = (CustomerState[])System.Enum.GetValues(typeof(CustomerState));
            int index = System.Array.IndexOf(states, currentState);
            index = (index + 1) % states.Length;
            currentState = states[index];
            if (currentState == CustomerState.GoingToCashRegister)
            {
                _target = _cashRegister.transform;
                var cart = shoppingCart.GetComponent<ShoppingCart>();
                cart.AddProducts();
            }

        }
            private void ChooseTargetBuilding()
        {
            if (Building.ActiveBuildings.Count == 0)
            {
                Destroy(gameObject);
                return;
            }

            var buildingIndex = Random.Range(0, Building.ActiveBuildings.Count);
            var build = Building.ActiveBuildings[buildingIndex].transform;
            _target = FindClosestWaypointToTarget(build).transform;

            currentState = CustomerState.GoingToProduct;
        }

        public void SetWaipoint(Waypoint waypoint)
        {
            _currentWaypoint = waypoint;
        }

        private void FindStartWaypoint()
        {
            if (_currentWaypoint != null)
            {
                transform.position = _currentWaypoint.transform.position;
                ChooseNextWaypoint();
            }
        }

        private void MoveToNextWaypoint()
        {
            if (_currentWaypoint == null) return;

            Transform targetPosition = _currentWaypoint.transform;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);

            Vector3 direction = (targetPosition.position - transform.position).normalized;
            if (direction != Vector3.zero) 
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }


            if (Vector3.Distance(transform.position, targetPosition.position) < 0.1f)
            {
                _isMoving = false;

                if(_currentWaypoint.stopHere)
                    StartCoroutine(HandleStop());

                if (Vector3.Distance(transform.position, _target.position) < 0.1f)
                {
                    StartCoroutine(HandleStop());
                    NextEnumState();

                    GameManager.Instance.AddMoney(_currentWaypoint.GetProductCost());
                }
                else
                {
                    ChooseNextWaypoint();
                }
            }
        }
        private Waypoint FindClosestWaypointToTarget(Transform targetPosition)
        {
            Waypoint[] waypoints = FindObjectsByType<Waypoint>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            Waypoint closestWaypoint = null;
            float closestDistance = Mathf.Infinity;

            foreach (var waypoint in waypoints)
            {
                float distance = Vector3.Distance(waypoint.transform.position, targetPosition.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestWaypoint = waypoint;
                }
            }

            return closestWaypoint;
        }
        private IEnumerator HandleStop()
        {
            if (_animator != null && currentState == CustomerState.GoingToProduct)
            {
                _animator.Play("pick-up");
                _animator.SetLayerWeight(1, 1);
                shoppingCart.SetActive(true);
            }

            yield return new WaitForSeconds(_currentWaypoint.stopDuration);
            
            ChooseNextWaypoint();
        }

        private void ChooseNextWaypoint()
        {
            if (_currentWaypoint == null || _currentWaypoint.connectedWaypoints.Length == 0)
            {
                Destroy(gameObject); 
                return;
            }

            Waypoint bestWaypoint = null;
            float closestDistanceToTarget = Mathf.Infinity;

            foreach (var waypoint in _currentWaypoint.connectedWaypoints)
            {
                float distanceToTarget = Vector3.Distance(waypoint.transform.position, _target.position);
                if (distanceToTarget < closestDistanceToTarget)
                {
                    closestDistanceToTarget = distanceToTarget;
                    bestWaypoint = waypoint;
                }
            }

            _currentWaypoint = bestWaypoint;

            _isMoving = true;
        }
    }
}