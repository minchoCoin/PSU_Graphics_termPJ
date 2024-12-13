using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerAnimatorControler : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public float SetAnimeMoveSpeed
    {
        set => animator.SetFloat("PlayerSpeed", value);
        get => animator.GetFloat("PlayerSpeed");
    }
}
