using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver01 : MonoBehaviour
{
    public GameObject gameoverText,btn1,btn2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame(){
        gameoverText.SetActive(true);
        btn1.SetActive(true);
        btn2.SetActive(true);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void TryAgain(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
