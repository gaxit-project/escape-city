using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShootingsecond : MonoBehaviour
{
    private Animator anim;
    private GameObject hand;

    private int damagePerShot = 20;
    private float timeBetweenBullets = 0.15f;
    private float range = 100f;

    private GameObject weapon;
    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    bool Fire=false;


    void Awake ()
    {
        anim = GameObject.Find("Player").GetComponent<Animator>();
        hand = GameObject.Find("righthand");
        shootableMask = LayerMask.GetMask ("enemy");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }
    public void OnFire(InputAction.CallbackContext context){
        if (context.performed){
            Fire=true;
        }else{
            Fire=false;
        }
    }

    void Update ()
    {
        if(anim.GetBool("Die"))return;
        if(anim.GetInteger("type")==0){
            DisableEffects ();
            return;
        }
        weapon = hand.GetComponent<weaponscript>().weapon;
        damagePerShot=weapon.GetComponent<WeaponStates>().damagePerShot;
        timeBetweenBullets=weapon.GetComponent<WeaponStates>().timeBetweenBullets;
        range=weapon.GetComponent<WeaponStates>().range;
        timer += Time.deltaTime;
        if(timeBetweenBullets<0.2){
            if(Fire && timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                Shoot ();
            }
        }else{
            if(Fire && timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                Shoot ();
                Fire=false;
            }
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            //EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            var status = shootHit.collider.GetComponent<CharacterStatusScript>();
            if(status!=null){
                status.Damage(damagePerShot);
            }
            /*if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }*/
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject obj in enemy){
            obj.GetComponent<Patrol>().sound=15;
        }
    }
}
