using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletBehaviour : MonoBehaviour
{
    private float speed = 3.0f;
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D coll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameObject.layer = 1;
        coll = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {   
        Physics2D.IgnoreLayerCollision(0, 0, true);
        Physics2D.IgnoreLayerCollision(0, 2, false);
        Vector2 movement = new Vector2(transform.position.x, transform.position.y);
        rb.velocity = movement * speed;  
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Enemy"){
            Destroy(this.gameObject);  
            collision.gameObject.SendMessage("ApplyDamage_Enemy",1); 
        }else if(collision.gameObject.tag == "Enemy02"){
            coll.isTrigger = true;
            animator.SetInteger("Damage",1); 
            speed = 0.9f;
            StartCoroutine(WaitTime_Fire());
            collision.gameObject.SendMessage("ApplyDamage_Enemy",1); 
        }else if(collision.gameObject.tag == "MainCamera"){
            Destroy(this.gameObject);  
        }
        //Destroy(collision.gameObject);
    }

    IEnumerator WaitTime_Fire()                  //Esperar 0.5 segundos para destruir 
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);  
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

}
