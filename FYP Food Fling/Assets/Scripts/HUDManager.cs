using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDManager : MonoBehaviour
{

    public Text Timer;
    public GameObject PlayerPortrait,EnemyPotrait;

    public HealthBar PlayerHealth, EnemyHealth;
    public UltimateBar PlayerUltBar, EnemyUltBar;

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

        PlayerUltBar.SetMaxBar(MaxUltValue);
        EnemyUltBar.SetMaxBar(MaxUltValue);
    }

    // Update is called once per frame
    void Update()
    {


        while (PlayerHealth.currenthealth>0 || EnemyHealth.currenthealth>0)
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
        if (collision.gameObject.tag == "food")
        {
            PlayerHealth.UpdateHealth(12.5f);
        }
        else if (collision.gameObject.tag == "superFood")
        {
            PlayerHealth.UpdateHealth(25f);
        }

    }
}
