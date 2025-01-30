using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Scene : MonoBehaviour
    {
        public GameObject setings;
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
        }

        public void ReturnToMenu()
        {
            setings.SetActive(false);
        }
        public void Quit()
        {
            Application.Quit();
        }
    }
}