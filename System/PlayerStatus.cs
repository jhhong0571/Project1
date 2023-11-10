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

        // BarsFillAnimations 클래스의 인스턴스
        public BarsFillAnimations barsFillAnimations;

        void start()
        {
            barsFillAnimations = FindObjectOfType<BarsFillAnimations>();
        }

        void Update()
        {
            // 플레이어의 체력과 최대 체력을 BarsFillAnimations 클래스에 전달하여 업데이트
            barsFillAnimations.UpdateHealthAnimation(health, maxHealth);
            UpdateHealthAnimation();

            // 테스트용: G 키를 누를 때 체력 감소
            if (Input.GetKeyDown(KeyCode.G))
            {
                TakeDamage(80);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                Heal();
            }


        }
        public void Heal()
        {
            //회복되는 값 알아서
            //예시
            int Healhealth = 30;
            health += Healhealth;
        }

        // 플레이어가 피해를 받는 메서드
        public void TakeDamage(int damage)
        {
            // 방어력을 고려하여 피해 계산
            int finalDamage = Mathf.Max(damage - 0, 0);

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
        // 사망 처리 메서드
        private void Die()
        {
            // 사망 처리 로직 추가
            Debug.Log("Player has died.");
        }
    }
}
