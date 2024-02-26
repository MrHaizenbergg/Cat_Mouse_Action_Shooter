using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInMenuController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Shoot()
    {
        animator.SetTrigger("shootInMenu");
    }
    public void Roll()
    {
        animator.SetTrigger("isRoll");
    }
}