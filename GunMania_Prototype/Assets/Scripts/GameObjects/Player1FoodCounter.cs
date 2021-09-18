using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player1FoodCounter : MonoBehaviour
{

    public GameObject player1;
    private int FoodCount;
    public Text foodText;

    // Update is called once per frame
    void Update()
    {
        FoodCount = player1.GetComponent<PlayerBehaviour>().FoodCarried;
        foodText.text = FoodCount.ToString();
    }
}
