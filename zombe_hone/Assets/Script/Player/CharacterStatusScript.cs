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
            if(this.gameObject.CompareTag("Player")){
                OnDie();
                gameManager.Over();
            }
            else if(this.gameObject.CompareTag("Enemy")||this.gameObject.CompareTag("arbinoEnemy")){
                OnDie();
                gameManager.scoreUpdate(30,"ゾンビを倒した");
                GetComponent<DieandDrop>().Drop();
                //GetComponent<Patrol>().stopmove=true;

                Vector3 newPosition = transform.position;newPosition.y = 0.6f;//ここで埋まる位置を決めいている
                transform.position = newPosition;
                player_Item.DefeatEnemy();

                anim.SetBool("Z_Die", true);
                Invoke("destroy",2f);
            }else if(this.gameObject.CompareTag("villager")){
                OnDie();
                gameManager.subtaskUpdate(0);
                //gameManager.scoreUpdate(30);
                //GetComponent<DieandDrop>().Drop();
                //GetComponent<Patrol>().stopmove=true;

                Vector3 newPosition = transform.position;newPosition.y = 0.6f;//ここで埋まる位置を決めいている
                transform.position = newPosition;
                //player_Item.DefeatEnemy();

                anim.SetBool("Z_Die", true);
                Invoke("destroy",2f);
            }else{
                Invoke("destroy",0.3f);
            }
        }else{
            if(this.gameObject.CompareTag("Player")){
            }else if(this.gameObject.CompareTag("Enemy")){
                GetComponent<Patrol>().stopmove=true;
                StartCoroutine(ResumeAfterDelay(0.1f));
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
        GetComponent<Patrol>().stopmove = false;
    }
}