using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrapScript : MonoBehaviour
{
    public bool wait=false;
    public TestTrapScript TestTrap;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!TestTrap.wait&&!wait)
        {
            wait=true;
            var status = other.GetComponent<CharacterStatusScript>();
            var move = other.GetComponent<PlayerControllerWithCamera>();
            status.Damage(10);
            StartCoroutine("E_Damage");
            move.P_Damage();
        }
        
    }

    IEnumerator E_Damage()
    {
        yield return new WaitForSeconds(1);
        wait=false;
    }
}