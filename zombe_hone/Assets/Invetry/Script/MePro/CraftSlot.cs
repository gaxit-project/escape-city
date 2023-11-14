using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour
{
    public Image icon;
    public GameObject removeButton;
    Item item;
    //アイテムを追加する
    public void AddItem(Item newItem) 
    {
        item = newItem;
        icon.sprite = newItem.icon;
        removeButton.SetActive(true);
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
        CraftInventry.instance.Remove(item);
    }
    //アイテムの使用ボタン
    public void UseItem() 
    {
        if(item == null)
        {
            return;
        }
        item.Use();
    }
}
