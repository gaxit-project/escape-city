using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialcolor : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject A;
    public byte R;
    public byte G;
    public byte B;
    public byte W;
    
    void Start()
    { 
        A=GameObject.Find("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        A.GetComponent<Renderer>().material.color = new Color32(R, G, B, W);
    }
}
