using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn02 : MonoBehaviour
{

    public GameObject enemy;
    Vector2 WhereToSpawn;
    private float nextSpawn = 6.0f, randX = 0.0f;
    void Start()
    {
        
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad>nextSpawn && GameObject.FindGameObjectWithTag("Player") != null){
            nextSpawn = Time.timeSinceLevelLoad + 50.0f;
            randX = Random.Range(-10f,10f);
            WhereToSpawn = new Vector2(randX, transform.position.y);
            Instantiate(enemy, WhereToSpawn, Quaternion.identity);
        }   
        
    }
}
