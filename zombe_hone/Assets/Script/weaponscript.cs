using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponscript : MonoBehaviour
{
    public int weaponnumber=0;
    private int pastweapon=0;

    private Animator anim;
    public GameObject weapon;
    void Start(){
        weapon=transform.GetChild(weaponnumber).gameObject;
        anim = GameObject.Find("Player").GetComponent<Animator>();
        changeweapon();
    }
    public void changeweapon()
    {
        weapon = transform.GetChild(weaponnumber).gameObject;
        GameObject pastweaponobj = transform.GetChild(pastweapon).gameObject;
        pastweaponobj.SetActive(false);
        weapon.SetActive(true);
        anim.SetInteger("type", weapon.GetComponent<WeaponStates>().type);
        pastweapon=weaponnumber;
    }
    public GameObject acseceweapon(int i){
        GameObject weap=transform.GetChild(i).gameObject;
        return weap;
    }

}
