using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Weapon_damageScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var status = other.GetComponent<CharacterStatusScript>();
            status.Damage(10);
            Thread.Sleep(1000);
        }

    }
}
