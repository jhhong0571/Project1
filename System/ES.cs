using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES : MonoBehaviour
{
    public static ES instance;

    
    public int Ehealth = 100;
    public int EmaxHealth = 100;

    public int speed = 20; //�ӵ� - ���� ���� ���� ������
    public int actionPoints = 0; // �ʱⰪ�� 0���� ����
    public int APgen = 10; // �ϸ��� ȸ���Ǵ� �ൿ������ ��

    public float defense = 0.1f; // ���� 10%

    public float CriticalChance = 0.2f;
    public float CriticalDamage = 2f;
    public int damage = 20;
    public void Test()
    {
        Debug.Log("�׽�Ʈ");
    }

    public bool TakeDamage(int damage)
    {
        // ġ��Ÿ ���� ����
        bool isCritical = Random.value < CriticalChance;
        if(isCritical)
        {
            Debug.Log("���� ġ��Ÿ!");
        }
        // �⺻ ������ �Ǵ� ġ��Ÿ ������ ����
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
