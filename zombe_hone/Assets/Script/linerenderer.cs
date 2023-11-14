using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linerenderer : MonoBehaviour
{
    LineRenderer gunLine;
    public GameObject first;
    public GameObject second;
    void Start()
    {
        gunLine = GetComponent<LineRenderer>();
        gunLine.SetPosition (1, first.transform.position);
        gunLine.SetPosition (0, second.transform.position);
    }
    void Update()
    {
        gunLine.SetPosition (1, first.transform.position);
        gunLine.SetPosition (0, second.transform.position);
    }
}
