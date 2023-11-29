using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class EnemyHit : MonoBehaviour
{
    private bool wait=false;
    //�I�u�W�F�N�g�ƐڐG�����u�ԂɌĂяo�����
    void OnTriggerEnter(Collider other)
    {

        //�U���������肪Enemy�̏ꍇ
        if (other.CompareTag("Enemy")&&wait!=true)
        {
            var status = other.GetComponent<CharacterStatusScript>();
            status.Damage(GetComponent<WeaponStates>().damage);
            StartCoroutine("E_Damage");
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
    IEnumerator E_Damage()
    {
        wait=true;
        yield return new WaitForSeconds(0.3f);
        wait=false;
    }
}