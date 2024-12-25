using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SC_PlayerAnimatorControler : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator = GetComponentInChildren<Animator>();
    }

    public float AnimeMoveSpeed
    {

        set => animator.SetFloat("PlayerSpeed", value);
        get => animator.GetFloat("PlayerSpeed");
    }

    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

 
    public void ResetTrigger(string triggerName)
    {
        animator.ResetTrigger(triggerName);
    }

 
    public bool IsInState(string stateName)
    {
        var isInState = animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        Debug.Log($"Animator is in state '{stateName}': {isInState}");
        return isInState;
    }

    public void PlayAnime(string stateName, int layer, float normalizedTime)
    {
        animator.Play(stateName, layer, normalizedTime);
    }

    public void DebugAnimatorState()
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log($"Current Animator State: {stateInfo.fullPathHash}, PlayerSpeed: {AnimeMoveSpeed}");
    }

}
