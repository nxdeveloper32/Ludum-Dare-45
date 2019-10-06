using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CannonManager : MonoBehaviour
{
    public static CannonManager instance;
    public GameObject MyFocus;
    public GameObject MyWeapon;
    public GameObject[] prefab;
    public Button ShootButton;
    public int lineOfCode;
    public int NumberOfBugs;
    public int NumberOfCoins;
    public TextMeshProUGUI LinesOfCodeUI;
    public TextMeshProUGUI NumBugsUI;
    public Image CriticalBar;
    float increaseBy = .25f;
    public GameObject LostScreenPanel;
    public GameObject SaveScreenPanel;
    public bool isEnabled;
    public bool ReloadOnce;
    public GameObject critialAnim;
    public ComputerScreen[] Monitor;
    float NumberOfMonitor = 7;
    public bool TriggerScreen;
    public int NumberOfMissAllowed = 5;
    public Transform MissPanel;
    public GameObject LostPanelUI;
    private float timeBtwSpawn;
    public float StartTimeBtwSpawn;
    public float decreaseTime;
    public float minTime = 0.65f;
    int System = 0;
    int CurrentSystem = 0;
    bool isAllScreenEnabled;
    public TextMeshProUGUI NumberOfCoinsUI;
    public GameObject HireButton;
    public ComputerScreen[] HireDeveloperButton;
    int increaseByLineOfCode;
    private void Awake()
    {
        instance = this;
        isEnabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        increaseByLineOfCode = 1;
        NumberOfMissAllowed = 5;
        UpdateUI();
    }
    public void LostScreenPanelFun()
    {
        SystemGameManager.Instance.lineOfCode += lineOfCode;
        SoundManager.Instance.PlaySFX(GameManager.Instance.Explosion);
        LostPanelUI.SetActive(true);
    }
    public void LoosePoint()
    {
        SoundManager.Instance.PlaySFX(GameManager.Instance.LoosePoint);
        Destroy(MissPanel.GetChild(0).gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (MyFocus != null && MyWeapon != null)
        {
            ShootButton.interactable = true;
        }
        if(lineOfCode >= 10 && !TriggerScreen && !isAllScreenEnabled )
        {
            TriggerScreen = true;
        }
        if (TriggerScreen)
        {
            if (timeBtwSpawn <= 0)
            {
                SoundManager.Instance.PlaySFX(GameManager.Instance.NewScreen);
                Monitor[System].IsEnabled = true;
                Monitor[System].IsBlank = false;
                Monitor[System].UpdateScreens();
                System++;
                increaseByLineOfCode++;
                if (System == 7)
                {
                    TriggerScreen = false;
                    isAllScreenEnabled = true;
                }
                timeBtwSpawn = StartTimeBtwSpawn;
                if (StartTimeBtwSpawn > minTime)
                {
                    StartTimeBtwSpawn -= decreaseTime;
                }
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            HireDeveloperButton[CurrentSystem].GetComponent<ComputerScreen>().AutomateComputer();
            CurrentSystem++;
        }
    }
    public void Shoot()
    {
        if(MyFocus != null && MyWeapon != null)
        {
            if (MyFocus.GetComponent<ComputerScreen>().MyText == MyWeapon.GetComponent<ColorSetter>().MyText)
            {
                lineOfCode += increaseByLineOfCode;
                SoundManager.Instance.PlaySFX(GameManager.Instance.success);
                MyFocus.GetComponent<ComputerScreen>().DestoryGO();
                MyFocus.GetComponent<ComputerScreen>().SpawnItemAnim();
            }
            else
            {
                CriticalBar.fillAmount += increaseBy;
                critialAnim.SetActive(true);
                NumberOfBugs++;
                SoundManager.Instance.PlaySFX(GameManager.Instance.error);
            }
            UpdateUI();
        }
    }
    public void UpdateUI()
    {
        LinesOfCodeUI.text = lineOfCode.ToString();
        NumberOfCoins = SystemGameManager.Instance.NumberOfCoins;
        NumberOfCoinsUI.text = NumberOfCoins.ToString();
        int requirecoins = NumberOfCoins - 2000;
        if (requirecoins >= 0 && System > 1)
        {
            HireButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            HireButton.GetComponent<Button>().interactable = false;
        }
        NumBugsUI.text = NumberOfBugs.ToString();
        if(CriticalBar.fillAmount >= 1)
        {
            isEnabled = false;
            LostScreenPanel.SetActive(true);
        }
    }
    public void SaveScreenMsg()
    {
        SystemGameManager.Instance.lineOfCode = lineOfCode;
        SaveScreenPanel.SetActive(true);
    }
    public void HireDeveloper()
    {
        HireDeveloperButton[CurrentSystem].GetComponent<ComputerScreen>().AutomateComputer();
        CurrentSystem++;
        NumberOfCoins -= 2000;
        SystemGameManager.Instance.NumberOfCoins = NumberOfCoins;
        NumberOfCoinsUI.text = NumberOfCoins.ToString();
    }
}
