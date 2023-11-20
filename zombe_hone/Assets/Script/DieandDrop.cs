using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieandDrop : MonoBehaviour
{
    Animator anim;
    public GameObject[] item;
    public int[] itemHIRITU;
    public bool changehiritu;
    public int[] changeditemHIRITU;//itemhirituとこれの値はその累計を100000にすることを推奨する。でないと正しく確率が推移しない
    public float maxtimer;
    public float parcent = 50;
    public float changedparcent = 50;
    private Transform _transform;
    float timer=0;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale==1f)
        {
            timer += Time.deltaTime; // 経過時間を計算
        }
    }
    public void Drop()
    {
        anim = GetComponent<Animator>();
        _transform=transform;
        if(item.Length!=0){
            if(item.Length!=itemHIRITU.Length){
                Debug.Log("itemDrop:％の数とアイテム数の不一致");
                return;
            }
            int sumhiritu=0;
            if(changehiritu){
                if(item.Length!=itemHIRITU.Length){
                    Debug.Log("itemDrop:％の数とchanged％の数の不一致");
                    return;
                }
                if(timer<=maxtimer){
                    int[] useitemHIRITU=new int[itemHIRITU.Length];
                    for(int i=0;i<itemHIRITU.Length;i++){
                        useitemHIRITU[i]=(itemHIRITU[i]+(int)((float)(changeditemHIRITU[i]-itemHIRITU[i])*(timer/maxtimer)));
                        sumhiritu+=useitemHIRITU[i];
                    }
                    int index=(int)Random.Range (0f, (float)(sumhiritu)*(100/(parcent+ ((changedparcent-parcent)*(timer/maxtimer)) )) );
                    int indexsum=0;
                    Debug.Log("index: "+index+" timer: "+timer+" : "+useitemHIRITU[0]+" :"+useitemHIRITU[1]+" :"+useitemHIRITU[2]);
                    for(int i=0;i<useitemHIRITU.Length;i++){
                        indexsum+=useitemHIRITU[i];
                        if(index<=indexsum){
                            Instantiate (item[i], _transform.position, Quaternion.identity);
                            break;
                        }
                    }
                }else{
                    for(int i=0;i<itemHIRITU.Length;i++){
                        sumhiritu+=changeditemHIRITU[i];
                    }
                    int index=(int)Random.Range (0f, (float)(sumhiritu)*(100/changedparcent));
                    int indexsum=0;
                    Debug.Log("index: "+index+" timer: "+timer+" maxtimerON: "+changeditemHIRITU[0]+" :"+changeditemHIRITU[1]+" :"+changeditemHIRITU[2]);
                    for(int i=0;i<changeditemHIRITU.Length;i++){
                        indexsum+=changeditemHIRITU[i];
                        if(index<=indexsum){
                            Instantiate (item[i], _transform.position, Quaternion.identity);
                            break;
                        }
                    }
                }
            }else{
                for(int i=0;i<itemHIRITU.Length;i++){
                    sumhiritu+=itemHIRITU[i];
                }
                int index=(int)Random.Range (0f, (float)(sumhiritu)*(100/parcent));
                int indexsum=0;
                for(int i=0;i<itemHIRITU.Length;i++){
                    indexsum+=itemHIRITU[i];
                    if(index<=indexsum){
                        Instantiate (item[i], _transform.position, Quaternion.identity);
                        break;
                    }
                }
            }
        }
        //Destroy(gameObject,1);
    }
}
