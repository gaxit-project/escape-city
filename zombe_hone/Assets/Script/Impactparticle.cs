using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impactparticle : MonoBehaviour
{
    public ParticleSystem Particles;
    public ParticleSystem Particles1;

    public void bakuhatu(){
        Particles.Stop ();
        Particles.Play ();
        Invoke("bakuen", 1.0f);
    }
    void bakuen(){
        Particles1.Stop();
        Particles1.Play();
    }
}
