using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreRanking : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI cardNameText;
    public int[] score_num; // スコア変数

    public GameObject data;
    private GameObject datacheck;

      // 初期化
    void Start(){
        datacheck=GameObject.Find("scoredata(Clone)");
        if(datacheck==null)datacheck=Instantiate(data, new Vector3(-100f,-100f,-100f), Quaternion.identity);

        ScorerankingUpdate(0);
    }
      // 更新
    public void ScorerankingUpdate (int score) {
        for(int i=0;i<3;i++){
            score_num[i]=datacheck.GetComponent<scoredata>().data[i];
            if(score_num[i]<score){
                int sum=score_num[i];
                score_num[i]=score;
                score=sum;
            }
            datacheck.GetComponent<scoredata>().data[i]=score_num[i];
        }
        cardNameText.text ="1-Score:" + score_num[0]+"\n2-Score:" + score_num[1]+"\n3-Score:"+score_num[2];
    }
}
