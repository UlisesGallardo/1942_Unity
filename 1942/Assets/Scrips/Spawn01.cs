using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn01 : MonoBehaviour
{

    public GameObject enemy;
    Vector2 WhereToSpawn;
    private float nextSpawn, randX;
    void Start()
    {
        nextSpawn = 5.0f;
        randX = 0.0f;
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad>nextSpawn && GameObject.FindGameObjectWithTag("Player") != null){
            nextSpawn = Time.timeSinceLevelLoad + 1.9f;
            randX = Random.Range(-20f,20f);
            WhereToSpawn = new Vector2(randX, transform.position.y);
            Instantiate(enemy, WhereToSpawn, Quaternion.identity);
        }   
        
    }
}
