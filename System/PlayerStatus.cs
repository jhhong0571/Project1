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
        public float speed = 30; //�ӵ� - ���� ���� ���� ������
        public int actionPoints = 100; // �ൿ ������
        public float defense = 0.1f; // ���� 10%
        public float criticalChance = 0.1f; // ġ��Ÿ Ȯ�� 10%
        public float criticalDamageMultiplier = 2.0f; // ġ��Ÿ ���ط�

        public BarsFillAnimations barsFillAnimations; //ü�¹� �ִϸ��̼�

        void Start()
        {
            barsFillAnimations = FindObjectOfType<BarsFillAnimations>();
        }

        void Update()
        {
            barsFillAnimations.UpdateHealthAnimation(health, maxHealth);
            UpdateHealthAnimation();

            // �׽�Ʈ��: G Ű�� ���� �� ü�� ����
            if (Input.GetKeyDown(KeyCode.G))
            {
                TakeDamage(80);
            }
            //�׽�Ʈ�� : H������ ȸ��
            if (Input.GetKeyDown(KeyCode.H))
            {
                Heal(30);
            }
        }

        public void Heal(int heal)
        {
            //ȸ���Ǵ� �� �˾Ƽ�
            //����
            int Healhealth = heal;
            health += Healhealth;
        }

        // �÷��̾ ���ظ� �޴� �޼���
        public void TakeDamage(int damage)
        {
            // ������ ����Ͽ� ���� ���
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
            Debug.Log("����.");
        }
    }
}
