using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class enemystatesScript : MonoBehaviour
{
    Animator anim;
    public UnityEvent onDieCallback = new UnityEvent();

    public int life = 100;

    public Slider hpBar;

    private GameManager gameManager;

    void Start()
    {
        anim = GetComponent<Animator>();

        gameManager = FindObjectOfType<GameManager>();

        if (hpBar != null)
        {
            hpBar.value = life;
        }
    }

    public void Damage(int damage)
    {
        if (life <= 0) return;

        life = life - damage;
        if (hpBar != null)
        {
            hpBar.value = life;
        }
        if (life <= 0)
        {
            OnDie();
            gameManager.Over();
        }
    }

    void OnDie()
    {
        anim.SetBool("Die", true);
        onDieCallback.Invoke();
    }
}