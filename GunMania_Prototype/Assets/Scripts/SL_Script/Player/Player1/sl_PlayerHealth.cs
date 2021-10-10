using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Photon.Pun;

public class sl_PlayerHealth : MonoBehaviour
{
    public Text healthText;
    PhotonView view;

    //HEALTH

    [Space(10)]
    [Header("Health")]
    private int maxHealth = 16;
    public int currentHealth = 0;

    public GameObject bulletScript;


    void Start()
    {
        view = GetComponent<PhotonView>();
        currentHealth = maxHealth;
    }

    public void Update()
    {
        healthText.text = currentHealth.ToString();


        //HEALTH
        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }


    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentHealth);
        }
        else if (stream.IsReading)
        {
            currentHealth = (int)stream.ReceiveNext();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "P2Bullet")
        {
            view.RPC("BulletDamage", RpcTarget.All);
            
        }
    }


    [PunRPC]
    public void BulletDamage()
    {
        currentHealth -= bulletScript.GetComponent<sl_BulletScript>().bulletDmg;
    }

}
