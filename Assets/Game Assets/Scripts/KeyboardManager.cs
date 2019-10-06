using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    public GameObject[] LookAtMe;
    public GameObject[] SelectMe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CannonManager.instance.isEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                LookAtMe[0].GetComponent<ComputerScreen>().LookAtMe();
            }
            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                LookAtMe[1].GetComponent<ComputerScreen>().LookAtMe();
            }
            if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                LookAtMe[2].GetComponent<ComputerScreen>().LookAtMe();
            }
            if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                LookAtMe[3].GetComponent<ComputerScreen>().LookAtMe();
            }
            if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
            {
                LookAtMe[4].GetComponent<ComputerScreen>().LookAtMe();
            }
            if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))
            {
                LookAtMe[5].GetComponent<ComputerScreen>().LookAtMe();
            }
            if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7))
            {
                LookAtMe[6].GetComponent<ComputerScreen>().LookAtMe();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                SelectMe[0].GetComponent<ColorSetter>().SelectMe();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SelectMe[1].GetComponent<ColorSetter>().SelectMe();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                SelectMe[2].GetComponent<ColorSetter>().SelectMe();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                SelectMe[3].GetComponent<ColorSetter>().SelectMe();
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                SelectMe[4].GetComponent<ColorSetter>().SelectMe();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                SelectMe[5].GetComponent<ColorSetter>().SelectMe();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                SelectMe[6].GetComponent<ColorSetter>().SelectMe();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                CannonManager.instance.Shoot();
            }
        }
    }
}
