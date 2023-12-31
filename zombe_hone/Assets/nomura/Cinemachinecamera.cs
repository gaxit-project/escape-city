using System;
using Cinemachine;
using UnityEngine;





public class Cinemachinecamara : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private GameObject player;
    public GameObject obj;
    [Header("Mode マウス=0,方向=1"), SerializeField]public int mode = 1;
    public float visionlengthX= 10f;
    public float visionlengthZ= 10f;
    Vector3 visionsize;
    

    void Start()
    {
        visionsize=new Vector3(24,12);
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
        if(mode==0){
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
            lookpoint+=new Vector3(visionsize.x*vision.x,0,visionsize.y*vision.y);
            obj.transform.position=lookpoint;
        }else if(mode==1){
            Vector3 forward = player.transform.forward;
            Vector3 lookpoint=player.transform.position;
            lookpoint+=new Vector3(visionlengthX*forward.x,0,visionlengthZ*forward.z);
            obj.transform.position=lookpoint;
        }else if(mode == 2){
        }
    }
}