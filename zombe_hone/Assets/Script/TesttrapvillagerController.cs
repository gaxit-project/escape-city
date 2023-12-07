using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesttrapController2 : MonoBehaviour
{
    // Start is called before the first frame update
    public TestTrapvillager testTrap;

    public void startTrap(){
        testTrap.AttackStart();
    }
    public void endTrap(){
        testTrap.AttackEnd();
    }
}
