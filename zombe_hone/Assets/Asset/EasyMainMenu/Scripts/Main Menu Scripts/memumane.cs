using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;

public class GameManager2 : MonoBehaviour
{
    //public GameObject keepCanvas;
    //public GameObject PauseMenu;
    //public GameObject Clear;
    //public GameObject GameOver;
    //public GameObject Cursor;
    public GameObject Message;
    public GameObject fade;
    private textchanger Messagestr;
    public AudioSource footwalk;
    public AudioSource radio2;
    public AudioSource radio1;
    public AudioSource glass;
    //public GameObject score;
    //public GameObject virtalmouse;
    /*public AudioSource audioCliar;
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
    public GameObject scoreranking;*/
    private GameObject brige;
    public FadeScript Fadepanel;
    public bool mainmenufanction=true;
    private Cinemachinecamara Vircamscript;
    private GameManager gameManager;
    bool main=false;
    bool flag=false;
    bool tuitlialflag=false;
    /*
    private int itemnum;
    public bool overcount=false;
    private bool gameclear=false;
    public TimerScript timer;*/
    
    
    void Start()
    {
        Messagestr=Message.GetComponent<textchanger>();
        //brige=GameObject.Find("brige");
        //if(brige!=null)brige.SetActive(false);
        /*if(mainmenufanction){
            PauseMenu.SetActive(false);
            Clear.SetActive(false);
            GameOver.SetActive(false);
            Cursor.SetActive(false);
            rectTransform = Cursor.GetComponent<RectTransform>();
        }*/
        
    }


    void Update()
    {
        if(!mainmenufanction)return;
        
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            if(!main){
                //audioSe.Play();
                Fadepanel.Fadein=true;
                DontDestroyOnLoad(fade);
                DontDestroyOnLoad(gameObject);
                Invoke("changescineMain", 1.0f);
                Invoke("kaijodestroy", 1.1f);
                main=true;
            }else{
                if(!flag&&tuitlialflag){
                    //SceneManager.LoadScene("Main");
                    tuitlialend();
                }
            }
        }
    }
    public void fadeandChangeMain(){
        Fadepanel.Fadein=true;
        DontDestroyOnLoad(fade);
        DontDestroyOnLoad(gameObject);
        Invoke("changescineMain", 1.0f);
        Invoke("kaijodestroy", 1.1f);
    }

    public void changeMessage(string str)
    {
        Message.SetActive(true);
        Messagestr.ChangeMessage(str);
    }
    public void changescineMain(){
        main=true;
        SceneManager.LoadScene("Main");
        glass.Play();
        radio2.Stop();
        radio1.Stop();
        footwalk.Stop();
        Invoke("tuitlial",0.05f);
    }
    /*
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
    /*
    void changeTimeupcine(){
        SceneManager.LoadScene("TimeUp");
    }*/
    void kaijodestroy(){
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(fade, SceneManager.GetActiveScene());
        Fadepanel.Fadeout=true;
    }
    public void tuitlial(){
        if(Vircamscript==null){
            Vircamscript = GameObject.Find("VirtualCamera").GetComponent<Cinemachinecamara>();
        }
        if(gameManager==null){
            gameManager = FindObjectOfType<GameManager>();
        }
        tuitlialflag=true;
        gameManager.Tuitlial=true;
        gameManager.playerinput=false;
        gameManager.enemymove=false;
        gameManager.CanvasOFF=true;
        Invoke("tuitlialbrigelook",1.9f);
        StartCoroutine(DelayMethod(1.9f,"橋が、壊れてる、、"));
        StartCoroutine(DelayMethod(3.5f,"この街はあの橋以外から出れないのに"));
        StartCoroutine(DelayMethod(7.0f,"直すしかなさそうだな、、"));
        Invoke("looknormal",4.0f);
        Invoke("tuitlialend",10.0f);
    }
    private void tuitlialbrigelook(){
        if(flag)return;
        Vircamscript.mode=2;
        brige=GameObject.Find("brigetrans");
        Vircamscript.obj.transform.position=brige.transform.position;
    }
    private void looknormal(){
        if(flag)return;
        Vircamscript.mode=1;
    }
    private void tuitlialend(){
        if(flag)return;
        Vircamscript.mode=1;
        gameManager.playerinput=true;
        gameManager.enemymove=true;
        gameManager.CanvasOFF=false;
        flag=true;
        gameManager.changeTuitlial();
        Destroy(fade);
        Destroy(gameObject);
    }
    IEnumerator DelayMethod(float delay, string text) {
        //delay秒待つ
        yield return new WaitForSeconds(delay);
        /*処理*/
        changeMessage(text);
    }
}
