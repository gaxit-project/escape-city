using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuidMasage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI cardNameText;
      // 初期化
    void Start(){
        MesageUpdate(0);
    }
      // 更新
    public void MesageUpdate (int keynum) {
        if(keynum<3){
            cardNameText.text ="橋の部品を集める:" + keynum + "/3";
        }else{
            cardNameText.text ="橋の部品を集める:" + keynum + "/3\n街から逃げる:";
        }
    }
}
