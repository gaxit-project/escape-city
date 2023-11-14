using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCon : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up") || Input.GetKey("right") || Input.GetKey("left") || Input.GetKey("down"))
        {
            _animator.SetBool("Running", true);
        }
        else
        {
            _animator.SetBool("Running", false);
        }
    }
}
