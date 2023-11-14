using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowscript : MonoBehaviour
{
    public float deleteTime = 1.0f;
    void Start()
    {
        
        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 p = Camera.main.transform.position;
		p.y = this.transform.position.y;
		this.transform.LookAt (p);
        this.transform.rotation = Quaternion.AngleAxis(90, Vector3.left); 
	}
}
