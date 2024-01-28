using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName = "TEMP";
    public CharacterStats stats;
    //private CharacterAction[] characterActions;

    public AttackAction attackAction;
    public SpecialAction specialAction;

    public Animator animator;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
        //characterActions = GetComponents<CharacterAction>();
        attackAction = GetComponent<AttackAction>();
        specialAction = GetComponent<SpecialAction>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stats.Death += Die;
    }

    public void PlayAnimation(string animationName)
    {
        if (animator == null || animator.runtimeAnimatorController == null)
            return;

        animator.SetTrigger(animationName);
    }

    public void Die()
    {
        PlayAnimation("Death");
    }
}
