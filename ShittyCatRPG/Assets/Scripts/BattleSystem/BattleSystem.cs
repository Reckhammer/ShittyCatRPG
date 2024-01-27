using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class BattleSystem : MonoBehaviour
{
    private BattleState currentState;

    public Character[] playerGOs;
    public Character[] enemyGOs;

    public BattleStation[] playerBattleStations;
    public BattleStation[] enemyBattleStations;

    public BattleSystemMenu menu;

    // Start is called before the first frame update
    void Start()
    {
        currentState = BattleState.START;
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        menu.SetDialogueText("Battle Begins");
        SpawnCharacters();

        yield return new WaitForSeconds(2f);

        currentState = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void SpawnCharacters()
    {
        // Spawn Players

        // Spawn Enemies
        
        SetUpCharacterHUDs();
    }

    void SetUpCharacterHUDs()
    {
        // Loop thru all of the lists
    }

    void PlayerTurn()
    {
        menu.SetDialogueText("Choose an action");
    }

    IEnumerator EnemyTurn()
    {
        yield return null;
    }

    void EndBattle()
    {

    }

    public void OnAttackButton()
    {
        if (currentState != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        // enemy take damage
        yield return new WaitForSeconds(2f);


    }
}
