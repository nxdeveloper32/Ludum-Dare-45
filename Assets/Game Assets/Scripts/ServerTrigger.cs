using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ServerTrigger : MonoBehaviour
{
    public GameObject BugPrefab;
    public GameObject InfoPanel;
    public GameObject FixThreeBugBeforeServerPanel;
    bool isPlayerInside;
    public GameObject FixingPanel;
    public Image LoadingImg;
    float fixing = 0;
    Coroutine LoadingCoroutine;
    Coroutine BugsCoroutine;
    bool TriggerLoading;
    bool UpdateServer;
    public GameObject SecuredServer;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (GameManager.Instance.SwitchCountBug != 3)
            {
                FixThreeBugBeforeServerPanel.SetActive(true);
            }
            else
            {
                FixThreeBugBeforeServerPanel.SetActive(false);
                InfoPanel.SetActive(true);
                isPlayerInside = true;
            }
        }
        else
        {
            FixThreeBugBeforeServerPanel.SetActive(false);
            InfoPanel.SetActive(false);
            isPlayerInside = false;
        }
    }
    private void Update()
    {
            if (Input.GetMouseButton(1) && isPlayerInside)
            {
                
                    InfoPanel.SetActive(false);
                    FixingPanel.SetActive(true);
                    if (!TriggerLoading)
                    {
                        TriggerLoading = true;
                        LoadingCoroutine = StartCoroutine(Loading());
                    }
                
                //Debug.Log("Temp");
            }
            if (Input.GetMouseButtonUp(1) && isPlayerInside)
            {
                UpdateServer = false;
                fixing = 0;
                LoadingImg.fillAmount = 0;
                if(LoadingCoroutine != null)
                    StopCoroutine(LoadingCoroutine);
                TriggerLoading = false;
                FixingPanel.SetActive(false);
            }
    }
    IEnumerator Loading()
    {
        while (true){
            fixing++;
            LoadingImg.fillAmount = fixing / 100;
            if (LoadingImg.fillAmount == 1)
            {
                if (!UpdateServer)
                {
                    if (LoadingCoroutine != null)
                        StopCoroutine(LoadingCoroutine);
                    LoadingImg.fillAmount = 0;
                    UpdateServer = true;
                    GameManager.Instance.completedServer++;
                    transform.GetComponent<SphereCollider>().enabled = false;
                    fixing = 0;
                    FixingPanel.SetActive(false);
                    isPlayerInside = false;
                    if (BugsCoroutine != null)
                        StopCoroutine(BugsCoroutine);
                    UIManager.instance.UpdateServerListUI();
                    SecuredServer.SetActive(true);
                }
            }
            yield return null ;
        }
    }
    private void Start()
    {
        BugsCoroutine = StartCoroutine(SpawnBug());
    }
    IEnumerator SpawnBug()
    {
        while (true)
        {
            if (GameManager.Instance.NumberOfBugs < 30 && Time.time >= GameManager.Instance.timestamp)
            {
                GameManager.Instance.NumberOfBugs++;
                Instantiate(BugPrefab, transform.position, Quaternion.identity);
                if (GameManager.Instance.NumberOfBugs == 29)
                {
                    GameManager.Instance.timestamp = Time.time + GameManager.Instance.SpawnRate;
                }
            }
            yield return new WaitForSeconds(1f);
        }
        
    }
}
