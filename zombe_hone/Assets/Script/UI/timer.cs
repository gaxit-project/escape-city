using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

 
public class TimerScript : MonoBehaviour {
 
    [SerializeField]
    private TextMeshProUGUI cardNameText;
	[SerializeField]
	private int minute;
	[SerializeField]
	private float seconds;
	//　前のUpdateの時の秒数
	private float oldSeconds;
    private GameManager gameManager;
    public bool timerstop;
 
	void Start () {
		//minute = 10;
		//seconds = 0f;
		oldSeconds = seconds;
        gameManager = FindObjectOfType<GameManager>();
	}
 
	void Update () {
        if(timerstop)return;
        if(minute<=0&&seconds<=0f){
            seconds=0f;
            minute=0;
            return;
        }
		seconds -= Time.deltaTime;
		if(seconds < 0f) {
            if(minute>0){
                minute--;
                seconds = seconds + 60;
            }else{
                gameManager.movieStart();
            }
		}
		//　値が変わった時だけテキストUIを更新
		if((int)seconds != (int)oldSeconds) {
            cardNameText.text =minute.ToString("00") + ":" + ((int) seconds).ToString ("00");
		}
        if(minute==0){
            cardNameText.color = Color.red;
        }else{
            cardNameText.color = Color.white;
        }
		oldSeconds = seconds;
	}
    public void AddScore(){
        gameManager.scoreUpdate(((minute*60)+(int)seconds)*10,"脱出時残り時間ボーナス");
    }
}