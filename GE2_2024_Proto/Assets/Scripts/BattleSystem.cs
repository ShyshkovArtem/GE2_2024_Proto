using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public enum BattleState {  START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{

    public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    private GameObject playerGO;
    private GameObject enemyGO;

    Unit playerUnit;
    Unit enemyUnit;

    Animator playerAnimator;
    Animator enemyAnimator;


    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    public void SetupBattle()
    {
        playerGO = Instantiate(playerPrefab, new Vector3(-7, 0.8f, 0), Quaternion.Euler(0, 90, 0));
        playerUnit = playerGO.GetComponent<Unit>();
        playerAnimator = playerGO.GetComponent<Animator>();


        enemyGO = Instantiate(enemyPrefab,  new Vector3(7, 0.8f, 0), Quaternion.Euler(0, -90, 0));
        enemyUnit = enemyGO.GetComponent<Unit>();
        enemyAnimator = enemyGO.GetComponent <Animator>();

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
    // Function to move the unit forward
    void MoveUnitForward(GameObject unit, float distance)
    {
        // Move the player forward by the specified distance
        unit.transform.position += unit.transform.forward * distance;
    }

    IEnumerator PlayerAttack()
    {
        //work, but insta teleport, atleast it works now 
        MoveUnitForward(playerGO, 13f);
        yield return new WaitForSeconds(0.4f);

        playerAnimator.SetTrigger("trPunching");

        //wait until end of animation and get back to position
        yield return new WaitForSeconds(2f);
        MoveUnitForward(playerGO, -13f);


        // damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.unitDamage);      


        enemyHUD.SetHPnMana(enemyUnit.unitCurrentHP, enemyUnit.unitCurrentMana);
        yield return new WaitForSeconds(1f);

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
        //wait
        yield return new WaitForSeconds(2f);

        MoveUnitForward(enemyGO, 13f);
        yield return new WaitForSeconds(0.4f);

        enemyAnimator.SetTrigger("trPunching");

        //wait until end of animation and get back to position
        yield return new WaitForSeconds(2f);
        MoveUnitForward(enemyGO, -13f);

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
