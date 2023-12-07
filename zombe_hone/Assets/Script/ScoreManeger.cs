using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MenuScoreManager : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI cardNameText;
    public bool scoreRock=false;
    public int score_num = 0; // スコア変数
    public string[] str_list;
    public int[] score_list;
    public GameObject ranking;

      // 初期化
    void Start(){
        cardNameText.text ="Score:" + score_num;
    }
      // 更新
    public void ScoreUpdate (int add,string strings) {
      if(scoreRock)return;
        score_num+=add;
        cardNameText.text ="Score:" + score_num;
        bool flag=false;
        for(int i=0;i<str_list.Length;i++){
          if(str_list[i]==strings){
            score_list[i]+=add;
            flag=true;
            break;
          }
        }
        if(flag==false){
          System.Array.Resize<int>(ref score_list, score_list.Length+1);
          System.Array.Resize<string>(ref str_list, str_list.Length+1);
          score_list[score_list.Length-1]=add;
          str_list[score_list.Length-1]=strings;
        }
    }
    public void ScoreRankingUpdate(){
        ranking.GetComponent<ScoreRanking>().ScorerankingUpdate(score_num);
    }
    public void Allscoreoutput(){
        cardNameText.text ="Score:" + score_num;
        for(int i=0;i<score_list.Length;i++){
          cardNameText.text+="\n"+score_list[i]+"  "+str_list[i];
        }
    }
}