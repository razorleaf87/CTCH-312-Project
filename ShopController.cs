using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    // need the player to get stats and inventory.
    public GameObject thePlayer;
    public Player playerUnit;

    //  Item shop
    public int y; //shop tracker to scroll through  shop
    public Item[] shopInventory; // array of shop inventory
    //the text boxes and sprites for the inventory items
    public Text item1Name;
    public Text item2Name;
    public Text item3Name;
    public Text item4Name;
    public Text item5Name;
    public Text item1Cost;
    public Text item2Cost;
    public Text item3Cost;
    public Text item4Cost;
    public Text item5Cost;
    public Image item1Image;
    public Image item2Image;
    public Image item3Image;
    public Image item4Image;
    public Image item5Image;

    public Text yourScrap;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Player");
        playerUnit = thePlayer.GetComponent<Player>();
        y = 0;

        //setup shop UI
        item1Name.text = shopInventory[y].name;
        item1Image.sprite = shopInventory[y].costImage;
        item1Cost.text = "" + shopInventory[y].price;
        item2Name.text = shopInventory[y + 1].name;
        item2Image.sprite = shopInventory[y + 1].costImage;
        item2Cost.text = "" + shopInventory[y + 1].price;
        item3Name.text = shopInventory[y + 2].name;
        item3Image.sprite = shopInventory[y + 2].costImage;
        item3Cost.text = "" + shopInventory[y + 2].price;
        item4Name.text = shopInventory[y + 3].name;
        item4Image.sprite = shopInventory[y + 3].costImage;
        item4Cost.text = "" + shopInventory[y + 3].price;
        item5Name.text = shopInventory[y + 4].name;
        item5Image.sprite = shopInventory[y + 4].costImage;
        item5Cost.text = "" + shopInventory[y + 4].price;
        yourScrap.text = playerUnit.tier1Scrap + "\n\n" + playerUnit.tier2Scrap + "\n\n" + playerUnit.tier3Scrap;
    }

    // Update is called once per frame
    void Update()
    {
        //update shop UI
        item1Name.text = shopInventory[y].name;
        item1Image.sprite = shopInventory[y].costImage;
        item1Cost.text = "" + shopInventory[y].price;
        item2Name.text = shopInventory[y + 1].name;
        item2Image.sprite = shopInventory[y + 1].costImage;
        item2Cost.text = "" + shopInventory[y + 1].price;
        item3Name.text = shopInventory[y + 2].name;
        item3Image.sprite = shopInventory[y + 2].costImage;
        item3Cost.text = "" + shopInventory[y + 2].price;
        item4Name.text = shopInventory[y + 3].name;
        item4Image.sprite = shopInventory[y + 3].costImage;
        item4Cost.text = "" + shopInventory[y + 3].price;
        item5Name.text = shopInventory[y + 4].name;
        item5Image.sprite = shopInventory[y + 4].costImage;
        item5Cost.text = "" + shopInventory[y + 4].price;
        yourScrap.text = playerUnit.tier1Scrap + "\n\n" + playerUnit.tier2Scrap + "\n\n" + playerUnit.tier3Scrap;
    }

    // scroll up
    public void OnUpButton()
    {
        if (y > 0)
        {
            y--;
        }
    }

    // scroll down 
    public void OnDownButton()
    {
        if (y + 5 < shopInventory.Length)
        {
            y++;
        }
    }

    // the next 5 functions control the 5 buttons to buy items
    public void OnShopButton1()
    {
        bool bought = false;
        if (shopInventory[y].tier == 1 && playerUnit.tier1Scrap >= shopInventory[y].price)
        {
            playerUnit.tier1Scrap -= shopInventory[y].price;
            bought = true;
        }

        else if (shopInventory[y].tier == 2 && playerUnit.tier2Scrap >= shopInventory[y].price)
        {
            playerUnit.tier2Scrap -= shopInventory[y].price;
            bought = true;
        }

        else if (shopInventory[y].tier == 3 && playerUnit.tier3Scrap >= shopInventory[y].price)
        {
            playerUnit.tier3Scrap -= shopInventory[y].price;
            bought = true;
        }

        if (bought)
        {
            if (shopInventory[y].itemType == Type.WEAPON)
                playerUnit.armInventory.Add(shopInventory[y].GetComponent<Weapon>());
            else if (shopInventory[y].itemType == Type.BACK)
                playerUnit.shoulderInventory.Add(shopInventory[y].GetComponent<Weapon>());
            else if (shopInventory[y].itemType == Type.FRAME)
                playerUnit.frameInventory.Add(shopInventory[y].GetComponent<Frame>());
            else
                playerUnit.legsInventory.Add(shopInventory[y].GetComponent<Leg>());
        }
    }

    public void OnShopButton2()
    {
        bool bought = false;
        if (shopInventory[y + 1].tier == 1 && playerUnit.tier1Scrap >= shopInventory[y + 1].price)
        {
            playerUnit.tier1Scrap -= shopInventory[y + 1].price;
            bought = true;
        }

        else if (shopInventory[y + 1].tier == 2 && playerUnit.tier2Scrap >= shopInventory[y + 1].price)
        {
            playerUnit.tier2Scrap -= shopInventory[y + 1].price;
            bought = true;
        }

        else if (shopInventory[y + 1].tier == 3 && playerUnit.tier3Scrap >= shopInventory[y + 1].price)
        {
            playerUnit.tier3Scrap -= shopInventory[y + 1].price;
            bought = true;
        }

        if (bought)
        {
            if (shopInventory[y + 1].itemType == Type.WEAPON)
                playerUnit.armInventory.Add(shopInventory[y + 1].GetComponent<Weapon>());
            else if (shopInventory[y + 1].itemType == Type.BACK)
                playerUnit.shoulderInventory.Add(shopInventory[y + 1].GetComponent<Weapon>());
            else if (shopInventory[y + 1].itemType == Type.FRAME)
                playerUnit.frameInventory.Add(shopInventory[y + 1].GetComponent<Frame>());
            else
                playerUnit.legsInventory.Add(shopInventory[y + 1].GetComponent<Leg>());
        }
    }

    public void OnShopButton3()
    {
        bool bought = false;
        if (shopInventory[y + 2].tier == 1 && playerUnit.tier1Scrap >= shopInventory[y + 2].price)
        {
            playerUnit.tier1Scrap -= shopInventory[y + 2].price;
            bought = true;
        }

        else if (shopInventory[y + 2].tier == 2 && playerUnit.tier2Scrap >= shopInventory[y + 2].price)
        {
            playerUnit.tier2Scrap -= shopInventory[y + 2].price;
            bought = true;
        }

        else if (shopInventory[y + 2].tier == 3 && playerUnit.tier3Scrap >= shopInventory[y + 2].price)
        {
            playerUnit.tier3Scrap -= shopInventory[y + 2].price;
            bought = true;
        }

        if (bought)
        {
            if (shopInventory[y + 2].itemType == Type.WEAPON)
                playerUnit.armInventory.Add(shopInventory[y + 2].GetComponent<Weapon>());
            else if (shopInventory[y + 2].itemType == Type.BACK)
                playerUnit.shoulderInventory.Add(shopInventory[y + 2].GetComponent<Weapon>());
            else if (shopInventory[y + 2].itemType == Type.FRAME)
                playerUnit.frameInventory.Add(shopInventory[y + 2].GetComponent<Frame>());
            else
                playerUnit.legsInventory.Add(shopInventory[y + 2].GetComponent<Leg>());
        }
    }

    public void OnShopButton4()
    {
        bool bought = false;
        if (shopInventory[y + 3].tier == 1 && playerUnit.tier1Scrap >= shopInventory[y + 3].price)
        {
            playerUnit.tier1Scrap -= shopInventory[y + 3].price;
            bought = true;
        }

        else if (shopInventory[y + 3].tier == 2 && playerUnit.tier2Scrap >= shopInventory[y + 3].price)
        {
            playerUnit.tier2Scrap -= shopInventory[y + 3].price;
            bought = true;
        }

        else if (shopInventory[y + 3].tier == 3 && playerUnit.tier3Scrap >= shopInventory[y + 3].price)
        {
            playerUnit.tier3Scrap -= shopInventory[y + 3].price;
            bought = true;
        }

        if (bought)
        {
            if (shopInventory[y + 3].itemType == Type.WEAPON)
                playerUnit.armInventory.Add(shopInventory[y + 3].GetComponent<Weapon>());
            else if (shopInventory[y + 3].itemType == Type.BACK)
                playerUnit.shoulderInventory.Add(shopInventory[y + 3].GetComponent<Weapon>());
            else if (shopInventory[y + 3].itemType == Type.FRAME)
                playerUnit.frameInventory.Add(shopInventory[y + 3].GetComponent<Frame>());
            else
                playerUnit.legsInventory.Add(shopInventory[y + 3].GetComponent<Leg>());
        }
    }

    public void OnShopButton5()
    {
        bool bought = false;
        if (shopInventory[y + 4].tier == 1 && playerUnit.tier1Scrap >= shopInventory[y + 4].price)
        {
            playerUnit.tier1Scrap -= shopInventory[y + 4].price;
            bought = true;
        }

        else if (shopInventory[y + 4].tier == 2 && playerUnit.tier2Scrap >= shopInventory[y + 4].price)
        {
            playerUnit.tier2Scrap -= shopInventory[y + 4].price;
            bought = true;
        }

        else if (shopInventory[y + 4].tier == 3 && playerUnit.tier3Scrap >= shopInventory[y + 4].price)
        {
            playerUnit.tier3Scrap -= shopInventory[y + 4].price;
            bought = true;
        }

        if (bought)
        {
            if (shopInventory[y + 4].itemType == Type.WEAPON)
                playerUnit.armInventory.Add(shopInventory[y + 4].GetComponent<Weapon>());
            else if (shopInventory[y + 4].itemType == Type.BACK)
                playerUnit.shoulderInventory.Add(shopInventory[y + 4].GetComponent<Weapon>());
            else if (shopInventory[y + 4].itemType == Type.FRAME)
                playerUnit.frameInventory.Add(shopInventory[y + 4].GetComponent<Frame>());
            else
                playerUnit.legsInventory.Add(shopInventory[y + 4].GetComponent<Leg>());
        }
    }

    //back button returns you to menu
    public void OnBackButton()
    {
        //back to menu
            SceneManager.LoadScene("Menu");    
    }
}
