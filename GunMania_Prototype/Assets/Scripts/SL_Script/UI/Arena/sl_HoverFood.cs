using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class sl_HoverFood : MonoBehaviour
{
    public Image icon;

    public Sprite fooSprite;
    public Sprite transparentSprite;

    private void OnMouseOver()
    {
        icon.sprite = fooSprite;
    }

    private void OnMouseExit()
    {
        icon.sprite = transparentSprite;

    }
}
