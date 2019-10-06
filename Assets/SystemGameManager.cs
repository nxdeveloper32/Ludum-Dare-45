using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemGameManager : MonoBehaviour
{
    public int lineOfCode = 0;
    public int NumberOfBugs = 0;
    public int NumberOfCoins = 0;
    public float TimeOfStart;
    private static SystemGameManager instance;
    public static SystemGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SystemGameManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned System GameManager", typeof(SystemGameManager)).GetComponent<SystemGameManager>();
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        TimeOfStart = Time.time;
    }
    private void Start()
    {
        if(SystemUIManager.instance == null)
        {
            SoundManager.Instance.PlayMusicWithCrossFade(GameManager.Instance.BackgroundSound);
        }
        else
        {
            SoundManager.Instance.PlayMusicWithCrossFade(SystemUIManager.instance.BackgroundSound);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(Time.realtimeSinceStartup);
        }*/
    }
}
