using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesttrapController : MonoBehaviour
{
    // Start is called before the first frame update
    public TestTrapScript testTrap;

    public void startTrap(){
        testTrap.AttackStart();
    }
    public void endTrap(){
        testTrap.AttackEnd();
    }
}
