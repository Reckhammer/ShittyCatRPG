using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCombatSystem : MonoBehaviour
{
    enum WhosTurn
    {
        Player,
        Enemy
    }

    private WhosTurn currentTurn = WhosTurn.Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
