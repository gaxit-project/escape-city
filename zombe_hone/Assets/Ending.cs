using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    private GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        gameManager = FindObjectOfType<GameManager>();
        if (other.CompareTag("Player"))
        {
            Debug.Log("���\�b�h�����s���܂����B");
            gameManager.GameClear();
        }

    }

}
