using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateBar : MonoBehaviour
{
    public Slider slider;
    public float P1Ult, P2Ult;

    public Slider EnemySlider;

    public void SetMaxBar(float value,bool isEnemy)
    {
        if (isEnemy == false)
        {
            slider.maxValue = value;
            slider.value = 0.00f;
        }
        else
        {
            EnemySlider.maxValue = value;
            EnemySlider.value = 0.00f;
        }

    }


    public void SetBar(float value,bool isEnemy)
    {

        if (isEnemy == false)
        {
            P1Ult += value;
        }
        else
        {
            P2Ult += value;
        }
    }
    public void ResetBar(bool isEnemy)
    {
        if (isEnemy == false)
        {

            slider.value = 0.00f;
        }
        else
        {

            EnemySlider.value = 0.00f;
        }

    }

    private void Update()
    {
        if (P1Ult >= 100.00)
        {
            P1Ult = 100;
        }
    }

}
