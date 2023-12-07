using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NavMeshAgent�g���Ƃ��ɕK�v
using UnityEngine.AI;


//�I�u�W�F�N�g��NavMeshAgent�R���|�[�l���g��ݒu
[RequireComponent(typeof(NavMeshAgent))]

public class villager_ai : MonoBehaviour
{
    public bool stopmove=false;
    public GameObject bikkuri; 
    public Transform[] points;
    [SerializeField] int destPoint = 0;
    private NavMeshAgent agent;
    Animator anim;
    //攻撃のクールタイム
    public float cooltime=3;
    private float timeforcool;
    Vector3 playerPos;
    GameObject player;
    float distance;
    private float origenalspeed;
    [SerializeField] float trackingRange = 3f;
    [SerializeField] float quitRange = 5f;
    [SerializeField] float atackRange = 3f;
    //public float _sightAngle = 30f;
    [SerializeField] bool tracking = false;
    public bool attackwait=false;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // autoBraking �𖳌��ɂ���ƁA�ڕW�n�_�̊Ԃ��p���I�Ɉړ����܂�
        //(�܂�A�G�[�W�F���g�͖ڕW�n�_�ɋ߂Â��Ă�
        // ���x�����Ƃ��܂���)
        agent.autoBraking = false;

        GotoNextPoint();

        //�ǐՂ������I�u�W�F�N�g�̖��O������
        player = GameObject.Find("Player");
        timeforcool=cooltime;
        origenalspeed=agent.speed;
    }


    void GotoNextPoint()
    {
        // �n�_���Ȃɂ��ݒ肳��Ă��Ȃ��Ƃ��ɕԂ��܂�
        if(points.Length==0){
            agent.destination = agent.destination=transform.position;
            return;
        }

        // �G�[�W�F���g�����ݐݒ肳�ꂽ�ڕW�n�_�ɍs���悤�ɐݒ肵�܂�
        agent.destination = points[destPoint].position;

        // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A
        // �K�v�Ȃ�Ώo���n�_�ɂ��ǂ�܂�
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        if(anim.GetBool("Z_Die")){
            agent.destination=transform.position;
            return;
        }
        timeforcool+=Time.deltaTime;
        if(stopmove){
            agent.destination=transform.position;
            return;
        }
        playerPos = player.transform.position;
        distance = Vector3.Distance(this.transform.position, playerPos);

        if (tracking)
        {
            //�ǐՂ̎��AquitRange��苗�������ꂽ�璆�~
            if (distance > quitRange)
            {
                tracking = false;
            }
            /*if (distance < atackRange)
            {
                if(!attackwait)anim.SetTrigger("Z_atack");
            }*/
            //Player��ڕW�Ƃ���
            if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh) {
            agent.destination = playerPos;
            }
        }
        else
        {
            //Player��trackingRange���߂Â�����ǐՊJ�n
            if (distance < trackingRange){
                tracking = true;
                //Summonbikkuri();
            }


            // �G�[�W�F���g�����ڕW�n�_�ɋ߂Â��Ă�����A
            // ���̖ڕW�n�_��I�����܂�
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
            //Debug.Log(agent.velocity.magnitude);
        }
        if(distance>8.0f)changespeed(2f);
        else if(distance<1.0f&&tracking){
            agent.destination=transform.position;
            //transform.LookAt(new Vector3(playerPos.x,transform.position.y,playerPos.z));
        }
        else{
            changespeed(1f);
        }
        if(agent.velocity.magnitude > 0){
            anim.SetBool("move", true);
        }else{
            anim.SetBool("move", false);
        }
        if(distance>2.0f&&tracking)return;
        //sound=0;
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject obj in enemy){
            float enemydistance = Vector3.Distance(this.transform.position, obj.transform.position);
            if (enemydistance < atackRange)
            {
                if(!attackwait){//攻撃してないとき
                    if(timeforcool>cooltime){
                        anim.SetTrigger("Z_atack");
                        timeforcool=0f;
                        agent.destination = obj.transform.position;
                        transform.LookAt(new Vector3(obj.transform.position.x,transform.position.y,obj.transform.position.z));

                    }
                }else{//攻撃中
                    transform.LookAt(new Vector3(obj.transform.position.x,transform.position.y,obj.transform.position.z));
                    agent.destination = obj.transform.position;
                }
            }
        }
    }
    

    void OnDrawGizmosSelected()
    {
        //trackingRange�͈̔͂�Ԃ����C���[�t���[���Ŏ���
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, trackingRange);

        //quitRange�͈̔͂�����C���[�t���[���Ŏ���
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, quitRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, atackRange);
    }
    /*void Summonbikkuri(){
        if(timeforcool<cooltime)return;
        // Cubeプレハブを元に、インスタンスを生成
        Instantiate (bikkuri, new Vector3(this.transform.position.x,this.transform.position.y+0.2f,this.transform.position.z), Quaternion.identity);
        timeforcool=0f;
    }*/
    void Stop(){
        stopmove=true;
        anim.SetBool("move",false);
        anim.SetBool("run",false);
    }
    public void completeAttack(){
        //stopmove=false;
        //changespeed(1.0f);
        //anim.SetBool("stan",false);
        attackwait=false;
    }
    void Run(float runspeed){
        anim.SetBool("run",true);
        changespeed(runspeed);
    }
    public void StartAttack(){
        //anim.SetBool("stan",true);
        attackwait=true;
    }
    void changespeed(float speed){
        agent.speed=speed*origenalspeed;
    }
}