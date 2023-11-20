using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpObject : MonoBehaviour
{
  public static PickUpObject instance;
    int kazu=1;
    private GameObject hand;
    //Itemデータを入れる
    public Item item;

    public int itemtype=0;
    public int itemnumber=0;

    private void Awake()
    {
        hand=GameObject.Find("righthand");
        if (instance == null)
        {
            instance = this;
        }
       /* else
        {
            Destroy(this.gameObject);
        }
        GetComponent<Image>().sprite = item.icon;*/
    }

//インベントリにアイテムを追加
    public bool PickUp()
    {
        if(kazu==0)return false;
        kazu=0;
        Inventry.instance.Add(item);
        Destroy(gameObject);
        return true;
        //Destroy(gameObject);
    }
    public bool Powerup()
    {
        if(kazu==0)return false;
        kazu=0;
        GameObject weapon = hand.GetComponent<weaponscript>().acseceweapon2(itemtype,itemnumber);
        weapon.GetComponent<WeaponStates>().Upgrade();
        Destroy(gameObject);
        return true;
        //Destroy(gameObject);
    }
    public bool Cure(){
        if(kazu==0)return false;
        kazu=0;
        Destroy(gameObject);
        return true;
    }
}