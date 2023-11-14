using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrapScript : MonoBehaviour
{
    private bool wait=false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&wait==false)
        {
            var status = other.GetComponent<CharacterStatusScript>();
            status.Damage(10);
            StartCoroutine("E_Damage");
        }
        
    }

    IEnumerator E_Damage()
    {
        wait=true;
        yield return new WaitForSeconds(1);
        wait=false;
    }
}