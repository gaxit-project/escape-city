using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenArrow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject disforarrow;
    Vector3 target;
    void Update()
    {
        GameObject[] keyitem = GameObject.FindGameObjectsWithTag("SphereItem");
        float distance=9999f;
        if(keyitem.Length!=0){
            foreach(GameObject obj in keyitem){
                float dis = Vector3.Distance(obj.transform.position, player.transform.position);
                if(distance>=dis){
                    distance=dis;
                    target=obj.transform.position;
                }
            }
        }else{
            GameObject[] blockitem = GameObject.FindGameObjectsWithTag("BlockItem");
            if(blockitem.Length!=null){
                foreach(GameObject obj in blockitem){
                    float dis = Vector3.Distance(obj.transform.position, player.transform.position);
                    if(distance>=dis){
                        distance=dis;
                        target=obj.transform.position;
                    }
                }
            }
        }
        if(distance==9999f){
            distance=0;
        }
        Vector3 one=target-player.transform.position;
        transform.position=player.transform.position+(one.normalized*0.5f)+new Vector3(0f,1.2f,0f);
        disforarrow.GetComponent<distanceitem>().Updatedistance((int)distance);
        transform.LookAt(target);
    }

    // Update is called once per frame
}
