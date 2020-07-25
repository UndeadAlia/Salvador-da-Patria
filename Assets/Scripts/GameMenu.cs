using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{





    public GameObject theMenu;



    public GameObject[] windows;

    public Text activeObjective;

    public Text levelValue, mpValue, hpValue, curExp, expTo, strValue, defValue,nameText, goldValue;
    public Slider expSlider;
    public Image charImage;

    public GameObject charStatHolder;


    private PlayerStats playerStats;

    public ItemButton[] itemButtons;
    public string selectedItem;
    public Item activeItem;
    //public int activeItemNumber;
    public Text itemName, itemDescription, useButtonText;
    public static GameMenu instance;

    public Text equippedWpnName, equippedArmorName, equippedShieldName;
    public Image equippedWpnSprite, equippedArmorSprite, equippedShieldSprite;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //weapon = 
    }

    // Update is called once per frame
    void Update()
    {
        charStatHolder = GameObject.FindGameObjectWithTag("Player");
        //activeItemNumber

        if (activeItem == null)
        {
            itemName.text = " ";
            itemDescription.text = " ";
        }


        windows[1].SetActive(true);
        equippedArmorName.text = PlayerStats.instance.equippedArmor;
        equippedWpnName.text = PlayerStats.instance.equippedWpn;
        equippedShieldName.text = PlayerStats.instance.equippedShield;

        if (PlayerStats.instance.equippedShieldSprite != null)
        {
            equippedShieldSprite.sprite = PlayerStats.instance.equippedShieldSprite;
            equippedShieldSprite.gameObject.SetActive(true);
        }
        else
        {
            equippedShieldSprite.gameObject.SetActive(false);
        }
        if (PlayerStats.instance.equippedArmorSprite != null)
        {
            
            equippedArmorSprite.sprite = PlayerStats.instance.equippedArmorSprite;
            equippedArmorSprite.gameObject.SetActive(true);
        }
        else
        {
            equippedArmorSprite.gameObject.SetActive(false);
        }
        if (PlayerStats.instance.equippedWpnSprite != null)
        {
            equippedWpnSprite.sprite = PlayerStats.instance.equippedWpnSprite;
            equippedWpnSprite.gameObject.SetActive(true);
            //weapon.SetActive(true);

        }
        else
        {
            equippedWpnSprite.gameObject.SetActive(false);
            //weapon.SetActive(false);
        }
        if(windows[2].activeInHierarchy)
        {
            activeObjective.text = PlayerController.instance.activeQuest;

        }




        UpdateMainStats();
        if (Input.GetButtonDown("ESC"))
        {
            if(theMenu.activeInHierarchy)
            {
                //theMenu.SetActive(false);
                //GameManager.instance.gameMenuOpen = false;
                CloseMenu();
                
            }
            else
            {
                theMenu.SetActive(true);
                ShowItems();
            
                GameManager.instance.gameMenuOpen = true;
                windows[1].SetActive(true);
            }
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;
        
       
            if(playerStats.gameObject.activeInHierarchy)
            {
                charStatHolder.SetActive(true);

                nameText.text = playerStats.pName;
                hpValue.text = " " + playerStats.currentHP;
                mpValue.text = " " + playerStats.currentMP;
                strValue.text = " " + playerStats.strength;
                defValue.text = " " + playerStats.defence;
                curExp.text = " " + playerStats.currentEXP;
                expTo.text = " " + playerStats.expToNextLevel;
                levelValue.text = " " + playerStats.playerLevel;
                expSlider.maxValue = playerStats.expToNextLevel[playerStats.playerLevel];
                expSlider.value = playerStats.currentEXP;
                charImage.sprite = playerStats.charImage;
                goldValue.text = GameManager.instance.currentGold.ToString() + "g";
            }
            else
            {
                charStatHolder.SetActive(false);
            }
        }

        
    


    public void ToggleWindow(int windowNumber)
    {
        
        for (int i = 0; i < windows.Length; i++)
        {
            if(i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);

            }
            if (windows[0].activeInHierarchy)
            {
                ShowItems();
                itemButtons[0].Press();
            }
        }
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
        theMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;

    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();

        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;
        
            if (GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }
    public void SelectItem(Item newItem)
    {
        activeItem = newItem;

        if (activeItem.isConsumable)
        {
            useButtonText.text = "Use";
        }

        if (activeItem.isWeapon || activeItem.isArmor)
        {
            useButtonText.text = "Equip";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.itemDescription;
        
        if(activeItem == null)
        {
            itemName.text = " ";
            itemDescription.text = " ";
        }

        
    }

    public void DiscardItem()
    {
        if (activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void UseItem(int selectChar)
    {
        activeItem.Use();
        //if(GameManager.instance.numberOfItems[activeItem] <= 0)
        //{

        //}
        //activeItem = null;
        /*for(int i = 0; i < GameManager.instance.numberOfItems.Length; i++)
        {
            if (GameManager.instance.numberOfItems[i] == 0)
            {
                itemName.text = " ";
                itemDescription.text = " ";
            }
        }*/


    }


}