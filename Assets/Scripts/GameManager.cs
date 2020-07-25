using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public PlayerStats playerStats;

    public bool dialogActive, fadingBetweenAreas, gameMenuOpen, shopActive;

    //shopActive, battleActive;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    public int currentGold;

    // Use this for initialization
    void Start() {



        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        SortItems();
    }

    // Update is called once per frame
    void Update()
    {
        playerStats = FindObjectOfType<PlayerStats>();

        if ( dialogActive || fadingBetweenAreas || gameMenuOpen || shopActive)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }




        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadData();
        }


        /*if (Input.GetKeyDown(KeyCode.Y))
        {
            AddItem("Health Potion");
            AddItem("Blabla");


            RemoveItem("Mana Potion");
            RemoveItem("Mana Poçaozona");
        }*/


    }
    public Item GetItemDetails(string itemToGrab)
    {
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if (referenceItems[i].itemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }
        return null;
    }
    public void SortItems()
    {
        bool itemAfterSpace = true;

        while (itemAfterSpace)
        {
            itemAfterSpace = false;


            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";
                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if(itemsHeld[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }


    public void AddItem(string itemToAdd)
    {
        int newItemPosition = 0;
        bool foundSpace = false;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
            {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;
            }
        }

        if (foundSpace)
        {
            bool itemExists = false;

            for(int i = 0; i < referenceItems.Length; i++)
            {
                if(referenceItems[i].itemName == itemToAdd)
                {
                    itemExists = true;

                    i = referenceItems.Length;
                }
            }
            if (itemExists)
            {
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition]++;
            }
            else
            {
                
                //Debug.LogError(itemToAdd + " Does not Exist!!");
            }

        }

        GameMenu.instance.ShowItems();

    }
    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == itemToRemove)
            {
                foundItem = true;
                itemPosition = i;

                i = itemsHeld.Length;


            }

        }

        if (foundItem)
        {
            numberOfItems[itemPosition]--;

            if(numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
                GameMenu.instance.activeItem = null;


            }

            GameMenu.instance.ShowItems();
        }
        else
        {
            Debug.LogError(" Could not Find " + itemToRemove);
            GameMenu.instance.activeItem = null;
        }
    }


    public void SaveData()
    {

        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetString("Active_Quest", PlayerController.instance.activeQuest);


        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.instance.transform.position.z);
        //save character info
        
            if (playerStats.gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerStats.pName + "_active", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Player_" + playerStats.pName + "_active", 0);
            }

            PlayerPrefs.SetInt("Player_" + playerStats.pName + "_Level", playerStats.playerLevel);
            PlayerPrefs.SetInt("Player_" + playerStats.pName + "_CurrentExp", playerStats.currentEXP);
            PlayerPrefs.SetInt("Player_" + playerStats.pName + "_CurrentHP", playerStats.currentHP);
            PlayerPrefs.SetInt("Player_" + playerStats.pName + "_MaxHP", playerStats.maxHP);
            PlayerPrefs.SetInt("Player_" + playerStats.pName + "_CurrentMP", playerStats.currentMP);
            PlayerPrefs.SetInt("Player_" + playerStats.pName + "_MaxMP", playerStats.maxMP);
            PlayerPrefs.SetInt("Player_" + playerStats.pName + "_Strength", playerStats.strength);
            PlayerPrefs.SetInt("Player_" + playerStats.pName + "_BaseStrength", playerStats.baseStrength);

            PlayerPrefs.SetInt("Player_" + playerStats.pName + "_Defence", playerStats.defence);
            PlayerPrefs.SetInt("Player_" + playerStats.pName + "_BaseDefence", playerStats.baseDefence);

            PlayerPrefs.SetInt("Player_" + playerStats.pName + "_WpnPwr", playerStats.wpnPwr);
            PlayerPrefs.SetInt("Player_" + playerStats.pName + "_ArmrPwr", playerStats.armorPwr);
            PlayerPrefs.SetInt("Player_" + playerStats.pName + "_ShieldPwr", playerStats.shieldPwr);


            PlayerPrefs.SetString("Player_" + playerStats.pName + "_EquippedWpn", playerStats.equippedWpn);
            PlayerPrefs.SetString("Player_" + playerStats.pName + "_EquippedArmr", playerStats.equippedArmor);
            PlayerPrefs.SetString("Player_" + playerStats.pName + "_EquippedShield", playerStats.equippedShield);





        
        //store inventory data
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefs.SetString("ItemInInventory_" + i, itemsHeld[i]);
            PlayerPrefs.SetInt("ItemAmount_" + i, numberOfItems[i]);
        }



    }




    public void LoadData()
    {
        PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));

        //PlayerPrefs.SetString("Active_Quest", PlayerController.instance.activeQuest);
        PlayerController.instance.activeQuest = PlayerPrefs.GetString("Active_Quest");
        /*if (PlayerPrefs.GetInt("Player_" + playerStats.pName + "_active") == 0)
        {
            playerStats.gameObject.SetActive(false);
        }
        else
        {
            playerStats.gameObject.SetActive(true);
        }*/

        playerStats.playerLevel = PlayerPrefs.GetInt("Player_" + playerStats.pName + "_Level");
        playerStats.currentEXP = PlayerPrefs.GetInt("Player_" + playerStats.pName + "_CurrentExp");
        playerStats.currentHP = PlayerPrefs.GetInt("Player_" + playerStats.pName + "_CurrentHP");
        playerStats.maxHP = PlayerPrefs.GetInt("Player_" + playerStats.pName + "_MaxHP");
        playerStats.currentMP = PlayerPrefs.GetInt("Player_" + playerStats.pName + "_CurrentMP");
        playerStats.maxMP = PlayerPrefs.GetInt("Player_" + playerStats.pName + "_MaxMP");
        playerStats.strength = PlayerPrefs.GetInt("Player_" + playerStats.pName + "_Strength");
        playerStats.defence = PlayerPrefs.GetInt("Player_" + playerStats.pName + "_Defence");
        playerStats.wpnPwr = PlayerPrefs.GetInt("Player_" + playerStats.pName + "_WpnPwr");
        playerStats.armorPwr = PlayerPrefs.GetInt("Player_" + playerStats.pName + "_ArmrPwr");
        playerStats.shieldPwr = PlayerPrefs.GetInt("Player_" + playerStats.pName + "_ShieldPwr");

        playerStats.equippedWpn = PlayerPrefs.GetString("Player_" + playerStats.pName + "_EquippedWpn");
        playerStats.equippedArmor = PlayerPrefs.GetString("Player_" + playerStats.pName + "_EquippedArmr");
        playerStats.equippedShield = PlayerPrefs.GetString("Player_" + playerStats.pName + "_EquippedShield");


        for (int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
            numberOfItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
        }

    }

    public void QuitGame()
    {
        UIManager.instance.statsUI.SetActive(false);
        GameMenu.instance.CloseMenu();
        AudioManager.instance.StopMusic();

        SceneManager.LoadScene("MainMenu");

        //Destroy(PlayerController.instance.gameObject);
        UIManager.instance.statsUI.SetActive(false);
    }




}
   
    /*

    

    public void LoadData()
    {

        
        for(int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
            numberOfItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
        }
    }*/
