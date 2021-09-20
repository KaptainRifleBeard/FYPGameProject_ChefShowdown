using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public Image levelView;

    public Image level1, level2, level3, levelq;

    public Text levelName;

    Camera cam;

    public static void changeScene (int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);     
    }


    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray,out hit))
        {
            if (hit.collider != null && hit.collider.tag == "LevelSceneButton")
            {
                hit.collider.gameObject.GetComponent(LevelButtonValue.FindObjectOfType(int LevelVal));
            }
        }
    }
}
