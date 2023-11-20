using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS : MonoBehaviour
{
    public static PS instance;

    public float InitialCriticalChance = 0.2f;
    public float InitialCriticalDamage = 1.5f;
   

    public float CriticalChance { get; private set; }
    public float CriticalDamage { get; private set; }

    public int InitialDamage = 40;
    public int damage { get; private set; }


    
    public int maxHealth = 100;
    public int health = 100;

    public int speed = 30; //속도 - 선제 공격 순위 결정용

    public float InitialDefense = 0.1f;
    public float defense { get; private set; }

    public BarsFillAnimations barsFillAnimations;

    public void Awake()
    {  //값 초기화
        CriticalChance = InitialCriticalChance;
        CriticalDamage = InitialCriticalDamage;
        damage = InitialDamage;
        defense = InitialDefense;

    }

    public bool TakeDamage(int damage)
    {
        // 방어력을 고려한 최종 데미지 계산
        int finalDamage = Mathf.Max(damage - Armour(), 0);
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


    public bool CheckCritical()
    {
        return Random.value < CriticalChance;
    }

    public int CalculateCriticalDamage(int baseDamage)
    {
        return Mathf.FloorToInt(baseDamage * CriticalDamage);
    }

    public int PistolDamage()
    {
        float pistolMultiplier = 0.75f;
        int baseDamage = Mathf.FloorToInt(damage * pistolMultiplier);

        // 치명타 확률을 30%
        float increaseAmount = 0.3f;
        IncreaseCriticalChance(increaseAmount);

        // 치명타 여부 확인
        if (Random.value < CriticalChance)
        {
            // 치명타 데미지 계산 및 반환
            return CalculateCriticalDamage(baseDamage);
        }

        return baseDamage;
    }


    public int RifleDamage()
    {
        float rifleMultiplier = 0.85f;
        int baseDamage = Mathf.FloorToInt(damage * rifleMultiplier);

        float increaseAmount = 0.15f;
        IncreaseCriticalChance(increaseAmount);
        // 치명타 여부 확인
        if (Random.value < CriticalChance)
        {
            // 치명타 데미지 계산 및 반환
            return CalculateCriticalDamage(baseDamage);
        }

        return baseDamage;
    }

    public int ShotgunDamage()
    {
        float shotgunMultiplier = 1.6f;
        int baseDamage = Mathf.FloorToInt(damage * shotgunMultiplier);

        float decreaseAmount = 0.05f;
        DecreaseCriticalChance(decreaseAmount);

        // 치명타 여부 확인
        if (Random.value < CriticalChance)
        {
            // 치명타 데미지 계산 및 반환
            return CalculateCriticalDamage(baseDamage);
        }

        return baseDamage;
    }


    public int DaggerDamage()
    {
        float daggerMultiplier = 0.6f;
        int baseDamage = Mathf.FloorToInt(damage * daggerMultiplier);

        // 항상 치명타 데미지 계산 및 반환
        return CalculateCriticalDamage(baseDamage);
    }

    public int AxeDamage()
    {
        float axeMultiplier = 2.5f;
        int baseDamage = Mathf.FloorToInt(damage * axeMultiplier);

        // 치명타 확률을 10% 감소
        float decreaseAmount = 0.1f;
        DecreaseCriticalChance(decreaseAmount);

        // 치명타 여부 확인
        if (Random.value < CriticalChance)
        {
            // 치명타 데미지 계산 및 반환
            return CalculateCriticalDamage(baseDamage);
        }

        return baseDamage;
    }

    public int SwordDamage()
    {
        float swordMultiplier = 1.3f;
        int baseDamage = Mathf.FloorToInt(damage * swordMultiplier);

        //치명타 확률을 15%
        float increaseAmount = 0.15f;
        IncreaseCriticalChance(increaseAmount);

        // 치명타 여부 확인
        if (Random.value < CriticalChance)
        {
            // 치명타 데미지 계산 및 반환
            return CalculateCriticalDamage(baseDamage);
        }

        return baseDamage;
    }

    //치명타 확률 감소
    public void DecreaseCriticalChance(float decreaseAmount)
    {
        CriticalChance = Mathf.Max(0f, CriticalChance - decreaseAmount);
    }

    //치명타 확률 증가
    public void IncreaseCriticalChance(float increaseAmount)
    {
        CriticalChance = Mathf.Min(1f, CriticalChance + increaseAmount);
    }

    public int Armour()
    {
        return Mathf.FloorToInt(defense * maxHealth);
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
