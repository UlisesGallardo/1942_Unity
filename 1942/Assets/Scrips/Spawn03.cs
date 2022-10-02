using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn03 : MonoBehaviour
{

    public GameObject enemy;
    Vector2 WhereToSpawn;
    private float nextSpawn, randX;
    void Start()
    {
        nextSpawn = 2.0f;
        randX = 0.0f;
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad>nextSpawn && GameObject.FindGameObjectWithTag("Player") != null){
            nextSpawn = Time.timeSinceLevelLoad + 1.5f;
            //randX = Random.Range(-1f,1f);
            WhereToSpawn = new Vector2(transform.position.x, transform.position.y);
            Instantiate(enemy, WhereToSpawn, Quaternion.identity);
        }   
        
    }
}
