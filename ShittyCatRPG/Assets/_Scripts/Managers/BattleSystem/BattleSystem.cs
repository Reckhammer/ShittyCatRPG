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
    public static BattleSystem instance;
    private BattleState currentState;

    public Character[] players;
    public Character[] enemies;

    public BattleStation[] playerBattleStations;
    public BattleStation[] enemyBattleStations;

    public BattleHUD hudPrefab;

    public AttackAction currentPlayerAttackAction;
    public SpecialAction currentPlayerSpecialAction;
    public EnemyAttackAction currentEnemyAttackAction;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentState = BattleState.START;
        StartCoroutine(SetUpBattle());
    }

    private void OnDestroy()
    {
        instance = null;
    }

    IEnumerator SetUpBattle()
    {
        //BattleSystemMenu.instance.SetDialogueText("Battle Begins");
        Debug.Log("Battle Begins");
        SpawnCharacters();

        yield return new WaitForSeconds(2f);

        currentState = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void SpawnCharacters()
    {
        // Move Players
        int playerBattleStationIndex = 0;
        foreach (Character character in players)
        {
            if (playerBattleStationIndex >= playerBattleStations.Length)
            {
                Debug.LogError("Unequal amount of battle stations to amount of player characters");
                return;
            }

            playerBattleStations[playerBattleStationIndex++].SetCharacterPosition(character);
        }

        // Move Enemies
        int enemyBattleStationIndex = 0;
        foreach (Character character in enemies)
        {
            if (enemyBattleStationIndex >= enemyBattleStations.Length)
            {
                Debug.LogError("Unequal amount of battle stations to amount of enemy characters");
                return;
            }

            enemyBattleStations[enemyBattleStationIndex++].SetCharacterPosition(character);
        }

        SetUpCharacterHUDs();
    }

    void SetUpCharacterHUDs()
    {
        foreach (BattleStation station in playerBattleStations)
        {
            if (station.character != null)
            {
                BattleHUD hudInstance = Instantiate(hudPrefab, station.hudPosition.position, station.hudPosition.rotation);
                hudInstance.SetHUD(station.character);
                hudInstance.gameObject.name = station.character.characterName +" BattleHUD";
            }
        }

        foreach (BattleStation station in enemyBattleStations)
        {
            if (station.character != null)
            {
                BattleHUD hudInstance = Instantiate(hudPrefab, station.hudPosition.position, station.hudPosition.rotation);
                hudInstance.SetHUD(station.character);
                hudInstance.gameObject.name = station.character.characterName + " BattleHUD";
            }
        }
    }

    void PlayerTurn()
    {
        //BattleSystemMenu.instance.SetDialogueText("Choose an action");
        Debug.Log("Choose an action");
    }

    public void OnAttackButton()
    {
        if (currentState != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnPlayerSpecialButton()
    {
        if (currentState != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerSpecial());
    }

    IEnumerator PlayerAttack()
    {
        // enemy take damage
        yield return StartCoroutine(currentPlayerAttackAction.ActionSequence());

        yield return new WaitForSeconds(2f);

        if (isAllEnemiesDead())
        {
            currentState = BattleState.WON;
            EndBattle();
        }
        else
        {
            currentState = BattleState.ENEMYTURN;
            EnemyTurn();
        }
    }

    IEnumerator PlayerSpecial()
    {
        // enemy take damage
        yield return StartCoroutine(currentPlayerAttackAction.ActionSequence());

        yield return new WaitForSeconds(2f);

        if (isAllEnemiesDead())
        {
            currentState = BattleState.WON;
            EndBattle();
        }
        else
        {
            currentState = BattleState.ENEMYTURN;
            EnemyTurn();
        }
    }

    void EnemyTurn()
    {
        //BattleSystemMenu.instance.SetDialogueText("Opponents are attacking");
        Debug.Log("Opponents are attacking");

        StartCoroutine(EnemyTurnRoutine());
    }

    IEnumerator EnemyTurnRoutine()
    {
        // enemy take damage
        yield return StartCoroutine(currentEnemyAttackAction.ActionSequence());

        if (isAllPlayersDead())
        {
            currentState = BattleState.LOST;
            EndBattle();
        }
        else
        {
            currentState = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if (currentState == BattleState.WON)
        {
            // do win stuff
            Debug.Log("The Players won the battle");
        }
        else
        {
            // Go to lose screen
            Debug.Log("The Players lost the battle");
        }
    }

    bool isAllPlayersDead()
    {
        foreach (Character player in players)
        {
            if (!player.stats.isDead)
                return false;
        }

        return true;
    }

    bool isAllEnemiesDead()
    {
        foreach (Character enemy in enemies)
        {
            if (!enemy.stats.isDead)
                return false;
        }

        return true;
    }
}
