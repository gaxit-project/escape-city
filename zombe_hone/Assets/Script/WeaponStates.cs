using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStates : MonoBehaviour
{
    public int type;
    public int damage;
    public int num;
    public int damagePerShot = 20;
    public float timeBetweenBullets = 1f;
    public float range = 100f;

    public void Upgrade(){
        damage=(int)(damage+5);
        damagePerShot = (int)(5+damagePerShot);
        timeBetweenBullets *= 0.7f;
        if(timeBetweenBullets<0.1f)timeBetweenBullets=0.1f;
        if(type==0){
            transform.transform.localScale=new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z+20f);
        }
    }
}
