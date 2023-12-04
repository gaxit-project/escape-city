using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrapScript : MonoBehaviour
{
    public bool wait=true;
    public TestTrapScript TestTrap;

    public int attackdamage=10;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!TestTrap.wait&&!wait)
        {
            wait=true;
            var status = other.GetComponent<CharacterStatusScript>();
            var move = other.GetComponent<PlayerControllerWithCamera>();
            status.Damage(attackdamage);
            //StartCoroutine("E_Damage");
            move.P_Damage();
        }
        
    }
    public void AttackStart(){
        wait=false;
        TestTrap.wait=false;
    }
    public void AttackEnd(){
        wait=true;
        TestTrap.wait=true;
    }

    IEnumerator E_Damage()
    {
        yield return new WaitForSeconds(1);
        wait=true;
    }
}