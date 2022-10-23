using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy_Behaviour : MonoBehaviour
{
    private Animator animator;
    private bool stopped = false, fire = false;
    private Collider2D coll;
    public GameObject projectile;
    public GameObject pow;
    public Transform LaunchOffSet;
    private Rigidbody2D rb;
    private Transform target;
    private float speed = 0.09f;
    private TextMeshProUGUI countText;
    private TextMeshProUGUI countEvades;
    private Vector3 vDirection;
    private Animator target2;
    private bool animation;
    private int score;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        StartCoroutine(WaitTime_Fire());
        gameObject.layer = 0;
        if(GameObject.FindGameObjectsWithTag("Player") != null){
            target  = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            target2 =  GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        }
        countText = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        countEvades = GameObject.FindGameObjectWithTag("Evade").GetComponent<TextMeshProUGUI>();
        animation = true;
        score = 0;
    }

    void FixedUpdate()
    {
        if(target != null)transform.position = Vector2.MoveTowards(transform.position, target.position, speed); //Seguir al target
    
        if(fire && !coll.isTrigger && GameObject.FindGameObjectWithTag("Player") != null){
            GameObject bullet =  Instantiate(projectile, LaunchOffSet.position, transform.rotation) as GameObject;
            //bullet.transform.parent = this.transform;
            fire = false;
            StartCoroutine(WaitTime_Fire());
        } 
        if(stopped) Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player" ){
            coll.isTrigger = true;   
            speed = 0.02f;
            animator.SetInteger("Shot_Down",1); 
            SetCountText();
            fire = false;
            StartCoroutine(WaitTime());
        }
    }

    public void ApplyDamage_Enemy(int value)
    {
        coll.isTrigger = true;   
        speed = 0.02f;
        animator.SetInteger("Shot_Down",1); 
        
        StartCoroutine(WaitTime());
        fire = false;
    }

    IEnumerator WaitTime()                      //Esperar 0.667f segundos para destruir el objeto después de animación
    {
        stopped = false;
        rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(0.667f);
        SetCountText();
        stopped = true;
    }

    IEnumerator WaitTimeAnimation()                      
    {
        animation = false;
        yield return new WaitForSeconds(0.3f);
        animation = true;
    }

    IEnumerator WaitTime_Fire()                  //Esperar 1.333 segundos para destruir el objeto después de animación
    {
        fire = false;
        yield return new WaitForSeconds(2.0f);
        coll.isTrigger = false;   
        fire = true;
        rb.bodyType = RigidbodyType2D.Dynamic;          
    }


    void SetCountText(){
        int curret_evades = System.Convert.ToInt32(countEvades.text);
        score = System.Convert.ToInt32(countText.text);
        if(score>0 && score%600 == 0 && curret_evades<=5){
            GameObject bullet =  Instantiate(pow, LaunchOffSet.position, transform.rotation) as GameObject;
        }
        
        countText.text = (System.Convert.ToInt32(countText.text)+50).ToString();
    }

}
