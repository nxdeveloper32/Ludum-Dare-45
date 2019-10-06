using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    //public GameObject laserPrefab;
    //public GameObject firePoint;

    public GameObject Laser;
    public Camera FpsCamer;
    public GameObject escapePanel;
    bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        FpsCamer = transform.GetChild(0).GetComponent<Camera>();
        //spawnedLaser = Instantiate(laserPrefab, firePoint.transform) as GameObject;
        //DisableLaser();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0.0f;
                Cursor.visible = true;
                isPaused = true;
                escapePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1.0f;
                Cursor.visible = false;
                isPaused = false;
                escapePanel.SetActive(false);
            }
        }
        if (!GameManager.Instance.isFinished && !isPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                EnableLaser();
            }
            if (Input.GetMouseButton(0))
            {
                CheckRay();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            DisableLaser();
        }
    }
    void EnableLaser()
    {
        SoundManager.Instance.PlayLaserLoop(GameManager.Instance.Laser);
        Laser.SetActive(true);
    }
    void DisableLaser()
    {
        SoundManager.Instance.StopLaserLoop();
        Laser.SetActive(false);
    }
    void CheckRay()
    {
        RaycastHit hit;
        if(Physics.Raycast(FpsCamer.transform.position,FpsCamer.transform.forward,out hit, 20f)){
            if (hit.transform.tag == "Bug")
            {
                hit.transform.parent.GetComponent<BugAgent>().TakeDamage();
            }
        }
    }
}
