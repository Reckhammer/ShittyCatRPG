using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStation : MonoBehaviour
{
    public Transform hudPosition;
    public Transform characterPosition;
    public Character character;

    public void SetCharacterPosition(Character newChar)
    {
        character = newChar;
        character.transform.position = characterPosition.position;
    }
}
