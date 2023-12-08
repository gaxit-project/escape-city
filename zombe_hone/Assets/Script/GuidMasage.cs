using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GuidMasage : MonoBehaviour
{
    public struct Mission
    {
    public int achievement; //経過状態
    public int achievementmax; //最大値
    public string mission; //ミッション内容
    public bool outputdistance; //距離出力
    public Transform targettrans; //距離出力対象
    public bool clear; //クリア状態なら非表示 あくまで表示するかしないかのbool
    }
    [SerializeField]
    private TextMeshProUGUI cardNameText;
    public Mission[] Mission_List; 
    private int pastkeynum;
    Transform Playertrans;
    public GameObject target;
    private GameManager gameManager;
      // 初期化
    void Start(){
        gameManager = FindObjectOfType<GameManager>();
        //System.Array.Resize<Mission>(ref Mission_List, Mission_List.Length+1);
        Playertrans=GameObject.Find("Player").transform;
        Mission_List=new Mission[1];
        Mission_List[Mission_List.Length - 1].achievement = 0;
        Mission_List[Mission_List.Length - 1].achievementmax=0;
        Mission_List[Mission_List.Length - 1].mission="サブミッション";
        AddMission(0,1,"住民を助ける",true,target.transform);
        MesageUpdate(0);
    }
    void Update(){
        MesageUpdate(-1);
    }
      // 更新 -1入力で以前の値のまま更新可能
    public void MesageUpdate (int keynum) {
        if(keynum==-1){
            keynum=pastkeynum;
        }else{
            pastkeynum=keynum;
        }
        string str="ミッション\n";
        if(keynum<3){
            str +="橋の部品を集める:" + keynum + "/3";
        }else{
            str +="橋の部品を集める:" + keynum + "/3\n街から逃げる:";
        }
        for(int i=0;i<Mission_List.Length;i++){
            if(Mission_List[i].clear)continue;
            if(Mission_List[i].outputdistance){
                if(Mission_List[i].achievementmax!=0)
                    str+="\n"+Mission_List[i].mission+":"+Mission_List[i].achievement+"/"+Mission_List[i].achievementmax+":"+(int)Vector3.Distance(Mission_List[i].targettrans.position, Playertrans.position);
                else{
                    str+="\n"+Mission_List[i].mission+":"+(int)Vector3.Distance(Mission_List[i].targettrans.position, Playertrans.position);
                }
            }else{
                if(Mission_List[i].achievementmax!=0)
                    str+="\n"+Mission_List[i].mission+":"+Mission_List[i].achievement+"/"+Mission_List[i].achievementmax;
                else{
                    str+="\n"+Mission_List[i].mission;
                }
            }
        }
        cardNameText.text=str;
    }
    public int AddMission(int num,int nummax,string str,bool distanceon,Transform target){
        System.Array.Resize<Mission>(ref Mission_List, Mission_List.Length+1);
        Mission_List[Mission_List.Length - 1].achievement = num;
        Mission_List[Mission_List.Length - 1].achievementmax=nummax;
        Mission_List[Mission_List.Length - 1].mission=str;
        Mission_List[Mission_List.Length - 1].outputdistance=distanceon;
        Mission_List[Mission_List.Length - 1].targettrans=target;
        return Mission_List.Length - 1;
    }
    public void ChangeMission(int index, int num,int nummax,string str,bool distanceon,Transform target,bool clearr){
        Mission_List[index].achievement = num;
        Mission_List[index].achievementmax=nummax;
        Mission_List[index].mission=str;
        Mission_List[index].outputdistance=distanceon;
        Mission_List[index].targettrans=target;
        Mission_List[index].clear=clearr;
    }
    public void AddScore(){
        for(int i=1;i<Mission_List.Length;i++){
            if(Mission_List[i].achievement==Mission_List[i].achievementmax){
                gameManager.scoreUpdate(3000,"住民を救出して街を脱出");
            }
        }
    }
}
