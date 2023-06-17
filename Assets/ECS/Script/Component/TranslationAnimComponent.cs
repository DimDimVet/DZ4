using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class TranslationAnimComponent : MonoBehaviour, IAnimComponent
{
    private Animator animator;
    private float refFloat = 0.01f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void IAnimComponent.GetMove(float2 currentMove)
    {
        if (Mathf.Abs(currentMove.x) >= refFloat | Mathf.Abs(currentMove.y) >= refFloat)
        {
            animator.SetFloat("SpeedPlayer", 1);
        }
        else
        {
            animator.SetFloat("SpeedPlayer", 0);
        }
    }

    void IAnimComponent.GetPull(bool currentPull)
    {
        if (currentPull)
        {
            animator.SetBool("JampPlayer", true);
        }
        else
        {
            animator.SetBool("JampPlayer", false);
        }
    }
}
