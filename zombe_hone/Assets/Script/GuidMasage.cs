using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GuidMasage : MonoBehaviour
{
    public struct Mission
    {
    public int achievement;
    public int achievementmax;
    public string mission;
    public bool outputdistance;
    public Transform targettrans;
    public bool clear;
    }
    [SerializeField]
    private TextMeshProUGUI cardNameText;
    public Mission[] Mission_List; 
    private int pastkeynum;
    Transform Playertrans;
    public Transform Targettrans;
      // 初期化
    void Start(){
        //System.Array.Resize<Mission>(ref Mission_List, Mission_List.Length+1);
        Playertrans=GameObject.Find("Player").transform;
        Mission_List=new Mission[1];
        Mission_List[Mission_List.Length - 1].achievement = 0;
        Mission_List[Mission_List.Length - 1].achievementmax=0;
        Mission_List[Mission_List.Length - 1].mission="サブミッション";
        //AddMission(0,0,"住民を助ける",true,Targettrans);
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
    public void AddMission(int num,int nummax,string str,bool distanceon,Transform target){
        System.Array.Resize<Mission>(ref Mission_List, Mission_List.Length+1);
        Mission_List[Mission_List.Length - 1].achievement = num;
        Mission_List[Mission_List.Length - 1].achievementmax=nummax;
        Mission_List[Mission_List.Length - 1].mission=str;
        Mission_List[Mission_List.Length - 1].outputdistance=distanceon;
        Mission_List[Mission_List.Length - 1].targettrans=target;
    }
}
