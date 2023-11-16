using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{

    public GameObject player;
    public GameObject enemy;
    public GameObject turnUI;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    ButtonController BC;
    PS playerUnit;
    ES enemyUnit;

    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        BC = turnUI.GetComponent<ButtonController>();
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(player, playerBattleStation);
        playerUnit = playerGO.GetComponent<PS>();

        GameObject enemyGO = Instantiate(enemy, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<ES>();

        yield return new WaitForSeconds(2f);


            state = BattleState.PLAYERTURN;
            PlayerTurn();

        
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = playerUnit.TakeDamage(playerUnit.damage);

        Debug.Log("플레이어가 공격함");
        yield return new WaitForSeconds(3f);
        // 턴 종료 UI를 활성화하고 1초 뒤에 비활성화

        BC.SetActive(true);
        yield return new WaitForSeconds(2f);
        BC.SetActive(false);

        // 적 HP 감소 UI
        yield return new WaitForSeconds(1f);

        // 적이 죽었는지 확인
        if (isDead)
        {
            // 싸움 끝
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            // 적 턴
        }
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal();
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        bool isDead = enemyUnit.TakeDamage(enemyUnit.damage);
        Debug.Log("적이 공격하였다.");

        // 유저 체력 UI 표시
        yield return new WaitForSeconds(3f);

        // 턴 종료 UI를 활성화하고 1초 뒤에 비활성화
        BC.SetActive(true);
        yield return new WaitForSeconds(1f);
        BC.SetActive(false);

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
        if(state == BattleState.WON)
        {
            Debug.Log("전투 승리");
        }
        else if(state == BattleState.LOST)
        {
            Debug.Log("전투 패배");
        }
    }

    void PlayerTurn()
    {
        Debug.Log("플레이어 의 턴");
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }


}
