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
    float percentage;
    bool isDish;


    public static bool getDamage2;
    public static bool player2Dead;

    void Start()
    {
        view = GetComponent<PhotonView>();
        p2currentHealth = maxHealth;

        getDamage2 = false;
        player2Dead = false;
    }


    public void Update()
    {
        if (p2currentHealth <= 0)
        {
            player2Dead = true;
            StartCoroutine(Player2Dead());
        }

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
            if (other.gameObject.layer == 6)
            {
                isDish = true; //for katsuki to check dish
            }

            if (other.gameObject.tag == "WaterSpray")
            {
                float waterDamage;
                waterDamage = 1;
                view.RPC("BulletDamage2", RpcTarget.All, waterDamage);

                getDamage2 = true;
                StartCoroutine(StopGetDamage());
            }


            //****bullets
            if (other.gameObject.tag == "Bullet")
            {
                isDish = false;

                bulletDamage2 = 1.0f; //original
                percentage = (bulletDamage2 * 50f) / 100f;
                GetDamage(bulletDamage2, percentage);
            }

            //DISHES
            if (other.gameObject.tag == "Sinseollo")
            {
                bulletDamage2 = 3f; 
                percentage = (bulletDamage2 * 50f) / 100f;
                GetDamage(bulletDamage2, percentage);
            }

            if (other.gameObject.tag == "BirdNestSoup") //stay in the range deal more dmg per second
            {
                bulletDamage2 = 1.0f; 
                percentage = (bulletDamage2 * 50f) / 100f;
                GetDamage(bulletDamage2, percentage);


            }

            if (other.gameObject.tag == "BuddhaJumpsOvertheWall" || other.gameObject.tag == "FoxtailMillet" || other.gameObject.tag == "Mukozuke")
            {
                bulletDamage2 = 2.0f; 
                percentage = (bulletDamage2 * 50f) / 100f;
                GetDamage(bulletDamage2, percentage);

            }

            if (other.gameObject.tag == "P2Hassun") //heal
            {
                //rmb to add rpc to sync
                bulletDamage2 = 0.0f;

                p2currentHealth += 3.0f;
                if (p2currentHealth >= 8.0f)
                {
                    p2currentHealth = 8.0f;
                }
                view.RPC("BulletDamage", RpcTarget.All, bulletDamage2);

            }
        }
           
    }

    public void GetDamage(float damage, float percent)
    {
        getDamage2 = true;
        StartCoroutine(StopGetDamage());
        /*
                 //0 - b, 1 - w, 2 - j, 3 - k
         brock - extra 50% damage for all foods & dishes thrown, range -2
         wen - take extra 0.5 dmg from everyone, speed + 20
         jiho - deal less 50% damage when attacks, range+2
         katsuki - take less 50% damage from everyone, speed-30

         */

        //for my model
        if (sl_newP2Movement.changep2Icon == 1) //w
        {
            damage += 0.5f;
        }

        if (sl_newP2Movement.changep2Icon == 3 && !isDish)//k
        {
            damage -= 0; //if is food, get full damage
        }
        if (sl_newP2Movement.changep2Icon == 3 && isDish)//k
        {
            damage = damage - percent;
        }



        //from enemy bullet
        if (SL_newP1Movement.changeModelAnim == 0) //enemy brock
        {
            damage = damage + percent;
        }

        if (SL_newP1Movement.changeModelAnim == 2)//enemy jiho
        {
            damage = damage - percent;
        }


        view.RPC("BulletDamage2", RpcTarget.All, damage);

    }

    IEnumerator StopGetDamage()
    {
        yield return new WaitForSeconds(0.5f);
        getDamage2 = false;
    }
    IEnumerator Player2Dead()
    {
        yield return new WaitForSeconds(3.0f);
        p2currentHealth = 0;

        sl_p2InventoryManager.ClearAllInList();
        PhotonNetwork.Destroy(gameObject);
    }


    [PunRPC]
    public void BulletDamage2(float damage)
    {
        isDish = false; //everytime run this set to false

        if (p2currentHealth > 0)
        {
            //p2currentHealth -= 0.5f;
            p2currentHealth -= damage;

            if (p2currentHealth < 0 && view.IsMine && PhotonNetwork.IsConnected == true)
            {
                player2Dead = true;
                StartCoroutine(Player2Dead());
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