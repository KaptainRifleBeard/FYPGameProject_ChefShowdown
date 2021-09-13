using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDManager : MonoBehaviour
{

    public Text Timer;
    public GameObject PlayerPortrait,EnemyPotrait;

    public HealthBar Healthmanager;    // Wtf have i done?
    public UltimateBar UltBarManager;

    [SerializeField] float timeStart;
    [SerializeField] float MatchDuration;
    [SerializeField] float MaxUltValue = 100f;
    

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


        while (Healthmanager.currenthealth>0 || Healthmanager.enemyCurrentHealth>0)
        {
            updateTimer();
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
