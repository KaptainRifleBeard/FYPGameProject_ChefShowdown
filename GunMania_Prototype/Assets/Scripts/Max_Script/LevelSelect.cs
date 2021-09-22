using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public Image levelView;


    // will need to link all relevant images to these vars.
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
        int a = 0 ;
        //LevelButtonValue temp;

        if (Physics.Raycast(ray,out hit))
        {
            if (hit.collider != null && hit.collider.tag == "LevelSceneButton")
            {
                var selection = hit.transform;
                var selectionObject = selection.GetComponent<LevelButtonValue>();
                a = selectionObject.getVal();
            }
        }

        if (a == 1)
        {
            levelView = level1;
            levelName.text = "Level 1";
        }
        else if (a == 2)
        {
            levelView = level2;
            levelName.text = "Level 2";
        }
        else if (a == 3)
        {
            levelView = level3;
            levelName.text = "Level 3";
        }
        else if (a == 4)
        {
            levelView = levelq;
            levelName.text = "Level ?";
        }

    }
}
