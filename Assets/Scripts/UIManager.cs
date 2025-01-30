using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI moneyText;
        public GameObject pauseUI;

        private void Update()
        {
            moneyText.text = "Money: " + GameManager.Instance.money;
        }

        public void PauseUIEnable()
        {
            pauseUI.SetActive(true);
        }
        public void PauseUIDisable()
        {
            pauseUI.SetActive(false);
        }
    }
}