using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_P2vfx : MonoBehaviour
{
    PhotonView view;

    //To highlight object
    public Renderer mat;

    public Color highlightColor;
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
        if (PhotonNetwork.IsMasterClient)
        {
            if (sl_P2PlayerHealth.getDamage2 == true)
            {
                GetDamage2();
            }
        }


    }

    public void GetDamage2()
    {
        view.RPC("getDamageVFX2", RpcTarget.All);
    }


    [PunRPC]
    IEnumerator getDamageVFX2()
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


}
