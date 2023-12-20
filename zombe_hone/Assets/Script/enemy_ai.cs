using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NavMeshAgent�g���Ƃ��ɕK�v
using UnityEngine.AI;


//�I�u�W�F�N�g��NavMeshAgent�R���|�[�l���g��ݒu
[RequireComponent(typeof(NavMeshAgent))]

public class Patrol : MonoBehaviour
{
    private GameManager gameManager;
    public bool stopmove=false;
    public GameObject bikkuri; 
    public Transform[] points;
    [SerializeField] int destPoint = 0;
    private NavMeshAgent agent;
    Animator anim;
    float haikaicooltime=3;
    float timeforhaikai=0;
    public float cooltime=3;
    private float timeforcool;
    public float sound=0;
    Vector3 playerPos;
    GameObject player;
    public static float distance;
    private float origenalspeed;
    [SerializeField] float trackingRange = 3f;
    [SerializeField] float quitRange = 5f;
    [SerializeField] float atackRange = 3f;
    private float soundRange = 10f;
    public float _sightAngle = 30f;
    [SerializeField] bool tracking = false;
    bool attackwait=false;
    private bool zombiewait;
    bool nockback=false;
    float nocktime2;
    Vector3 nockpoint;
    Vector3 nockvec;
    float nocktime;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
            agent.destination = new Vector3(transform.position.x+Random.Range(-1f, 1f),transform.position.y,transform.position.z+Random.Range (-1f,1f));
            return;
        }

        // �G�[�W�F���g�����ݐݒ肳�ꂽ�ڕW�n�_�ɍs���悤�ɐݒ肵�܂�
        agent.destination = points[destPoint].position;

        // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A
        // �K�v�Ȃ�Ώo���n�_�ɂ��ǂ�܂�
        destPoint = (destPoint + 1) % points.Length;
    }
     private void ZombieSound() //ゾンビの鳴き声Sound
        {
            if (zombiewait==false) {
            StartCoroutine("Zombie_Wait_sound");
        }
        }
        IEnumerator Zombie_Wait_sound()
        {
            gameManager.SoundZombieSound();
            zombiewait = true;
            yield return new WaitForSeconds(4.0f);
            zombiewait = false;
        }


    void Update()
    {
        if(!gameManager.enemymove)return;
        if(anim.GetBool("Z_Die")){
             Vector3 newPosition = transform.position;newPosition.y = 0.6f;
             transform.position = newPosition;
            agent.destination=transform.position;
            return;
        }
        timeforhaikai+=Time.deltaTime;
        timeforcool+=Time.deltaTime;
        if(nockback){
            agent.destination=transform.position;
            nocktime2+=Time.deltaTime;
            if(nocktime2<=nocktime){
                transform.position += nockvec*(1/nocktime)*Time.deltaTime;
            }else{
                transform.position = nockpoint;
                nockback=false;
            }
            return;
        }
        if(stopmove){
            agent.destination=transform.position;
            return;
        }
        playerPos = player.transform.position;
        distance = Vector3.Distance(this.transform.position, playerPos);
        if(distance>30)return;
        bool sightjudge=IsVisible();
        //Player�Ƃ��̃I�u�W�F�N�g�̋����𑪂�

         if(distance < soundRange)
        {
            ZombieSound();
        }

        if (tracking)
        {
            //�ǐՂ̎��AquitRange��苗�������ꂽ�璆�~
            if (distance > quitRange)
            {
                tracking = false;
            }
            if (distance < atackRange)
            {
                if(!attackwait)anim.SetTrigger("Z_atack");
            }
            //Player��ڕW�Ƃ���
            if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh) {
            agent.destination = playerPos;
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
                    Summonbikkuri();
                }
                if(sightjudge){
                    tracking = true;
                    Summonbikkuri();
                }
            }


            // �G�[�W�F���g�����ڕW�n�_�ɋ߂Â��Ă�����A
            // ���̖ڕW�n�_��I�����܂�
            if (!agent.pathPending && agent.remainingDistance < 0.5f&&haikaicooltime<timeforhaikai){
                GotoNextPoint();
                haikaicooltime=Random.Range(3f,6f);
                timeforhaikai=0f;
            }
            //Debug.Log(agent.velocity.magnitude);
        }
        GameObject[] villager = GameObject.FindGameObjectsWithTag("villager");
        foreach(GameObject obj in villager){
            float vildistance = Vector3.Distance(this.transform.position, obj.transform.position);
            if (vildistance<trackingRange&&vildistance < distance)
            {
                agent.destination = obj.transform.position;
                if (vildistance < atackRange&&!attackwait){
                    anim.SetTrigger("Z_atack");
                    transform.LookAt(new Vector3(obj.transform.position.x,transform.position.y,obj.transform.position.z));
                }else if(attackwait){//攻撃中
                    agent.destination = obj.transform.position;
                }
            }
        }
        if(agent.velocity.magnitude > 0){
            anim.SetBool("move", true);
        }else{
            anim.SetBool("move", false);
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
        return innerProduct > cosHalf && targetDistance < trackingRange*6;
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
        if(timeforcool<cooltime)return;
        // Cubeプレハブを元に、インスタンスを生成
        gameManager.SoundBikkuri();
        Instantiate (bikkuri, new Vector3(this.transform.position.x,this.transform.position.y+0.2f,this.transform.position.z), Quaternion.identity);
        timeforcool=0f;
    }
    public void Nockback(Vector3 vec,float sec){
        nockback=true;
        anim.SetBool("move",false);
        anim.SetBool("run",false);
        nockvec=vec;
        nocktime=sec;
        nockpoint=transform.position+vec;
        nocktime2=0;
    }
    void Stop(){
        stopmove=true;
        anim.SetBool("move",false);
        anim.SetBool("run",false);
    }
    void completeStandUp(){
        stopmove=false;
        changespeed(1.0f);
        anim.SetBool("stan",false);
        attackwait=false;
    }
    void Run(float runspeed){
        anim.SetBool("run",true);
        changespeed(runspeed);
    }
    void StartNurceAttack(){
        anim.SetBool("stan",true);
        attackwait=true;
    }
    void changespeed(float speed){
        agent.speed=speed*origenalspeed;
    }
}