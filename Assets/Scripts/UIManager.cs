using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Slider healthBar, manaBar;
    public Text HPText, MPText, levelText, activeObjective;

    public GameObject statsUI;

    public static UIManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {



        if (Shop.instance.shopMenu.activeInHierarchy || GameMenu.instance.theMenu.activeInHierarchy || MainMenu.instance.mainMenu == true)
        {
            statsUI.SetActive(false);

        }
        else
        {
            statsUI.SetActive(true);
        }
        



        healthBar.maxValue = PlayerStats.instance.maxHP;
        healthBar.value = PlayerStats.instance.currentHP;

        manaBar.maxValue = PlayerStats.instance.maxMP;
        manaBar.value = PlayerStats.instance.currentMP;

        HPText.text = "HP: " + PlayerStats.instance.currentHP + "/" + PlayerStats.instance.maxHP;
        MPText.text = "MP: " + PlayerStats.instance.currentMP + "/" + PlayerStats.instance.maxMP;
        levelText.text = "Level: " + PlayerStats.instance.playerLevel;

        activeObjective.text = PlayerController.instance.activeQuest;

    }
}
