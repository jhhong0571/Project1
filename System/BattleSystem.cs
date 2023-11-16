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

        Debug.Log("�÷��̾ ������");
        yield return new WaitForSeconds(3f);
        // �� ���� UI�� Ȱ��ȭ�ϰ� 1�� �ڿ� ��Ȱ��ȭ

        BC.SetActive(true);
        yield return new WaitForSeconds(2f);
        BC.SetActive(false);

        // �� HP ���� UI
        yield return new WaitForSeconds(1f);

        // ���� �׾����� Ȯ��
        if (isDead)
        {
            // �ο� ��
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            // �� ��
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
        Debug.Log("���� �����Ͽ���.");

        // ���� ü�� UI ǥ��
        yield return new WaitForSeconds(3f);

        // �� ���� UI�� Ȱ��ȭ�ϰ� 1�� �ڿ� ��Ȱ��ȭ
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
            Debug.Log("���� �¸�");
        }
        else if(state == BattleState.LOST)
        {
            Debug.Log("���� �й�");
        }
    }

    void PlayerTurn()
    {
        Debug.Log("�÷��̾� �� ��");
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
