using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forstartCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetObject;
    public int cine;
    private bool changecine=true;
    float speed=3.5f;
    bool stop=false;
    /// <summary>
    private GameManager2 gameManager2;
    private Vector3 point;
    /// </summary>
    
    // Update is called once per frame
    void Start(){
        gameManager2 = FindObjectOfType<GameManager2>();
    }
    void Update()
    {
        Vector3 Tpos=targetObject.transform.position;
        this.transform.LookAt(Tpos);
        if(cine==0){
            if(changecine){
                changecine=false;
                targetObject.transform.position=GameObject.Find("radio").transform.position;
                radio();
                StartCoroutine(DelayMethod(1.0f, "<ラジオ>街は感染者に占拠されており、"));
                StartCoroutine(DelayMethod(3.0f, "<ラジオ>ゾンビとなった人々が非感染者を襲っています。"));
                StartCoroutine(DelayMethod(6.0f, "<ラジオ>国はこの状況を至急解決するため"));
                StartCoroutine(DelayMethod(8.0f, "<ラジオ>指定地区の爆撃許可を発令しました。"));
                StartCoroutine(DelayMethod(11.0f, "<ラジオ>10分以内に指定地区の生存者は直ちに避難してください。"));
                StartCoroutine(DelayMethod(13.0f, "<ラジオ>指定地区は、、、、"));
                Invoke("radio", 5.0f);
                Invoke("radio", 10.0f);
                Invoke("changecine1", 16.0f);
            }
        }else if(cine==1){
            gameManager2.radio1.volume-=Time.deltaTime;
            if(Vector3.Distance(point, Tpos)>0){
                Vector3 piace = point-Tpos;
                Vector3 movepiace = piace.normalized*speed*Time.deltaTime;
                /*Debug.Log(piace.normalized*speed*Time.deltaTime);
                Debug.Log(piace.normalized);
                Debug.Log(movepiace);
                Debug.Log(speed*Time.deltaTime);*/
                if(Vector3.Distance(point, Tpos)<speed*Time.deltaTime){
                    targetObject.transform.position=point;
                }else{
                    targetObject.transform.position+=movepiace;
                }
            }else{
                if(!stop)transform.position += new Vector3(transform.forward.x,0,transform.forward.z) * 0.1f * Time.deltaTime;
            }
            if(changecine){
                changecine=false;
                //gameManager2.radio1.Stop();
                //radio2();
                point=GameObject.Find("door").transform.position;
                StartCoroutine(DelayMethod(0f, "急いで逃げないと、、"));
                StartCoroutine(DelayMethod(6.0f, "足音がする、、"));
                StartCoroutine(DelayMethod(10.0f, "一人暮らしなんだぞ！"));
                StartCoroutine(DelayMethod(15.0f, "窓から逃げるか"));
                Invoke("Stop", 4.0f);
                //Invoke("radio2", 5.0f);
                //Invoke("radio2", 10.0f);
                Invoke("footsound",4.0f);
                //Invoke("radio2", 15.0f);
                Invoke("changecine1", 15.5f);
            }
        }else if(cine==2){
            if(Vector3.Distance(point, Tpos)>0){
                Vector3 piace = point-Tpos;
                Vector3 movepiace=piace.normalized*speed*2*Time.deltaTime;
                if(Vector3.Distance(point, Tpos)<speed*Time.deltaTime){
                    targetObject.transform.position=point;
                }else{
                    targetObject.transform.position+=movepiace;
                }
            }else{
                if(!stop)transform.position += new Vector3(transform.forward.x,0,transform.forward.z) * 0.5f*Time.deltaTime;
            }
            if(changecine){
                changecine=false;
                point=GameObject.Find("window").transform.position;
                Invoke("changecine1", 4.2f);
            }
        }else if(cine==3){
            if(changecine){
                changecine=false;
                gameManager2.fadeandChangeMain();
            }
        }
    }
    void changecine1(){
        cine++;
        changecine=true;
    }   
    void Stop(){
        stop=true;
    }
    void footsound(){
        gameManager2.footwalk.Play();
    }
    void radio(){
        gameManager2.radio1.Stop();
        gameManager2.radio1.Play();
    }
    void radio2(){
        gameManager2.radio2.Stop();
        gameManager2.radio2.Play();
    }
    IEnumerator DelayMethod(float delay, string text) {
        //delay秒待つ
        yield return new WaitForSeconds(delay);
        /*処理*/
        gameManager2.changeMessage(text);
    }
}
