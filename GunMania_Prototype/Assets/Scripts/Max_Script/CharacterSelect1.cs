using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon;



public class CharacterSelect1 : MonoBehaviour
{

    [Header("Internal Assets place all art assets and required stuff in this order: BrockChoi , Oficer Wen, Mr Katsuki, Aunt Ji Ho")]
    public GameObject[] tag1Characters;
    public GameObject[] tag2Characters;
    public GameObject[] characterSheetStuff;    
    
    
    public int selectedCharacter = 0;

    public bool tagPartner = false;

    int character1, character2;


    public void Awake()
    {
        PlayerPrefs.SetInt("Character1", -1);
        PlayerPrefs.SetInt("Character2", -1);
    }

    // insert 4 place holders into each target object.

    public void NextCharacter()
    {

        if (tagPartner)
        {
            tag1Characters[selectedCharacter].SetActive(false);
            characterSheetStuff[selectedCharacter].SetActive(false);
            selectedCharacter = (selectedCharacter + 1) % tag1Characters.Length;
            tag1Characters[selectedCharacter].SetActive(true);
            characterSheetStuff[selectedCharacter].SetActive(true);
        }
        else
        {
            tag2Characters[selectedCharacter].SetActive(false);
            characterSheetStuff[selectedCharacter].SetActive(false);
            selectedCharacter = (selectedCharacter + 1) % tag2Characters.Length;
            tag2Characters[selectedCharacter].SetActive(true);
            characterSheetStuff[selectedCharacter].SetActive(true);
        }

    }
    public void PreviousCharacter()
    {

        selectedCharacter--;
        if (selectedCharacter<0)
        {
            selectedCharacter += tag2Characters.Length;
        }

        if (tagPartner)
        {
            tag1Characters[selectedCharacter].SetActive(false);
            characterSheetStuff[selectedCharacter].SetActive(false);
            selectedCharacter = (selectedCharacter + 1) % tag1Characters.Length;
            tag1Characters[selectedCharacter].SetActive(true);
            characterSheetStuff[selectedCharacter].SetActive(true);

        }
        else
        {
            tag2Characters[selectedCharacter].SetActive(false);
            characterSheetStuff[selectedCharacter].SetActive(false);
            selectedCharacter = (selectedCharacter + 1) % tag2Characters.Length;
            tag2Characters[selectedCharacter].SetActive(true);
            characterSheetStuff[selectedCharacter].SetActive(true);
        }
    }

    public void ConfirmChoice()
    {
        if (tagPartner == false)
        {
            tagPartner = true;
            character1 = selectedCharacter;
            selectedCharacter = 1;
        }
        else if (tagPartner == true)
        {
            character2 = selectedCharacter;
            StartGame();
        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("Character1", character1);
        PlayerPrefs.SetInt("Character2", character2);
        SceneManager.LoadScene("WaitingLobby");


    }






}

