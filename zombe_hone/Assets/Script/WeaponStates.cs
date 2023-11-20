using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStates : MonoBehaviour
{
    public int lv=1;
    public Vector3 scaleAdd;
    public int lvmax=10;
    public int type;
    public int damage;
    public float speed;
    public int damagePerShot = 20;
    public float timeBetweenBullets = 1f;
    public float range = 100f;
    public float sumnumbullets= 10;
    public float maxnumbullets = 10;
    public float numbullets = 10;

    public bool Upgrade(){
        sumnumbullets+=maxnumbullets;
        if(lvmax<=lv){
            return false;
        }
        if(lv==0){
            lv+=1;
            return true;;
        }
        lv+=1;
        damage=(int)(damage*1.1);
        damagePerShot = (int)(damagePerShot*1.1);
        timeBetweenBullets *= 0.95f;
        if(timeBetweenBullets<0.1f)timeBetweenBullets=0.1f;
        if(type==0){
            transform.transform.localScale=new Vector3(transform.localScale.x+scaleAdd.x,transform.localScale.y+scaleAdd.y,transform.localScale.z+scaleAdd.z);
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
