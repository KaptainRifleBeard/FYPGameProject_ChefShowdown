using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDManager : MonoBehaviour
{
    const string plus = "+", equals = "=",space =" ";
    public Sprite empty,Transparent;
    // constant declaration to avoid having to redo constantly;


    public Text Timer;
    
    // consulted with mr zeren - no need to static fielded scripts if it's only for the scene
    public HealthBar Healthmanager;
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
    
    //swap partner and current player image
    public void TagPartner(Image current,Image partner)
    {
        var temp = current.sprite;

        current.sprite = partner.sprite;

        partner.sprite = temp;
    }
    
    // show superfood is available
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

    //clear food 2 and superfood stuff
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


    // generate superfood in food1
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


        timeStart -= Time.deltaTime;

    }

    // Start is called before the first frame update
    void Start()
    {
        timeStart = MatchDuration ;
        //Healthmanager;
    }

    // Update is called once per frame
    void Update()
    {


        if (Healthmanager.currenthealth>0 || Healthmanager.enemyCurrentHealth>0 || timeStart > 0)
        {
            updateTimer();
        }
        else
        {
            Debug.Log("Match Ended");
        }

        // countdown timer
        /// if playerhealth drops update with
        /// PlayerHealth.UpdateHealth( int value );
        /// 

        /// if player has been hit by attack
        /// 

 
    }


}
