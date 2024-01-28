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

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Sprite[] sprites;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
        //characterActions = GetComponents<CharacterAction>();
        attackAction = GetComponent<AttackAction>();
        specialAction = GetComponent<SpecialAction>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        stats.Death += Die;
    }

    public void PlayAnimation(string animationName)
    {
        if (animator == null || animator.runtimeAnimatorController == null)
            return;

        StartCoroutine(PlayAnimationRoutine(animationName));
    }

    IEnumerator PlayAnimationRoutine(string animationName)
    {

        if (animationName.Equals("Attack"))
        {
            spriteRenderer.sprite = sprites[1];
        }
        else // hurt or death
        {
            spriteRenderer.sprite = sprites[2];
        }

        animator.SetTrigger(animationName);

        yield return new WaitForSeconds(2f);

        if (!animationName.Equals("Death"))
        {
            spriteRenderer.sprite = sprites[0];
        }
    }

    public void Die()
    {
        PlayAnimation("Death");
    }
}
