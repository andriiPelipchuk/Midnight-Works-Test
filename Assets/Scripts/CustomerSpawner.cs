using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CustomerSpawner : MonoBehaviour
    {
        public GameObject[] customerPrefabs;
        public float spawnInterval = 5f; 

        private void Start()
        {
            InvokeRepeating(nameof(SpawnCustomer), 0f, spawnInterval);
        }

        private void SpawnCustomer()
        {
            GameObject newCustomer = Instantiate(customerPrefabs[Random.Range(0,customerPrefabs.Length)], transform.position, Quaternion.identity);
            var waypoint = GetComponent<Waypoint>();
            var customer = newCustomer.GetComponent<Customer>();
            customer.SetWaipoint(waypoint);
        }
    }
}