using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class sl_P2PlayerHealth : MonoBehaviour
{
    public Text healthText;
    PhotonView view;

    //HEALTH

    [Space(10)]
    [Header("Health")]
    private float maxHealth = 8;
    public static float p2currentHealth;

    public GameObject bulletScript;

    public GameObject spawnPostionB;

    public Sprite emptyHealth;
    public Sprite halfHealth;
    public Sprite fulllHealth;

    public Image[] hearts;

    float bulletDamage2;
    GameObject bulletToDestroy2;

    void Start()
    {
        view = GetComponent<PhotonView>();
        p2currentHealth = maxHealth;
    }


    public void Update()
    {
        //if (p2currentHealth <= 0)
        //{
        //    Destroy(gameObject);
        //}

    }


    public void FixedUpdate()
    {
        healthText.text = p2currentHealth.ToString();


        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < p2currentHealth)
            {
                if (i + 0.5 == p2currentHealth)
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
        if (other.gameObject.tag == "Bullet") //dont put this in masterclient, or else ur view wont destroy bullet
        {
            Destroy(other.gameObject);
        }

        if (!PhotonNetwork.IsMasterClient)
        {
            if (other.gameObject.tag == "Bullet")
            {
                bulletDamage2 = 1.0f; //original


                //0 - b, 1 - w, 2 - j, 3 - k
                float damagetake;

                //for my model
                if(sl_newP2Movement.changep2Icon == 1)
                {
                    damagetake = 0.5f;
                    bulletDamage2 = bulletDamage2 + damagetake; // stat: wen - take 1.5 dmg from everyone
                }
                
                if (sl_newP2Movement.changep2Icon == 3 || SL_newP1Movement.changeModelAnim == 2)
                {
                    damagetake = 0.5f;
                    bulletDamage2 = bulletDamage2 - damagetake; // stat: katsuki, jiho - take less 0.5 dmg from everyone
                }

                //from enemy bullet
                if(SL_newP1Movement.changeModelAnim == 0)
                {
                    damagetake = 0.5f;
                    bulletDamage2 = bulletDamage2 + damagetake; // stat: brock - deal more 50% dmg
                }

                view.RPC("BulletDamage2", RpcTarget.All, bulletDamage2);

                //p2currentHealth -= 1;
                
            }

        }
           
    }

    [PunRPC]
    public void BulletDamage2(float damage)
    {
        if (p2currentHealth > 0)
        {
            //p2currentHealth -= 0.5f;
            p2currentHealth -= damage;

            if (p2currentHealth < 0 && view.IsMine && PhotonNetwork.IsConnected == true)
            {
                p2currentHealth = 0;
                sl_p2InventoryManager.ClearAllInList();

                PhotonNetwork.Destroy(gameObject);

            }

        }
    }



    //public void SpawnBackPlayer()
    //{
    //    sl_p2InventoryManager.ClearAllInList();
    //    gameObject.SetActive(true);
    //    gameObject.transform.position = spawnPostionB.transform.position;
    //}

    //IEnumerator WaitToSpawnPlayer()
    //{
    //    yield return new WaitForSeconds(1.0f);
    //    SpawnBackPlayer();
    //    p2currentHealth = maxHealth;
    //}

    //public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        //stream.SendNext(p2currentHealth);
    //        //stream.SendNext(transform.position);
    //    }
    //    else if (stream.IsReading)
    //    {
    //        //p2currentHealth = (float)stream.ReceiveNext();
    //        //transform.position = (Vector3)stream.ReceiveNext();
    //    }

    //}


}