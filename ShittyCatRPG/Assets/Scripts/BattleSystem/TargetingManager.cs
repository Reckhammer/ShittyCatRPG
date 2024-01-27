using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingManager : MonoBehaviour
{
    public static TargetingManager instance;
    public CharacterAction actionRequestingTarget;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void SetActionRequesting(CharacterAction action)
    {
        Debug.Log($"{action.name}:{action.GetType()} is requesting a target");
        actionRequestingTarget = action;
    }

    public void SetTarget(Character target)
    {
        if (actionRequestingTarget == null)
        {
            Debug.LogError("Trying to Set Target for null action");
            return;
        }

        Debug.Log($"{actionRequestingTarget.name}:{actionRequestingTarget.GetType()}'s target to {target.name}");
        actionRequestingTarget.SetTarget(target);
        actionRequestingTarget = null;
    }
}
