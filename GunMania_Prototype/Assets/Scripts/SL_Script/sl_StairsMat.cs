using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sl_StairsMat : MonoBehaviour
{
    public MeshRenderer mat;

    public Color changeColor;
    public List<Color> originalColor;

    void Start()
    {
        for (int i = 0; i < mat.materials.Length; i++)
        {
            originalColor.Add(mat.materials[i].color);
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            Debug.Log("collide");
            for (int i = 0; i < mat.materials.Length; i++)
            {
                mat.materials[0].color = changeColor;

            }

        }
    }

    public void OnTriggerExit(Collider collision)
    {
        for (int i = 0; i < mat.materials.Length; i++)
        {
            mat.materials[0].color = originalColor[0];
        }

    }

}
