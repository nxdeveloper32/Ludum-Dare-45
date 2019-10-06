using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SystemUIManager : MonoBehaviour
{
    public TextMeshProUGUI TimeUI;
    public TextMeshProUGUI LineOfCodeUI;
    public TextMeshProUGUI BugsUI;
    public TextMeshProUGUI CoinsUI;
    public TextMeshProUGUI ScorePanelUI;
    public static SystemUIManager instance;
    public AudioClip BackgroundSound;
    SystemGameManager GameManager;
    public GameObject ScorePanel;
    public GameObject MenuButton;
    public GameObject QuitButton;
    public GameObject AboutPanel;
    public GameObject[] Pages;
    int currentPage = 0;
    bool isRunning;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameManager = SystemGameManager.Instance;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning)
        {
            float elapsedTime = Time.realtimeSinceStartup - GameManager.TimeOfStart;
            TimeUI.text = string.Format("{0:0}:{1:00}", elapsedTime / 60, elapsedTime % 60).ToString();
        }
    }

    public void UpdateUI()
    {
        LineOfCodeUI.text = GameManager.lineOfCode.ToString();
        BugsUI.text = GameManager.NumberOfBugs.ToString();
        CoinsUI.text = GameManager.NumberOfCoins.ToString();
    }
    public void NavigateToSceneIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void quitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
		                        Application.Quit();
        #endif
    }
    public void ScorePanelFun(bool isMenu)
    {
        isRunning = true;
        string temp = TimeUI.text;
        ScorePanelUI.text = "You Completed Project in " + temp + " Time with " + GameManager.lineOfCode + " lines of code and fixed " + GameManager.NumberOfBugs + " bugs and earned " + GameManager.NumberOfCoins  + " coins from marketing ";
        ScorePanel.SetActive(true);
        if (isMenu)
        {
            QuitButton.SetActive(false);
        }
        else
        {
            MenuButton.SetActive(false);
        }
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void RightSide()
    {
        Pages[currentPage].SetActive(false);
        currentPage++;
        if(currentPage >= 7)
        {
            currentPage = 0;
        }
        Pages[currentPage].SetActive(true);
    }
    public void LeftSide()
    {
        Pages[currentPage].SetActive(false);
        currentPage--;
        if (currentPage <= 0)
        {
            currentPage = 6;
        }
        Pages[currentPage].SetActive(true);
    }
}
