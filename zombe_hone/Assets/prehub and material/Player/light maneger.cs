using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightmaneger : MonoBehaviour
{
    public GameObject light;
    public float detectdistance;
    private bool Summonflag=true;

    public float timecountmax;
    private float counter;
    private float distance;
    private GameObject[] enemy;
    public Vector3 tpos;
    // Start is called before the first frame update
    void Start()
    {
        counter=timecountmax;
        if(light==null)Debug.Log("light=null error");
    }
    void Update (){
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
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
            tpos=targetposition;
            Summonlight();
            Summonflag=false;
            counter=0f;
        }else if(distance>=detectdistance){
            Summonflag=true;
        }

    }
    void Summonlight(){
        Debug.Log(tpos);
        Vector3 pos = transform.position;
        // Cubeプレハブを元に、インスタンスを生成
        Instantiate (light, new Vector3(pos.x,0.2f,pos.z), Quaternion.identity);
    }

}