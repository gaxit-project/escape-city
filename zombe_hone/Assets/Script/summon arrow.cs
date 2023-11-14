using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NavMeshAgent�g���Ƃ��ɕK�v
using UnityEngine.AI;

public class summonarrow : MonoBehaviour
{
    public GameObject arrow;
    public float detectdistance;
    private bool Summonflag=true;

    public float timecountmax;
    private float counter;
    private float distance;
    private GameObject[] enemy;
    void Start()
    {
        counter=timecountmax;
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
    }
    
    void Update (){
        Vector3 targetposition =new Vector3(0f,0f,0f);
        counter+=Time.deltaTime;
        if(timecountmax>counter){
            return;
        }
        distance=detectdistance;
        foreach(GameObject obj in enemy){
            Vector3 pos=obj.transform.position;
            float dis=Vector3.Distance(pos, transform.position);
            if(dis < distance){
                distance=dis;
                targetposition=pos;
            }
        }
        if(distance<detectdistance&&Summonflag==true){
            SummonArrow(targetposition);
            Summonflag=false;
            counter=0f;
        }else if(distance>=detectdistance){
            Summonflag=true;
        }

    }
    void SummonArrow(Vector3 tpos){
        Vector3 pos = transform.position;
        float routx=pos.x-tpos.x;
        float routz=pos.z-tpos.z;
        Vector3 dis = new Vector3(routx,0,routz).normalized;
        // Cubeプレハブを元に、インスタンスを生成
        Instantiate (arrow, new Vector3(pos.x-(dis.x/2),0.1f,pos.z-(dis.z/2)), Quaternion.identity);
    }
}