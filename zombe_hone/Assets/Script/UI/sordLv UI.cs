using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sordLvUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI cardNameText;
    public weaponscript weaponscript;
    public GameObject[] images;
    public FadeScript fadepanelst;
    public GameObject SUI2;
    int index=0;
    int stack=0;
    int pastlv=1;
    bool change=false;
      // 初期化
    void Start(){
        MesageUpdate();
    }
    void Update(){
      if(fadepanelst.alfa==1.0f){
        stack-=1;
        if(stack>0)fadepanelst.changeColor(1.0f,1.0f,1.0f,fadepanelst.speed*0.2f);
        else{
          fadepanelst.Fadeout=true;
        } 
      }
      if(stack>0){
        fadepanelst.Fadein=true;
        fadepanelst.Fadeout=false;
      }
      else{
      }
    }
      // 更新
    public void MesageUpdate () {
        WeaponStates ws = weaponscript.acseceweapon(0).GetComponent<WeaponStates>();
        cardNameText.text =" Lv."+ws.lv+"/"+ws.lvmax;
        if(weaponscript.checknextweapon(0)){
          SUI2.SetActive(true);
        }else{
          SUI2.SetActive(false);
        }
        if(change)pastlv=ws.lv;
        if(pastlv<ws.lv){
          stack+=ws.lv-pastlv;
          pastlv=ws.lv;
        }
    }
    public void changeweapon(int i){
        if(i>=images.Length)return;
        images[index].SetActive(false);
        images[i].SetActive(true);
        index=i;
        stack=0;
        change=true;
        MesageUpdate();
    }
}