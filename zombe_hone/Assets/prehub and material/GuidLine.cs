using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidLine : MonoBehaviour
{
    //かくつかせないためにPlayerのコンポーネントplayer_movesWithCameraでLineRendererを操作しています。
    private GameObject player;
    public bool guidemode = true;
    private bool past;
    LineRenderer gunLine;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gunLine = GetComponent <LineRenderer> ();
        past=guidemode;
        if(guidemode!=true){
            gunLine.enabled = false;
            return;
        }
        if(player==null)return;
        gunLine.enabled = true;
        gunLine.SetPosition (1, transform.position);
        gunLine.SetPosition (0, player.transform.position);
    }
    void Update(){
        if(past==guidemode)return;
        past=guidemode;
        if(guidemode!=true){
            gunLine.enabled = false;
            return;
        }
        if(player==null)return;
        gunLine.enabled = true;
        gunLine.SetPosition (1, transform.position);
        gunLine.SetPosition (0, player.transform.position);
    }
}

    // Update is called once per frame
