using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI RemainingPanelText;
    public GameObject ServerFixedPanel;
    public Image RedLight;
    public Image YellowLight;
    public Image GreenLight;
    public TextMeshProUGUI BugsCount;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        BugsCount.text = GameManager.Instance.fixedBugs.ToString();
        RemainingPanelText.text =( GameManager.Instance.completedServer + "/" + GameManager.Instance.remaingServer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateServerListUI()
    {
        if(GameManager.Instance.completedServer == 8)
        {
            RemainingPanelText.text = (GameManager.Instance.completedServer + "/" + GameManager.Instance.remaingServer);
            GameManager.Instance.isFinished = true;
            GameManager.Instance.loadOption();
            GameManager.Instance.SwitchCountBug = 0;
            return;
        }
        RemainingPanelText.text = (GameManager.Instance.completedServer + "/" + GameManager.Instance.remaingServer);
        ServerFixedPanel.SetActive(true);
        RedLight.color = Color.grey;
        YellowLight.color = Color.grey;
        GreenLight.color = Color.grey;
        GameManager.Instance.SwitchCountBug = 0;
        StartCoroutine(ShowAndHide());
    }
    IEnumerator ShowAndHide()
    {
        yield return new WaitForSeconds(5f);
        ServerFixedPanel.SetActive(false);
    }
    public void UpdateUI()
    {
        BugsCount.text = GameManager.Instance.fixedBugs.ToString();
        switch (GameManager.Instance.SwitchCountBug)
        {
            case 1:
                RedLight.color = Color.red;
                break;
            case 2:
                YellowLight.color = Color.yellow;
                break;
            case 3:
                GreenLight.color = Color.green;
                SoundManager.Instance.PlaySFX(GameManager.Instance.ServerUnloack);
                break;
        }
    }
}
