using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    protected Character myCharacter;
    protected Character target;

    public float actionTime = 2f;
    public string animationName = "DEFAULT";
    public AudioClip soundFX;

    private void Awake()
    {
        myCharacter = GetComponent<Character>();
    }

    public virtual IEnumerator ActionSequence()
    {
        yield return null;
    }

    public IEnumerator SelectTargetSequence()
    {
        //BattleSystemMenu.instance.SetDialogueText("Select a Target");
        Debug.Log("Select a Target");
        TargetingManager.instance.actionRequestingTarget = this;

        // Wait til the enemy location button is picked
        while (target = null)
            yield return null;
    }

    public void SetTarget(Character newTarget)
    {
        target = newTarget;
    }

    protected virtual void CleanUpAction()
    {
        target = null;
    }
}
