using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Scene : MonoBehaviour
    {
        public GameObject setings;
        public GameObject buttons;
        public void NewGame()
        {
            SaveSystem.DeleteSave();
            SceneManager.LoadScene("GameScene");
        }
        public void ContinueGame()
        {
            if (SaveSystem.PresenceSave())
                SceneManager.LoadScene("GameScene");
            else return;
        }
        public void GoToMenu()
        {
            GameManager.Instance.SaveGame();
            SceneManager.LoadScene("MenuScene");
        }

        public void EnableSettings()
        {
            setings.SetActive(true);
            buttons.SetActive(false);
        }

        public void ReturnToMenu()
        {
            setings.SetActive(false);
            buttons.SetActive(true);
        }
        public void Quit()
        {
            Application.Quit();
        }
    }
}