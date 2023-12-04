using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NavMeshAgent�g���Ƃ��ɕK�v
using UnityEngine.AI;

//�I�u�W�F�N�g��NavMeshAgent�R���|�[�l���g��ݒu
[RequireComponent(typeof(NavMeshAgent))]

public class arbinoPatrol : MonoBehaviour
{
    public GameObject bikkuri; 
    public Transform[] points;
    [SerializeField] int destPoint = 0;
    private NavMeshAgent agent;
    Animator anim;
    public float BikkuriCooltime=3;
    public float attackcooltime=3;
    private float timeforcool;
    private float timeforatackcool;
    //public bool attackwait=true;
    private float timeforsound;
    public float soundtrakingtime=10f;
    public float sound=0;
    Vector3 playerPos;
    Vector3 pastplayerpos;
    GameObject player;
    float distance;
    [SerializeField] float trackingRange = 3f;
    [SerializeField] float quitRange = 5f;
    [SerializeField] float atackRange = 3f;
    public float _sightAngle = 30f;
    [SerializeField] bool tracking = false;

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
        timeforcool=BikkuriCooltime;
        timeforsound=soundtrakingtime;
        timeforatackcool=attackcooltime;
    }


    void GotoNextPoint()
    {
        // �n�_���Ȃɂ��ݒ肳��Ă��Ȃ��Ƃ��ɕԂ��܂�
        if(points.Length==0){
            anim.SetBool("move", false);
            //agent.destination = new Vector3(transform.position.x+Random.Range(-1f, 1f),transform.position.y,transform.position.z+Random.Range (-1f,1f));
            return;
        }
        anim.SetBool("move", true);

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
        if(timeforatackcool<attackcooltime){
            timeforatackcool+=Time.deltaTime;
            agent.destination=transform.position;
            transform.LookAt(new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z));
            anim.SetBool("move", false);
            return;
        }
        //attackwait=true;
        playerPos = player.transform.position;
        distance = Vector3.Distance(this.transform.position, playerPos);
        timeforcool+=Time.deltaTime;
        timeforsound+=Time.deltaTime;
        if(distance>30)return;
        bool sightjudge=IsVisible();
        //Player�Ƃ��̃I�u�W�F�N�g�̋����𑪂�

        if (tracking)
        {
            pastplayerpos=playerPos;
            //�ǐՂ̎��AquitRange��苗�������ꂽ�璆�~
            if (distance > quitRange&&!sightjudge)
            {
                tracking = false;
            }
            if (distance < atackRange)
            {
                anim.SetTrigger("Z_atack");
                timeforatackcool=0f;
                //attackwait=false;
            }
            //Player��ڕW�Ƃ���
            if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh) {
            agent.destination = playerPos;
            anim.SetBool("move", true);
            }
        }
        else if(timeforsound<soundtrakingtime&&Vector3.Distance(this.transform.position, pastplayerpos)<0.1) {
            if(sound==0){
                //Player��trackingRange���߂Â�����ǐՊJ�n
                if (distance < trackingRange){
                    tracking = true;
                    Summonbikkuri();
                }
                if(sightjudge){
                    tracking = true;
                    Summonbikkuri();
                }
            }else{
                if (distance < sound||distance < trackingRange){
                    tracking = true;
                    Summonbikkuri();
                }
                if(sightjudge){
                    tracking = true;
                    Summonbikkuri();
                }
            }

            if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh) {
            agent.destination = pastplayerpos;
            anim.SetBool("move", true);
            }
        }
        else
        {
            if(sound==0){
                //Player��trackingRange���߂Â�����ǐՊJ�n
                if (distance < trackingRange){
                    tracking = true;
                    Summonbikkuri();
                }
                if(sightjudge){
                    tracking = true;
                    Summonbikkuri();
                }
            }else{
                if (distance < sound||distance < trackingRange){
                    tracking = true;
                    timeforsound=0f;
                    Summonbikkuri();
                }
                if(sightjudge){
                    tracking = true;
                    Summonbikkuri();
                }
            }


            // �G�[�W�F���g�����ڕW�n�_�ɋ߂Â��Ă�����A
            // ���̖ڕW�n�_��I�����܂�
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
            //Debug.Log(agent.velocity.magnitude);
        }
        sound=0;
    }
    public bool IsVisible()
    {
        // 自身の位置
        var selfPos = this.transform.position;
        // ターゲットの位置
        var targetPos = player.transform.position;

        // 自身の向き（正規化されたベクトル）
        var selfDir = this.transform.forward;
        
        // ターゲットまでの向きと距離計算
        var targetDir = targetPos - selfPos;
        var targetDistance = targetDir.magnitude;

        // cos(θ/2)を計算
        var cosHalf = Mathf.Cos(_sightAngle / 2 * Mathf.Deg2Rad);

        // 自身とターゲットへの向きの内積計算
        // ターゲットへの向きベクトルを正規化する必要があることに注意
        var innerProduct = Vector3.Dot(selfDir, targetDir.normalized);

        // 視界判定
        return innerProduct > cosHalf && targetDistance < trackingRange*2;
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
    void Summonbikkuri(){
        if(timeforcool<BikkuriCooltime)return;
        // Cubeプレハブを元に、インスタンスを生成
        Instantiate (bikkuri, new Vector3(this.transform.position.x,this.transform.position.y+0.2f,this.transform.position.z), Quaternion.identity);
        timeforcool=0f;
    }
}