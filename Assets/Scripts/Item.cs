using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [Header("Item Info")]
    public string itemName;
    public string itemDescription;
    public int value;
    public Sprite itemSprite;
    [Header("Item Kind")]
    public bool isConsumable;
    public bool isWeapon;
    public bool isArmor;
    public bool isShield;
    [Header("Item Effect Kind")]
    public bool affectHP;
    public bool affectMP;
    public bool affectStr;
    public bool affectDef;
    [Header("Item Effect Power")]


    public int effectValue;
    public int wpnPower;
    public int armorPower;
    public int shieldPwr;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {
        PlayerStats selectedChar = GameManager.instance.playerStats;

        if (isConsumable)
        {
            if (affectHP) 
            {
                selectedChar.currentHP += effectValue;

                if(selectedChar.currentHP > selectedChar.maxHP)
                {
                    selectedChar.currentHP = selectedChar.maxHP;
                }
            }
            if(affectMP)
            {
                selectedChar.currentMP += effectValue;

                if (selectedChar.currentMP > selectedChar.maxMP)
                {
                    selectedChar.currentMP = selectedChar.maxMP;
                }
            }
            if(affectStr)
            {
                selectedChar.strength += effectValue;
            }
            if(affectDef)
            {
                selectedChar.defence += effectValue;
            }

        }


        if(isWeapon)
        {
            if(PlayerStats.instance.equippedWpn != "")
            {
                GameManager.instance.AddItem(selectedChar.equippedWpn);


            }
            PlayerStats.instance.equippedWpn = itemName;
            PlayerStats.instance.wpnPwr = wpnPower;
            //PlayerStats.instance.
            PlayerStats.instance.equippedWpnSprite = itemSprite;


        }
        if (isArmor)
        {
            if (PlayerStats.instance.equippedArmor != "")
            {
                GameManager.instance.AddItem(PlayerStats.instance.equippedArmor);


            }
            PlayerStats.instance.equippedArmor = itemName;

            PlayerStats.instance.armorPwr = armorPower;

            //PlayerStats.instance.defence = PlayerStats.instance.baseDefence + PlayerStats.instance.armorPwr;

            PlayerStats.instance.equippedArmorSprite = itemSprite;
        }
        if (isShield)
        {
            if (PlayerStats.instance.equippedShield != "")
            {
                GameManager.instance.AddItem(selectedChar.equippedShield);


            }
            PlayerStats.instance.equippedShield = itemName;
            PlayerStats.instance.shieldPwr = shieldPwr;
            //PlayerStats.instance.
            PlayerStats.instance.equippedShieldSprite = itemSprite;


        }

        GameManager.instance.RemoveItem(itemName);


        
    }
}
