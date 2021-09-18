using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxBar(float value)
    {
        slider.maxValue = value;
        slider.value = 0.00f;
    }


    public void SetBar(float value)
    {
        slider.value += value;
    }
}
