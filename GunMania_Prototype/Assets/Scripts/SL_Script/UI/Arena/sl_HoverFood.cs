using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class sl_HoverFood : MonoBehaviour
{
    public Image icon;

    public Sprite fooSprite;
    public Sprite transparentSprite;

    public GameObject foodObject;

    private void Start()
    {
        foodObject.SetActive(false);
        icon.sprite = transparentSprite;
    }

    private void OnMouseOver()
    {
        foodObject.SetActive(true);
        icon.sprite = fooSprite;
    }

    private void OnMouseExit()
    {
        foodObject.SetActive(false);
        icon.sprite = transparentSprite;

    }
}
