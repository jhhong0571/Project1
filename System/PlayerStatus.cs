using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerUI
{
    public class PlayerStatus : MonoBehaviour
    {
        public int health = 100;
        public int maxHealth = 100;
        public float speed = 30; //속도 - 선제 공격 순위 결정용
        public int actionPoints = 100; // 행동 게이지
        public float defense = 0.1f; // 방어력 10%
        public float criticalChance = 0.1f; // 치명타 확률 10%
        public float criticalDamageMultiplier = 2.0f; // 치명타 피해량

        public BarsFillAnimations barsFillAnimations; //체력바 애니메이션

        void Start()
        {
            barsFillAnimations = FindObjectOfType<BarsFillAnimations>();
        }

        void Update()
        {
            barsFillAnimations.UpdateHealthAnimation(health, maxHealth);
            UpdateHealthAnimation();

            // 테스트용: G 키를 누를 때 체력 감소
            if (Input.GetKeyDown(KeyCode.G))
            {
                TakeDamage(80);
            }
            //테스트용 : H누르면 회복
            if (Input.GetKeyDown(KeyCode.H))
            {
                Heal(30);
            }
        }

        public void Heal(int heal)
        {
            //회복되는 값 알아서
            //예시
            int Healhealth = heal;
            health += Healhealth;
        }

        // 플레이어가 피해를 받는 메서드
        public void TakeDamage(int damage)
        {
            // 방어력을 고려하여 피해 계산
            int finalDamage = Mathf.Max(damage - Mathf.FloorToInt(defense * maxHealth), 0);

            health -= finalDamage;

            if (health <= 0)
            {
                Die();
            }
        }

        private void UpdateHealthAnimation()
        {
            barsFillAnimations.UpdateHealthAnimation(health, maxHealth);
        }

        private void Die()
        {
            Debug.Log("죽음.");
        }
    }
}
