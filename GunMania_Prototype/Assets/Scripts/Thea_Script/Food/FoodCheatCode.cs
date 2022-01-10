using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCheatCode : MonoBehaviour
{
    [Header("Food Prefabs")]
    public List<GameObject> prefabs;

    [Header("Player")]
    public Transform playerPOS;
    Vector3 offset = new Vector3(0, 2, 10);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Instantiate(prefabs[0], playerPOS.position + offset, Quaternion.identity);
            
        }
        else if (Input.GetKeyDown("2"))
        {
            Instantiate(prefabs[1], playerPOS.position + offset, Quaternion.identity);
            
        }
        else if (Input.GetKeyDown("3"))
        {
            Instantiate(prefabs[2], playerPOS.position + offset, Quaternion.identity);

        }
        else if (Input.GetKeyDown("4"))
        {
            Instantiate(prefabs[3], playerPOS.position + offset, Quaternion.identity);

        }
        else if (Input.GetKeyDown("5"))
        {
            Instantiate(prefabs[4], playerPOS.position + offset, Quaternion.identity);

        }
        else if (Input.GetKeyDown("6"))
        {
            Instantiate(prefabs[5], playerPOS.position + offset, Quaternion.identity);

        }
        else if (Input.GetKeyDown("7"))
        {
            Instantiate(prefabs[6], playerPOS.position + offset, Quaternion.identity);

        }
        else if (Input.GetKeyDown("8"))
        {
            Instantiate(prefabs[7], playerPOS.position + offset, Quaternion.identity);

        }
    }
}
