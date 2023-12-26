using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class textchanger : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI cardNameText;
    
    public float time;
    public float cooltime=3.0f;
    Transform Playertrans;
    bool kaigyo=false;
    public GameObject target;
    private GameManager gameManager;
      // 初期化
    void Start(){
        time=0;
    }  
    void Update(){
        time-=Time.deltaTime;
        if(time<=0){
            target.SetActive(false);
            gameObject.SetActive(false);
            kaigyo = false;
        }
    }

    public void ChangeMessage(string str){
        if(time>0){
            if(!kaigyo){
                cardNameText.text+="\n"+str;
                kaigyo=true;
            }else{
                kaigyo=false;
                cardNameText.text=str;
            }
        }else{
            cardNameText.text=str;
            kaigyo=false;
        }
        time=cooltime;
    }
}
