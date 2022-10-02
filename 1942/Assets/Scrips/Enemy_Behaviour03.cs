using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy_Behaviour03 : MonoBehaviour
{
    private Animator animator;
    private bool stopped = false, fire = false;
    private Collider2D coll;
    public GameObject projectile;
    public Transform LaunchOffSet;
    private Rigidbody2D rb;
    private Transform target;
    private float speed = 5.0f;
    private TextMeshProUGUI countText;
    private Vector3 vDirection;
    private bool animation;

    
    public GameObject [] points;
    private int index = 0;
    private Transform ToMove;
    public int Direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        //StartCoroutine(WaitTime_Fire());
        gameObject.layer = 0;
        countText = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        animation = true;
        
        ToMove = points[index].transform;
    }

    void FixedUpdate()
    {
        
        if(index >= points.Length){
                    Destroy(this.gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, ToMove.position, speed * Time.fixedDeltaTime);

        if(Vector2.Distance(transform.position, ToMove.position)<=0){
                speed = 10.0f;
                index++;
                if(index==1)fire = true;
                if(index == 4){
                    animator.SetInteger("Direction",Direction); 
                }
                ToMove = points[index].transform;
        }

        if(fire && GameObject.FindGameObjectWithTag("Player") != null){
            Instantiate(projectile, LaunchOffSet.position, transform.rotation);
            fire = false;
            StartCoroutine(WaitTime_Fire());
        } 
        if(stopped) Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
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
        SetCountText();
        StartCoroutine(WaitTime());
        fire = false;
    }

    IEnumerator WaitTime()                      //Esperar 0.667f segundos para destruir el objeto después de animación
    {
        stopped = false;
        rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(0.667f);
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
    }


    void SetCountText(){
        countText.text = (System.Convert.ToInt32(countText.text)+50).ToString();
    }

}
