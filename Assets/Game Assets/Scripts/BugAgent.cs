using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BugAgent : MonoBehaviour
{
    float health = 2000;
    public GameObject BugParticles;
    Transform temp;
    int current;
    NavMeshAgent PathFinder;
    public Vector3[] FlotaingPoints;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        PathFinder = GetComponent<NavMeshAgent>();
        temp = transform.GetChild(0).transform;
        FindTarget();
    }

    // Update is called once per frame
    void Update()
    {
        temp.position += Vector3.up * (Mathf.Sin(Time.time) * .03f);
    }
    public void TakeDamage()
    {
        if (health < 2000)
        {
            GameManager.Instance.NumberOfBugs--;
            GameManager.Instance.fixedBugs++;
            if (GameManager.Instance.SwitchCountBug < 3)
            {
                GameManager.Instance.SwitchCountBug++;
            }
            UIManager.instance.UpdateUI();
            SoundManager.Instance.PlaySFX(GameManager.Instance.KillBug);
            Instantiate(BugParticles, temp.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            
            health--;
        }
    }
    public void FindTarget()
    {
        for (int i = 0; i < 10; i++)
        {
            FlotaingPoints[i] = GameManager.Instance.GetSpawnPosition().position;
        }
        PathFinder.enabled = true;
        StartCoroutine(UpdatePath());
    }
    IEnumerator UpdatePath()
    {
            while (true)
            {
                Vector3 dir = FlotaingPoints[current] - transform.position;
                float dis = dir.sqrMagnitude;
                if (dis > 2f)
                    pos = Vector3.MoveTowards(transform.position, FlotaingPoints[current], 10 * Time.deltaTime);
                else
                    current = (current + 1) % FlotaingPoints.Length;
                PathFinder.SetDestination(pos);
                yield return new WaitForSeconds(.1f);
            }
        
    }
}
