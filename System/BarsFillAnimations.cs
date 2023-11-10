using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerUI
{
    public class BarsFillAnimations : MonoBehaviour
    {
        Slider[] sliders;
        float[] fillTimeInSeconds;
        float[] durations = { 0.75f, 1.0f, 1.2f, 1.5f, 2.0f, 3.0f };

        void Start()
        {
            sliders = FindObjectsOfType<Slider>();
            fillTimeInSeconds = new float[sliders.Length];

            for (int i = 0; i < sliders.Length; i++)
            {
                float number = durations[Mathf.RoundToInt(Random.Range(0, durations.Length))];
                fillTimeInSeconds[i] = number;
            }
        }

        public void UpdateHealthAnimation(int health, int maxHealth)
        {
            int healthSliderIndex = 0;
            float fillPercentage = (float)health / maxHealth;
            float targetValue = fillTimeInSeconds[healthSliderIndex] * fillPercentage;

            // 직접 슬라이더 값을 업데이트
            sliders[healthSliderIndex].value = targetValue;
        }
    }
}
