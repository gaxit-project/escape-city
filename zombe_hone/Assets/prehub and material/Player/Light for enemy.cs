using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightforenemy : MonoBehaviour
{
    public float deleteTime=3;
    private GameObject player;
    public Vector3 targetps;
    void Start()
    {
        player = GameObject.Find("Player");
        targetps=player.GetComponent<Lightmaneger>().tpos;
        Destroy(gameObject, deleteTime);
        if(targetps!=null)
            transform.LookAt(new Vector3(targetps.x,0.2f,targetps.z));
    }
    void Update(){
        if(targetps==null||player==null){
            Debug.Log("error:non player");
            return;
        }
        this.transform.position=new Vector3(player.transform.position.x,0.2f,player.transform.position.z);
        transform.LookAt(new Vector3(targetps.x,0.2f,targetps.z));
    }
}
