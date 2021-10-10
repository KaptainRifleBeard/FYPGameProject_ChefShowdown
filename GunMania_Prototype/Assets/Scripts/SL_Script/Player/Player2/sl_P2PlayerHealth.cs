using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Photon.Pun;


public class sl_P2PlayerHealth : MonoBehaviour
{
    public Text healthText;
    PhotonView view;

    //HEALTH

    [Space(10)]
    [Header("Health")]
    private int maxHealth = 16;
    public int p2currentHealth = 0;

    public GameObject bulletScript;


    void Start()
    {
        view = GetComponent<PhotonView>();
        p2currentHealth = maxHealth;
    }

    public void Update()
    {
        healthText.text = p2currentHealth.ToString();

        //HEALTH
        if (p2currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }


    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(p2currentHealth);
        }
        else if (stream.IsReading)
        {
            p2currentHealth = (int)stream.ReceiveNext();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            view.RPC("BulletDamage2", RpcTarget.All);

        }
    }

    [PunRPC]
    public void BulletDamage2()
    {
        p2currentHealth -= bulletScript.GetComponent<sl_BulletScript>().bulletDmg;
    }

}
