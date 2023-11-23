using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forGameoverCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetObject;
    public int cine;
    private bool changecine=false;
    public float speed=120f; 
    public flyter flyscript;
    public Impactparticle impactparticle1;
    public Impactparticle impactparticle2;
    public Impactparticle impactparticle3;
    public Impactparticle impactparticle4;
    private GameManager gameManager;
    
    // Update is called once per frame
    void Start(){
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        Vector3 Tpos=targetObject.transform.position;
        if(cine==0){
            targetObject.transform.position+=new Vector3(0,0,speed*Time.deltaTime);
            this.transform.LookAt(Tpos);
            if(Tpos.z>=400f){
                changecine1();
            }
        }else if(cine==1){
            targetObject.transform.position+=new Vector3(0,0,speed*Time.deltaTime);
            this.transform.position=Tpos-new Vector3(50,50,0);
            this.transform.LookAt(Tpos);
            if(changecine){
                changecine=false;
                Invoke("bakugeki1", 1.0f);
                Invoke("bakugeki1", 1.5f);
                Invoke("bakugeki1", 2.0f);
                Invoke("bakugeki1", 2.5f);
                Invoke("changecine1", 5.0f);
            }
        }else if(cine==2){
            targetObject=GameObject.Find("city");
            transform.position=targetObject.transform.position+new Vector3(0,200f,0);
            this.transform.LookAt(targetObject.transform);
            if(changecine){
                changecine=false;
                Invoke("startbakuhatu1", 1.0f);
                Invoke("startbakuhatu2", 1.3f);
                Invoke("startbakuhatu3", 1.3f);
                Invoke("startbakuhatu4", 1.8f);
                Invoke("changecine1", 2.2f);
            }
        }else if(cine==3){
            if(changecine){
                changecine=false;
                gameManager.Over();
            }
        }
    }
    void bakugeki1(){
        flyscript.bakugeki();
    }
    void changecine1(){
        cine++;
        changecine=true;
    }   
    void startbakuhatu1(){
        impactparticle1.bakuhatu();
    }
    void startbakuhatu2(){
        impactparticle2.bakuhatu();
    }
    void startbakuhatu3(){
        impactparticle3.bakuhatu();
    }
    void startbakuhatu4(){
        impactparticle4.bakuhatu();
    }
}
