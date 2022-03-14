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
    float percentage;
    bool isDish;
    float molotovTimer;

    public static bool getDamage;
    public static bool playerDead;

    //dish vfx
    [Space(10)]
    [Header("Particle vfx")]
    public ParticleSystem[] healVfx;
    public ParticleSystem[] knockbackVfx;
    public ParticleSystem[] explodeVfx;
    public ParticleSystem[] dropVfx;
    public ParticleSystem[] noPickVfx;
    public ParticleSystem[] stunVfx;

    public GameObject newNoPickVfx;

    int numVfx;
    int timeDestroy;

    //AUDIO
    string audioName;

    //public AudioSource hitAudio;

    void Start()
    {
        view = GetComponent<PhotonView>();
        currentHealth = maxHealth;

        isDish = false;
        getDamage = false;
        playerDead = false;
        newNoPickVfx.SetActive(false);

        numVfx = 0; //no effect
    }

    public void Update()
    {
        if (currentHealth <= 0 || sl_MatchCountdown.timeRemaining == 0)
        {
            playerDead = true;
            StartCoroutine(PlayerDead());
        }

        if(sl_RematchAndLeave.rematchCount == 2)
        {
            playerDead = false;
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
            getDamage = true;

            Destroy(other.gameObject);

        }

        if (PhotonNetwork.IsMasterClient) //make sure it run only once
        {
            if (other.gameObject.tag == "WaterSpray")
            {
                getDamage = true;

                float waterDamage;
                waterDamage = 1;
                view.RPC("BulletDamage", RpcTarget.All, waterDamage);

                StartCoroutine(StopGetDamage());
            }


            if (other.gameObject.tag == "Cat")
            {
                getDamage = true;

                audioName = "CatHit";
                SyncAudio();

                bulletDamage = 0.5f;
                percentage = (bulletDamage * 50f) / 100f;

                GetDamage(bulletDamage, percentage);
            }

            if (other.gameObject.tag == "Dog")
            {
                getDamage = true;

                audioName = "DogHit";
                SyncAudio();

                bulletDamage = 0.5f;
                percentage = (bulletDamage * 50f) / 100f;

                GetDamage(bulletDamage, percentage);
            }

            //*****bullets
            if (other.gameObject.tag == "P2Bullet")
            {
                getDamage = true;

                audioName = "HitSFX";
                SyncAudio();

                isDish = false;
                bulletDamage = 1.0f; //original
                percentage = (bulletDamage * 50f) / 100f;

                GetDamage(bulletDamage, percentage);
            }

            //DISHES
            #region
            if (other.gameObject.tag == "P2Sinseollo")//explode
            {
                getDamage = true;

                isDish = true; //for katsuki to check dish

                numVfx = 1;
                GetVisualEffect();

                audioName = "ExplodeSfx";
                SyncAudio();

                bulletDamage = 3f; 
                percentage = (bulletDamage * 50f) / 100f;

                GetDamage(bulletDamage, percentage);

            }

            if (other.gameObject.tag == "P2FoxtailMillet")//kb
            {
                getDamage = true;

                isDish = true; //for katsuki to check dish

                numVfx = 2;
                GetVisualEffect();

                audioName = "KnockbackSfx";
                SyncAudio();

                bulletDamage = 2.0f;
                percentage = (bulletDamage * 50f) / 100f;

                GetDamage(bulletDamage, percentage);

            }

            if (other.gameObject.tag == "P2BuddhaJumpsOvertheWall")//no pick
            {
                getDamage = true;

                isDish = true; //for katsuki to check dish

                numVfx = 3;
                GetVisualEffect();

                audioName = "HitSFX";
                SyncAudio();

                bulletDamage = 2.0f;
                percentage = (bulletDamage * 50f) / 100f;

                GetDamage(bulletDamage, percentage);

            }

            if (other.gameObject.tag == "Hassun") //heal
            {
                isDish = true; //for katsuki to check dish

                audioName = "HealSfx";
                SyncAudio();

                numVfx = 4;
                GetVisualEffect();

                view.RPC("P1Heal", RpcTarget.All);

            }

            if (other.gameObject.tag == "P2Tojangjochi") //stun
            {
                getDamage = true;

                isDish = true; //for katsuki to check dish

                numVfx = 5;
                GetVisualEffect();

                audioName = "StunSfx";
                SyncAudio();

                //rmb to add rpc to sync
                bulletDamage = 0.0f;
                percentage = 0f;

                GetDamage(bulletDamage, percentage);

            }

            if (other.gameObject.tag == "P2RawStinkyTofu") //drop
            {
                isDish = true; //for katsuki to check dish

                audioName = "HitSFX";
                SyncAudio();

                numVfx = 6;
                GetVisualEffect();
            }

            if (other.gameObject.tag == "P2Mukozuke") //pull in
            {
                getDamage = true;

                isDish = true; //for katsuki to check dish

                audioName = "PullInSfx";
                SyncAudio();

                bulletDamage = 2.0f;
                percentage = (bulletDamage * 50f) / 100f;

                GetDamage(bulletDamage, percentage);

            }

            if (other.gameObject.tag == "P2BirdNestSoup" || other.gameObject.layer == LayerMask.NameToLayer("DamageArea")) //molotov
            {
                getDamage = true;

                isDish = true; //for katsuki to check dish

                audioName = "MolotovSfx";
                SyncAudio();

                bulletDamage = 2.0f;
                percentage = (bulletDamage * 50f) / 100f;

                GetDamage(bulletDamage, percentage);
            }
            #endregion

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "P2BirdNestSoup" || other.gameObject.layer == LayerMask.NameToLayer("DamageArea"))
        {
            audioName = "MolotovSfx";
            SyncAudio();

            molotovTimer += Time.deltaTime;

            if (molotovTimer >= 1.0f)
            {
                bulletDamage = 1.0f;
                percentage = 0;

                GetDamage(bulletDamage, percentage);
                molotovTimer = 0;

            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "P2BirdNestSoup" || other.gameObject.layer == LayerMask.NameToLayer("DamageArea"))
        {
            molotovTimer = 0;
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
        katsuki - dish take less 50% damage from everyone, speed-30

        */

        //for my model
        if (SL_newP1Movement.changeModelAnim == 1) //w
        {
            audioName = "Wen_GetDamage";
            SyncAudio();

            damage += 0.5f;
        }

        if (SL_newP1Movement.changeModelAnim == 3 && !isDish)//k
        {
            audioName = "Katsuki_GetDamage";
            SyncAudio();

            damage -= 0; //if is food, get full damage
        }
        if (SL_newP1Movement.changeModelAnim == 3 && isDish)//k
        {
            audioName = "Katsuki_GetDamage";
            SyncAudio();

            damage = damage - percent;
        }



        //from enemy bullet
        if (sl_newP2Movement.changep2Icon == 0) //enemy brock
        {
            audioName = "Brock_GetDamage";
            SyncAudio();

            damage = damage + percent;
        }

        if (sl_newP2Movement.changep2Icon == 2)//enemy jiho
        {
            audioName = "Jiho_GetDamage";
            SyncAudio();


            damage = damage - percent;
        }
        view.RPC("BulletDamage", RpcTarget.All, damage);

    }

    public void GetVisualEffect()
    {
        if (numVfx == 1)//ex
        {
            timeDestroy = 3;
            StartCoroutine(StopVfx(timeDestroy));
            view.RPC("DishVisualEffect", RpcTarget.All, timeDestroy, numVfx);

        }
        if (numVfx == 2)//kb
        {
            timeDestroy = 1;
            StartCoroutine(StopVfx(timeDestroy));

            view.RPC("DishVisualEffect", RpcTarget.All, timeDestroy, numVfx);

        }
        if (numVfx == 3)//np
        {
            timeDestroy = 4;
            StartCoroutine(StopVfx(timeDestroy));

            view.RPC("DishVisualEffect", RpcTarget.All, timeDestroy, numVfx);

        }
        if (numVfx == 4)//heal
        {
            timeDestroy = 2;
            StartCoroutine(StopVfx(timeDestroy));

            view.RPC("DishVisualEffect", RpcTarget.All, timeDestroy, numVfx);

        }
        if (numVfx == 5)//stun
        {
            timeDestroy = 6;
            StartCoroutine(StopVfx(timeDestroy));
            view.RPC("DishVisualEffect", RpcTarget.All, timeDestroy, numVfx);

        }
        if (numVfx == 6)//drop
        {
            timeDestroy = 3;
            StartCoroutine(StopVfx(timeDestroy));
            view.RPC("DishVisualEffect", RpcTarget.All, timeDestroy, numVfx);

        }
    }

    [PunRPC]
    IEnumerator DishVisualEffect(int timeToDestroy, int n)
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
            newNoPickVfx.SetActive(true);
            //for (int i = 0; i < noPickVfx.Length; i++)
            //{
            //    noPickVfx[i].Play();
            //}

            //yield return new WaitForSeconds(timeToDestroy);

            //for (int i = 0; i < noPickVfx.Length; i++)
            //{
            //    if (noPickVfx[i].isPlaying)
            //    {
            //        noPickVfx[i].Stop();
            //    }

            //}
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

    IEnumerator StopGetDamage()
    {
        yield return new WaitForSeconds(1.0f);
        getDamage = false;
    }

    IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(1.0f);
        playerDead = false;

        //currentHealth = 0;

        //sl_InventoryManager.ClearAllInList();
        //PhotonNetwork.Destroy(gameObject);
    }

    IEnumerator StopVfx(int time)
    {
        yield return new WaitForSeconds(time);
        newNoPickVfx.SetActive(false);

        numVfx = 0;
    }

    [PunRPC]
    public void BulletDamage(float damage)
    {
        isDish = false; //everytime run this set to false
        if (currentHealth > 0)
        {
            //currentHealth -= 0.5f; //because it run 2 times, so i cut it half
            currentHealth -= damage;

            if (currentHealth < 0 && view.IsMine && PhotonNetwork.IsConnected == true)
            {
                FindObjectOfType<sl_AudioManager>().Play("WinScreen");

                playerDead = true;
            }

        }

    }

    [PunRPC]
    public void P1Health_SFX(string n)
    {
        audioName = n;
        FindObjectOfType<sl_AudioManager>().Play(n);

    }

    [PunRPC]
    public void P1Heal()
    {
        currentHealth += 3.0f;
        if (currentHealth >= 8.0f)
        {
            currentHealth = 8.0f;
        }

    }

    public void SyncAudio()
    {
        view.RPC("P1Health_SFX", RpcTarget.All, audioName);
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