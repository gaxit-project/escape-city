using UnityEngine;

public class Sample : MonoBehaviour
{

    public GameObject obj;

    void Start()
    {


        obj.SetActive(true);    // 有効にする

        obj.SetActive(false);   // 無効にする
    }
}