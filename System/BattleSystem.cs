using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, ENEMYTURN2, ENEMYTURN3, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public string scene;
    public TextMeshProUGUI APText;
    public TextMeshProUGUI APText2;
    public TextMeshProUGUI HText;
    public TextMeshProUGUI UIText;
    public TextMeshProUGUI UIText2;
    public TextMeshProUGUI UIText3;

    public int actionPoints = 20; // 초기값을 10으로 설정
    public int AP = 2; //ActionPoints 소모값 용 변수

    public int pistolAPCost = 2;
    public int rifleAPCost = 3;
    public int DaggerAPCost = 1;
    public int AxeAPCost = 5;
    public int SwordAPCost = 3;
    public int ShotgunAPCost = 4;

    public GameObject player;
    public GameObject enemy;
    public GameObject turnUI;
    public GameObject battleUI;
    public GameObject Cam;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    //소리
    private SoundManager soundManager;

    CameraShake cam;
    BattleUI BU;
    ButtonController BC;
    PS playerUnit;
    ES enemyUnit;

    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.LoadPlayerData();

        state = BattleState.START;
        BC = turnUI.GetComponent<ButtonController>();
        BU = battleUI.GetComponent<BattleUI>();
        cam = Cam.GetComponent<CameraShake>();
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(player, playerBattleStation);
        playerUnit = playerGO.GetComponent<PS>();

        GameObject enemyGO = Instantiate(enemy, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<ES>();

        yield return new WaitForSeconds(0.5f);


        state = BattleState.PLAYERTURN;
        PlayerTurn();

        
    }
    void Update()
    {
        CCUI();
        ActionPoint();
        Health();
    }

    //일반공격
    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        BU.SetAttack(true);
        BU.SetEnemyHit(true);
        BU.SetBlood(true);
        DamageUI(playerUnit.damage);

        actionPoints += pistolAPCost;
        Debug.Log("플레이어가 공격함");
        yield return new WaitForSeconds(0.5f);
        BU.SetAttack(false);
        BU.SetBlood(false);
        BU.SetEnemyHit(false);
        // 턴 종료 UI를 활성화하고 1초 뒤에 비활성화

        BC.SetActive(true);
        yield return new WaitForSeconds(1f);
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
        yield return new WaitForSeconds(0.5f);
        actionPoints += AxeAPCost;

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        Debug.Log("적이 공격하였다.");

        DefendUI(enemyUnit.damage);
        BU.SetEnemy(true);
        BU.SetPBlood(true);
        // 유저 체력 UI 표시
        yield return new WaitForSeconds(0.5f);
        BU.SetEnemy(false);
        BU.SetPBlood(false);
        // 턴 종료 UI를 활성화하고 1초 뒤에 비활성화
        BC.SetActive(true);
        yield return new WaitForSeconds(0.5f);
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
            //state = BattleState.ENEMYTURN2;
            PlayerTurn();
        }
    }
    /*
    IEnumerator EnemyTurn2()
    {
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
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
            state = BattleState.ENEMYTURN3;
            PlayerTurn();
        }
    }

    IEnumerator EnemyTurn3()
    {
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
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
    */

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            Debug.Log("전투 승리");
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.SavePlayerData();

            FightManager fightManager = FindObjectOfType<FightManager>();
            SceneManager.LoadScene(scene);
            fightManager.onBattle = 1;
        }
        else if(state == BattleState.LOST)
        {
            SceneManager.LoadScene("Dead");
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

        if (state == BattleState.PLAYERTURN) {

            StartCoroutine(PlayerAttack());
        }
            
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        if (state == BattleState.PLAYERTURN)
        {
            StartCoroutine(PlayerHeal());
        }
    }

    public void OnPistolButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        if (state == BattleState.PLAYERTURN)
        {
            if (actionPoints >= pistolAPCost)
            {
                StartCoroutine(Pistol());
            }
            else
            {
                BU.SetWaring(true);
                //yield return new WaitForSeconds(1f);
                BU.SetWaring(false);
            }
        }
    }

    public void OnRifleButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        if (state == BattleState.PLAYERTURN)
        {
            if (actionPoints >= rifleAPCost)
            {
                StartCoroutine(Rifle());
            }
            else
            {
                BU.SetWaring(true);
                //yield return new WaitForSeconds(1f);
                BU.SetWaring(false);
            }
        }
    }

    public void OnDaggerButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        if (state == BattleState.PLAYERTURN)
        {
            if (actionPoints >= DaggerAPCost)
            {
                StartCoroutine(Dagger());
            }
            else
            {
                BU.SetWaring(true);
                // yield return new WaitForSeconds(1f);
                BU.SetWaring(false);
            }
        }
    }

    public void OnAxeButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        if (state == BattleState.PLAYERTURN)
        {
            if (actionPoints >= AxeAPCost)
            {
                StartCoroutine(Axe());
            }
            else
            {
                BU.SetWaring(true);
                //yield return new WaitForSeconds(1f);
                BU.SetWaring(false);
            }
        }
    }

    public void OnShotgunButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        if (state == BattleState.PLAYERTURN)
        {
            if (actionPoints >= ShotgunAPCost)
            {
                StartCoroutine(Shotgun());
            }
            else
            {
                BU.SetWaring(true);
                // yield return new WaitForSeconds(1f);
                BU.SetWaring(false);
            }
        }
    }

    public void OnSwordButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        if (state == BattleState.PLAYERTURN)
        {
            if (actionPoints >= SwordAPCost)
            {
                StartCoroutine(Sword());
            }
            else
            {
                BU.SetWaring(true);
                //  yield return new WaitForSeconds(1f);
                BU.SetWaring(false);
            }
        }
    }


    IEnumerator Pistol()
    {
        soundManager.PlayPistolSound();
        int pistolDamage = playerUnit.PistolDamage();
        bool isDead = enemyUnit.TakeDamage(pistolDamage);
        actionPoints -= pistolAPCost;

        DamageUI(pistolDamage);
        Debug.Log("플레이어가 권총 공격함");
        BU.SetPistol(true);
        BU.SetBullet(true);
        BU.SetEnemyHit(true);
        yield return new WaitForSeconds(0.5f);
        // 턴 종료 UI를 활성화하고 1초 뒤에 비활성화
        BU.SetPistol(false);
        BU.SetBullet(false);
        BU.SetEnemyHit(false);
        BC.SetActive(true);
        yield return new WaitForSeconds(0.5f);
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

    IEnumerator Rifle()
    {
        int rifleDamage = playerUnit.RifleDamage();
        bool isDead = enemyUnit.TakeDamage(rifleDamage);
        actionPoints -= rifleAPCost;

        DamageUI(rifleDamage);
        BU.SetRifle(true);
        BU.SetBullet(true);
        BU.SetEnemyHit(true);
        Debug.Log("플레이어가 소총으로 공격함");
        yield return new WaitForSeconds(0.5f);
        // 턴 종료 UI를 활성화하고 1초 뒤에 비활성화
        BU.SetRifle(false);
        BU.SetBullet(false);
        BU.SetEnemyHit(false);
        BC.SetActive(true);
        yield return new WaitForSeconds(0.5f);
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

    IEnumerator Sword()
    {
        int swordDamage = playerUnit.SwordDamage();
        bool isDead = enemyUnit.TakeDamage(swordDamage);

        DamageUI(swordDamage);
        BU.SetSword(true);
        BU.SetBlood(true);
        BU.SetEnemyHit(true);
        actionPoints -= SwordAPCost;

        Debug.Log("플레이어가 검으로 공격함");
        yield return new WaitForSeconds(0.5f);
        BU.SetSword(false);
        BU.SetBlood(false);
        BU.SetEnemyHit(false);
        BC.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        BC.SetActive(false);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator Dagger()
    {
        int daggerDamage = playerUnit.DaggerDamage();
        bool isDead = enemyUnit.TakeDamage(daggerDamage);
        actionPoints -= DaggerAPCost;

        DamageUI(daggerDamage);
        BU.SetDagger(true);
        BU.SetBlood(true);
        BU.SetEnemyHit(true);
        Debug.Log("플레이어가 단검으로 공격함");
        yield return new WaitForSeconds(0.5f);
        BU.SetDagger(false);
        BU.SetBlood(false);
        BU.SetEnemyHit(false);
        BC.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        BC.SetActive(false);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator Axe()
    {
        int axeDamage = playerUnit.AxeDamage();
        bool isDead = enemyUnit.TakeDamage(axeDamage);
        actionPoints -= AxeAPCost;

        DamageUI(axeDamage);
        BU.SetAxe(true);
        BU.SetBlood(true);
        BU.SetEnemyHit(true);
        cam.TriggerShake(0.5f);
        Debug.Log("플레이어가 도끼로 공격함");
        yield return new WaitForSeconds(0.5f);
        BU.SetAxe(false);
        BU.SetBlood(false);
        BU.SetEnemyHit(false);
        BC.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        BC.SetActive(false);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator Shotgun()
    {

        int shotgunDamage = playerUnit.ShotgunDamage();
        bool isDead = enemyUnit.TakeDamage(shotgunDamage);
        actionPoints -= ShotgunAPCost;

        DamageUI(shotgunDamage);
        BU.SetShotgun(true);
        BU.SetBullet(true);
        BU.SetEnemyHit(true);
        Debug.Log("플레이어가 샷건으로 공격함");
        yield return new WaitForSeconds(0.5f);
        BU.SetShotgun(false);
        BU.SetBullet(false);
        BU.SetEnemyHit(false);
        BC.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        BC.SetActive(false);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }


    void DamageUI(int damageValue)
    {
        UIText.text = damageValue.ToString();
        StartCoroutine(ResetAndHideUIText(UIText));
    }

    void DefendUI(int damageValue)
    {
        UIText2.text = damageValue.ToString();
        StartCoroutine(ResetAndHideUIText(UIText2));
    }

    IEnumerator ResetAndHideUIText(TextMeshProUGUI textComponent)
    {
        yield return new WaitForSeconds(1f);
        textComponent.text = "";
        textComponent.gameObject.SetActive(false);

        textComponent.text = "";
        textComponent.gameObject.SetActive(true);
    }

    void CCUI()
    {
        UIText3.text = "HP:" + enemyUnit.health;
    }

    void ActionPoint()
    {
        APText.text = "AP : " + actionPoints.ToString();
    }

    void Health()
    {
        HText.text = (playerUnit.health).ToString();
    }
}
