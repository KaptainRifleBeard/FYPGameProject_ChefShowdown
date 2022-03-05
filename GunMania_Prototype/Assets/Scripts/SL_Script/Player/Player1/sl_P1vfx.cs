using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_P1vfx : MonoBehaviour
{
    PhotonView view;

    //To highlight object
    public Renderer mat;

    public Color highlightColor;
    public Color stunColor;

    public List<Color> defaultColor;

    void Start()
    {
        view = GetComponent<PhotonView>();

        for (int i = 0; i < mat.materials.Length; i++)
        {
            defaultColor.Add(mat.materials[i].color);
        }
    }

    void Update()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if (sl_PlayerHealth.getDamage == true)
            {
                GetDamage();
            }
            if (sl_PlayerHealth.freezePlayer == true)
            {
                GetStundDamage();
            }
        }


    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.layer == LayerMask.NameToLayer("DamageArea"))
    //    {
    //        Debug.Log("collide with bird");
    //        GetDamage();
    //    }
    //}


    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("DamageArea"))
    //    {
    //        GetDamage();
    //    }
    //}

    public void GetDamage()
    {
        view.RPC("getDamageVFX", RpcTarget.All);
    }


    public void GetStundDamage()
    {
        view.RPC("stunVFX", RpcTarget.All);
    }

    [PunRPC]
    IEnumerator getDamageVFX()
    {
        for (int n = 0; n < 2; n++)
        {
            for (int i = 0; i < mat.materials.Length; i++)
            {
                mat.materials[i].color = highlightColor;
            }
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < mat.materials.Length; i++)
            {
                mat.materials[i].color = defaultColor[i];
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    [PunRPC]
    IEnumerator stunVFX()
    {
        for (int n = 0; n < 6; n++)
        {
            for (int i = 0; i < mat.materials.Length; i++)
            {
                mat.materials[i].color = stunColor;
            }
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < mat.materials.Length; i++)
            {
                mat.materials[i].color = defaultColor[i];
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

}
