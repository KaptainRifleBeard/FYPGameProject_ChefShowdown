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
    float molotovTimer;

    //dish vfx
    [Space(10)]
    [Header("Particle vfx")]
    public ParticleSystem[] healVfx;
    public ParticleSystem[] knockbackVfx;
    public ParticleSystem[] explodeVfx;
    public ParticleSystem[] dropVfx;
    public ParticleSystem[] noPickVfx;
    public ParticleSystem[] stunVfx;

    int numVfx;
    int timeDestroy;


    public static bool getDamage2;
    public static bool player2Dead;


    //AUDIO
    string audioName;

    //public AudioSource hitAudio;

    void Start()
    {
        view = GetComponent<PhotonView>();
        p2currentHealth = maxHealth;

        getDamage2 = false;
        player2Dead = false;

        numVfx = 0; //no effect
    }


    public void Update()
    {
        if (p2currentHealth <= 0 || sl_MatchCountdown.timeRemaining == 0)
        {
            player2Dead = true;
            StartCoroutine(Player2Dead());
        }
        if (sl_RematchAndLeave.rematchCount == 2)
        {
            player2Dead = false;
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
            getDamage2 = true;
            Destroy(other.gameObject);
        }

        if (!PhotonNetwork.IsMasterClient)
        {

            if (other.gameObject.tag == "WaterSpray")
            {
                float waterDamage;
                waterDamage = 1;
                view.RPC("BulletDamage2", RpcTarget.All, waterDamage);

                getDamage2 = true;
                StartCoroutine(StopGetDamage());
            }

            if (other.gameObject.tag == "Cat")
            {
                getDamage2 = true;

                audioName = "CatHit";
                SyncAudio();

                bulletDamage2 = 0.5f;
                percentage = (bulletDamage2 * 50f) / 100f;

                GetDamage(bulletDamage2, percentage);
            }

            if (other.gameObject.tag == "Dog")
            {
                getDamage2 = true;

                audioName = "DogHit";
                SyncAudio();

                bulletDamage2 = 0.5f;
                percentage = (bulletDamage2 * 50f) / 100f;

                GetDamage(bulletDamage2, percentage);
            }



            //****bullets
            if (other.gameObject.tag == "Bullet")
            {
                getDamage2 = true;

                audioName = "HitSFX";
                SyncAudio();

                isDish = false;

                bulletDamage2 = 1.0f; //original
                percentage = (bulletDamage2 * 50f) / 100f;
                GetDamage(bulletDamage2, percentage);
            }

            //DISHES
            #region
            if (other.gameObject.tag == "Sinseollo")//explode
            {
                getDamage2 = true;

                isDish = true; //for katsuki to check dish

                numVfx = 1;
                GetVisualEffect();

                audioName = "ExplodeSfx";
                SyncAudio();

                bulletDamage2 = 3f;
                percentage = (bulletDamage2 * 50f) / 100f;

                GetDamage(bulletDamage2, percentage);

            }

            if (other.gameObject.tag == "FoxtailMillet")//kb
            {
                getDamage2 = true;

                isDish = true; //for katsuki to check dish

                numVfx = 2;
                GetVisualEffect();

                audioName = "KnockbackSfx";
                SyncAudio();

                bulletDamage2 = 2.0f;
                percentage = (bulletDamage2 * 50f) / 100f;

                GetDamage(bulletDamage2, percentage);

            }

            if (other.gameObject.tag == "BuddhaJumpsOvertheWall")//no pick
            {
                getDamage2 = true;

                isDish = true; //for katsuki to check dish

                numVfx = 3;
                GetVisualEffect();

                audioName = "HitSFX";
                SyncAudio();

                bulletDamage2 = 2.0f;
                percentage = (bulletDamage2 * 50f) / 100f;

                GetDamage(bulletDamage2, percentage);

            }

            if (other.gameObject.tag == "P2Hassun") //heal
            {
                isDish = true; //for katsuki to check dish

                audioName = "HealSfx";
                SyncAudio();

                numVfx = 4;
                GetVisualEffect();

                view.RPC("P2Heal", RpcTarget.All);

            }

            if (other.gameObject.tag == "Tojangjochi") //stun
            {
                getDamage2 = true;

                isDish = true; //for katsuki to check dish

                numVfx = 5;
                GetVisualEffect();

                audioName = "StunSfx";
                SyncAudio();

                //rmb to add rpc to sync
                bulletDamage2 = 0.0f;
                percentage = 0f;

                GetDamage(bulletDamage2, percentage);

            }

            if (other.gameObject.tag == "RawStinkyTofu") //drop
            {
                getDamage2 = true;

                isDish = true; //for katsuki to check dish

                audioName = "HitSFX";
                SyncAudio();

                numVfx = 6;
                GetVisualEffect();
            }

            if (other.gameObject.tag == "Mukozuke") //pull in
            {
                getDamage2 = true;

                isDish = true; //for katsuki to check dish

                audioName = "PullInSfx";
                SyncAudio();

                bulletDamage2 = 2.0f;
                percentage = (bulletDamage2 * 50f) / 100f;

                GetDamage(bulletDamage2, percentage);

            }

            if (other.gameObject.tag == "BirdNestSoup" || other.gameObject.layer == LayerMask.NameToLayer("DamageArea"))
            {
                getDamage2 = true;

                isDish = true; //for katsuki to check dish

                audioName = "MolotovSfx";
                SyncAudio();

                bulletDamage2 = 2.0f;
                percentage = (bulletDamage2 * 50f) / 100f;

                GetDamage(bulletDamage2, percentage);
            }
            #endregion

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "BirdNestSoup" || other.gameObject.layer == LayerMask.NameToLayer("DamageArea"))
        {
            audioName = "MolotovSfx";
            SyncAudio();

            molotovTimer += Time.deltaTime;

            if (molotovTimer >= 1.0f)
            {
                bulletDamage2 = 1.0f;
                percentage = 0;

                GetDamage(bulletDamage2, percentage);
                molotovTimer = 0;

            }

        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BirdNestSoup" || other.gameObject.layer == LayerMask.NameToLayer("DamageArea"))
        {
            molotovTimer = 0;
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
            audioName = "Wen_GetDamage";
            SyncAudio();

            damage += 0.5f;
        }

        if (sl_newP2Movement.changep2Icon == 3 && !isDish)//k
        {
            audioName = "Katsuki_GetDamage";
            SyncAudio();

            damage -= 0; //if is food, get full damage
        }
        if (sl_newP2Movement.changep2Icon == 3 && isDish)//k
        {
            audioName = "Katsuki_GetDamage";
            SyncAudio();

            damage = damage - percent;
        }



        //from enemy bullet
        if (SL_newP1Movement.changeModelAnim == 0) //enemy brock
        {
            audioName = "Brock_GetDamage";
            SyncAudio();

            damage = damage + percent;
        }

        if (SL_newP1Movement.changeModelAnim == 2)//enemy jiho
        {
            audioName = "Jiho_GetDamage";
            SyncAudio();

            damage = damage - percent;
        }


        view.RPC("BulletDamage2", RpcTarget.All, damage);

    }

    public void GetVisualEffect()
    {
        if (numVfx == 1)//ex
        {
            timeDestroy = 3;
            StartCoroutine(StopVfx(timeDestroy));
            view.RPC("DishVisualEffect2", RpcTarget.All, timeDestroy, numVfx);

        }
        if (numVfx == 2)//kb
        {
            timeDestroy = 1;
            StartCoroutine(StopVfx(timeDestroy));

            view.RPC("DishVisualEffect2", RpcTarget.All, timeDestroy, numVfx);

        }
        if (numVfx == 3)//np
        {
            timeDestroy = 4;
            StartCoroutine(StopVfx(timeDestroy));

            view.RPC("DishVisualEffect2", RpcTarget.All, timeDestroy, numVfx);

        }
        if (numVfx == 4)//heal
        {
            timeDestroy = 4;
            StartCoroutine(StopVfx(timeDestroy));

            view.RPC("DishVisualEffect2", RpcTarget.All, timeDestroy, numVfx);

        }
        if (numVfx == 5)//stun
        {
            timeDestroy = 6;
            StartCoroutine(StopVfx(timeDestroy));
            view.RPC("DishVisualEffect2", RpcTarget.All, timeDestroy, numVfx);

        }
        if (numVfx == 6)//drop
        {
            timeDestroy = 3;
            StartCoroutine(StopVfx(timeDestroy));
            view.RPC("DishVisualEffect2", RpcTarget.All, timeDestroy, numVfx);

        }
    }

    [PunRPC]
    IEnumerator DishVisualEffect2(int timeToDestroy, int n)
    {
        timeDestroy = timeToDestroy;
        numVfx = n;

        if (n == 1)//explode
        {
            for (int i = 0; i < explodeVfx.Length; i++)
            {
                explodeVfx[i].Play();
            }

            yield return new WaitForSeconds(timeToDestroy);

            for (int i = 0; i < explodeVfx.Length; i++)
            {
                if (explodeVfx[i].isPlaying)
                {
                    explodeVfx[i].Stop();
                }

            }
        }

        if (n == 2)//kb
        {
            for (int i = 0; i < knockbackVfx.Length; i++)
            {
                knockbackVfx[i].Play();
            }

            yield return new WaitForSeconds(timeToDestroy);

            for (int i = 0; i < knockbackVfx.Length; i++)
            {
                if (knockbackVfx[i].isPlaying)
                {
                    knockbackVfx[i].Stop();
                }

            }
        }

        if (n == 3)//nopick
        {
            for (int i = 0; i < noPickVfx.Length; i++)
            {
                noPickVfx[i].Play();
            }

            yield return new WaitForSeconds(timeToDestroy);

            for (int i = 0; i < noPickVfx.Length; i++)
            {
                if (noPickVfx[i].isPlaying)
                {
                    noPickVfx[i].Stop();
                }

            }
        }

        if (n == 4)//heal
        {
            for (int i = 0; i < healVfx.Length; i++)
            {
                healVfx[i].Play();
            }

            yield return new WaitForSeconds(timeToDestroy);

            for (int i = 0; i < healVfx.Length; i++)
            {
                if (healVfx[i].isPlaying)
                {
                    healVfx[i].Stop();
                }

            }
        }

        if (n == 5)//stun
        {
            for (int i = 0; i < stunVfx.Length; i++)
            {
                stunVfx[i].Play();
            }

            yield return new WaitForSeconds(timeToDestroy);

            for (int i = 0; i < stunVfx.Length; i++)
            {
                if (stunVfx[i].isPlaying)
                {
                    stunVfx[i].Stop();
                }

            }
        }

        if (n == 6)//drop
        {
            for (int i = 0; i < dropVfx.Length; i++)
            {
                dropVfx[i].Play();
            }

            yield return new WaitForSeconds(timeToDestroy);

            for (int i = 0; i < dropVfx.Length; i++)
            {
                if (dropVfx[i].isPlaying)
                {
                    dropVfx[i].Stop();
                }

            }
        }

    }

    IEnumerator StopVfx(int time)
    {
        yield return new WaitForSeconds(time);
        numVfx = 0;
    }

    IEnumerator StopGetDamage()
    {
        yield return new WaitForSeconds(0.5f);
        getDamage2 = false;
    }

    IEnumerator Player2Dead()
    {
        yield return new WaitForSeconds(3.0f);
        //p2currentHealth = 0;

        //sl_p2InventoryManager.ClearAllInList();
        //PhotonNetwork.Destroy(gameObject);
    }


    [PunRPC]
    public void BulletDamage2(float damage)
    {
        isDish = false; //everytime run this set to false

        if (p2currentHealth > 0)
        {
            p2currentHealth -= damage;

            if (p2currentHealth < 0 && view.IsMine && PhotonNetwork.IsConnected == true)
            {
                FindObjectOfType<sl_AudioManager>().Play("WinScreen");

                player2Dead = true;

            }

        }
    }


    [PunRPC]
    public void P2Health_SFX(string n)
    {
        audioName = n;
        FindObjectOfType<sl_AudioManager>().Play(n);

    }


    [PunRPC]
    public void P2Heal()
    {
        p2currentHealth += 3.0f;
        if (p2currentHealth >= 8.0f)
        {
            p2currentHealth = 8.0f;
        }

    }


    public void SyncAudio()
    {
        view.RPC("P2Health_SFX", RpcTarget.All, audioName);
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