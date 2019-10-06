using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public Vector2 targetPos;
    public float Yincrement;
    public float speed;
    public float maxHeight;
    public float minHeight;
    public float health;
    public Transform HealthPanel;
    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            SoundManager.Instance.PlaySFX(ObstablesSpawner.instance.Explosion);
            Destroy(HealthPanel.gameObject);
            ObstablesSpawner.instance.isPlaneDead = true;
            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if ((Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.W)) && transform.position.y < maxHeight))
        {
            targetPos = new Vector2(transform.position.x, transform.position.y + Yincrement);
        }
        else if((Input.GetKeyDown(KeyCode.DownArrow)||(Input.GetKeyDown(KeyCode.S)) && transform.position.y > minHeight))
        {
            targetPos = new Vector2(transform.position.x, transform.position.y - Yincrement);
        }
    }
    public void TakeDamage()
    {
        Destroy(HealthPanel.GetChild(0).gameObject);
        SoundManager.Instance.PlaySFX(ObstablesSpawner.instance.HitSound);
    }
}
