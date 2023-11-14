using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mover : MonoBehaviour
{
    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("ジャンプしました");
    }
}
