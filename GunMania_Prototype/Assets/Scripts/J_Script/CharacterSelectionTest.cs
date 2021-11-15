using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionTest : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject[] charactersDes;
    public int selectedCharacter = 0;

    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        charactersDes[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
        charactersDes[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        charactersDes[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
        charactersDes[selectedCharacter].SetActive(true);
    }

    public void Confirm()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        Debug.Log ("Character Selected");
    }
}
