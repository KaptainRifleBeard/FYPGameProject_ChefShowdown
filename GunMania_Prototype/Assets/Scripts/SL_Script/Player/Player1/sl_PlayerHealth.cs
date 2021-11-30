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

    void Start()
    {
        view = GetComponent<PhotonView>();
        currentHealth = maxHealth;
    }

    public void Update()
    {

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
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
                bulletDamage = 1;

                view.RPC("BulletDamage", RpcTarget.All, bulletDamage);
                //currentHealth -= 1;

            }

        }

    }


    //IEnumerator Respawn()
    //{
    //    sl_InventoryManager.ClearAllInList();
    //    yield return new WaitForSeconds(1.0f);
    //    gameObject.transform.position = spawnPostionA.transform.position;

    //    currentHealth = 16;
    //}

    [PunRPC]
    public void BulletDamage(float damage)
    {
        if (currentHealth > 0)
        {
            //currentHealth -= 0.5f; //because it run 2 times, so i cut it half
            currentHealth -= damage;

            if (currentHealth < 0)
            {
                currentHealth = 0;
                Destroy(gameObject);
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