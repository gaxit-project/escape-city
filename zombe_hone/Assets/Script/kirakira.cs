using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kirakira : MonoBehaviour
{
    public Vector3 gorl;
    private GameManager gameManager;
    public float speed=8.0f;
    private Vector3 pastcameratrans;
    private Player_Item playerItemScript;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start(){
        pastcameratrans=Camera.main.gameObject.transform.position;
        gorl=new Vector3(transform.position.x-0.5f,transform.position.y,transform.position.z+4.2f);
        gameManager = FindObjectOfType<GameManager>();
        playerItemScript=FindObjectOfType<Player_Item>();
    }
    void Update()
    {
        if(transform.position==gorl){
            int count=0;
            if (playerItemScript != null)
            {
                count = playerItemScript.sphereItemCount;
                Debug.Log("sphereItemCount: " + count);
            }
            Destroy(gameObject);
            gameManager.taskUpdate(count);
            gameManager.scoreUpdate(500);
        }
        gorl+=Camera.main.gameObject.transform.position-pastcameratrans;
        transform.position+=Camera.main.gameObject.transform.position-pastcameratrans;
        pastcameratrans=Camera.main.gameObject.transform.position;
        Vector3 distance=gorl-transform.position;
        Vector3 move = distance.normalized*Time.deltaTime*speed;
        if(move.magnitude>=distance.magnitude){
            transform.position=gorl;
        }else{
            transform.position+=move;
        }
        this.transform.LookAt(gorl);
    }
}
