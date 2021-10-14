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
    private int maxHealth = 16;
    public int p2currentHealth;

    public GameObject bulletScript;
    public GameObject playerHealth;

    public GameObject spawnPostionB;

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
            stream.SendNext(p2currentHealth);
            //stream.SendNext(transform.position);
        }
        else if (stream.IsReading)
        {
            p2currentHealth = (int)stream.ReceiveNext();
            //transform.position = (Vector3)stream.ReceiveNext();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            view.RPC("BulletDamage2", RpcTarget.All);

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

    [PunRPC]
    public void BulletDamage2()
    {
        p2currentHealth -= bulletScript.GetComponent<sl_BulletScript>().bulletDmg;
    }

}
