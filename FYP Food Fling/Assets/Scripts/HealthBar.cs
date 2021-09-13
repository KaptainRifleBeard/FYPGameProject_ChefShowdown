using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthbar;
    public float currenthealth;

    public Sprite bar1, bar2, bar3;
    public void UpdateHealth(float value)
    {
        currenthealth -= value;
    }

    private void Update()
    {
        if (currenthealth == 3)
        {
            healthbar.sprite = bar3;
        }
        else if (currenthealth == 2)
        {
            healthbar.sprite = bar2;
        }
        else if (currenthealth == 1)
        {
            healthbar.sprite = bar1;
        }
    }

}
