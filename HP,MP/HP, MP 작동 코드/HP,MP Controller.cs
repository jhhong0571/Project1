using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp_mp_InterFace : MonoBehaviour
{
    public Slider HpBarSlider, MpBarSlider;
    public float curHp; // ���� ü��
    public float curMp; // ���� ����
    public float maxHp; // �ִ� ü��
    public float maxMp; // �ִ� ����
    public Text hp_text;
    public Text mp_text;
    public float damage; // ������
    public float used_skill; //��ų ��� �� ���ҵ� MP
    public float recovery; // ȸ��
    public float used_Mpportion; // MP ���� ���

    public void SetHp(float Hp_amount) // �ʱ� Hp����
    {
        maxHp = Hp_amount;
        curHp = maxHp;
    }

    public void SetMp(float Mp_amount) // �ʱ� Mp����
    {
        maxMp = Mp_amount;
        curMp = maxMp;
    }

    public void SetMaxHp(float Hp_amount) // �ִ� Hp����
    {
        maxHp += Hp_amount;
    }

    public void SetMaxMp(float Mp_amount) // �ִ� Mp����
    {
        maxMp += Mp_amount;
    }

    public void CheckHp() //*HP ����
    {
        if (HpBarSlider != null)
            HpBarSlider.value = curHp;
    }

    public void CheckMp() //*MP ����
    {
        if (MpBarSlider != null)
            MpBarSlider.value = curMp;
    }

    public void Damage(float damage) //* ������ �޴� �Լ�
    {
        if (maxHp == 0 || curHp <= 0) //* �̹� ü�� 0���ϸ� �н�
            return;
        curHp -= damage;
        CheckHp(); //* ü�� ����
        if (curHp <= 0)
        {
            //* ü���� 0 ���϶� ����
            Debug.Log("Player Dead");
        }
    }

    public void Use(float used_skill) //* ���� ���� �Լ�
    {
        if (maxMp == 0 || curMp <= 0 || curHp <= 0) //* �̹� ���� 0���ϸ� �н�
            return;
        curMp -= used_skill;
        CheckMp(); //* ���� ����
        if (curMp <= 0)
        {
            //* ������ 0 ���϶� ��ųX
            Debug.Log("Not enought MP");
        }
    }

    public void Recovery_HP(float recovery_hp) //* ü�� ȸ�� �Լ�
    {
        if (maxHp == 0 || curHp <= 0) //* ü���� 0�̸� �н�
            return;
        curHp += recovery_hp;
        CheckHp(); //* ���� ����
        if (curHp >= maxMp)
        {
            curHp = maxHp;
            //* �̹� �ִ� ü���̶� ȸ��X
            Debug.Log("Already Full HP");
        }
    }

    public void Recovery_MP(float recovery_mp) //* ���� ȸ�� �Լ�
    {
        if (maxMp == 0 || curHp <= 0) //* �ִ� ������ 0�̸� �н�
            return;
        curMp += recovery_mp;
        CheckMp(); //* ���� ����
        if (curMp >= maxMp)
        {
            curMp = maxMp;
            //* �̹� �ִ� �����̶� ȸ��X
            Debug.Log("Already Full MP");
        }
    }

    void Start()
    {
        HpBarSlider = GameObject.Find("HP").GetComponent<Slider>();
        MpBarSlider = GameObject.Find("MP").GetComponent<Slider>();
        HpBarSlider.minValue = 0;
        MpBarSlider.minValue = 0;
        HpBarSlider.maxValue = 1000;
        MpBarSlider.maxValue = 1000;
        SetHp(HpBarSlider.maxValue);
        SetMp(MpBarSlider.maxValue);
        CheckHp();
        CheckMp();



    }

    void Update()
    {
        hp_text.text = (HpBarSlider.value.ToString() + "/" + HpBarSlider.maxValue.ToString());
        mp_text.text = (MpBarSlider.value.ToString() + "/" + MpBarSlider.maxValue.ToString());
        //CheckHp();
        //CheckMp();
        if(Input.GetKeyDown(KeyCode.A))
        {
            Damage(damage);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Use(used_skill);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Recovery_HP(recovery);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Recovery_MP(used_Mpportion);
        }
    }
}
