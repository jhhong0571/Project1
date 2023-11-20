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

    public int speed = 30; //�ӵ� - ���� ���� ���� ������

    public float InitialDefense = 0.1f;
    public float defense { get; private set; }

    public BarsFillAnimations barsFillAnimations;

    public void Awake()
    {  //�� �ʱ�ȭ
        CriticalChance = InitialCriticalChance;
        CriticalDamage = InitialCriticalDamage;
        damage = InitialDamage;
        defense = InitialDefense;

    }

    public bool TakeDamage(int damage)
    {
        // ������ ����� ���� ������ ���
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
        Debug.Log("ȸ���Ͽ���!");
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

        // ġ��Ÿ Ȯ���� 30%
        float increaseAmount = 0.3f;
        IncreaseCriticalChance(increaseAmount);

        // ġ��Ÿ ���� Ȯ��
        if (Random.value < CriticalChance)
        {
            // ġ��Ÿ ������ ��� �� ��ȯ
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
        // ġ��Ÿ ���� Ȯ��
        if (Random.value < CriticalChance)
        {
            // ġ��Ÿ ������ ��� �� ��ȯ
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

        // ġ��Ÿ ���� Ȯ��
        if (Random.value < CriticalChance)
        {
            // ġ��Ÿ ������ ��� �� ��ȯ
            return CalculateCriticalDamage(baseDamage);
        }

        return baseDamage;
    }


    public int DaggerDamage()
    {
        float daggerMultiplier = 0.6f;
        int baseDamage = Mathf.FloorToInt(damage * daggerMultiplier);

        // �׻� ġ��Ÿ ������ ��� �� ��ȯ
        return CalculateCriticalDamage(baseDamage);
    }

    public int AxeDamage()
    {
        float axeMultiplier = 2.5f;
        int baseDamage = Mathf.FloorToInt(damage * axeMultiplier);

        // ġ��Ÿ Ȯ���� 10% ����
        float decreaseAmount = 0.1f;
        DecreaseCriticalChance(decreaseAmount);

        // ġ��Ÿ ���� Ȯ��
        if (Random.value < CriticalChance)
        {
            // ġ��Ÿ ������ ��� �� ��ȯ
            return CalculateCriticalDamage(baseDamage);
        }

        return baseDamage;
    }

    public int SwordDamage()
    {
        float swordMultiplier = 1.3f;
        int baseDamage = Mathf.FloorToInt(damage * swordMultiplier);

        //ġ��Ÿ Ȯ���� 15%
        float increaseAmount = 0.15f;
        IncreaseCriticalChance(increaseAmount);

        // ġ��Ÿ ���� Ȯ��
        if (Random.value < CriticalChance)
        {
            // ġ��Ÿ ������ ��� �� ��ȯ
            return CalculateCriticalDamage(baseDamage);
        }

        return baseDamage;
    }

    //ġ��Ÿ Ȯ�� ����
    public void DecreaseCriticalChance(float decreaseAmount)
    {
        CriticalChance = Mathf.Max(0f, CriticalChance - decreaseAmount);
    }

    //ġ��Ÿ Ȯ�� ����
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
