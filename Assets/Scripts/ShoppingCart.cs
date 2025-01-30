using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ShoppingCart : MonoBehaviour
    {
        public GameObject products;

        public void AddProducts()
        {
            products.SetActive(true);
        }
    }
}