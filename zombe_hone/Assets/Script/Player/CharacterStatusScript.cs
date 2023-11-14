using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterStatusScript : MonoBehaviour
{
    Animator anim;
    public UnityEvent onDieCallback = new UnityEvent();

    public int lifemax=100;
    public int life = 100;

    public Slider hpBar;

    private GameManager gameManager;

   

    void Start()
    {
        anim = GetComponent<Animator>();

        gameManager = FindObjectOfType<GameManager>();


        if (hpBar != null)
        {
            hpBar.value = life;
        }
    }

    public void Damage(int damage)
    {
        if (life <= 0) return;

        life = life - damage;
        if (hpBar != null)
        {
            hpBar.value = life;
        }
        if (life <= 0)
        {
            OnDie();
            if(this.gameObject.CompareTag("Player"))
                gameManager.Over();
            else{
                gameManager.scoreUpdate(30);
                GetComponent<DieandDrop>().Drop();
                anim.SetBool("Z_Die", true);
                Invoke("destroy",2f);
            }
        }
    }
    public void Cure(int cure){
        if (life >= lifemax||life <= 0) return;

        life = life + cure;
        if(life >= lifemax) life=lifemax;
        if (hpBar != null)
        {
            hpBar.value = life;
        }
    }

    void OnDie()
    {
        anim.SetBool("Die", true);
        onDieCallback.Invoke();
        GetComponent<CapsuleCollider>().enabled = false;
    }

    private void destroy(){
        
        Destroy(gameObject);
    }
}