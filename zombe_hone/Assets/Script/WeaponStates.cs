using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStates : MonoBehaviour
{
    public int lv=1;
    public int lvmax=10;
    public int type;
    public int damage;
    public int num;
    public int damagePerShot = 20;
    public float timeBetweenBullets = 1f;
    public float range = 100f;

    public float sumnumbullets= 10;
    public float maxnumbullets = 10;
    public float numbullets = 10;

    public bool Upgrade(){
        sumnumbullets+=maxnumbullets*2;
        if(lvmax<=lv){
            return false;
        }
        lv+=1;
        damage=(int)(damage+3);
        damagePerShot = (int)(3+damagePerShot);
        timeBetweenBullets *= 0.9f;
        if(timeBetweenBullets<0.1f)timeBetweenBullets=0.1f;
        if(type==0){
            transform.transform.localScale=new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z+20f);
        }
        return true;
    }
    public bool shot(){
        if(numbullets==0)return false;
        numbullets-=1;
        return true;
    }
    public bool relord(){
        if(sumnumbullets==0)return false;
        if(sumnumbullets>=maxnumbullets-numbullets){
            sumnumbullets-=maxnumbullets-numbullets;
            numbullets=maxnumbullets;
        }else{
            numbullets+=sumnumbullets;
            sumnumbullets=0;
        }
        return true;
    }
    public bool relordcheck(){
        if(sumnumbullets==0)return false;
        if(numbullets==maxnumbullets)return false;
        return true;
    }
}
