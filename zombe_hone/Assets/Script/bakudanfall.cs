using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bakudanfall : MonoBehaviour
{
    //ParticleSystem Particles;
    private bool dest=false;
    Vector3 speed;
    // Update is called once per frame
    void Start(){
        //Particles=GetComponent<ParticleSystem> ();
        speed=new Vector3(0,0,100);
    }
    void Update()
    {
        if(dest)return;
        speed=new Vector3(0,speed.y+(-100f*Time.deltaTime),speed.z*Mathf.Pow(0.5f, Time.deltaTime));
        this.transform.position+=speed*Time.deltaTime;
        if(this.transform.position.y<=-135){
            Destroy(gameObject);
            //Particles.Stop ();
            //Particles.Play ();
            dest=true;
            return;
        }
    }
}
