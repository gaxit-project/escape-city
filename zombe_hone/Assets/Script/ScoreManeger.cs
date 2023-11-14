using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MenuScoreManager : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI cardNameText;
    public int score_num = 0; // スコア変数
    public GameObject ranking;

      // 初期化
    void Start(){
        ScoreUpdate(0);
    }
      // 更新
    public void ScoreUpdate (int add) {
        score_num+=add;
        cardNameText.text ="Score:" + score_num;
    }
    public void ScoreRankingUpdate(){
        ranking.GetComponent<ScoreRanking>().ScorerankingUpdate(score_num);
    }
}