using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Targetable : MonoBehaviour
{
    private Character character;
    private Button button;

    private void Awake()
    {
        character = GetComponentInParent<Character>();
        button = GetComponentInChildren<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(delegate { TargetingManager.instance.SetTarget(character); });
    }
}
