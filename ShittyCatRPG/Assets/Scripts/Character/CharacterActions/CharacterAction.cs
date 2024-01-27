using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    public string animationName = "DEFAULT";
    public AudioClip soundFX;

    public Coroutine StartActionSequence(Character target)
    {
        return StartCoroutine(ActionSequence(target));
    }

    public virtual IEnumerator ActionSequence(Character target)
    {
        yield return null;
    }
}
