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
    private bool isDelayActive = false;
    private Player_Item player_Item;
   

    void Start()
    {
        anim = GetComponent<Animator>();
        player_Item=FindObjectOfType<Player_Item>();
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
                GetComponent<UnityEngine.AI.NavMeshAgent>().enabled=false;
            Vector3 newPosition = transform.position;newPosition.y = 0.6f;//ここで埋まる位置を決めいている
            transform.position = newPosition;
            player_Item.DefeatEnemy();
                anim.SetBool("Z_Die", true);
                Invoke("destroy",2f);
            }
        }else{
            if(this.gameObject.CompareTag("Player")){}else{
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled=false;
            StartCoroutine(ResumeAfterDelay(0.5f));
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
    private IEnumerator ResumeAfterDelay(float delay){
        yield return new WaitForSeconds(delay); // 指定時間待機する

        anim.speed = 1f;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
    }
}