using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{

    //����̃R���C�_�[
    private Collider WeaponCollider;

    private Animator anim;  //Animator��anim�Ƃ����ϐ��Œ�`����

    private GameObject hand;
    private GameManager gameManager;
    private bool wait=false;

    // Use this for initialization
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //�ϐ�anim�ɁAAnimator�R���|�[�l���g��ݒ肷��
        anim = gameObject.GetComponent<Animator>();
        //����̃R���C�_�[���擾
        hand=GameObject.Find("righthand");
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if(anim.GetBool("Die"))return;
        if (context.performed&&anim.GetInteger("type")==0&&wait!=true)
        {
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
            gameManager.Soundswing();
            foreach(GameObject obj in enemy){
                obj.GetComponent<Patrol>().sound=4;
            }
            GameObject[] arbinoenemy = GameObject.FindGameObjectsWithTag("arbinoEnemy");
            foreach(GameObject obj in arbinoenemy){
                obj.GetComponent<arbinoPatrol>().sound=6;
            }
            WeaponCollider = hand.GetComponent<weaponscript>().weapon.GetComponent<CapsuleCollider>();
            //����R���C�_�[���I���ɂ���
            WeaponCollider.enabled = true;

            //��莞�Ԍ�ɃR���C�_�[�̋@�\���I�t�ɂ���

            //Bool�^�̃p�����[�^�[�ł���blRot��True�ɂ���
            anim.SetTrigger("atack");
            anim.SetBool("itaiyou", false);
            StartCoroutine("E_Damage");
        }
    }

    private void ColliderReset()
    {
        WeaponCollider.enabled = false;
    }
    IEnumerator E_Damage()
    {
        wait=true;
        yield return new WaitForSeconds(0.3f);
        ColliderReset();
        wait=false;
    }

}