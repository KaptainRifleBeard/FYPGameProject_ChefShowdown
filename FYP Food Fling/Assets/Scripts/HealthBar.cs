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
    public Image healthbar6;
    public Image healthbar7;
    public Image healthbar8;



    public Image Enemyhealthbar1;
    public Image Enemyhealthbar2;
    public Image Enemyhealthbar3;
    public Image Enemyhealthbar4;
    public Image Enemyhealthbar5;
    public Image Enemyhealthbar6;
    public Image Enemyhealthbar7;
    public Image Enemyhealthbar8;

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
        if (currenthealth == 8)
        {
            
        }
        else if (currenthealth == 7)
        {
            healthbar8.color = Color.white;
        }
        else if (currenthealth == 6)
        {
            healthbar8.color = Color.white;
            healthbar7.color = Color.white;
        }
        else if (currenthealth==5)
        {
            healthbar8.color = Color.white;
            healthbar7.color = Color.white;
            healthbar6.color = Color.white;
        }
        else if (currenthealth == 4)
        {
            healthbar8.color = Color.white;
            healthbar7.color = Color.white;
            healthbar6.color = Color.white;
            healthbar5.color = Color.white;
        }
        else if (currenthealth == 3)
        {
            healthbar8.color = Color.white;
            healthbar7.color = Color.white;
            healthbar6.color = Color.white;
            healthbar5.color = Color.white;
            healthbar4.color = Color.white;
        }
        else if (currenthealth == 2)
        {
            healthbar8.color = Color.white;
            healthbar7.color = Color.white;
            healthbar6.color = Color.white;
            healthbar5.color = Color.white;
            healthbar4.color = Color.white;
            healthbar3.color = Color.white;
        }
        else if (currenthealth == 1)
        {
            healthbar8.color = Color.white;
            healthbar7.color = Color.white;
            healthbar6.color = Color.white;
            healthbar5.color = Color.white;
            healthbar4.color = Color.white;
            healthbar3.color = Color.white;
            healthbar2.color = Color.white;
        }
        else if (currenthealth == 0)
        {
            healthbar8.color = Color.white;
            healthbar7.color = Color.white;
            healthbar6.color = Color.white;
            healthbar5.color = Color.white;
            healthbar4.color = Color.white;
            healthbar3.color = Color.white;
            healthbar2.color = Color.white;
            healthbar1.color = Color.white;
        }


        if (enemyCurrentHealth == 8)
        {

        }
        else if (enemyCurrentHealth==7)
        {
            Enemyhealthbar8.color = Color.white;

        }
        else if (enemyCurrentHealth == 6)
        {
            Enemyhealthbar8.color = Color.white;
            Enemyhealthbar7.color = Color.white;

        }
        else if (enemyCurrentHealth == 5)
        {
            Enemyhealthbar8.color = Color.white;
            Enemyhealthbar7.color = Color.white;
            Enemyhealthbar6.color = Color.white;
        }
        else if (enemyCurrentHealth == 4)
        {
            Enemyhealthbar8.color = Color.white;
            Enemyhealthbar7.color = Color.white;
            Enemyhealthbar6.color = Color.white;
            Enemyhealthbar8.color = Color.white;
            Enemyhealthbar7.color = Color.white;
            Enemyhealthbar5.color = Color.white;
        }
        else if (enemyCurrentHealth == 3)
        {
            Enemyhealthbar8.color = Color.white;
            Enemyhealthbar7.color = Color.white;
            Enemyhealthbar6.color = Color.white;
            Enemyhealthbar8.color = Color.white;
            Enemyhealthbar7.color = Color.white;
            Enemyhealthbar5.color = Color.white;
            Enemyhealthbar4.color = Color.white;
        }
        else if (enemyCurrentHealth == 2)
        {
            Enemyhealthbar8.color = Color.white;
            Enemyhealthbar7.color = Color.white;
            Enemyhealthbar6.color = Color.white;
            Enemyhealthbar8.color = Color.white;
            Enemyhealthbar7.color = Color.white;
            Enemyhealthbar5.color = Color.white;
            Enemyhealthbar4.color = Color.white;
            Enemyhealthbar3.color = Color.white;
        }
        else if (enemyCurrentHealth == 1)
        {
            Enemyhealthbar8.color = Color.white;
            Enemyhealthbar7.color = Color.white;
            Enemyhealthbar6.color = Color.white;
            Enemyhealthbar8.color = Color.white;
            Enemyhealthbar7.color = Color.white;
            Enemyhealthbar5.color = Color.white;
            Enemyhealthbar4.color = Color.white;
            Enemyhealthbar3.color = Color.white;
            Enemyhealthbar2.color = Color.white;
        }
        else if (enemyCurrentHealth == 0)
        {
            Enemyhealthbar8.color = Color.white;
            Enemyhealthbar7.color = Color.white;
            Enemyhealthbar6.color = Color.white;
            Enemyhealthbar8.color = Color.white;
            Enemyhealthbar7.color = Color.white;
            Enemyhealthbar5.color = Color.white;
            Enemyhealthbar4.color = Color.white;
            Enemyhealthbar3.color = Color.white;
            Enemyhealthbar2.color = Color.white;
            Enemyhealthbar1.color = Color.white;
        }
    }

}
