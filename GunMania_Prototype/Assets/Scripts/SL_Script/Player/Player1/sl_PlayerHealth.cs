using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;

public class sl_PlayerHealth : MonoBehaviour, IOnEventCallback
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



    void Start()
    {
        view = GetComponent<PhotonView>();
        currentHealth = maxHealth;
    }

    public void Update()
    {
        healthText.text = currentHealth.ToString();

        //HEALTH
        //if (currentHealth == 0)
        //{
        //    Destroy(gameObject);
        //    //gameObject.SetActive(false);
        //    //StartCoroutine(WaitToSpawnPlayer());

        //    if (view.IsMine)
        //    {
        //        loseScreen.SetActive(true);
        //    }
        //}
        //else if(sl_P2PlayerHealth.p2currentHealth == 0)
        //{
        //    winScreen.SetActive(true);
        //}

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


    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentHealth);
            //stream.SendNext(transform.position);

        }
        else if (stream.IsReading)
        {
            currentHealth = (int)stream.ReceiveNext();
            //transform.position = (Vector3)stream.ReceiveNext();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "P2Bullet")
        {
            view.RPC("BulletDamage", RpcTarget.All);

        }

    }


    IEnumerator Respawn()
    {
        sl_InventoryManager.ClearAllInList();
        yield return new WaitForSeconds(1.0f);
        gameObject.transform.position = spawnPostionA.transform.position;

        currentHealth = 8;
    }

    [PunRPC]
    public void BulletDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth -= bulletScript.GetComponent<sl_BulletScript>().bulletDmg;

            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }

    }


    //will fire when event is activated
    public void OnEvent(EventData photonEvent)
    {
        //    if(photonEvent.Code == sl_WinLoseUI.RestartEventCode)
        //    {
        //        currentHealth = 8;
        //        sl_P2PlayerHealth.p2currentHealth = 8;

        //        StartCoroutine(Respawn());

        //    }
    }

    //private void OnEnable()
    //{
    //    PhotonNetwork.AddCallbackTarget(this);
    //}

    //private void OnDisable()
    //{
    //    PhotonNetwork.RemoveCallbackTarget(this);
    //}
}