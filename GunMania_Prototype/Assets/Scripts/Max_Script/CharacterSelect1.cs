using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CharacterSelect1 : MonoBehaviour
{

    public RawImage tag1, tag2;
    public static RawImage tag3, tag4;
    public Text desct, descA;


    public RawImage BCPortrait, OWPortrait, KSPortrait, JHPortrait;

    int step = 0;


    struct craftedstuff
    {
        public Text description1, description2;
        public RawImage tag;
    }
    //   0         1            2        3

    craftedstuff BrockChoi, OfficerWen, KatsukiSan, JiHo;


    private void Start()
    {
        BrockChoi.description1.text = "Creates a random superfood. If you have food in inventory, it removes both and gives a random superfood. If there is no food, 1 superfood just appears in the inventory.";
        BrockChoi.description2.text = "Will automatically give the other food needed to combine for a Superfood. For example, if you have an apple and no other food, when you activate his assist, he will give you the cherry needed to combine for your Superfood. ";
        BrockChoi.tag = BCPortrait;

        OfficerWen.description1.text = "Causes Foods/ Superfoods not be able to be thrown or used for 4 secs";
        OfficerWen.description2.text = "The opponent is unable to combine Superfoods for 5 secs";
        OfficerWen.tag = OWPortrait;


        KatsukiSan.description1.text = "When Food/ Superfood hits him, heals a respective amount, depending on what hits him for 5 secs. Cannot throw food at this time but can still pick up and combine. Healing amount is as follows: \n Regular Food = Half a Heart \n Superfood = 1 Heart ";
        KatsukiSan.description2.text = "Foods & Superfoods deal 50% less damage when used for 5 secs";
        KatsukiSan.tag = KSPortrait;


        JiHo.description1.text = "Causes the opponent to be unable to pick up food for 5 seconds";
        JiHo.description2.text = "Disables the opponent’s assist for 5 secs";
        JiHo.tag = JHPortrait;

    }


    public static void changeP2Stuff(RawImage tag1, RawImage tag2)
    {
        tag3 = tag1;
        tag4 = tag2;

    }
    public void changeCharacter(bool isNxt, bool isPrtNr)
    {
        if (isNxt == true && step <= 3)
        {
            step++;
        }
        else if (isNxt == false)
        {
            step--;
        }
        else if (step >= 4)
        {
            step = 0;
        }


        if (step == 0)
        {
            if (!isPrtNr)
            {
                tag1 = BrockChoi.tag;
            }
            else
            {
                tag2 = BrockChoi.tag;
            }
            desct.text = BrockChoi.description1.text;
            descA.text = BrockChoi.description1.text;
        }
        else if (step == 1)
        {
            if (!isPrtNr)
            {
                tag1 = OfficerWen.tag;
            }
            else
            {
                tag2 = OfficerWen.tag;
            }
            desct.text = OfficerWen.description1.text;
            descA.text = OfficerWen.description2.text;

        }
        else if (step == 2)
        {
            if (!isPrtNr)
            {
                tag1 = KatsukiSan.tag;
            }
            else
            {
                tag2 = KatsukiSan.tag;
            }
            desct.text = KatsukiSan.description1.text;
            descA.text = KatsukiSan.description2.text;
        }
        else if (step == 3)
        {
            if (!isPrtNr)
            {
                tag1 = JiHo.tag;

            }
            else
            {
                tag2 = JiHo.tag;
            }
            desct.text = JiHo.description1.text;
            descA.text = JiHo.description2.text;
        }





    }



}

