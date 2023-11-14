using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Item : MonoBehaviour
{
    private int keyitemCount = 0;
    private bool Clear=false;
    
    private GameManager gameManager;
    ParticleSystem gunParticles;

    public void Start()
    {
        gunParticles = GetComponent<ParticleSystem> ();
        gameManager = FindObjectOfType<GameManager>();
    }

    private int sphereItemCount = 0;
    private bool canCollectBlock = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SphereItem"))
        {
            var pick = other.GetComponent<PickUpObject>();
            if(pick.PickUp()){
                gameManager.Sounditem();
                //Destroy(other.gameObject); // 球体アイテムを削除
                sphereItemCount++;
                gameManager.taskUpdate(sphereItemCount);
                gameManager.scoreUpdate(500);
                //Debug.Log("橋の部品を手に入れた: " + sphereItemCount);
                if (sphereItemCount >= 3)
                {
                    var guid = GameObject.Find("Clear").GetComponent<GuidLine>();
                    guid.guidemode=true;
                    canCollectBlock = true;
                    //Debug.Log("橋を渡ろう!");
                }else if(other.CompareTag("BlockItem")){
                    //Debug.Log("橋の部品が足りないよ!");
                }
            }
        }
        else if (other.CompareTag("BlockItem") && canCollectBlock)
        {
            Destroy(other.gameObject); // ブロックアイテムを削除
            gameManager.scoreUpdate(1000);
            gameManager.GameClear();
            GetComponent<CapsuleCollider>().enabled = false;
        }
        if(other.CompareTag("item")){
            var pick = other.GetComponent<PickUpObject>();
            if(pick.Powerup()){
                gameManager.Sounditem();
                gunParticles.Stop ();
                gunParticles.Play ();
            }
        }else if(other.CompareTag("cureitem")){
            var pick = other.GetComponent<PickUpObject>();
            if(pick.Cure()){
                gameManager.Sounditem();
                GetComponent<CharacterStatusScript>().Cure(30);
            }
        }
    }
}
