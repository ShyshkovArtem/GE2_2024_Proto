using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {  START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{

    public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, new Vector3(-7, 0.8f, 0), Quaternion.Euler(0, 90, 0));
        playerUnit = playerGO.GetComponent<Unit>();


        GameObject enemyGO = Instantiate(enemyPrefab,  new Vector3(7, 0.8f, 0), Quaternion.Euler(0, -90, 0));
        enemyUnit = enemyGO.GetComponent<Unit>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {

    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) return;
        else StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        // damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.unitDamage);

        enemyHUD.SetHPnMana(enemyUnit.unitCurrentHP, enemyUnit.unitCurrentMana);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn() );
        }
        yield return new WaitForSeconds(2f);

        // check if the enemy is dead -> change state 
    }

    IEnumerator EnemyTurn ()
    {


        yield return new WaitForSeconds(2f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.unitDamage);

        playerHUD.SetHPnMana(playerUnit.unitCurrentHP, playerUnit.unitCurrentMana);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    void EndBattle() 
    { 
        if (state == BattleState.WON)
        {
            // you win
        } else if (state == BattleState.LOST)
        {
            //you lost
        }
    }


}
