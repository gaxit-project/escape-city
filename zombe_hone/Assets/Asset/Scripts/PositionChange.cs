using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionChange : MonoBehaviour
{
    void Start(){
        int position=(int)(Random.Range(1f,8f));
        print(position);
        GameObject key1=GameObject.Find("Key-item1");
        GameObject key2=GameObject.Find("Key-item2");
        GameObject key3=GameObject.Find("Key-item3");
        GameObject key4=GameObject.Find("Key-item4");
        GameObject key5=GameObject.Find("Key-item5");
        GameObject key6=GameObject.Find("Key-item6");
        GameObject key7=GameObject.Find("Key-item7");
        GameObject key8=GameObject.Find("Key-item8");
        switch (position)
        {
            case 1:
                key1.SetActive(true);
                key2.SetActive(true);
                key3.SetActive(true);
                key4.SetActive(false);
                key5.SetActive(false);
                key6.SetActive(false);
                key7.SetActive(false);
                key8.SetActive(false);
                break;
            case 2:
                key1.SetActive(false);
                key2.SetActive(true);
                key3.SetActive(true);
                key4.SetActive(true);
                key5.SetActive(false);
                key6.SetActive(false);
                key7.SetActive(false);
                key8.SetActive(false);
                break;
            case 3:
                key1.SetActive(false);
                key2.SetActive(false);
                key3.SetActive(true);
                key4.SetActive(true);
                key5.SetActive(true);
                key6.SetActive(false);
                key7.SetActive(false);
                key8.SetActive(false);
                break;
            case 4:
                key1.SetActive(false);
                key2.SetActive(false);
                key3.SetActive(false);
                key4.SetActive(true);
                key5.SetActive(true);
                key6.SetActive(true);
                key7.SetActive(false);
                key8.SetActive(false);
                break;
            case 5:
                key1.SetActive(false);
                key2.SetActive(false);
                key3.SetActive(false);
                key4.SetActive(false);
                key5.SetActive(true);
                key6.SetActive(true);
                key7.SetActive(true);
                key8.SetActive(false);
                break;
            case 6:
                key1.SetActive(false);
                key2.SetActive(false);
                key3.SetActive(false);
                key4.SetActive(false);
                key5.SetActive(false);
                key6.SetActive(true);
                key7.SetActive(true);
                key8.SetActive(true);
                break;
            case 7:
                key1.SetActive(true);
                key2.SetActive(false);
                key3.SetActive(false);
                key4.SetActive(false);
                key5.SetActive(false);
                key6.SetActive(false);
                key7.SetActive(true);
                key8.SetActive(true);
                break;
            case 8:
                key1.SetActive(true);
                key2.SetActive(true);
                key3.SetActive(false);
                key4.SetActive(false);
                key5.SetActive(false);
                key6.SetActive(false);
                key7.SetActive(false);
                key8.SetActive(true);
                break;
            default:
                break;
        }
    }
    void Update(){
    
    }
}
