using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuSystem : MonoBehaviour
{
    public GameObject HighScorePanel;
    public GameObject MenuPanel;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void HighScore()
    {
        HighScorePanel.SetActive(true);
        MenuPanel.SetActive(false);
    }
    public void quitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
		                Application.Quit();
        #endif
    }
    public void ClosePanel()
    {
        HighScorePanel.SetActive(false);
        MenuPanel.SetActive(true);
    }
}
