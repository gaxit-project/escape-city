using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftInventry : MonoBehaviour
{
    public static CraftInventry instance;
    CraftInventryUI inventryUI;
    private void Awake()
    {
        if(instance== null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        inventryUI = GetComponent<CraftInventryUI>();
        inventryUI.UpdateUI();
    }

    public List<Item> items = new List<Item>();

    public void Add(Item item) 
    {
        items.Add(item);
        inventryUI.UpdateUI();
    }

    public void Remove(Item item) 
    {
        items.Remove(item);
        inventryUI.UpdateUI();
    }
}