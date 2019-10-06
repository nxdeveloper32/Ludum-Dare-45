using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ObstablesSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public static ObstablesSpawner instance;
    public Transform[] Obstable;
    public GameObject ObstablePrefab;
    private float timeBtwSpawn;
    public float StartTimeBtwSpawn;
    public float decreaseTime;
    public float minTime = 0.65f;
    public float speed = 6;
    public bool isPlaneDead;
    bool PlayerDeadUI;
    public GameObject PanelUI;
    public AudioClip CoinSound;
    public AudioClip HitSound;
    public AudioClip Explosion;
    public int Coins;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI NavMenuCoinText;
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaneDead)
        {
            if (timeBtwSpawn <= 0)
            {
                int rand = Random.Range(0, Obstable.Length);
                Instantiate(ObstablePrefab, Obstable[rand].position, Quaternion.identity);
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
            Coins++;
            CoinText.text = Coins.ToString();
        }
        else
        {
            if (!PlayerDeadUI)
            {
                PlayerDeadUI = true;
                NavMenuCoinText.text = "Coins = " + Coins.ToString();
                PanelUI.SetActive(true);
                SoundManager.Instance.PlaySFX(CoinSound);
            }
        }
        
    }
    public void NavigateToGameSystem()
    {
        SystemGameManager.Instance.NumberOfCoins += Coins;
        SceneManager.LoadScene(1);
    }
}
