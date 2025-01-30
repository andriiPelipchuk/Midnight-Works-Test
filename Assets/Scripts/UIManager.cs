using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI moneyText;

        private void Update()
        {
            moneyText.text = "Money: " + GameManager.Instance.money;
        }
    }
}