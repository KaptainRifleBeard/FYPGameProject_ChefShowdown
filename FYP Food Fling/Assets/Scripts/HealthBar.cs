using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthbar1;
    public Image healthbar2;
    public Image healthbar3;
    public Image healthbar4;
    public Image healthbar5;


    public Image Enemyhealthbar1;
    public Image Enemyhealthbar2;
    public Image Enemyhealthbar3;
    public Image Enemyhealthbar4;
    public Image Enemyhealthbar5;

    public float currenthealth;
    public float enemyCurrentHealth;


    public void UpdateHealth(float value,bool isEnemy)
    {
        if (isEnemy == false)
        {
         currenthealth -= value;
        }
        else if (isEnemy == true)
        {
            enemyCurrentHealth -= value;
        }

    }

    private void Update()
    {
        if (currenthealth==5)
        {

        }
        else if (currenthealth == 4)
        {
            healthbar5.color = Color.white;
        }
        else if (currenthealth == 3)
        {
            healthbar5.color = Color.white;
            healthbar4.color = Color.white;
        }
        else if (currenthealth == 2)
        {
            healthbar5.color = Color.white;
            healthbar4.color = Color.white;
            healthbar3.color = Color.white;
        }
        else if (currenthealth == 1)
        {
            healthbar5.color = Color.white;
            healthbar4.color = Color.white;
            healthbar3.color = Color.white;
            healthbar2.color = Color.white;
        }
        else if (currenthealth == 0)
        {
            healthbar5.color = Color.white;
            healthbar4.color = Color.white;
            healthbar3.color = Color.white;
            healthbar2.color = Color.white;
            healthbar1.color = Color.white;
        }


        if (enemyCurrentHealth == 5)
        {

        }
        else if (enemyCurrentHealth == 4)
        {
            healthbar5.color = Color.white;
        }
        else if (enemyCurrentHealth == 3)
        {
            healthbar5.color = Color.white;
            healthbar4.color = Color.white;
        }
        else if (enemyCurrentHealth == 2)
        {
            healthbar5.color = Color.white;
            healthbar4.color = Color.white;
            healthbar3.color = Color.white;
        }
        else if (enemyCurrentHealth == 1)
        {
            healthbar5.color = Color.white;
            healthbar4.color = Color.white;
            healthbar3.color = Color.white;
            healthbar2.color = Color.white;
        }
        else if (enemyCurrentHealth == 0)
        {
            healthbar5.color = Color.white;
            healthbar4.color = Color.white;
            healthbar3.color = Color.white;
            healthbar2.color = Color.white;
            healthbar1.color = Color.white;
        }
    }

}
