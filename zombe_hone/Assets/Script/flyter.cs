using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyter : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public GameObject bakudan;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void bakugeki(){
        Instantiate (bakudan, transform.position+new Vector3(3f,0,0), transform.rotation);
        Instantiate (bakudan, transform.position+new Vector3(-3f,0,0), transform.rotation);
    }
}
