using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName = "TEMP";
    public CharacterStats stats;
    private CharacterAction[] characterActions;

    public Animator animator;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
        characterActions = GetComponents<CharacterAction>();
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string animationName)
    {
        animator.SetTrigger(animationName);
    }

}
