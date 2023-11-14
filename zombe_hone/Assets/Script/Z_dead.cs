using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z_dead : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void E_Die()
    {
        anim.SetBool("Z_Die", true);
    }
}
