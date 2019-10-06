using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned GameManager", typeof(GameManager)).GetComponent<GameManager>();
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public AudioMixer audioMixer;
    public AudioMixerGroup SoundEffectGroup;
    public AudioMixerGroup MusicGroup;
    public AudioClip buttonClick;
    public AudioClip error;
    public AudioClip success;
    public AudioClip Laser;
    public AudioClip KillBug;
    public AudioClip ServerUnloack;
    public AudioClip NewScreen;
    public AudioClip LoosePoint;
    public AudioClip Explosion;
    public AudioClip BackgroundSound;
    public AudioClip Coins;
    public Transform[] FloatingPoints;
    public int NumberOfBugs;
    public float SpawnRate = 5F;
    public float timestamp = 0F;
    public int remaingServer;
    public int completedServer;
    public int SwitchCountBug;
    public int fixedBugs;
    public GameObject ParticleSystemGO;
    public bool isFinished;
    // Start is called before the first frame update
    void Awake()
    {
        completedServer = 0;
        remaingServer = 8;
        //DontDestroyOnLoad(gameObject);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.K))
        {
            loadOption();
        }*/
    }
    public Transform GetSpawnPosition()
    {
        int randomNum = Random.Range(0, FloatingPoints.Length);
        return FloatingPoints[randomNum];
    }
    public void loadOption()
    {
        ParticleSystemGO.SetActive(true);
        SystemGameManager.Instance.NumberOfBugs += fixedBugs;
        StartCoroutine(LoadMe());
        
    }
    IEnumerator LoadMe()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadSceneAsync(1);
    }
    public void loadScene(int index)
    {
        if(index == 3)
        {
            CannonManager.instance.SaveScreenMsg();
        }
        else
        {
            SceneManager.LoadSceneAsync(index);
        }
    }
    public void LoadBugscene()
    {
        SceneManager.LoadSceneAsync(3);
    }
}
