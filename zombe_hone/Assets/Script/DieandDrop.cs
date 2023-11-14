using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieandDrop : MonoBehaviour
{
    Animator anim;
    public GameObject[] item;
    private Transform _transform;
    // Start is called before the first frame update

    // Update is called once per frame
    public void Drop()
    {
        anim = GetComponent<Animator>();
        _transform=transform;
        if(item.Length!=0){
            int index=(int)Random.Range (0f, (float)(item.Length*2));
            if(index<=item.Length-1){
                Instantiate (item[index], _transform.position, Quaternion.identity);
            }
        }
        //Destroy(gameObject,1);
    }
}
