using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateBar : MonoBehaviour
{
    public Slider slider;
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
            slider.value += value;
        }
        else
        {
            EnemySlider.value += value;
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
}
