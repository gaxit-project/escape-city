/*using UnityEngine;
using UnityEngine.InputSystem;

public class UnityEventExample : MonoBehaviour
{
    private Vector3 _velocity;
    private Animator m_Animator = null;

    //-----------------------------------------------------------------------
    // Unityメゾット
    //-----------------------------------------------------------------------

    // 初期化処理
    private void Start()
    {
        m_Animator = GameObject.Find("Custom simple human_prefab").GetComponent<Animator>();
    }

    // メソッド名は何でもOK
    // publicにする必要がある
    public void OnMove(InputAction.CallbackContext context)
    {
        // MoveActionの入力値を取得
        var axis = context.ReadValue<Vector2>();

        // 移動速度を保持
        _velocity = new Vector3(axis.x, 0, axis.y);

    }

    private void Update()
    {
        // オブジェクト移動
        transform.position += _velocity * Time.deltaTime;
        // 静止している状態だと、進行方向を特定できないため回転しない
        if (_velocity * Time.deltaTime == Vector3.zero){
            m_Animator.SetInteger("legs", 5);
            return;
        }else{
            m_Animator.SetInteger("legs", 2);
        }

        // 進行方向（移動量ベクトル）に向くようなクォータニオンを取得
        var rotation = Quaternion.LookRotation(_velocity * Time.deltaTime, Vector3.up);

        // オブジェクトの回転に反映
        transform.rotation = rotation;
        Debug.Log (_velocity);
    }
}*/