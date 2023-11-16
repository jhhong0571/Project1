using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES : MonoBehaviour
{
    public static ES instance;

    
    public int Ehealth = 100;
    public int EmaxHealth = 100;

    public int speed = 20; //속도 - 선제 공격 순위 결정용
    public int actionPoints = 0; // 초기값을 0으로 설정
    public int APgen = 10; // 턴마다 회복되는 행동게이지 값

    public float defense = 0.1f; // 방어력 10%

    public float CriticalChance = 0.2f;
    public float CriticalDamage = 2f;
    public int damage = 20;
    public void Test()
    {
        Debug.Log("테스트");
    }

    public bool TakeDamage(int damage)
    {
        // 치명타 여부 결정
        bool isCritical = Random.value < CriticalChance;
        if(isCritical)
        {
            Debug.Log("적이 치명타!");
        }
        // 기본 데미지 또는 치명타 데미지 적용
        int calculatedDamage = isCritical ? Mathf.FloorToInt(damage * CriticalDamage) : damage;

        int finalDamage = Mathf.Max(calculatedDamage - Mathf.FloorToInt(defense * EmaxHealth), 0);
        Ehealth -= finalDamage;

        if (Ehealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public int GetSpeed()
    {
        return speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
