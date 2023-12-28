using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour {
    public float alfa;
    public float speed = 1.5f;
    float red, green, blue;
    public bool Fadein=false;
    public bool Fadeout=false;
    
    void Start () {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }

    void Update () {
        if(!Fadein&&!Fadeout)return;
        if(Fadein){
            FadeIn();
        }else if(Fadeout){
            FadeOut();
        }
    }

    //どうも1f がcolor.alfa=255と対応してるっぽい
    void FadeIn(){
       alfa += speed*Time.deltaTime;
       if(alfa>=1f){
            alfa=1f;
            Fadein=false;
        }
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
    }
    void FadeOut(){
        alfa -= speed*Time.deltaTime;
        if(alfa<=0f){
            alfa=0f;
            Fadeout=false;
        }
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
    }
    public void changeColor(float red, float green, float blue, float alfa){
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
    }
}