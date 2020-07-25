using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;

    public Text goldText;

    public string[] itemsForSale;
    //public string[] itemsForBuy;

    public ItemButton[] buyItemButtons;
    public ItemButton[] SellItemButtons;

    public Item selectedItem;

    public Text buyItemName, buyItemDescription, buyItemValue;
    public Text sellItemName, sellItemDescription, sellItemValue;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.O) && !shopMenu.activeInHierarchy)
        {
            OpenShop();
        }*/
    }
    public void OpenShop()
    {
        shopMenu.SetActive(true);
        OpenBuyMenu();

        GameManager.instance.shopActive = true;
        goldText.text = GameManager.instance.currentGold.ToString() + "g";
        
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopActive = false;
    }

    public void OpenBuyMenu()
    {
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);
        buyItemButtons[0].Press();


        for (int i = 0; i < buyItemButtons.Length; i ++)
        {
            buyItemButtons[i].buttonValue = i;

            if (itemsForSale[i] != "")
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(true);
                buyItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                buyItemButtons[i].amountText.text = "";
            }
            else
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
            }
        }

    }
public void OpenSellMenu()
    {
        buyMenu.SetActive(false);
        sellMenu.SetActive(true);
        SellItemButtons[0].Press();
        

        ShowSellItems();
        

    }
    private void ShowSellItems()
    {
        GameManager.instance.SortItems();
        for (int i = 0; i < SellItemButtons.Length; i++)
        {
            SellItemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                SellItemButtons[i].buttonImage.gameObject.SetActive(true);
                SellItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                //SellItemButtons[i].amountText.text = "";
                SellItemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();



            }
            else
            {
                SellItemButtons[i].buttonImage.gameObject.SetActive(false);

                SellItemButtons[i].amountText.text = "";
            }
        }
    }
    public void SelectBuyItem(Item buyItem)
    {
        selectedItem = buyItem;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.itemDescription;
        buyItemValue.text = " " + selectedItem.value + "g";
    }

    public void SelectSellItem(Item sellItem)
    {
        selectedItem = sellItem;
        sellItemName.text = selectedItem.itemName;
        sellItemDescription.text = selectedItem.itemDescription;
        sellItemValue.text = " " + Mathf.FloorToInt(selectedItem.value * .5f).ToString();
    }

    public void BuyItem()
    {
        if(selectedItem != null)
        {

            if (GameManager.instance.currentGold >= selectedItem.value)
            {
                GameManager.instance.currentGold -= selectedItem.value;
                GameManager.instance.AddItem(selectedItem.itemName);
                goldText.text = GameManager.instance.currentGold.ToString() + "g";
            }
        }
    }
    public void SellItem()
    {
        if(selectedItem != null)
        {
            GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.value * .5f);
            GameManager.instance.RemoveItem(selectedItem.itemName);


        }

        goldText.text = GameManager.instance.currentGold.ToString() + "g";
        ShowSellItems();

    }

}
