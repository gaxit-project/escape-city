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
            var move = other.GetComponent<PlayerControllerWithCamera>();
            status.Damage(10);
            StartCoroutine("E_Damage");
            move.P_Damage();
        }
        
    }

    IEnumerator E_Damage()
    {
        wait=true;
        yield return new WaitForSeconds(1);
        wait=false;
    }
}