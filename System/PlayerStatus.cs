using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS : MonoBehaviour
{
    public static PS instance;

    public float CriticalChance = 0.2f;
    public float CriticalDamage = 2f;
    public int damage = 20;

    public int health = 100;
    public int maxHealth = 100;

    public int speed = 30; //속도 - 선제 공격 순위 결정용
    public int actionPoints = 0; // 초기값을 0으로 설정
    public int APgen = 10; // 턴마다 회복되는 행동게이지 값

    public float defense = 0.1f; // 방어력 10%

    public BarsFillAnimations barsFillAnimations;

    public void Awake()
    {

    }

    public bool TakeDamage(int damage)
    {
        // 치명타 여부 결정
        bool isCritical = Random.value < CriticalChance;
        if(isCritical)
        {
            Debug.Log("플레이어 치명타!");
        }

        // 기본 데미지 또는 치명타 데미지 적용
        int calculatedDamage = isCritical ? Mathf.FloorToInt(damage * CriticalDamage) : damage;

        int finalDamage = Mathf.Max(calculatedDamage - Mathf.FloorToInt(defense * maxHealth), 0);
        health -= finalDamage;

        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal()
    {
        int Heal = (int)(maxHealth * 0.2f);
        health += Heal;
        Debug.Log("회복하였다!");
    }

    public int GetSpeed()
    {
        return speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        barsFillAnimations = FindObjectOfType<BarsFillAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        barsFillAnimations.UpdateHealthAnimation(health, maxHealth);
        UpdateHealthAnimation();
    }

    private void UpdateHealthAnimation()
    {
        barsFillAnimations.UpdateHealthAnimation(health, maxHealth);
    }

}

