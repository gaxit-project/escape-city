using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="ScriptableObject/Create Item") ]
public class Item : ScriptableObject
{
    //アイテムの名前
   new public string name = "New Item";
    //アイテムのアイコン
    public Sprite icon = null;

    //アイテムの使用
    public void Use() 
    {
        Debug.Log(name+"を使用しました");
    
    }
}