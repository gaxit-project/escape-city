using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
   Animator anim;
    public GameObject GameOptions;
    public AudioSource audioSource;
    public GameObject Cursor;
    public GameObject virtalmouse;
    public GameObject scoreranking;

    void Start () 
    {
        scoreranking.SetActive(true);
        anim = GetComponent<Animator>(); 
    }

    public void ToTitle()
    {
        anim.Play("buttonTweenAnims_on");
        audioSource.Play();
        scoreranking.SetActive(false);
        Invoke("ChangeScene",0.1f);
        Time.timeScale = 0.2f;
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("shimo1");
    }

   
    public void Back()
    {
        Time.timeScale = 1;
        virtalmouse.SetActive(false);
        anim.Play("buttonTweenAnims_on");
        audioSource.Play();
        GameOptions.SetActive(false);
        Cursor.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        audioSource.Play();
        Invoke("ResetScene",0.1f);
    }
    void ResetScene()
    {
        SceneManager.LoadScene("Main");
    }
    
    public void Quit()
    {   
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        
#else
        Application.Quit();
        GetComponent<AudioSource>().Play();
#endif
    }
    public void playHoverClip()
    {
       
    }

    void playClickSound() 
    {

    }

}
