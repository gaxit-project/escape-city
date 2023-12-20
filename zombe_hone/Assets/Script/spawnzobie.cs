using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    public GameObject[] EnemyList;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.itemnum>=3){
            for(int i=0;i<EnemyList.Length;i++){
                EnemyList[i].SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
