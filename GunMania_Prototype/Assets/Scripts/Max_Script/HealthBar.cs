using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthbarhalf;
    public Image healthbar1;
    public Image healthbar1half;
    public Image healthbar2;
    public Image healthbar2half;
    public Image healthbar3;
    public Image healthbar3half;
    public Image healthbar4;
    public Image healthbar4half;
    public Image healthbar5;
    public Image healthbar5half;
    public Image healthbar6;
    public Image healthbar6half;
    public Image healthbar7;
    public Image healthbar7half;
    public Image healthbar8;


    public Image EnemyHealthbarhalf;
    public Image Enemyhealthbar1;
    public Image Enemyhealthbar1half;
    public Image Enemyhealthbar2;
    public Image Enemyhealthbar2half;
    public Image Enemyhealthbar3;
    public Image Enemyhealthbar3half;
    public Image Enemyhealthbar4;
    public Image Enemyhealthbar4half;
    public Image Enemyhealthbar5;
    public Image Enemyhealthbar5half;
    public Image Enemyhealthbar6;
    public Image Enemyhealthbar6half;
    public Image Enemyhealthbar7;
    public Image Enemyhealthbar7half;
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
        else if (currenthealth == 7.5)
        {
            healthbar8.gameObject.SetActive(false);
        }
        else if (currenthealth == 7)
        {
            healthbar8.gameObject.SetActive(false);
            healthbar7half.gameObject.SetActive(false);
        }
        else if (currenthealth == 6.5)
        {
            healthbar8.gameObject.SetActive(false);
            healthbar7half.gameObject.SetActive(false);
            healthbar7.gameObject.SetActive(false);
        }else if (currenthealth == 6)
        {
            healthbar8.gameObject.SetActive(false);
            healthbar7half.gameObject.SetActive(false);
            healthbar7.gameObject.SetActive(false);
            healthbar6half.gameObject.SetActive(false);
        }else if (currenthealth == 5.5)
        {
            healthbar8.gameObject.SetActive(false);
            healthbar7half.gameObject.SetActive(false);
            healthbar7.gameObject.SetActive(false);
            healthbar6half.gameObject.SetActive(false);
            healthbar6.gameObject.SetActive(false);
        }else if (currenthealth == 5.0)
        {
            healthbar8.gameObject.SetActive(false);
            healthbar7half.gameObject.SetActive(false);
            healthbar7.gameObject.SetActive(false);
            healthbar6half.gameObject.SetActive(false);
            healthbar6.gameObject.SetActive(false);
            healthbar5half.gameObject.SetActive(false);
        }else if (currenthealth == 4.5)
        {
            healthbar8.gameObject.SetActive(false);
            healthbar7half.gameObject.SetActive(false);
            healthbar7.gameObject.SetActive(false);
            healthbar6half.gameObject.SetActive(false);
            healthbar6.gameObject.SetActive(false);
            healthbar5half.gameObject.SetActive(false);
            healthbar5.gameObject.SetActive(false);
        }else if (currenthealth == 4)
        {
            healthbar8.gameObject.SetActive(false);
            healthbar7half.gameObject.SetActive(false);
            healthbar7.gameObject.SetActive(false);
            healthbar6half.gameObject.SetActive(false);
            healthbar6.gameObject.SetActive(false);
            healthbar5half.gameObject.SetActive(false);
            healthbar5.gameObject.SetActive(false);
            healthbar4half.gameObject.SetActive(false);
        }else if (currenthealth == 3.5)
        {
            healthbar8.gameObject.SetActive(false);
            healthbar7half.gameObject.SetActive(false);
            healthbar7.gameObject.SetActive(false);
            healthbar6half.gameObject.SetActive(false);
            healthbar6.gameObject.SetActive(false);
            healthbar5half.gameObject.SetActive(false);
            healthbar5.gameObject.SetActive(false);
            healthbar4half.gameObject.SetActive(false);
            healthbar4.gameObject.SetActive(false);
        }else if (currenthealth == 3.0)
        {
            healthbar8.gameObject.SetActive(false);
            healthbar7half.gameObject.SetActive(false);
            healthbar7.gameObject.SetActive(false);
            healthbar6half.gameObject.SetActive(false);
            healthbar6.gameObject.SetActive(false);
            healthbar5half.gameObject.SetActive(false);
            healthbar5.gameObject.SetActive(false);
            healthbar4half.gameObject.SetActive(false);
            healthbar4.gameObject.SetActive(false);
            healthbar3half.gameObject.SetActive(false);

        }else if (currenthealth == 2.5)
        {
            healthbar8.gameObject.SetActive(false);
            healthbar7half.gameObject.SetActive(false);
            healthbar7.gameObject.SetActive(false);
            healthbar6half.gameObject.SetActive(false);
            healthbar6.gameObject.SetActive(false);
            healthbar5half.gameObject.SetActive(false);
            healthbar5.gameObject.SetActive(false);
            healthbar4half.gameObject.SetActive(false);
            healthbar4.gameObject.SetActive(false);
            healthbar3half.gameObject.SetActive(false);
            healthbar3.gameObject.SetActive(false);

        }else if (currenthealth == 2.0)
        {
            healthbar8.gameObject.SetActive(false);
            healthbar7half.gameObject.SetActive(false);
            healthbar7.gameObject.SetActive(false);
            healthbar6half.gameObject.SetActive(false);
            healthbar6.gameObject.SetActive(false);
            healthbar5half.gameObject.SetActive(false);
            healthbar5.gameObject.SetActive(false);
            healthbar4half.gameObject.SetActive(false);
            healthbar4.gameObject.SetActive(false);
            healthbar3half.gameObject.SetActive(false);
            healthbar3.gameObject.SetActive(false);
            healthbar2half.gameObject.SetActive(false);

        }else if (currenthealth == 1.5)
        {
            healthbar8.gameObject.SetActive(false);
            healthbar7half.gameObject.SetActive(false);
            healthbar7.gameObject.SetActive(false);
            healthbar6half.gameObject.SetActive(false);
            healthbar6.gameObject.SetActive(false);
            healthbar5half.gameObject.SetActive(false);
            healthbar5.gameObject.SetActive(false);
            healthbar4half.gameObject.SetActive(false);
            healthbar4.gameObject.SetActive(false);
            healthbar3half.gameObject.SetActive(false);
            healthbar3.gameObject.SetActive(false);
            healthbar2half.gameObject.SetActive(false);
            healthbar2.gameObject.SetActive(false);
        }else if (currenthealth == 1.0)
        {
            healthbar8.gameObject.SetActive(false);
            healthbar7half.gameObject.SetActive(false);
            healthbar7.gameObject.SetActive(false);
            healthbar6half.gameObject.SetActive(false);
            healthbar6.gameObject.SetActive(false);
            healthbar5half.gameObject.SetActive(false);
            healthbar5.gameObject.SetActive(false);
            healthbar4half.gameObject.SetActive(false);
            healthbar4.gameObject.SetActive(false);
            healthbar3half.gameObject.SetActive(false);
            healthbar3.gameObject.SetActive(false);
            healthbar2half.gameObject.SetActive(false);
            healthbar2.gameObject.SetActive(false);
            healthbar1half.gameObject.SetActive(false);
        }else if (currenthealth == 0.5)
        {
            healthbar8.gameObject.SetActive(false);
            healthbar7half.gameObject.SetActive(false);
            healthbar7.gameObject.SetActive(false);
            healthbar6half.gameObject.SetActive(false);
            healthbar6.gameObject.SetActive(false);
            healthbar5half.gameObject.SetActive(false);
            healthbar5.gameObject.SetActive(false);
            healthbar4half.gameObject.SetActive(false);
            healthbar4.gameObject.SetActive(false);
            healthbar3half.gameObject.SetActive(false);
            healthbar3.gameObject.SetActive(false);
            healthbar2half.gameObject.SetActive(false);
            healthbar2.gameObject.SetActive(false);
            healthbar1half.gameObject.SetActive(false);
            healthbar1.gameObject.SetActive(false);
        }else if (currenthealth == 0.0)
        {
            healthbar8.gameObject.SetActive(false);
            healthbar7half.gameObject.SetActive(false);
            healthbar7.gameObject.SetActive(false);
            healthbar6half.gameObject.SetActive(false);
            healthbar6.gameObject.SetActive(false);
            healthbar5half.gameObject.SetActive(false);
            healthbar5.gameObject.SetActive(false);
            healthbar4half.gameObject.SetActive(false);
            healthbar4.gameObject.SetActive(false);
            healthbar3half.gameObject.SetActive(false);
            healthbar3.gameObject.SetActive(false);
            healthbar2half.gameObject.SetActive(false);
            healthbar2.gameObject.SetActive(false);
            healthbar1half.gameObject.SetActive(false);
            healthbar1.gameObject.SetActive(false);
            healthbarhalf.gameObject.SetActive(false);
        }



        if (enemyCurrentHealth == 8)
        {

        }
        else if (enemyCurrentHealth == 7.5)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
        }
        else if (enemyCurrentHealth == 7)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
            Enemyhealthbar7half.gameObject.SetActive(false);
        }
        else if (enemyCurrentHealth == 6.5)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
            Enemyhealthbar7half.gameObject.SetActive(false);
            Enemyhealthbar7.gameObject.SetActive(false);
        }
        else if (enemyCurrentHealth == 6)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
            Enemyhealthbar7half.gameObject.SetActive(false);
            Enemyhealthbar7.gameObject.SetActive(false);
            Enemyhealthbar6half.gameObject.SetActive(false);
        }
        else if (enemyCurrentHealth == 5.5)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
            Enemyhealthbar7half.gameObject.SetActive(false);
            Enemyhealthbar7.gameObject.SetActive(false);
            Enemyhealthbar6half.gameObject.SetActive(false);
            Enemyhealthbar6.gameObject.SetActive(false);
        }
        else if (enemyCurrentHealth == 5.0)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
            Enemyhealthbar7half.gameObject.SetActive(false);
            Enemyhealthbar7.gameObject.SetActive(false);
            Enemyhealthbar6half.gameObject.SetActive(false);
            Enemyhealthbar6.gameObject.SetActive(false);
            Enemyhealthbar5half.gameObject.SetActive(false);
        }
        else if (enemyCurrentHealth == 4.5)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
            Enemyhealthbar7half.gameObject.SetActive(false);
            Enemyhealthbar7.gameObject.SetActive(false);
            Enemyhealthbar6half.gameObject.SetActive(false);
            Enemyhealthbar6.gameObject.SetActive(false);
            Enemyhealthbar5half.gameObject.SetActive(false);
            Enemyhealthbar5.gameObject.SetActive(false);
        }
        else if (enemyCurrentHealth == 4)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
            Enemyhealthbar7half.gameObject.SetActive(false);
            Enemyhealthbar7.gameObject.SetActive(false);
            Enemyhealthbar6half.gameObject.SetActive(false);
            Enemyhealthbar6.gameObject.SetActive(false);
            Enemyhealthbar5half.gameObject.SetActive(false);
            Enemyhealthbar5.gameObject.SetActive(false);
            Enemyhealthbar4half.gameObject.SetActive(false);
        }
        else if (enemyCurrentHealth == 3.5)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
            Enemyhealthbar7half.gameObject.SetActive(false);
            Enemyhealthbar7.gameObject.SetActive(false);
            Enemyhealthbar6half.gameObject.SetActive(false);
            Enemyhealthbar6.gameObject.SetActive(false);
            Enemyhealthbar5half.gameObject.SetActive(false);
            Enemyhealthbar5.gameObject.SetActive(false);
            Enemyhealthbar4half.gameObject.SetActive(false);
            Enemyhealthbar4.gameObject.SetActive(false);
        }
        else if (enemyCurrentHealth == 3.0)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
            Enemyhealthbar7half.gameObject.SetActive(false);
            Enemyhealthbar7.gameObject.SetActive(false);
            Enemyhealthbar6half.gameObject.SetActive(false);
            Enemyhealthbar6.gameObject.SetActive(false);
            Enemyhealthbar5half.gameObject.SetActive(false);
            Enemyhealthbar5.gameObject.SetActive(false);
            Enemyhealthbar4half.gameObject.SetActive(false);
            Enemyhealthbar4.gameObject.SetActive(false);
            Enemyhealthbar3half.gameObject.SetActive(false);

        }
        else if (enemyCurrentHealth == 2.5)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
            Enemyhealthbar7half.gameObject.SetActive(false);
            Enemyhealthbar7.gameObject.SetActive(false);
            Enemyhealthbar6half.gameObject.SetActive(false);
            Enemyhealthbar6.gameObject.SetActive(false);
            Enemyhealthbar5half.gameObject.SetActive(false);
            Enemyhealthbar5.gameObject.SetActive(false);
            Enemyhealthbar4half.gameObject.SetActive(false);
            Enemyhealthbar4.gameObject.SetActive(false);
            Enemyhealthbar3half.gameObject.SetActive(false);
            Enemyhealthbar3.gameObject.SetActive(false);

        }
        else if (enemyCurrentHealth == 2.0)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
            Enemyhealthbar7half.gameObject.SetActive(false);
            Enemyhealthbar7.gameObject.SetActive(false);
            Enemyhealthbar6half.gameObject.SetActive(false);
            Enemyhealthbar6.gameObject.SetActive(false);
            Enemyhealthbar5half.gameObject.SetActive(false);
            Enemyhealthbar5.gameObject.SetActive(false);
            Enemyhealthbar4half.gameObject.SetActive(false);
            Enemyhealthbar4.gameObject.SetActive(false);
            Enemyhealthbar3half.gameObject.SetActive(false);
            Enemyhealthbar3.gameObject.SetActive(false);
            Enemyhealthbar2half.gameObject.SetActive(false);

        }
        else if (enemyCurrentHealth == 1.5)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
            Enemyhealthbar7half.gameObject.SetActive(false);
            Enemyhealthbar7.gameObject.SetActive(false);
            Enemyhealthbar6half.gameObject.SetActive(false);
            Enemyhealthbar6.gameObject.SetActive(false);
            Enemyhealthbar5half.gameObject.SetActive(false);
            Enemyhealthbar5.gameObject.SetActive(false);
            Enemyhealthbar4half.gameObject.SetActive(false);
            Enemyhealthbar4.gameObject.SetActive(false);
            Enemyhealthbar3half.gameObject.SetActive(false);
            Enemyhealthbar3.gameObject.SetActive(false);
            Enemyhealthbar2half.gameObject.SetActive(false);
            Enemyhealthbar2.gameObject.SetActive(false);
        }
        else if (enemyCurrentHealth == 1.0)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
            Enemyhealthbar7half.gameObject.SetActive(false);
            Enemyhealthbar7.gameObject.SetActive(false);
            Enemyhealthbar6half.gameObject.SetActive(false);
            Enemyhealthbar6.gameObject.SetActive(false);
            Enemyhealthbar5half.gameObject.SetActive(false);
            Enemyhealthbar5.gameObject.SetActive(false);
            Enemyhealthbar4half.gameObject.SetActive(false);
            Enemyhealthbar4.gameObject.SetActive(false);
            Enemyhealthbar3half.gameObject.SetActive(false);
            Enemyhealthbar3.gameObject.SetActive(false);
            Enemyhealthbar2half.gameObject.SetActive(false);
            Enemyhealthbar2.gameObject.SetActive(false);
            Enemyhealthbar1half.gameObject.SetActive(false);
        }
        else if (enemyCurrentHealth == 0.5)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
            Enemyhealthbar7half.gameObject.SetActive(false);
            Enemyhealthbar7.gameObject.SetActive(false);
            Enemyhealthbar6half.gameObject.SetActive(false);
            Enemyhealthbar6.gameObject.SetActive(false);
            Enemyhealthbar5half.gameObject.SetActive(false);
            Enemyhealthbar5.gameObject.SetActive(false);
            Enemyhealthbar4half.gameObject.SetActive(false);
            Enemyhealthbar4.gameObject.SetActive(false);
            Enemyhealthbar3half.gameObject.SetActive(false);
            Enemyhealthbar3.gameObject.SetActive(false);
            Enemyhealthbar2half.gameObject.SetActive(false);
            Enemyhealthbar2.gameObject.SetActive(false);
            Enemyhealthbar1half.gameObject.SetActive(false);
            Enemyhealthbar1.gameObject.SetActive(false);
        }
        else if (enemyCurrentHealth == 0.0)
        {
            Enemyhealthbar8.gameObject.SetActive(false);
            Enemyhealthbar7half.gameObject.SetActive(false);
            Enemyhealthbar7.gameObject.SetActive(false);
            Enemyhealthbar6half.gameObject.SetActive(false);
            Enemyhealthbar6.gameObject.SetActive(false);
            Enemyhealthbar5half.gameObject.SetActive(false);
            Enemyhealthbar5.gameObject.SetActive(false);
            Enemyhealthbar4half.gameObject.SetActive(false);
            Enemyhealthbar4.gameObject.SetActive(false);
            Enemyhealthbar3half.gameObject.SetActive(false);
            Enemyhealthbar3.gameObject.SetActive(false);
            Enemyhealthbar2half.gameObject.SetActive(false);
            Enemyhealthbar2.gameObject.SetActive(false);
            Enemyhealthbar1half.gameObject.SetActive(false);
            Enemyhealthbar1.gameObject.SetActive(false);
            EnemyHealthbarhalf.gameObject.SetActive(false);
        }
    }

}
