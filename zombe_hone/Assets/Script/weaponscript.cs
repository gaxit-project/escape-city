using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponscript : MonoBehaviour
{
    public int weaponnumber=0;
    private Animator anim;
    public GameObject weapon;
    void Start(){
        weapon=transform.GetChild(weaponnumber).gameObject;
        anim = GameObject.Find("Player").GetComponent<Animator>();
        anim.SetInteger("type", weapon.GetComponent<WeaponStates>().type);
    }
    public void changeweapon(int i)
    {
        weaponnumber=i;
        weapon.SetActive(false);
        weapon = transform.GetChild(weaponnumber).gameObject;
        weapon.SetActive(true);
        anim.SetInteger("type", weapon.GetComponent<WeaponStates>().type);
    }
    public GameObject acseceweapon(int i){
        GameObject weap=transform.GetChild(i).gameObject;
        return weap;
    }

}
