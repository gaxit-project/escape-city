using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySteal : MonoBehaviour
{
    // Start is called before the first frame update
    private Player_Item player_Item;
    void Start(){
        player_Item = FindObjectOfType<Player_Item>();
    }
    private void OnTriggerEnter(Collider other){
        if(this.gameObject.CompareTag("Enemy")){
            player_Item.DecreaseSphereItemCount();
        }
    }
}
