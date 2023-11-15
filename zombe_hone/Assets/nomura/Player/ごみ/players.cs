using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class players : MonoBehaviour
{
	private bool Grounded;
	private float jumptime;
	public Rigidbody rb;
	private float jumpbuttonkeeptime=1.1f,keeptime;
	float jumppower=5.0f;
	private Ray ray; // 飛ばすレイ
    private float distance = 0.09f; // レイを飛ばす距離
    private RaycastHit hit; // レイが何かに当たった時の情報
    private Vector3 rayPosition; // レイを発射する位置

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

		rayPosition = this.transform.position + new Vector3(0, -1.0f, 0); // レイを発射する位置の調整
        ray = new Ray(rayPosition, transform.up * -1); // レイを下に飛ばす
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red); // レイを赤色で表示させる
		//print(Grounded);
		if (Physics.Raycast(ray, out hit, distance))
		{
			//Rayが当たったオブジェクトの名前と位置情報をログに表示する
			Debug.Log(hit.collider.gameObject.name);
			Debug.Log(hit.collider.gameObject.transform.position);
		}
		/*if (Physics.Raycast(ray, out hit)) // レイが当たった時の処理
        {
            if (hit.collider.tag == "Ground") // レイが地面に触れたら、
            {
                Grounded = true; // 地面に触れたことにする
     
            }

            else // そうでなければ、
            {
                Grounded = false; // 地面に触れてないことにする
             
            }
        }*/
        if (Input.GetKey (KeyCode.W)&&Input.GetKey (KeyCode.E)) {
			this.transform.Translate (0.00f,0.00f,0.005f);
		}else if (Input.GetKey (KeyCode.W)) {
			this.transform.Translate (0.00f,0.00f,0.003f);
		} else {
		}
		if (Input.GetKey (KeyCode.S)) {
			if(Input.GetKey (KeyCode.W)&&Input.GetKey (KeyCode.E)){
				this.transform.Translate (0.00f,0.00f,-0.002f);
			}
			this.transform.Translate (0.00f,0.00f,-0.003f);
		} else {
		}
		if (Input.GetKey (KeyCode.A)) {
			this.transform.Rotate (0f, -0.3f, 0f);
		} else {
		}
		if (Input.GetKey (KeyCode.D)) {
			this.transform.Rotate (0f, 0.3f, 0f);
		} else {
        }
		if(jumptime<=0.2){
			jumptime+=Time.deltaTime;
		}
		if(keeptime<=jumpbuttonkeeptime){
			keeptime+=Time.deltaTime;
			if(keeptime<=jumpbuttonkeeptime){
				if (Physics.Raycast(ray, out hit, distance)){
					rb.AddForce(transform.up * jumppower, ForceMode.VelocityChange);
					jumptime=0;
					keeptime+=jumpbuttonkeeptime;
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			if(jumptime>0.2){
				keeptime=0;
				if (Physics.Raycast(ray, out hit, distance)){
					rb.AddForce(transform.up * jumppower, ForceMode.VelocityChange);
					jumptime=0;
					keeptime+=jumpbuttonkeeptime;
				}
			}
		}
	}
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.gameObject.name); // ぶつかった相手の名前を取得
    }
}
