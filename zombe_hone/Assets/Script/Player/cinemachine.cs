using System;
using Cinemachine;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private GameObject player;
    public GameObject obj;

    Vector3 visionsize;
    

    void Start()
    {
        visionsize=new Vector3(24,12);
        obj = new GameObject("lookpoint");
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (player == null)
        { 
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                _virtualCamera.Follow = obj.transform;
            }
        }
        Vector3 mousePosition = Input.mousePosition;
        Vector3 vision=new Vector3(mousePosition.x/Screen.width-0.5f,mousePosition.y/Screen.height-0.5f);
        Debug.Log (vision);
        if(vision.x>0.1f){
            vision.x=0.1f;
        }else if(vision.x<-0.1f){
            vision.x=-0.1f;
        } 
        if(vision.y>0.1f){
            vision.y=0.1f;
        }else if(vision.y<-0.1f){
            vision.y=-0.1f;
        }
        Vector3 lookpoint=player.transform.position;
        lookpoint+=new Vector3(visionsize.x*vision.x,visionsize.y*vision.y);
        obj.transform.position=lookpoint;
    }
}