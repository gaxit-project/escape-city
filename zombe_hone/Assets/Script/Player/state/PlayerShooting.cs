using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShootingsecond : MonoBehaviour
{
    private Animator anim;
    public bulletLvUI bUI;
    public weaponscript weaponscript;

    private int damagePerShot = 20;
    private float timeBetweenBullets = 0.15f;
    private float range = 100f;
    private GameManager gameManager;
    float timer;
    private WeaponStates weaponst;
    Ray shootRay = new Ray();
    bool relord=false;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    public AudioSource gunAudio;
    public AudioSource NonebulletAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    bool Fire=false;


    void Awake ()
    {
        gameManager = FindObjectOfType<GameManager>();
        anim = GameObject.Find("Player").GetComponent<Animator>();
        shootableMask = LayerMask.GetMask ("enemy");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunLight = GetComponent<Light> ();
    }
    public void OnFire(InputAction.CallbackContext context){
        if(!gameManager.playerinput)return;
        if(anim.GetBool("relord"))return;
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
            Fire=false;
            DisableEffects ();
            return;
        }
        if(Time.timeScale==0f)return;
        weaponst = weaponscript.weapon.GetComponent<WeaponStates>();
        damagePerShot=weaponst.damagePerShot;
        timeBetweenBullets=weaponst.timeBetweenBullets;
        range=weaponst.range;
        timer += Time.deltaTime;
        if(timeBetweenBullets<=0.2f){

            if(Fire && timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                if(weaponst.shot()){
                    Shoot ();
                }else{
                    NonebulletAudio.Play ();
                    Fire=false;
                }
            }
        }else{
            if(Fire && timer >= timeBetweenBullets && Time.timeScale != 0 )
            {
                if(weaponst.shot()){
                    Shoot ();
                    Fire=false;
                }else{
                    NonebulletAudio.Play();
                    Fire=false;
                }
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
        bUI.MesageUpdate();

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
        GameObject[] arbinoenemy = GameObject.FindGameObjectsWithTag("arbinoEnemy");
        foreach(GameObject obj in arbinoenemy){
            obj.GetComponent<arbinoPatrol>().sound=25;
        }
    }
}
