using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftPickUp : MonoBehaviour
{
    public Item item;
    public void PickUp()
    {
        CraftInventry.instance.Add(item);
        Slot.instance.ClearItem();
    }
}

