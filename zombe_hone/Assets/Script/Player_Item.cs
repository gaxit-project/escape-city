using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Item : MonoBehaviour
{
    private int keyitemCount = 0;
    private bool Clear=false;
    public GameObject effect;
    private GameManager gameManager;
    ParticleSystem gunParticles;
    public bulletLvUI bUI;
    public sordLvUI sUI;

    public GameObject villager;

    public void Start()
    {
        gunParticles = GetComponent<ParticleSystem> ();
        gameManager = FindObjectOfType<GameManager>();
        CharacterStatusScript characterStatus = FindObjectOfType<CharacterStatusScript>();
    }

    public int sphereItemCount = 0;
    private bool canCollectBlock = false;
    private bool itemlost=false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SphereItem"))
        {
            var pick = other.GetComponent<PickUpObject>();
            if(pick.PickUp()){
                gameManager.Sounditem();
                //Destroy(other.gameObject); // 球体アイテムを削除
                sphereItemCount++;
                GameObject kira = Instantiate(effect, transform.position, Quaternion.identity);
                //Debug.Log("橋の部品を手に入れた: " + sphereItemCount);
                if (sphereItemCount == 1) {
                    gameManager.changeMessage("よし、修理用の部品だ。あと二つ");
                }
                if (sphereItemCount == 2) {
                    gameManager.changeMessage("二つ目だ。あと一つ。");
                }
                if (sphereItemCount == 3) {
                    gameManager.changeMessage("よし、これで橋が修理できる。早くこの街からでないと。");
                }
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
            gameManager.scoreUpdate(1000,"街から脱出!");
            gameManager.endmovieStart();
            GetComponent<CapsuleCollider>().enabled = false;
        }
        if(other.CompareTag("item")){
            var pick = other.GetComponent<PickUpObject>();
            if(pick.Powerup()){
                gameManager.Sounditem();
                gunParticles.Stop ();
                gunParticles.Play ();
                if(bUI!=null&&sUI!=null){
                    bUI.MesageUpdate();
                    sUI.MesageUpdate();
                }
            }
        }else if(other.CompareTag("cureitem")){
            var pick = other.GetComponent<PickUpObject>();
            if(pick.Cure()){
                if(villager!=null){
                    if(villager.GetComponent<villager_ai>().tracking){
                        villager.GetComponent<CharacterStatusScript>().Cure(40);
                    }
                }
                gameManager.Sounditem();
                GetComponent<CharacterStatusScript>().Cure(30);

            }
        }
    }
    public void DecreaseSphereItemCount(){
        //print("aaa");
            if(sphereItemCount>0){
                sphereItemCount--;
                gameManager.taskUpdate(sphereItemCount);
                gameManager.scoreUpdate(-500,"キーアイテムを失った");
                print("LOST");
                itemlost=true;
            }
    }
    public void DefeatEnemy(){
        if (itemlost) {
            sphereItemCount++; // アイテムを取り戻す
            gameManager.taskUpdate(sphereItemCount);
            gameManager.scoreUpdate(500,"キーアイテムを取り戻した"); 
            itemlost=false;
            print("RETURN");
        }
    }
}
