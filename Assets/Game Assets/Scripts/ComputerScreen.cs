using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ComputerScreen : MonoBehaviour
{
    public bool IsEnabled;
    public bool IsBlank;
    public bool isAutomated;
    public GameObject Screen;
    public GameObject Image;
    GameObject go;
    Image SelectionImage;
    int random;
    CannonManager Cannon;
    public string MyText;
    GameObject Temp;

    private void Start()
    {
        SelectionImage = GetComponent<Image>();
        Cannon = CannonManager.instance;
        SelectionImage.enabled = false;
        if (IsEnabled)
        {
            Screen.SetActive(IsEnabled);
            Image.SetActive(!IsEnabled);
        }
        else
        {
            if (!IsBlank)
            {
                Screen.SetActive(IsEnabled);
                Image.SetActive(!IsEnabled);
            }
            else
            {
                Screen.SetActive(IsEnabled);
            }
        }
        //StartCoroutine(SpawnItem());
        SpawnItemAnim();
    }
    public void DestoryGO()
    {
        Destroy(go);
        go = null;
    }
    void Update()
    {
        
    }
    public void UpdateScreens()
    {
        if (IsEnabled)
        {
            Screen.SetActive(IsEnabled);
            Image.SetActive(!IsEnabled);
        }
        else
        {
            if (!IsBlank)
            {
                Screen.SetActive(IsEnabled);
                Image.SetActive(!IsEnabled);
            }
            else
            {
                Screen.SetActive(IsEnabled);
            }
        }
    }
    public void LookAtMe()
    {
        //Cannon.transform.LookAt(transform);
        if (IsEnabled)
        {
            SoundManager.Instance.PlaySFX(GameManager.Instance.buttonClick);
            if (Cannon.MyFocus != null)
            {
                Temp = Cannon.MyFocus;
                Temp.GetComponent<Image>().enabled = false;
            }
            SelectionImage.enabled = true;
            Cannon.MyFocus = transform.gameObject;
            Vector3 diff = (transform.position - Cannon.transform.position);
            float angle = Mathf.Atan2(diff.y, diff.x);
            Cannon.transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg - 20);
        }
        
    }
    /*IEnumerator SpawnItem()
    {
        while (IsEnabled)
        {
            
        }
        
    }*/
    public void SpawnItemAnim()
    {
        if (go != null)
        {
            CannonManager.instance.NumberOfMissAllowed--;
            if(CannonManager.instance.NumberOfMissAllowed <= 0)
            {
                //CannonManager.instance.LoosePoint();
                CannonManager.instance.LostScreenPanelFun();
            }
            else
            {
                CannonManager.instance.LoosePoint();
            }
            Destroy(go);
        }
        random = Random.Range(0, Cannon.prefab.Length);
        go = Instantiate(Cannon.prefab[random], Screen.transform.position, Quaternion.identity);
        go.transform.SetParent(Screen.transform);
        go.transform.localScale = Vector3.one;
        MyText = go.GetComponent<ColorSetter>().MyText;
    }
    public void AutomateComputer()
    {
        IsEnabled = false;
        isAutomated = true;
        UpdateScreens();
    }
}
