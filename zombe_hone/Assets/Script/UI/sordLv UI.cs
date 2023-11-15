using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sordLvUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI cardNameText;
    public weaponscript weaponscript;
      // 初期化
    void Start(){
        MesageUpdate();
    }
      // 更新
    public void MesageUpdate () {
        WeaponStates ws = weaponscript.acseceweapon(0).GetComponent<WeaponStates>();
        cardNameText.text =" Lv."+ws.lv+"/"+ws.lvmax;
    }
}