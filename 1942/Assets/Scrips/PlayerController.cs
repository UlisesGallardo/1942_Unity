using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movementX, movementY;
    private float speed = 10.0f;
    public GameObject projectile;
    public Transform LaunchOffSet;
    private bool locked = false, stopped = false, inAnimation = false;
    private Collider2D coll;

    private Animator animator;
    private TextMeshProUGUI countText, highscore;
    public GameObject gameoverText,btn1,btn2;
    public GameOver01 gameover;
    private int score=0, hscore=0, life = 3;
    public bool isInvisible;
    public GameObject [] hearts;

    public TextMeshProUGUI countEvades;
    private int evades;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetInteger("Direction",0);
        
        coll = GetComponent<Collider2D>();
        gameObject.layer = 1;
        countText = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        highscore = GameObject.FindGameObjectWithTag("HScore").GetComponent<TextMeshProUGUI>();

        hscore = PlayerPrefs.GetInt("HighScore");

        gameoverText.SetActive(false);
        btn1.SetActive(false);
        btn2.SetActive(false);
        gameover = GameObject.FindGameObjectWithTag("TagGO").GetComponent<GameOver01>();

        locked = true;
        isInvisible = false;
        evades = 3;
        animator.SetInteger("Direction",6);
        StartCoroutine(Iniciar(2.083f));
    }

    private void OnMove(InputValue movementValue){
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;

            if(!inAnimation){
                if(animator.GetInteger("Direction") == 1 && Keyboard.current.aKey.isPressed) animator.SetInteger("Direction",2);
                else if(animator.GetInteger("Direction") != 3 && Keyboard.current.dKey.isPressed ) animator.SetInteger("Direction",1);
                else if(animator.GetInteger("Direction") == 3 && Keyboard.current.dKey.isPressed) animator.SetInteger("Direction",4);
                else if(Keyboard.current.aKey.isPressed) animator.SetInteger("Direction",3);
                else if(Keyboard.current.wKey.isPressed || Keyboard.current.sKey.isPressed) animator.SetInteger("Direction",5); 
            }
    }

    void FixedUpdate(){
        score  = System.Convert.ToInt32(countText.text);
        hscore = System.Convert.ToInt32(highscore.text);
        Debug.Log(score+" "+hscore);
        if(score > hscore){
            
            hscore = score;
            highscore.text = hscore.ToString();
        } 

        Vector2 movement = new Vector2(movementX*speed, movementY*speed);
        rb.velocity = movement;   
        //transform.Translate(0, -Time.fixedDeltaTime * speed, 0);

        if(Keyboard.current.spaceKey.isPressed && !locked && !inAnimation){
            GameObject bullet = Instantiate(projectile, LaunchOffSet.position, transform.rotation) as GameObject;
            StartCoroutine(WaitTime());
        }

        if(Keyboard.current.qKey.isPressed && !inAnimation && evades>0){
                evades-=1;
                countEvades.text = (evades).ToString();
                locked = true;
                isInvisible = true;
                invisible();
        }
        
        if(stopped){
            Destroy(this.gameObject);
        } 
        
    }

    public void SaveScore(){
            PlayerPrefs.SetInt("HighScore",hscore);
            PlayerPrefs.Save();
    }

    void invisible(){
        animator.SetInteger("Direction",6);
        StartCoroutine(WaitTime(2.083f));
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy02" ){
            ApplyDamage_Player(1);
        }
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pow01"){
            evades++;
            countEvades.text = (evades).ToString();
            Destroy(collision.gameObject);
        }
    }

    public void ApplyDamage_Player(int value)
    {
        life--;
        if(life <= 0){
            for(int i=0; i<3; i++)hearts[i].SetActive(false);
            gameover.EndGame();
            animator.SetInteger("Shot_Down",1); 
            rb.bodyType = RigidbodyType2D.Static;
            StartCoroutine(WaitTime_Animation());
        }else{
            hearts[life].SetActive(false);
        }
        
    }

    IEnumerator WaitTime()                          //Esperar 0.1 segundos para el siguiente disparo
    {
        locked = true;
        yield return new WaitForSeconds(0.1f);
        locked = false;
    }

    IEnumerator WaitTime_Animation()                //Esperar 1.333 segundos para destruir el objeto después de animación
    {
        stopped = false;
        yield return new WaitForSeconds(1.333f);
        stopped = true;
    }
  
    IEnumerator WaitTime(float value)              
    {
        
       // coll.isTrigger = true;   
        gameObject.layer = 8;
        inAnimation = true;
        locked = true;
        yield return new WaitForSeconds(value);
        //coll.isTrigger =  false; 
        inAnimation = false;
        locked = false;
        animator.SetInteger("Direction",0);
        isInvisible = false;
        gameObject.layer = LayerMask.NameToLayer("Player");

    }

    IEnumerator Iniciar(float value)              
    {
        inAnimation = true;
        locked = true;
        yield return new WaitForSeconds(value);
        inAnimation = false;
        locked = false;
        animator.SetInteger("Direction",0);
    }
}
