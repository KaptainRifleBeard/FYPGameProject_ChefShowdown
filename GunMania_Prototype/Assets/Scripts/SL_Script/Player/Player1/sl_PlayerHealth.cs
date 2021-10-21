using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class sl_PlayerHealth : MonoBehaviour
{
    public Text healthText;
    PhotonView view;

    //HEALTH

    [Space(10)]
    [Header("Health")]
    private int maxHealth = 16;
    public int currentHealth;

    public GameObject bulletScript;
    public GameObject playerHealth;

    public GameObject spawnPostionA;

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
            //gameObject.SetActive(false);
            //StartCoroutine(WaitToSpawnPlayer());

            if (view.IsMine)
            {
                
                //SceneManager.LoadScene("LoseScreen");
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


    //public void SpawnBackPlayer()
    //{
    //    sl_InventoryManager.ClearAllInList();
    //    gameObject.SetActive(true);
    //    gameObject.transform.position = spawnPostionA.transform.position;
    //}

    //IEnumerator WaitToSpawnPlayer()
    //{
    //    yield return new WaitForSeconds(1.0f);
    //    SpawnBackPlayer();
    //    currentHealth = maxHealth;
    //}

    [PunRPC]
    public void BulletDamage()
    {
        currentHealth -= bulletScript.GetComponent<sl_BulletScript>().bulletDmg;
    }


}
