using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Animator anim;
    public Item[] registeritem;
    public Image icon;
    public GameObject removeButton;
    public static Slot instance;
    private void Awake()
    {
        anim = GameObject.Find("Player").GetComponent<Animator>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    Item item;
    //アイテムを追加する
    public void AddItem(Item newItem) 
    {
        item = newItem;
        icon.sprite = newItem.icon;
        //removeButton.SetActive(true);
    }
    //アイテムを取り除く
    public void ClearItem()
    {
        item = null;
        icon.sprite = null;
        removeButton.SetActive(false);
    }
    //アイテムの消去ボタン
    public void OnRemoveButton()
    {
        Inventry.instance.Remove(item);
    }
    //アイテムの使用ボタン
    public void UseItem() 
    {
        if(item== null) 
        {
            return;
        }
        else
        {
            /*Debug.Log(item);
            for(int i=0;i<registeritem.Length;i++){
                if(item==registeritem[i]){
                    Debug.Log("武器持ち替え"+item);
                    anim.SetInteger("type",i);
                }
            }*/
            /*CraftInventry.instance.Add(item);
            item = null;
            icon.sprite = null;
            removeButton.SetActive(false);
            item.Use();*/
        }
    }
}
