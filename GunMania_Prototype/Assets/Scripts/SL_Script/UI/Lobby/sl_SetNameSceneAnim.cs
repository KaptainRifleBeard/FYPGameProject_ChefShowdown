using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_SetNameSceneAnim : MonoBehaviour
{
    public GameObject nicknameUI;
    public GameObject creditScreenUI;

    public Animator nameInputAnim;
    public Animator creditAnim;

    public void ShowNameInputfield()
    {
        StartCoroutine(Open_NameInput());

    }


    IEnumerator Open_NameInput()
    {
        yield return new WaitForSeconds(0.2f);
        nicknameUI.SetActive(true);

        nameInputAnim.SetBool("ShowNameInput", true);

    }

}
