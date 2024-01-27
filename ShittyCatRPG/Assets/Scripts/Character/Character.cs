using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName = "TEMP";
    public CharacterStats stats;
    private List<CharacterAction> characterActions;

    public Animator animator;

    public void PlayAnimation(string animationName)
    {
        animator.SetTrigger(animationName);
    }

}
