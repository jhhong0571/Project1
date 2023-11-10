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

        // BarsFillAnimations Ŭ������ �ν��Ͻ�
        public BarsFillAnimations barsFillAnimations;

        void start()
        {
            barsFillAnimations = FindObjectOfType<BarsFillAnimations>();
        }

        void Update()
        {
            // �÷��̾��� ü�°� �ִ� ü���� BarsFillAnimations Ŭ������ �����Ͽ� ������Ʈ
            barsFillAnimations.UpdateHealthAnimation(health, maxHealth);
            UpdateHealthAnimation();

            // �׽�Ʈ��: G Ű�� ���� �� ü�� ����
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
            //ȸ���Ǵ� �� �˾Ƽ�
            //����
            int Healhealth = 30;
            health += Healhealth;
        }

        // �÷��̾ ���ظ� �޴� �޼���
        public void TakeDamage(int damage)
        {
            // ������ ����Ͽ� ���� ���
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
        // ��� ó�� �޼���
        private void Die()
        {
            // ��� ó�� ���� �߰�
            Debug.Log("Player has died.");
        }
    }
}
