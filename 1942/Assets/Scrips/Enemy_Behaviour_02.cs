using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy_Behaviour_02 : MonoBehaviour
{
    private Animator animator;
    private bool stopped = false, fire = false;
    private Collider2D coll;
    public GameObject projectile;
    public Transform LaunchOffSet;
    private Rigidbody2D rb;
    private Transform target;
    private float speed = 0.09f;
    private TextMeshProUGUI countText;
    private int life = 15;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        StartCoroutine(WaitTime_Fire());
        gameObject.layer = 0;
        countText = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        transform.position += Vector3.up * 0.01f;
        if(stopped) Destroy(this.gameObject);
        if(fire && !coll.isTrigger && GameObject.FindGameObjectWithTag("Player") != null){
            GameObject bullet =  Instantiate(projectile, LaunchOffSet.position, transform.rotation) as GameObject;
            bullet.transform.parent = this.transform;
            fire = false;
            StartCoroutine(WaitTime_Fire());
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            coll.isTrigger = true;   
            speed = 0.02f;
            animator.SetInteger("Shot_Down",1); 
            SetCountText();
            StartCoroutine(WaitTime());
            fire = false;
            collision.gameObject.SendMessage("ApplyDamage",1); 
        }
    }

    public void ApplyDamage_Enemy(int value)
    {
        life--;
        if(life <=0 ){
            //coll.isTrigger = true;   
            speed = 0.02f;
            animator.SetInteger("Shot_Down",1); 
            SetCountText();
            StartCoroutine(WaitTime());
            fire = false;
        }
    }

    IEnumerator WaitTime()                      //Esperar 0.667f segundos para destruir el objeto después de animación
    {
        stopped = false;
        //rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(1.3333f);
        stopped = true;
    }

    IEnumerator WaitTime_Fire()                  //Esperar 1.333 segundos para destruir el objeto después de animación
    {
        fire = false;
        yield return new WaitForSeconds(3.0f);
        coll.isTrigger = false;   
        fire = true;
        //rb.bodyType = RigidbodyType2D.Dynamic;          
    }


    void SetCountText(){
        countText.text = (System.Convert.ToInt32(countText.text)+100).ToString();
    }

}
