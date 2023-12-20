using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;

public class GameManager : MonoBehaviour
{
    public GameObject keepCanvas;
    public GameObject gunCanvas;
    public GameObject invCanvas;
    public GameObject PauseMenu;
    public GameObject Map;
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
    public AudioSource kubakusound;
    public AudioSource hikokisound;
    public AudioSource yokohikokisound;
    private GameObject mousemem;
    private RectTransform rectTransform;
    private GameObject brige;
    public GameObject scoreranking;
    public FadeScript Fadepanel;
    public bool mainmenufanction=true;
    public int itemnum;
    public bool overcount=false;
    private bool gameclear=false;
    public bool timeover=false;
    public TimerScript timer;
    public bool playerinput=false;
    public bool enemymove=false;
    public bool CanvasOFF=true;
    public bool Tuitlial=true;
    private float pausetimeScale=1.0f;
    
    
    void Start()
    {
        if(GameObject.Find("TuitlialOBJ")==null){
            playerinput=true;
            Tuitlial=false;
            enemymove=true;
            CanvasOFF=false;
        }
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
        if(CanvasOFF&&!timeover){
            keepCanvas.SetActive(false);
            gunCanvas.SetActive(false);
            invCanvas.SetActive(false);
        }else if(!timeover){
            keepCanvas.SetActive(true);
            gunCanvas.SetActive(true);
            invCanvas.SetActive(true);
        }
        if(PauseMenu.activeInHierarchy)
        {
            if((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))&&!Tuitlial)
            {
            audioSe.Play();
            Map.SetActive(true);
            PauseMenu.SetActive(false);
            Cursor.SetActive(false);
            virtalmouse.SetActive(false);
            scoreranking.SetActive(true);
            //Destroy(mousemem);
            Time.timeScale = pausetimeScale;
            }
            
        }
        else
        {
            if((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))&&!Tuitlial)
            {
            audioSe.Play();
            Map.SetActive(false);
            scoreranking.SetActive(false);
            PauseMenu.SetActive(true);
            Cursor.SetActive(true);
            virtalmouse.SetActive(true);
            /*rectTransform.anchoredPosition = new Vector2(412,222);
            mousemem=Instantiate (virtalmouse, virtalmouse.transform.position, Quaternion.identity);
            mousemem.SetActive(true);*/
            pausetimeScale=Time.timeScale;
            Time.timeScale = 0f;
            }
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            audioCliar.Play();
            Clear.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.K))//デバック用　Timer 0
        {
            movieStart();
        }
        if(Input.GetKeyDown(KeyCode.O))//デバック用　GameOver
        {
            Over();
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
        
        if(!timeover)
        {
            audioGameOver.Play();
        }
        GameOver.SetActive(true);
        virtalmouse.SetActive(true);
        Cursor.SetActive(true);
    }
    public void changeTuitlial(){
        Invoke("Tuitlials",0.5f);
    }
    public void Tuitlials(){
        Tuitlial=false;
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
        timeover=true;
        Fadepanel.Fadein=true;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(keepCanvas);
        DontDestroyOnLoad(virtalmouse);
        Invoke("changeTimeupcine", 1.0f);
        Invoke("kaijodestroy", 1.5f);
        hikokisound.Play();
        Invoke("yokohikouki", 6.7f);
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

    void yokohikouki()
    {
        yokohikokisound.Play();
        Invoke("soundstop",5.2f);
        Invoke("kubaku", 6.5f);
    }
    void soundstop()
    {
        yokohikokisound.Stop();
    }
    void kubaku()
    {
        kubakusound.Play();
    }
}
