using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static PlayerStats instance;



    [Header("Player Info")]

    public string pName;
    public Sprite charImage;

    [Header("Player Level")]

    public int playerLevel = 1;
    public int maxLevel = 100;
    
    [Header("Player Experience")]

    public int currentEXP;
    public int[] expToNextLevel;
    public int baseEXP = 1000;

    [Header("Player Stats")]

    public int currentHP;
    public int currentMP;
    public int maxHP;
    public int maxMP;
    public int strength;
    public int baseDefence;
    public int defence;
    public int baseStrength;

    [Header("Player Weapon Stats")]

    public int wpnPwr;
    public int armorPwr;
    public int shieldPwr;

    public string equippedWpn;
    public string equippedArmor;
    public string equippedShield;

    [Header("Weapon Sprites")]
    public Sprite equippedWpnSprite;
    public Sprite equippedArmorSprite;
    public Sprite equippedShieldSprite;

    [Header("Player Effects")]

    public bool flashActive;
    public float flashLenght;
    public float flashCounter;
    private SpriteRenderer playerSprite;
    public GameObject weapon;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        playerSprite = GetComponent<SpriteRenderer>();
        equippedWpn = null;
        
        maxHP = 100;
        currentHP = maxHP;
        currentMP = maxMP;
        maxLevel = 100;
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        for(int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }
    }

    // Update is called once per frame
    void Update()

    {
        //weapon = GameObject.FindGameObjectWithTag("Weapon");

        defence = baseDefence + armorPwr + shieldPwr;
        strength = baseStrength + wpnPwr;

        if (flashActive)
        {

            if(flashCounter > flashLenght * .66f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);

            }
            else if (flashCounter > flashLenght * .33f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);

            }else if(flashCounter > 0)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);

            }else




            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;
        }



        if(currentHP <= 0)
        {
            gameObject.SetActive(false);
        }
        
        if(equippedWpn == null)
        {
            weapon.SetActive(false);
        }
        else
        {
            weapon.SetActive(true);

        }

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddExp(500);
        }
    }

    public void AddExp(int expToAdd)
    {
        currentEXP += expToAdd;

        if(currentEXP > expToNextLevel[playerLevel])
        {
            currentEXP -= expToNextLevel[playerLevel];

            playerLevel++;


            maxHP = maxHP + 20;
            maxMP = maxMP + 10;

            currentHP = maxHP;
            currentMP = maxMP;

            baseStrength = baseStrength + 2;
            baseDefence = baseDefence + 1;

            //strenght = strenght + 1.5;

            //statss


        }




    }


    public void HurtPlayer(int damageToGive)
    {
        currentHP -= damageToGive;
        //Destroy(gameObject);
        flashActive = true;
        flashCounter = flashLenght;

    }

    public void SetMaxHealth()
    {
        currentHP = maxHP;
    }
}
