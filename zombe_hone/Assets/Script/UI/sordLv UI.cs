using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sordLvUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI cardNameText;
    public weaponscript weaponscript;
    public GameObject[] image;
    int index=0;
      // 初期化
    void Start(){
        MesageUpdate();
    }
      // 更新
    public void MesageUpdate () {
        WeaponStates ws = weaponscript.acseceweapon(0).GetComponent<WeaponStates>();
        cardNameText.text =" Lv."+ws.lv+"/"+ws.lvmax;
    }
    public void changeweapon(int i){
        if(i>=image.Length)return;
        image[index].SetActive(false);
        image[i].SetActive(true);
        index=i;
        MesageUpdate();
    }
}