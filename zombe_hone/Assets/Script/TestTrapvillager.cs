using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Threading;

public class TestTrapvillager : MonoBehaviour
{
    public bool wait=true;
    public int attackdamage=25;
    void OnTriggerStay(Collider other)
    {
        Debug.Log("hit yet hit : "+wait+":"+other.name);
        if (other.CompareTag("Enemy")&&wait!=true)
        {
            Debug.Log("hit");
            wait=true;
            var status = other.GetComponent<CharacterStatusScript>();
            //var move = other.GetComponent<PlayerControllerWithCamera>();
            status.Damage(attackdamage);
            var ai = other.GetComponent<Patrol>();
            //var move = other.GetComponent<PlayerControllerWithCamera>();
            Vector3 vec = other.transform.position - transform.position;
            vec=new Vector3(vec.x,0,vec.z);
            ai.Nockback(vec.normalized*2.0f,0.3f);
            //StartCoroutine("E_Damage");
            //move.P_Damage();
        }   
        if (other.CompareTag("arbinoEnemy")&&wait!=true)
        {
            var status = other.GetComponent<CharacterStatusScript>();
            status.Damage(GetComponent<WeaponStates>().damage);
            StartCoroutine("E_Damage");
        }
        if (other.CompareTag("canBreakStruct")&&wait!=true)
        {
            var status = other.GetComponent<CharacterStatusScript>();
            status.Damage(GetComponent<WeaponStates>().damage);
            StartCoroutine("E_Damage");
        }
    }
    public void AttackStart(){
        wait=false;
        //TestTrap.wait=false;
    }
    public void AttackEnd(){
        wait=true;
        ///TestTrap.wait=true;
    }

    IEnumerator E_Damage()
    {
        yield return new WaitForSeconds(1);
        wait=true;
    }
}