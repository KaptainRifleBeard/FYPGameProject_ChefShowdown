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

    public static bool takingDamage;  //for animation

    void Start()
    {
        view = GetComponent<PhotonView>();
        currentHealth = maxHealth;
        takingDamage = false;
    }

    public void Update()
    {
        //if (currentHealth <= 0)
        //{
        //    Destroy(gameObject);
        //}

        if(takingDamage)
        {
            StartCoroutine(TakeDamage());
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
            if (other.gameObject.tag == "P2Bullet")
            {
                bulletDamage = 1.0f; //original
                takingDamage = true;

                //0 - b, 1 - w, 2 - j, 3 - k
                float damagetake;

                //for my model
                if (SL_newP1Movement.changeModelAnim == 1)
                {
                    damagetake = 0.5f;
                    bulletDamage = bulletDamage + damagetake; // stat: wen - take 1.5 dmg from everyone
                }

                if (SL_newP1Movement.changeModelAnim == 3 || sl_newP2Movement.changep2Icon == 2)
                {
                    damagetake = 0.5f;
                    bulletDamage = bulletDamage - damagetake; // stat: katsuki, jiho - take less 0.5 dmg from everyone
                }

                //from enemy bullet
                if (sl_newP2Movement.changep2Icon == 0)
                {
                    damagetake = 0.5f;
                    bulletDamage = bulletDamage + damagetake; // stat: brock - deal more 50% dmg
                }

                view.RPC("BulletDamage", RpcTarget.All, bulletDamage);
                //currentHealth -= 1;

            }

        }

    }


    IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(0.2f);
        takingDamage = false;
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
                currentHealth = 0;
                sl_InventoryManager.ClearAllInList();

                PhotonNetwork.Destroy(gameObject);

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

}