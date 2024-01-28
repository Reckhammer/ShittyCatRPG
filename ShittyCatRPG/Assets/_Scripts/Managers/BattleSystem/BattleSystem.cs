using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public BattleReward reward;

    public Character[] players;
    public Character[] enemies;

    public BattleStation[] playerBattleStations;
    public BattleStation[] enemyBattleStations;

    public BattleHUD hudPrefab;

    public AttackAction currentPlayerAttackAction;
    public SpecialAction currentPlayerSpecialAction;
    public AttackAction currentEnemyAttackAction;

    private bool playerTurnComplete = false;

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
        BattleSystemMenu.instance.SetDialogueText("The Battle Begins");
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
        BattleSystemMenu.instance.SetDialogueText("Players Turn");
        Debug.Log("Player's Turn");

        StartCoroutine(PlayerTeamTurnRoutine());
    }

    IEnumerator PlayerTeamTurnRoutine()
    {
        yield return new WaitForSeconds(2f);

        foreach (Character player in players)
        {
            if (!player.stats.isDead)
            {
                BattleSystemMenu.instance.SetDialogueText($"{player.characterName} chose an action.");
                Debug.Log($"{player.characterName} chose an action.");

                SetPlayerActions(player.attackAction, player.specialAction);

                yield return StartCoroutine(PlayerTurnRoutine());

                SetPlayerActions(null, null);

                if (isAllEnemiesDead())
                {
                    currentState = BattleState.WON;
                    EndBattle();
                    yield break;
                }

            }
        }

        currentState = BattleState.ENEMYTURN;
        EnemyTurn();
    }

    IEnumerator PlayerTurnRoutine()
    {
        while (!playerTurnComplete)
        {
            yield return null;
        }

        playerTurnComplete = false;

        yield return new WaitForSeconds(2f);
    }

    public void OnAttackButton()
    {
        if (currentState != BattleState.PLAYERTURN)
            return;

        if (currentPlayerAttackAction == null)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnPlayerSpecialButton()
    {
        if (currentState != BattleState.PLAYERTURN)
            return;

        if (currentPlayerSpecialAction == null)
            return;

        StartCoroutine(PlayerSpecial());
    }

    IEnumerator PlayerAttack()
    {
        // enemy take damage
        yield return StartCoroutine(currentPlayerAttackAction.ActionSequence());

        currentPlayerAttackAction = null;
        currentPlayerSpecialAction = null;

        yield return new WaitForSeconds(2f);
        playerTurnComplete = true;
    }

    IEnumerator PlayerSpecial()
    {
        // enemy take damage
        yield return StartCoroutine(currentPlayerSpecialAction.ActionSequence());

        currentPlayerAttackAction = null;
        currentPlayerSpecialAction = null;

        yield return new WaitForSeconds(2f);
        playerTurnComplete = true;
    }

    void EnemyTurn()
    {
        BattleSystemMenu.instance.SetDialogueText("Opponents are attacking");
        Debug.Log("Opponents are attacking");

        StartCoroutine(EnemyTeamTurnRoutine());
    }

    IEnumerator EnemyTeamTurnRoutine()
    {
        yield return new WaitForSeconds(2f);

        foreach(Character enemy in enemies)
        {
            if (!enemy.stats.isDead)
            {
                currentEnemyAttackAction = enemy.attackAction;
                yield return StartCoroutine(EnemyTurnRoutine());
                currentEnemyAttackAction = null;

                if (isAllPlayersDead())
                {
                    currentState = BattleState.LOST;
                    EndBattle();
                    yield break;
                }
                
            }
        }
        
        currentState = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator EnemyTurnRoutine()
    {
        // enemy take damage
        yield return StartCoroutine(currentEnemyAttackAction.ActionSequence());

        yield return new WaitForSeconds(2f);
    }

    void EndBattle()
    {
        StopAllCoroutines();

        if (currentState == BattleState.WON)
        {
            // do win stuff
            StartCoroutine(WinRoutine());
        }
        else
        {
            // Go to lose screen
            StartCoroutine(LoseRoutine());
        }
    }

    IEnumerator WinRoutine()
    {
        BattleSystemMenu.instance.SetDialogueText("Players have won the Battle!");
        Debug.Log("The Players won the battle");

        yield return new WaitForSeconds(2f);

        if (reward != null)
            reward.GiveReward();

        yield return new WaitForSeconds(5f);

        // Change scenes
        if (!SceneManager.GetActiveScene().name.Equals("BattleScene_Final"))
        {
            SceneManager.LoadScene("WorldMap");
        }
        else
        {
            SceneManager.LoadScene("Victory");
        }    
    }

    IEnumerator LoseRoutine()
    {
        BattleSystemMenu.instance.SetDialogueText("Players have been defeated");
        Debug.Log("Players have been defeated");

        yield return new WaitForSeconds(5f);

        // change to lose scene
        SceneManager.LoadScene("GameOver");
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

    void SetPlayerActions(AttackAction attack, SpecialAction special)
    {
        currentPlayerAttackAction = attack;
        currentPlayerSpecialAction = special;
    }
}
