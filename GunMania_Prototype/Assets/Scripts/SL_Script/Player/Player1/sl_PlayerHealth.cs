using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;

public class sl_PlayerHealth : MonoBehaviour/*, IOnEventCallback*/
{
    public Text healthText;
    PhotonView view;

    //HEALTH

    [Space(10)]
    [Header("Health")]
    private float maxHealth = 8;
    public static float currentHealth;

    public GameObject bulletScript;
    public GameObject spawnPostionA;

    public Sprite emptyHealth;
    public Sprite halfHealth;
    public Sprite fulllHealth;

    public Image[] hearts;


    float bulletDamage;
    float statDamage;
    float percentage;

    public static bool getDamage;
    public static bool playerDead;

    void Start()
    {
        view = GetComponent<PhotonView>();
        currentHealth = maxHealth;

        getDamage = false;
        playerDead = false;
    }

    public void Update()
    {

        if (currentHealth <= 0)
        {
            playerDead = true;
            StartCoroutine(PlayerDead());
        }

    }

    public void FixedUpdate()
    {
        healthText.text = currentHealth.ToString();

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                if (i + 0.5 == currentHealth)
                {
                    hearts[i].sprite = halfHealth;
                }
                else
                {
                    hearts[i].sprite = fulllHealth;
                }
            }
            else
            {
                hearts[i].sprite = emptyHealth;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "P2Bullet")//dont put this in masterclient, or else ur view wont destroy bullet
        {
            Destroy(other.gameObject);

        }

        if (PhotonNetwork.IsMasterClient) //make sure it run only once
        {
            if (other.gameObject.tag == "WaterSpray")
            {
                float waterDamage;
                waterDamage = 1;
                view.RPC("BulletDamage", RpcTarget.All, waterDamage);

                getDamage = true;
                StartCoroutine(StopGetDamage());
            }



            //bullets
            if (other.gameObject.tag == "P2Bullet")
            {
                bulletDamage = 1.0f; //original
                percentage = (bulletDamage * 50f) / 100f;

                GetDamage(bulletDamage, percentage);
            }

            //DISHES
            if (other.gameObject.tag == "P2Sinseollo")
            {
                bulletDamage = 3f; 
                percentage = (bulletDamage * 50f) / 100f;

                GetDamage(bulletDamage, percentage);

            }

            if (other.gameObject.tag == "P2BirdNestSoup") //stay in the range deal more dmg per second
            {
                bulletDamage = 1.0f; 
                percentage = (bulletDamage * 50f) / 100f;

                GetDamage(bulletDamage, percentage);


            }

            if (other.gameObject.tag == "P2BuddhaJumpsOvertheWall" || other.gameObject.tag == "P2FoxtailMillet" || other.gameObject.tag == "P2Mukozuke")
            {
                bulletDamage = 2.0f;
                percentage = (bulletDamage * 50f) / 100f;

                GetDamage(bulletDamage, percentage);

            }

            if (other.gameObject.tag == "Hassun") //heal
            {
                currentHealth += 3.0f;
                if (currentHealth >= 8.0f)
                {
                    currentHealth = 8.0f;
                }

            }

        }

    }

    public void GetDamage(float damage, float percent)
    {

        getDamage = true;
        StartCoroutine(StopGetDamage());

        /*
                //0 - b, 1 - w, 2 - j, 3 - k
        brock - extra 50% damage for all foods & dishes thrown, range -2
        wen - take extra 0.5 dmg from everyone, speed + 20
        jiho - deal less 50% damage when attacks, range+2
        katsuki - take less 50% damage from everyone, speed-30

        */

        //for my model
        if (SL_newP1Movement.changeModelAnim == 1) //w
        {
            damage += 0.5f;
        }

        if (SL_newP1Movement.changeModelAnim == 3)//k
        {
            damage -= 0.5f;
        }



        //from enemy bullet
        if (sl_newP2Movement.changep2Icon == 0) //enemy brock
        {
            damage = damage + percent;
        }

        if (sl_newP2Movement.changep2Icon == 2)//enemy jiho
        {
            damage = damage - percent;
        }
        view.RPC("BulletDamage", RpcTarget.All, damage);

    }

    IEnumerator StopGetDamage()
    {
        yield return new WaitForSeconds(1.0f);
        getDamage = false;
    }

    IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(3.0f);
        currentHealth = 0;

        sl_InventoryManager.ClearAllInList();
        PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    public void BulletDamage(float damage)
    {
        if (currentHealth > 0)
        {
            //currentHealth -= 0.5f; //because it run 2 times, so i cut it half
            currentHealth -= damage;

            if (currentHealth < 0 && view.IsMine && PhotonNetwork.IsConnected == true)
            {
                playerDead = true;
                StartCoroutine(PlayerDead());


            }

        }

    }







    //will fire when event is activated
    //public void OnEvent(EventData photonEvent)
    //{
    //    if(photonEvent.Code == sl_WinLoseUI.RestartEventCode)
    //    {
    //        currentHealth = 8;
    //        sl_P2PlayerHealth.p2currentHealth = 8;

    //        StartCoroutine(Respawn());

    //    }
    //}

    //private void OnEnable()
    //{
    //    PhotonNetwork.AddCallbackTarget(this);
    //}

    //private void OnDisable()
    //{
    //    PhotonNetwork.RemoveCallbackTarget(this);
    //}



    //public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        //stream.SendNext(currentHealth);

    //    }
    //    else if (stream.IsReading)
    //    {
    //        //currentHealth = (float)stream.ReceiveNext();
    //    }

    //}



    //IEnumerator Respawn()
    //{
    //    sl_InventoryManager.ClearAllInList();
    //    yield return new WaitForSeconds(1.0f);
    //    gameObject.transform.position = spawnPostionA.transform.position;

    //    currentHealth = 16;
    //}


}