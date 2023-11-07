using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp_mp_InterFace : MonoBehaviour
{
    public Slider HpBarSlider, MpBarSlider;
    public float curHp; // 현재 체력
    public float curMp; // 현재 마력
    public float maxHp; // 최대 체력
    public float maxMp; // 최대 마력
    public Text hp_text;
    public Text mp_text;
    public float damage; // 데미지
    public float used_skill; //스킬 사용 시 감소된 MP
    public float recovery; // 회복
    public float used_Mpportion; // MP 포션 사용

    public void SetHp(float Hp_amount) // Hp설정
    {
        maxHp = Hp_amount;
        curHp = maxHp;
    }

    public void SetMp(float Mp_amount) // Mp설정
    {
        maxMp = Mp_amount;
        curMp = maxMp;
    }

    public void CheckHp() //*HP 갱신
    {
        if (HpBarSlider != null)
            HpBarSlider.value = curHp;
    }

    public void CheckMp() //*MP 갱신
    {
        if (MpBarSlider != null)
            MpBarSlider.value = curMp;
    }

    public void Damage(float damage) //* 데미지 받는 함수
    {
        if (maxHp == 0 || curHp <= 0) //* 이미 체력 0이하면 패스
            return;
        curHp -= damage;
        CheckHp(); //* 체력 갱신
        if (curHp <= 0)
        {
            //* 체력이 0 이하라 죽음
        }
    }

    public void Use(float use) //* 마력 쓰는 함수
    {
        if (maxMp == 0 || curMp <= 0) //* 이미 마력 0이하면 패스
            return;
        curMp -= use;
        CheckMp(); //* 마력 갱신
        if (curMp <= 0)
        {
            //* 마력이 0 이하라 스킬X
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
        CheckHp();
        CheckMp();
    }
}
