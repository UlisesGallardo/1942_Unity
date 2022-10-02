using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour_Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 1.0f;
    private Transform target;
    private Vector3 vDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.layer = 0;
        if(GameObject.FindGameObjectWithTag("Player") != null){
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
            vDirection = target.position - transform.position;  
        }else{
            vDirection = transform.position;
            vDirection = vDirection.normalized; 
        }
                //Physics2D.IgnoreLayerCollision(0, 0, true);
                //Physics2D.IgnoreLayerCollision(0, 2, false);

    }

    void FixedUpdate()
    {       
        transform.position += vDirection * speed * Time.fixedDeltaTime ;
    }

    
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            collision.gameObject.SendMessage("ApplyDamage_Player",1);  
            Destroy(gameObject); 
        }
    }
    
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
