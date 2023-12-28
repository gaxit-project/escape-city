using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponscript : MonoBehaviour
{
    public int weapontype=0;//type  0近接種　1銃種
    public int[] weaponnumber;//type毎の武器Listのindex値を保存する
    private Animator anim;
    public GameObject weapon;//現在の所有武器
    public int[] listgunnum;//type1の武器のchild番号を保持
    public int[] listsordnum;//type0の武器のchild番号を保持
    void Start(){
        if(weaponnumber.Length!=0){
            if(weapontype==0){
                weapon=transform.GetChild(listsordnum[weaponnumber[weapontype]]).gameObject;
            }else{
                weapon=transform.GetChild(listgunnum[weaponnumber[weapontype]]).gameObject;
            }
            anim = GameObject.Find("Player").GetComponent<Animator>();
            anim.SetInteger("type", weapon.GetComponent<WeaponStates>().type);
        }
    }
    public void changeweapon(int i)
    {
        weapontype=i;
        weapon.SetActive(false);
        if(weapontype==0){
            weapon=transform.GetChild(listsordnum[weaponnumber[weapontype]]).gameObject;
        }else{
            weapon=transform.GetChild(listgunnum[weaponnumber[weapontype]]).gameObject;
        }
        weapon.SetActive(true);
        anim.SetInteger("type", weapon.GetComponent<WeaponStates>().type);
    }
    public bool changenextweapon()
    {
        weapon.SetActive(false);  
        int pastnum=weaponnumber[weapontype];
        if(weapontype==0){
            for(int i=0;i<listsordnum.Length;i++){
                if((listsordnum.Length-1)>weaponnumber[weapontype]){
                    weaponnumber[weapontype]+=1;
                    if(transform.GetChild(listsordnum[weaponnumber[weapontype]]).gameObject.GetComponent<WeaponStates>().lv!=0)break;
                }else{
                    weaponnumber[weapontype]=0;
                    if(transform.GetChild(listsordnum[weaponnumber[weapontype]]).GetComponent<WeaponStates>().lv!=0)break;
                }
            }
            weapon=transform.GetChild(listsordnum[weaponnumber[weapontype]]).gameObject;
        }else{
            for(int i=0;i<listgunnum.Length;i++){
                if((listgunnum.Length-1)>weaponnumber[weapontype]){
                    weaponnumber[weapontype]+=1;
                    if(transform.GetChild(listgunnum[weaponnumber[weapontype]]).GetComponent<WeaponStates>().lv!=0)break;
                }else{
                    weaponnumber[weapontype]=0;
                    if(transform.GetChild(listgunnum[weaponnumber[weapontype]]).GetComponent<WeaponStates>().lv!=0)break;
                }
            }
            weapon=transform.GetChild(listgunnum[weaponnumber[weapontype]]).gameObject;
        }
        weapon.SetActive(true);
        if(pastnum!=weaponnumber[weapontype]){
            return true;
        }
        return false;
    }
    public bool checknextweapon(int type)
    {
        //weapon.SetActive(false);  
        int pastnum=weaponnumber[type];
        if(type==0){
            for(int i=0;i<listsordnum.Length;i++){
                if((listsordnum.Length-1)>weaponnumber[type]){
                    weaponnumber[type]+=1;
                    if(transform.GetChild(listsordnum[weaponnumber[type]]).gameObject.GetComponent<WeaponStates>().lv!=0)break;
                }else{
                    weaponnumber[type]=0;
                    if(transform.GetChild(listsordnum[weaponnumber[type]]).GetComponent<WeaponStates>().lv!=0)break;
                }
            }
            //weapon=transform.GetChild(listsordnum[weaponnumber[weapontype]]).gameObject;
        }else{
            for(int i=0;i<listgunnum.Length;i++){
                if((listgunnum.Length-1)>weaponnumber[type]){
                    weaponnumber[type]+=1;
                    if(transform.GetChild(listgunnum[weaponnumber[type]]).GetComponent<WeaponStates>().lv!=0)break;
                }else{
                    weaponnumber[type]=0;
                    if(transform.GetChild(listgunnum[weaponnumber[type]]).GetComponent<WeaponStates>().lv!=0)break;
                }
            }
            //weapon=transform.GetChild(listgunnum[weaponnumber[weapontype]]).gameObject;
        }
        //weapon.SetActive(true);
        if(pastnum!=weaponnumber[type]){
            weaponnumber[type]=pastnum;
            return true;
        }
        weaponnumber[type]=pastnum;
        return false;
    }
    public GameObject acseceweapon(int i){
        GameObject weap;
        if(i==0){
            weap=transform.GetChild(listsordnum[weaponnumber[i]]).gameObject;
        }else{
            weap=transform.GetChild(listgunnum[weaponnumber[i]]).gameObject;
        }
        return weap;
    }
    public GameObject acseceweapon2(int Ltype,int Lnumber){
        GameObject weap;
        if(Ltype==0){
            weap=transform.GetChild(listsordnum[Lnumber]).gameObject;
        }else{
            weap=transform.GetChild(listgunnum[Lnumber]).gameObject;
        }
        return weap;
    }

}
