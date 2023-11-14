using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoredata : MonoBehaviour
{
    public int[] data;
    void Start(){
        DontDestroyOnLoad(gameObject);
    }
}
