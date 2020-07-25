using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public string newGameScene;
    public string loadGameScene;

    public GameObject continueButton;
    public GameObject exitButton;

    public bool mainMenu;

    public static MainMenu instance;

    //public GameObject statsUI;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = true;

        instance = this;

        if (PlayerPrefs.HasKey("Current_Scene"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if(exitButton.activeInHierarchy)
        {
        }
        else
        {
            mainMenu = false;

        }
        //statsUI.SetActive(false);
        //GameMenu.instance.CloseMenu();*/

    }

    public void Continue()
    {
        SceneManager.LoadScene(loadGameScene);

        //GameManager.instance.LoadData();
        //QuestManager.instance.LoadQuestData();
        mainMenu = false;

    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
        mainMenu = false;

    }

    public void Exit()
    {
        Application.Quit();

    }
}
