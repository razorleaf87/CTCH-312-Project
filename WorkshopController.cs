using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorkshopController : MonoBehaviour
{
    public int view; // to track what view to display, stat view, garage view, shop view 

    //buttons to be enabled and disabled and invisible or visible
    public Button statButton;
    public GameObject stats;
    public Button garageButton;
    public GameObject garage;
    public Button engineeringButton;
    public GameObject engineering;

    // to make it visible or invisible
    public GameObject statScreen;

    // need the player to get stats and inventory.
    public GameObject thePlayer;
    public Player playerUnit;

    //stat text
    public Text level;
    public Text HP;
    public Text armour;
    public Text evasion;
    public Text regen;
    public Text accuracy;
    public Text expToNext;

    //for the garage
    public GameObject garageScreen;
    //button text
    public Text frame;
    public Text legs;
    public Text rightArm;
    public Text leftArm;
    public Text shoulder;
    // Inventory
    public GameObject inventory;
    public Text slot1;
    public Text slot2;
    public Text slot3;
    public int inventoryNumber; // the 4 types of gear are kept in separate arrays
    public int armNumber; //arms use same inventory so this track what arm to swap 0 = right 1 = left
    public int x; //inventory tracker to scroll through slots
    public string text1; // strings to dynamically change the inventory displays
    public string text2;
    public string text3;

    // Engineering Item shop
    public GameObject engineeringMenu;
    public int y; //shop tracker to scroll through engineering shop
    public Item[] engineeringInventory; // array of shop inventory
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

    //dynamic mech sprites
    public Image playerFrontLegs;
    public Image playerBackLegs;
    public Image playerFrame;
    public Image playerFrontArm;
    public Image playerBackArm;
    public Image playerShoulder;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Player");
        playerUnit = thePlayer.GetComponent<Player>();
        view = 0;
        x = 0;
        inventoryNumber = 0;
        y = 0;
        statButton.enabled = true;
        stats.SetActive(true);
        garageButton.enabled = true;
        garage.SetActive(true);
        engineeringButton.enabled = true;
        engineering.SetActive(true);
        statButton.image.enabled = true;
        statScreen.SetActive(false);
        garageScreen.SetActive(false);
        inventory.SetActive(false);
        engineeringMenu.SetActive(false);
    }

    // updates every frame
    private void Update()
    {
        // set sprites
        playerFrontLegs.sprite = thePlayer.GetComponent<Player>().mech.legs.itemImageFront;
        playerBackLegs.sprite = thePlayer.GetComponent<Player>().mech.legs.itemImageBack;
        playerFrame.sprite = thePlayer.GetComponent<Player>().mech.frame.itemImageFront;
        playerFrontArm.sprite = thePlayer.GetComponent<Player>().mech.rightWeapon.itemImageFront;
        playerBackArm.sprite = thePlayer.GetComponent<Player>().mech.leftWeapon.itemImageBack;
        playerShoulder.sprite = thePlayer.GetComponent<Player>().mech.backWeapon.itemImageFront;
        // garage
        if (view == 2)
        {
            frame.text = playerUnit.mech.frame.frameName;
            legs.text = playerUnit.mech.legs.name;
            rightArm.text = playerUnit.mech.rightWeapon.weaponName;
            leftArm.text = playerUnit.mech.leftWeapon.weaponName;
            shoulder.text = playerUnit.mech.backWeapon.weaponName;
        }
        //inventory
        if ( inventoryNumber != 0) // if inventory is active display the proper inventory
        {
            if (inventoryNumber == 1)
            {

                if (playerUnit.frameInventory.Count > x) // if there is no item at that slot display blank
                    text1 = playerUnit.frameInventory[x].frameName;
                else
                    text1 = " ";
                if (playerUnit.frameInventory.Count > x + 1) // if there is no item at that slot display blank
                    text2 = playerUnit.frameInventory[x + 1].frameName;
                else
                    text2 = " ";
                if (playerUnit.frameInventory.Count > x + 2) // if there is no item at that slot display blank
                    text3 = playerUnit.frameInventory[x + 2].frameName;
                else
                    text3 = " ";

                slot1.text = text1;
                slot2.text = text2;
                slot3.text = text3;
            }
            else if (inventoryNumber == 2)
            {

                if (playerUnit.legsInventory.Count > x) // if there is no item at that slot display blank
                    text1 = playerUnit.legsInventory[x].name;
                else
                    text1 = " ";
                if (playerUnit.legsInventory.Count > x + 1) // if there is no item at that slot display blank
                    text2 = playerUnit.legsInventory[x + 1].name;
                else
                    text2 = " ";
                if (playerUnit.legsInventory.Count > x + 2) // if there is no item at that slot display blank
                    text3 = playerUnit.legsInventory[x + 2].name;
                else
                    text3 = " ";

                slot1.text = text1;
                slot2.text = text2;
                slot3.text = text3;
            }
            else if (inventoryNumber == 3)
            {

                if (playerUnit.armInventory.Count > x) // if there is no item at that slot display blank
                    text1 = playerUnit.armInventory[x].weaponName;
                else
                    text1 = " ";
                if (playerUnit.armInventory.Count > x + 1) // if there is no item at that slot display blank
                    text2 = playerUnit.armInventory[x + 1].weaponName;
                else
                    text2 = " ";
                if (playerUnit.armInventory.Count > x + 2) // if there is no item at that slot display blank
                    text3 = playerUnit.armInventory[x + 2].weaponName;
                else
                    text3 = " ";

                slot1.text = text1;
                slot2.text = text2;
                slot3.text = text3;
            }
            else
            {
                if (playerUnit.shoulderInventory.Count > x) // if there is no item at that slot display blank
                    text1 = playerUnit.shoulderInventory[x].weaponName;
                else
                    text1 = " ";
                if (playerUnit.shoulderInventory.Count > x + 1) // if there is no item at that slot display blank
                    text2 = playerUnit.shoulderInventory[x + 1].weaponName;
                else
                    text2 = " ";
                if (playerUnit.shoulderInventory.Count > x + 2) // if there is no item at that slot display blank
                    text3 = playerUnit.shoulderInventory[x + 2].weaponName;
                else
                    text3 = " ";

                slot1.text = text1;
                slot2.text = text2;
                slot3.text = text3;
            }
        }

        // Engineering
        if (view == 3)
        {
            item1Name.text = engineeringInventory[y].name;
            item1Image.sprite = engineeringInventory[y].costImage;
            item1Cost.text = "" + engineeringInventory[y].price;
            item2Name.text = engineeringInventory[y + 1].name;
            item2Image.sprite = engineeringInventory[y + 1].costImage;
            item2Cost.text = "" + engineeringInventory[y + 1].price;
            item3Name.text = engineeringInventory[y + 2].name;
            item3Image.sprite = engineeringInventory[y + 2].costImage;
            item3Cost.text = "" + engineeringInventory[y + 2].price;
            item4Name.text = engineeringInventory[y + 3].name;
            item4Image.sprite = engineeringInventory[y + 3].costImage;
            item4Cost.text = "" + engineeringInventory[y + 3].price;
            item5Name.text = engineeringInventory[y + 4].name;
            item5Image.sprite = engineeringInventory[y + 4].costImage;
            item5Cost.text = "" + engineeringInventory[y + 4].price;
            yourScrap.text = playerUnit.tier1Scrap + "\n\n" + playerUnit.tier2Scrap + "\n\n" + playerUnit.tier3Scrap;
        }
    }

    // disable and hide buttons display stat screen
    public void OnStatButton()
    {
        // disable buttons
        statButton.enabled = false;
        stats.SetActive(false);
        garageButton.enabled = false;
        garage.SetActive(false);
        engineeringButton.enabled = false;
        engineering.SetActive(false);
        statButton.image.enabled = false;
        // display stats
        statScreen.SetActive(true);
        view = 1;
        level.text = "Level: " + playerUnit.mech.level;
        HP.text = "HP: " + playerUnit.mech.frame.HP;
        armour.text = "Armour: " + (playerUnit.mech.frame.armour + playerUnit.mech.legs.armourBonus);
        evasion.text = "Evasion: " + (playerUnit.mech.frame.evasion + playerUnit.mech.legs.evasionBonus);
        regen.text = "Energy Regen: " + (playerUnit.mech.frame.regen + playerUnit.mech.legs.regenBonus);
        accuracy.text = "Accuracy Bonus: " + (playerUnit.mech.legs.accuracyBonus);
        expToNext.text = "Exp To Next: " + (playerUnit.mech.expToNext - playerUnit.mech.exp);
    }

    // disable and hide buttons display garage screen
    public void OnGarageButton()
    {
        //disable buttons
        statButton.enabled = false;
        stats.SetActive(false);
        garageButton.enabled = false;
        garage.SetActive(false);
        engineeringButton.enabled = false;
        engineering.SetActive(false);
        statButton.image.enabled = false;
        //activate garage screen
        garageScreen.SetActive(true);
        view = 2;
    }

    //click on frame to change it out
    public void OnFrameButton()
    {
        inventoryNumber = 1;
        inventory.SetActive(true);
    }

    //click on legs to change it out
    public void OnLegsButton()
    {
        inventoryNumber = 2;
        inventory.SetActive(true);
    }

    //click on right arm to change it out
    public void OnRightArmButton()
    {
        inventoryNumber = 3;
        armNumber = 0;
        inventory.SetActive(true);
    }

    //click on left arm to change it out
    public void OnLeftArmButton()
    {
        inventoryNumber = 3;
        armNumber = 1;
        inventory.SetActive(true);
    }

    //click on shoulder to change it out
    public void OnShoulderButton()
    {
        inventoryNumber = 4;
        inventory.SetActive(true);
    }

    // equip item in slot 1
    public void OnSlot1Button()
    {
        if (inventoryNumber != 0)
        {
            if (inventoryNumber == 1) //swap frame
            {
                Frame temp = playerUnit.mech.frame; //store current part
                playerUnit.mech.frame = playerUnit.frameInventory[x]; //equip part
                playerUnit.frameInventory[x] = temp; // put old part into inventory
            }
            else if (inventoryNumber == 2)
            {
                Leg temp = playerUnit.mech.legs;
                playerUnit.mech.legs = playerUnit.legsInventory[x]; //equip part
                playerUnit.legsInventory[x] = temp; // put old part into inventory
            }
            else if (inventoryNumber == 3)
            {
                if (armNumber == 0)
                {
                    Weapon temp = playerUnit.mech.rightWeapon;
                    playerUnit.mech.rightWeapon = playerUnit.armInventory[x]; //equip part
                    playerUnit.armInventory[x] = temp; // put old part into inventory
                }
                else if (armNumber == 1)
                {
                    Weapon temp = playerUnit.mech.leftWeapon;
                    playerUnit.mech.leftWeapon = playerUnit.armInventory[x]; //equip part
                    playerUnit.armInventory[x] = temp; // put old part into inventory
                }
            }
            else
            {
                Weapon temp = playerUnit.mech.backWeapon;
                playerUnit.mech.backWeapon = playerUnit.shoulderInventory[x]; //equip part
                playerUnit.shoulderInventory[x] = temp; // put old part into inventory
            }
        }
    }

    // equip item in slot 2
    public void OnSlot2Button()
    {
        if (inventoryNumber != 0)
        {
            if (inventoryNumber == 1) //swap frame
            {
                Frame temp = playerUnit.mech.frame; //store current part
                playerUnit.mech.frame = playerUnit.frameInventory[x + 1]; //equip part
                playerUnit.frameInventory[x + 1] = temp; // put old part into inventory
            }
            else if (inventoryNumber == 2)
            {
                Leg temp = playerUnit.mech.legs;
                playerUnit.mech.legs = playerUnit.legsInventory[x + 1]; //equip part
                playerUnit.legsInventory[x + 1] = temp; // put old part into inventory
            }
            else if (inventoryNumber == 3)
            {
                if (armNumber == 0)
                {
                    Weapon temp = playerUnit.mech.rightWeapon;
                    playerUnit.mech.rightWeapon = playerUnit.armInventory[x + 1]; //equip part
                    playerUnit.armInventory[x + 1] = temp; // put old part into inventory
                }
                else if (armNumber == 1)
                {
                    Weapon temp = playerUnit.mech.leftWeapon;
                    playerUnit.mech.leftWeapon = playerUnit.armInventory[x + 1]; //equip part
                    playerUnit.armInventory[x + 1] = temp; // put old part into inventory
                }
            }
            else
            {
                Weapon temp = playerUnit.mech.backWeapon;
                playerUnit.mech.backWeapon = playerUnit.shoulderInventory[x + 1]; //equip part
                playerUnit.shoulderInventory[x + 1] = temp; // put old part into inventory
            }
        }
    }

    // equip item in slot 3
    public void OnSlot3Button()
    {
        if (inventoryNumber != 0)
        {
            if (inventoryNumber == 1) //swap frame
            {
                Frame temp = playerUnit.mech.frame; //store current part
                playerUnit.mech.frame = playerUnit.frameInventory[x + 2]; //equip part
                playerUnit.frameInventory[x + 2] = temp; // put old part into inventory
            }
            else if (inventoryNumber == 2)
            {
                Leg temp = playerUnit.mech.legs;
                playerUnit.mech.legs = playerUnit.legsInventory[x + 2]; //equip part
                playerUnit.legsInventory[x + 2] = temp; // put old part into inventory
            }
            else if (inventoryNumber == 3)
            {
                if (armNumber == 0)
                {
                    Weapon temp = playerUnit.mech.rightWeapon;
                    playerUnit.mech.rightWeapon = playerUnit.armInventory[x + 2]; //equip part
                    playerUnit.armInventory[x + 2] = temp; // put old part into inventory
                }
                else if (armNumber == 1)
                {
                    Weapon temp = playerUnit.mech.leftWeapon;
                    playerUnit.mech.leftWeapon = playerUnit.armInventory[x + 2]; //equip part
                    playerUnit.armInventory[x + 2] = temp; // put old part into inventory
                }
            }
            else
            {
                Weapon temp = playerUnit.mech.backWeapon;
                playerUnit.mech.backWeapon = playerUnit.shoulderInventory[x + 2]; //equip part
                playerUnit.shoulderInventory[x + 2] = temp; // put old part into inventory
            }
        }
    }

    // scroll up 
    public void OnUpButton()
    {
        if (view == 2) // scroll up in inventory
        {
            if (x > 0)
            {
                x--;
            }
        }
        else if (view == 3) // scroll up in engineering
        {
            if (y > 0)
            {
                y--;
            }
        }
    }

    // scroll down 
    public void OnDownButton()
    {
        if (view == 2)// scroll down in inventory
        {
            x++;
        }
        else if (view == 3)// scroll down in engineering
        {
            if (y + 5 < engineeringInventory.Length)
            { 
                y++; 
            }
        }
    }

    //open engineering item shop
    public void OnEngineeringButton()
    {
        //disable buttons
        statButton.enabled = false;
        stats.SetActive(false);
        garageButton.enabled = false;
        garage.SetActive(false);
        engineeringButton.enabled = false;
        engineering.SetActive(false);
        statButton.image.enabled = false;
        // enable engineering menu
        engineeringMenu.SetActive(true);
        view = 3;
    }

    // the next 5 functions control the 5 engineering buttons to buy items
    public void OnEngineeringButton1()
    {
        bool bought = false;
        if (engineeringInventory[y].tier == 1 && playerUnit.tier1Scrap >= engineeringInventory[y].price)
        {
            playerUnit.tier1Scrap -= engineeringInventory[y].price;
            bought = true;
        }

        else if (engineeringInventory[y].tier == 2 && playerUnit.tier2Scrap >= engineeringInventory[y].price)
        {
            playerUnit.tier2Scrap -= engineeringInventory[y].price;
            bought = true;
        }

        else if (engineeringInventory[y].tier == 3 && playerUnit.tier3Scrap >= engineeringInventory[y].price)
        {
            playerUnit.tier3Scrap -= engineeringInventory[y].price;
            bought = true;
        }

        if (bought)
        {
            if (engineeringInventory[y].itemType == Type.WEAPON)
                playerUnit.armInventory.Add(engineeringInventory[y].GetComponent<Weapon>());
            else if (engineeringInventory[y].itemType == Type.BACK)
                playerUnit.shoulderInventory.Add(engineeringInventory[y].GetComponent<Weapon>());
            else if (engineeringInventory[y].itemType == Type.FRAME)
                playerUnit.frameInventory.Add(engineeringInventory[y].GetComponent<Frame>());
            else
                playerUnit.legsInventory.Add(engineeringInventory[y].GetComponent<Leg>());
        }
    }

    public void OnEngineeringButton2()
    {
        bool bought = false;
        if (engineeringInventory[y + 1].tier == 1 && playerUnit.tier1Scrap >= engineeringInventory[y + 1].price)
        {
            playerUnit.tier1Scrap -= engineeringInventory[y + 1].price;
            bought = true;
        }

        else if (engineeringInventory[y + 1].tier == 2 && playerUnit.tier2Scrap >= engineeringInventory[y + 1].price)
        {
            playerUnit.tier2Scrap -= engineeringInventory[y + 1].price;
            bought = true;
        }

        else if (engineeringInventory[y + 1].tier == 3 && playerUnit.tier3Scrap >= engineeringInventory[y + 1].price)
        {
            playerUnit.tier3Scrap -= engineeringInventory[y + 1].price;
            bought = true;
        }

        if (bought)
        {
            if (engineeringInventory[y + 1].itemType == Type.WEAPON)
                playerUnit.armInventory.Add(engineeringInventory[y + 1].GetComponent<Weapon>());
            else if (engineeringInventory[y + 1].itemType == Type.BACK)
                playerUnit.shoulderInventory.Add(engineeringInventory[y + 1].GetComponent<Weapon>());
            else if (engineeringInventory[y + 1].itemType == Type.FRAME)
                playerUnit.frameInventory.Add(engineeringInventory[y + 1].GetComponent<Frame>());
            else
                playerUnit.legsInventory.Add(engineeringInventory[y + 1].GetComponent<Leg>());
        }
    }

    public void OnEngineeringButton3()
    {
        bool bought = false;
        if (engineeringInventory[y + 2].tier == 1 && playerUnit.tier1Scrap >= engineeringInventory[y + 2].price)
        {
            playerUnit.tier1Scrap -= engineeringInventory[y + 2].price;
            bought = true;
        }

        else if (engineeringInventory[y + 2].tier == 2 && playerUnit.tier2Scrap >= engineeringInventory[y + 2].price)
        {
            playerUnit.tier2Scrap -= engineeringInventory[y + 2].price;
            bought = true;
        }

        else if (engineeringInventory[y + 2].tier == 3 && playerUnit.tier3Scrap >= engineeringInventory[y + 2].price)
        {
            playerUnit.tier3Scrap -= engineeringInventory[y + 2].price;
            bought = true;
        }

        if (bought)
        {
            if (engineeringInventory[y + 2].itemType == Type.WEAPON)
                playerUnit.armInventory.Add(engineeringInventory[y + 2].GetComponent<Weapon>());
            else if (engineeringInventory[y + 2].itemType == Type.BACK)
                playerUnit.shoulderInventory.Add(engineeringInventory[y + 2].GetComponent<Weapon>());
            else if (engineeringInventory[y + 2].itemType == Type.FRAME)
                playerUnit.frameInventory.Add(engineeringInventory[y + 2].GetComponent<Frame>());
            else
                playerUnit.legsInventory.Add(engineeringInventory[y + 2].GetComponent<Leg>());
        }
    }

    public void OnEngineeringButton4()
    {
        bool bought = false;
        if (engineeringInventory[y + 3].tier == 1 && playerUnit.tier1Scrap >= engineeringInventory[y + 3].price)
        {
            playerUnit.tier1Scrap -= engineeringInventory[y + 3].price;
            bought = true;
        }

        else if (engineeringInventory[y + 3].tier == 2 && playerUnit.tier2Scrap >= engineeringInventory[y + 3].price)
        {
            playerUnit.tier2Scrap -= engineeringInventory[y + 3].price;
            bought = true;
        }

        else if (engineeringInventory[y + 3].tier == 3 && playerUnit.tier3Scrap >= engineeringInventory[y + 3].price)
        {
            playerUnit.tier3Scrap -= engineeringInventory[y + 3].price;
            bought = true;
        }

        if (bought)
        {
            if (engineeringInventory[y + 3].itemType == Type.WEAPON)
                playerUnit.armInventory.Add(engineeringInventory[y + 3].GetComponent<Weapon>());
            else if (engineeringInventory[y + 3].itemType == Type.BACK)
                playerUnit.shoulderInventory.Add(engineeringInventory[y + 3].GetComponent<Weapon>());
            else if (engineeringInventory[y + 3].itemType == Type.FRAME)
                playerUnit.frameInventory.Add(engineeringInventory[y + 3].GetComponent<Frame>());
            else
                playerUnit.legsInventory.Add(engineeringInventory[y + 3].GetComponent<Leg>());
        }
    }

    public void OnEngineeringButton5()
    {
        bool bought = false;
        if (engineeringInventory[y + 4].tier == 1 && playerUnit.tier1Scrap >= engineeringInventory[y + 4].price)
        {
            playerUnit.tier1Scrap -= engineeringInventory[y + 4].price;
            bought = true;
        }

        else if (engineeringInventory[y + 4].tier == 2 && playerUnit.tier2Scrap >= engineeringInventory[y + 4].price)
        {
            playerUnit.tier2Scrap -= engineeringInventory[y + 4].price;
            bought = true;
        }

        else if (engineeringInventory[y + 4].tier == 3 && playerUnit.tier3Scrap >= engineeringInventory[y + 4].price)
        {
            playerUnit.tier3Scrap -= engineeringInventory[y + 4].price;
            bought = true;
        }

        if (bought)
        {
            if (engineeringInventory[y + 4].itemType == Type.WEAPON)
                playerUnit.armInventory.Add(engineeringInventory[y + 4].GetComponent<Weapon>());
            else if (engineeringInventory[y + 4].itemType == Type.BACK)
                playerUnit.shoulderInventory.Add(engineeringInventory[y + 4].GetComponent<Weapon>());
            else if (engineeringInventory[y + 4].itemType == Type.FRAME)
                playerUnit.frameInventory.Add(engineeringInventory[y + 4].GetComponent<Frame>());
            else
                playerUnit.legsInventory.Add(engineeringInventory[y + 4].GetComponent<Leg>());
        }
    }

    //back button doesnt change so depending on what view it will do different things
    public void OnBackButton()
    {
        //back to menu
        if (view == 0)
        {
            SceneManager.LoadScene("Menu");
        }
        // back to workshop
        else
        {
            view = 0;
            statButton.enabled = true;
            stats.SetActive(true);
            garageButton.enabled = true;
            garage.SetActive(true);
            engineeringButton.enabled = true;
            engineering.SetActive(true);
            statButton.image.enabled = true;
            statScreen.SetActive(false);
            garageScreen.SetActive(false);
            inventory.SetActive(false);
            inventoryNumber = 0;
            x = 0;
            engineeringMenu.SetActive(false);
            y = 0;
        }
    }
}
