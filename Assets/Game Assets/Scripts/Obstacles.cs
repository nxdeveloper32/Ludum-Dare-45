using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public int damage = 1;
    public float increseSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!ObstablesSpawner.instance.isPlaneDead)
        {
            transform.Translate(Vector2.left * ObstablesSpawner.instance.speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlaneController>().health -= damage;
            other.GetComponent<PlaneController>().TakeDamage();
            Destroy(gameObject);
        }
        if (other.CompareTag("Fire"))
        {
            ObstablesSpawner.instance.speed += increseSpeed;
            Destroy(gameObject);
        }
    }
}
