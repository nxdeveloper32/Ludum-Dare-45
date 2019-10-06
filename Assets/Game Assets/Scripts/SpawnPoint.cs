using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject obstable;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(obstable, transform.position, Quaternion.identity);
    }
}
