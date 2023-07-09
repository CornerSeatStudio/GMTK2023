using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject targetUI;
    //public UIManager uiManager;

    private float fadeTimer;
    public Image fadeObject;

    public GameObject click;
    public bool isPaused;
    public bool activeOnEscape;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        
        
    }

    void Start(){
        fadeTimer=0;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeOnEscape)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
            {
                Pause();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
                {
                    Resume();
                }
            }
        }
        
    }
    public void RestartGame(){
        Instantiate(click, Vector3.zero, Quaternion.identity);
        Time.timeScale=1f;
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        
    }

    public void Resume(){
        Instantiate(click, Vector3.zero, Quaternion.identity);
        isPaused = false;
        Time.timeScale=1f;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        targetUI.SetActive(false);
        //uiManager.canPause=true;
        //print("resume called");
    }
    public void Pause(){
        Instantiate(click, Vector3.zero, Quaternion.identity);
            isPaused = true;
            targetUI.SetActive(true);
            Time.timeScale=0f;
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
            
            //print("pause called");
    }

    public void StartGame(string scene){
        Instantiate(click, Vector3.zero, Quaternion.identity);
        Debug.Log("test");
        fadeObject.gameObject.SetActive(true);
        //print("start game");
        StartCoroutine(FadeBlackStart(scene));
        Time.timeScale=1f;
    }
    IEnumerator FadeBlackStart(string scene){
        yield return new WaitForSeconds(0.02f);
        //print("start coroutine");
        if(fadeTimer<1f){
            //print(fadeTimer);
            fadeTimer+=0.02f;
            fadeObject.color= new Color(fadeObject.color.r, fadeObject.color.g, fadeObject.color.b, fadeObject.color.a + 0.02f);
            StartCoroutine(FadeBlackStart(scene));
        }
        else{
            SceneManager.LoadScene(scene);
        }
        
    
        
        
        
    }

    public void ClearedLevel()
    {
        Instantiate(click, Vector3.zero, Quaternion.identity);
        
        targetUI.SetActive(true);
        Time.timeScale = 0f;
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;

        //print("pause called");
    }

}
