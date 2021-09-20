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
            Debug.Log("apple");
        }
        else if (Input.GetKeyDown("2"))
        {
            Instantiate(prefabs[1], playerPOS.position + offset, Quaternion.identity);
            Debug.Log("cherry");
        }
    }
}
