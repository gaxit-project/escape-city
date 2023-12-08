using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;

public class GameManager : MonoBehaviour
{
    public GameObject keepCanvas;
    public GameObject PauseMenu;
    public GameObject Clear;
    public GameObject GameOver;
    public GameObject Cursor;
    public GameObject task;
    public GameObject score;
    public GameObject virtalmouse;
    public AudioSource audioCliar;
    public AudioSource audioGameOver;
    public AudioSource audioSe;
    public AudioSource audiobullet;
    public AudioSource audioitem;
    public AudioSource changeweapon;
    public AudioSource footwalk;
    public AudioSource swing;
    public AudioSource damagesound;
    public AudioSource bikkurisound;
    public AudioSource zombiesound;
    private GameObject mousemem;
    private RectTransform rectTransform;
    private GameObject brige;
    public GameObject scoreranking;
    public FadeScript Fadepanel;
    public bool mainmenufanction=true;
    private int itemnum;
    public bool overcount=false;
    private bool gameclear=false;
    public TimerScript timer;
    
    
    void Start()
    {
        brige=GameObject.Find("brige");
        if(brige!=null)brige.SetActive(false);
        if(mainmenufanction){
            PauseMenu.SetActive(false);
            Clear.SetActive(false);
            GameOver.SetActive(false);
            Cursor.SetActive(false);
            rectTransform = Cursor.GetComponent<RectTransform>();
        }
        
    }


    void Update()
    {
        if(!mainmenufanction)return;
        if(PauseMenu.activeInHierarchy)
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
            {
            audioSe.Play();
            PauseMenu.SetActive(false);
            Cursor.SetActive(false);
            virtalmouse.SetActive(false);
            scoreranking.SetActive(true);
            //Destroy(mousemem);
            Time.timeScale = 1;
            }
            
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
            {
            audioSe.Play();
            scoreranking.SetActive(false);
            PauseMenu.SetActive(true);
            Cursor.SetActive(true);
            virtalmouse.SetActive(true);
            /*rectTransform.anchoredPosition = new Vector2(412,222);
            mousemem=Instantiate (virtalmouse, virtalmouse.transform.position, Quaternion.identity);
            mousemem.SetActive(true);*/
            Time.timeScale = 0f;
            }
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            audioCliar.Play();
            Clear.SetActive(true);
        }
        
    }

    public void Over()
    {
        if(gameclear)return;
        if(overcount)return;
        overcount=true;
        timer.timerstop=true;
        task.SetActive(false);
        score.GetComponent<MenuScoreManager>().Allscoreoutput();
        score.GetComponent<MenuScoreManager>().scoreRock=true;
        score.GetComponent<MenuScoreManager>().ScoreRankingUpdate();
        audioGameOver.Play();
        GameOver.SetActive(true);
        virtalmouse.SetActive(true);
        Cursor.SetActive(true);
    }
    public void GameClear(){
        if(overcount)return;
        if(gameclear)return;
        gameclear=true;
        timer.timerstop=true;
        timer.AddScore();
        task.GetComponent<GuidMasage>().AddScore();
        task.SetActive(false);
        score.GetComponent<MenuScoreManager>().Allscoreoutput();
        score.GetComponent<MenuScoreManager>().scoreRock=true;
        score.GetComponent<MenuScoreManager>().ScoreRankingUpdate();
        audioCliar.Play();
        Clear.SetActive(true);
        brige.SetActive(true);
        virtalmouse.SetActive(true);
        Cursor.SetActive(true);
        Time.timeScale = 0f;
    }
    public void SoundBullet()
    {
        audiobullet.Play();
    }
    public void Soundchangeweapon()
    {
        changeweapon.Play();
    }
    public void Sounditem()
    {
        audioitem.Play();
    }
    public void Soundfoot()
    {
        footwalk.Play();
    }
    public void Soundswing()
    {
        swing.Play();
    }
    public void Sounddamage()
    {
        damagesound.Play();
    }
    public void SoundBikkuri()
    {
        bikkurisound.PlayOneShot(bikkurisound.clip);
    }
    public void SoundZombieSound()
    {
        zombiesound.PlayOneShot(zombiesound.clip, 1 / Patrol.distance);
    }
    public void taskUpdate(int count){
        itemnum=count;
        task.GetComponent<GuidMasage>().MesageUpdate(itemnum);
    }
    public void subtaskUpdate(int ach){
        if(ach>=1){
            task.GetComponent<GuidMasage>().Mission_List[1].outputdistance=false;
            task.GetComponent<GuidMasage>().Mission_List[1].achievement=1;
        }else{
            task.GetComponent<GuidMasage>().Mission_List[1].outputdistance=true;
            task.GetComponent<GuidMasage>().Mission_List[1].achievement=0;
        }
    }
    public void failsubtask(){
        task.GetComponent<GuidMasage>().Mission_List[1].clear=true;
    }
    public void scoreUpdate(int add,string strings){
        score.GetComponent<MenuScoreManager>().ScoreUpdate(add,strings);
    }
    public void movieStart(){
        overcount=true;
        Fadepanel.Fadein=true;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(keepCanvas);
        DontDestroyOnLoad(virtalmouse);
        Invoke("changeTimeupcine", 1.0f);
        Invoke("kaijodestroy", 1.5f);
    }
    void changeTimeupcine(){
        SceneManager.LoadScene("TimeUp");
    }
    void kaijodestroy(){
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(keepCanvas, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(virtalmouse, SceneManager.GetActiveScene());
        Fadepanel.Fadeout=true;
        overcount=false;
    }
}
