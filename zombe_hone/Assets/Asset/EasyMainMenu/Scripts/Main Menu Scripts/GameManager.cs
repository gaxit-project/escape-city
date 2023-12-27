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
    private GameObject Message;
    private GameObject Messageimage;
    private textchanger Messagestr;
    public bool Main=true;
    private bool villagerDie = false; //villagerの生死

    void Start()
    {
        if(GameObject.Find("TuitlialOBJ")==null){
            playerinput=true;
            Tuitlial=false;
            enemymove=true;
            CanvasOFF=false;
        }
        Messageimage=GameObject.Find("zimaku");
        Message=GameObject.Find("MessageZIMAKU");
        if(Message!=null){
            Messagestr=Message.GetComponent<textchanger>();
        }
        if(Messageimage!=null){
            Messageimage.SetActive(false);
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
        if(Main){
            if(CanvasOFF&&!timeover){
                keepCanvas.SetActive(false);
                gunCanvas.SetActive(false);
                invCanvas.SetActive(false);
            }else if(!timeover){
                keepCanvas.SetActive(true);
                gunCanvas.SetActive(true);
                invCanvas.SetActive(true);
            }
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
        if (Input.GetKeyDown(KeyCode.C))//デバック用　Clear
        {
            // GameClear();
            brige.SetActive(true);
            endmovieStart();
        }
        if (Input.GetKeyDown(KeyCode.L))//デバック用　Clear Solo
        {
            // GameClear();
            brige.SetActive(true);
            villagerDie = true;
            endmovieStart();
        }
        if (Input.GetKeyDown(KeyCode.K))//デバック用　Timer 0
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
    public void GameClear()
    {
        //Debug.Log("メソッドを実行します");
        if (overcount) return;
        //Debug.Log("メソッドを返します１");
        if (gameclear) return;
        //Debug.Log("メソッドを返します２");
        gameclear = true;
        //Debug.Log("メソッドを実行しています。１");
        timer.timerstop = true;
        timer.AddScore();
        task.GetComponent<GuidMasage>().AddScore();
        task.SetActive(false);
        //Debug.Log("メソッドを実行しています。２");
        score.GetComponent<MenuScoreManager>().Allscoreoutput();
        score.GetComponent<MenuScoreManager>().scoreRock = true;
        score.GetComponent<MenuScoreManager>().ScoreRankingUpdate();
        //Debug.Log("メソッドを実行しています。２.1");
        audioCliar.Play();
        Clear.SetActive(true);
        virtalmouse.SetActive(true);
        Cursor.SetActive(true);
        //Debug.Log("メソッドを実行しています。３");
        Time.timeScale = 0f;
        //Debug.Log("メソッドを実行しました。");
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

    public void endmovieStart()
    {
        brige.SetActive(true);
        timer.timerstop = true;
        timeover = true;                //なんかバグってたけどこれ入れたらいけた？？
        Fadepanel.Fadein = true;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(keepCanvas);
        DontDestroyOnLoad(virtalmouse);
        Invoke("changeEndingcine", 1.0f);
        Invoke("kaijodestroy", 1.5f);
    }

    void changeTimeupcine(){
        Main=false;
        SceneManager.LoadScene("TimeUp");
    }

    void changeEndingcine()
    {
        Main=false;
        SceneManager.LoadScene("Ending");
    }

    void kaijodestroy(){
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(keepCanvas, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(virtalmouse, SceneManager.GetActiveScene());
        Fadepanel.Fadeout=true;
        overcount=false;
    }
    public void changeMessage(string str)
    {
        Message.SetActive(true);
        Messageimage.SetActive(true);
        Messagestr.ChangeMessage(str);
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

    public bool villager_Die()
    {
        return villagerDie;
    }
    public void villager_switch()
    {
        villagerDie = true;
    }
}
