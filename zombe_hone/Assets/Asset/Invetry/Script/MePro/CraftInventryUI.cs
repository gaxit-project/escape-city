using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftInventryUI : MonoBehaviour
{
    public Transform CraftPanel;

    CraftSlot[] slots;
    
    void Start()
    {
        slots = CraftPanel.GetComponentsInChildren<CraftSlot>();
        
    }

    // Update is called once per frame
  public void UpdateUI()
    {
        Debug.Log("UpdateUi");
        for(int i = 0; i < slots.Length; i++) 
        {
            if (i < CraftInventry.instance.items.Count) 
            {
                slots[i].AddItem(CraftInventry.instance.items[i]);
            
            }
            else 
            {
                slots[i].ClearItem();
            
            }
        
        }
    }
}
