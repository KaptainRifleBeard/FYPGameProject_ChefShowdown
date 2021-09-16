using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDManager : MonoBehaviour
{
    const string plus = "+", equals = "=",space =" ";
    Sprite empty;
    // constant declaration to avoid having to redo constantly;


    public Text Timer;
    public GameObject PlayerPortrait,EnemyPotrait,PlayerTagPortrait,EnemyTagPortrait;

    public HealthBar Healthmanager;    // Wtf have i done?
    public UltimateBar UltBarManager;

    [Header ("PlayerFoodItems")]
    public Image playerfood1, playerfood2, foodCombo;
    public Text playerPlus, PlayerEquals;

    [Header("Player2FoodItems")]
    public Image opponentfood1, opponentfood2, opponentfoodCombo;
    public Text opponentPlus, OpponentEquals;

    [SerializeField] float timeStart;
    [SerializeField] float MatchDuration;
    [SerializeField] float MaxUltValue = 100f;
    
    public void TagPartner(Image current,Image partner)
    {
        var temp = current.sprite;

        current.sprite = partner.sprite;

        partner.sprite = temp;
    }

    public void SuperFoodAvailable(Sprite superfoodItem,bool isPlayer1)
    {
        if (isPlayer1 == true)
        {
            playerPlus.text = plus;
            PlayerEquals.text = equals;
            foodCombo.sprite = superfoodItem;
        }
        else
        {
            opponentPlus.text = plus;
            OpponentEquals.text = equals;
            opponentfoodCombo.sprite = superfoodItem;
        }
    }

    public void ClearSuperFood(bool isPlayer)
    {
        if (isPlayer == true)
        {
            playerPlus.text = space;
            PlayerEquals.text = space;
            foodCombo.sprite = empty;
        }
        else
        {
            opponentPlus.text = space;
            OpponentEquals.text = space;
            opponentfoodCombo.sprite = empty;
        }


    }

    public void GenerateSuperFood(bool isPlayer)
    {
        if (isPlayer == true)
        {
            playerfood1 = foodCombo;
            playerfood2.sprite = empty;
            ClearSuperFood(true);
        }
        else
        {
            opponentfood1 = opponentfoodCombo;
            opponentfood2.sprite = empty;
            ClearSuperFood(false);
        }
    }



    private void updateTimer()
    {
        timeStart += Time.deltaTime;
        float temp = MatchDuration - timeStart;

        Timer.text = temp.ToString();

    }

    // Start is called before the first frame update
    void Start()
    {
        timeStart = Time.time;
        Timer.text = MatchDuration.ToString();

        UltBarManager.SetMaxBar(MaxUltValue,false);
        UltBarManager.SetMaxBar(MaxUltValue,true);
    }

    // Update is called once per frame
    void Update()
    {


        if (Healthmanager.currenthealth>0 || Healthmanager.enemyCurrentHealth>0)
        {
            updateTimer();
        }
        else
        {
            
        }

        // countdown timer
        /// if playerhealth drops update with
        /// PlayerHealth.UpdateHealth( int value );
        /// 

        /// if player has been hit by attack
        /// 

 
    }

    private void OnCollisionEnter(Collision collision)
    {
        // need to insert damage to healthbar.
        //refer to gamedesign document for further references.

        if (collision.gameObject.tag == "food")
        {
            if (collision.gameObject.tag == "enemy")
            {
                UltBarManager.SetBar(12.5f,true);
            }
            else if (collision.gameObject.tag == "player")
            {
                UltBarManager.SetBar(12.5f,false);
            }
        }
        else if (collision.gameObject.tag == "superFood")
        {
            if (collision.gameObject.tag == "enemy")
            {
                UltBarManager.SetBar(25f,true);
            }
            else if (collision.gameObject.tag == "player")
            {
                UltBarManager.SetBar(25f,false);
            }
        }

    }
}
