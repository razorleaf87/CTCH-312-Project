using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, ATTACKING, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    // objects from scene 0
    public GameObject globalController;
    public GameObject thePlayer;

    // battle stations
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Player setupPlayer;
    // units to use in battle
    Mech playerUnit;
    Mech enemyUnit;
    public GameObject enemy, player;

    // track what enemy is selected for the battle
    public string scriptName;

    // control the text boxes for the dialogue box and the buttons
    public Text Dialogue;
    public Text LeftArmText;
    public Text RightArmText;
    public Text BackText;
    public Text GuardText;

    public HUD playerHUD;
    public HUD enemyHUD;

    // track what state the battle is in so the player cant attack continuously
    public BattleState state;

    private bool playerBackWeaponUsed = false;
    private bool enemyBackWeaponUsed = false;
    private int multihit = 1; //tracks number of hits completed 
    string ability; //tracks what ability is on the weapon

    //rewards screen
    public GameObject rewardScreen;
    public Text scrapRewards;
    public Text expRewards;

    //dynamic mech sprites
    public Image playerPortrait;    
    public Image playerFrontLegs;
    public Image playerBackLegs;
    public Image playerFrame;
    public Image playerFrontArm;
    public Image playerBackArm;
    public Image playerShoulder;

    public Image enemyPortrait;
    public Image enemyFrontLegs;
    public Image enemyBackLegs;
    public Image enemyFrame;
    public Image enemyFrontArm;
    public Image enemyBackArm;
    public Image enemyShoulder;

    //burn status effect
    public GameObject playerStatus;
    public GameObject enemyStatus;
    public Text playerDots;
    public Text enemyDots;

    //music
    public AudioSource source;
    public AudioClip[] audioClips;

    void Awake()
    {
        globalController = GameObject.Find("GlobalController");
        thePlayer = GameObject.Find("Player");
        rewardScreen.SetActive(false);
        globalController.GetComponent<GlobalController>().day++;
        playerStatus.SetActive(false);
        enemyStatus.SetActive(false);
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //update status markers
        if (playerUnit != null && enemyUnit != null) 
        {
            if (playerUnit.burn > 0)
            {
                playerStatus.SetActive(true);
                playerDots.text = playerUnit.burn.ToString();
            }
            if (enemyUnit.burn > 0)
            {
                enemyStatus.SetActive(true);
                enemyDots.text = enemyUnit.burn.ToString();
            }
        }
    }

    // instantiate an enemy to fight
    public void InstatiateEnemy() 
    {
        if (globalController.GetComponent<GlobalController>().bossFight == true)
        {
            enemy = Instantiate(globalController.GetComponent<GlobalController>().boss[globalController.GetComponent<GlobalController>().bossNumber], enemyBattleStation);
            scriptName = enemy.name;
            
        }
        else if (globalController.GetComponent<GlobalController>().tutorial == true)
        {
            scriptName = "Rat";
            enemy = Instantiate(globalController.GetComponent<GlobalController>().enemyList[0], enemyBattleStation); // making the enemy
        }
        else
        {
            // pick a random enemy to battle
            int length, random;
            length = globalController.GetComponent<GlobalController>().enemyList.Length;
            if (globalController.GetComponent <GlobalController>().week == 1)
                random = Random.Range(0, 2);
            else if (globalController.GetComponent <GlobalController>().week == 2)
                random = Random.Range(0, 3);
            else
                random = Random.Range(0, length);
            enemy = Instantiate(globalController.GetComponent<GlobalController>().enemyList[random], enemyBattleStation); // making the enemy

            if (random == 0)
            {
                scriptName = "Rat";
            }
            else if (random == 1)
            {
                scriptName = "Junk Boy";
            }
            else if (random == 2)
            {
                scriptName = "Radiated Crab";
            }
            else
            {
                scriptName = "Mercenary";
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;

        StartCoroutine(SetupBattle());
    }

    //Setup the battle and go to the player's turn
    IEnumerator SetupBattle()
    {
        // setup audio
        if (globalController.GetComponent<GlobalController>().bossFight)
        {
            source.PlayOneShot(audioClips[globalController.GetComponent<GlobalController>().bossNumber + 1]);
        }
        else
        {
            source.PlayOneShot(audioClips[0]);
        }
        
        //setup player
        player = Instantiate(globalController.GetComponent<GlobalController>().thePlayer, playerBattleStation);
        setupPlayer = player.GetComponent<Player>();
        setupPlayer.mech.SetStats();
        playerUnit = setupPlayer.mech;
        InstatiateEnemy(); //enemy will be created under the gameobject "enemy"

        //setup enemy
        enemyUnit = enemy.GetComponent<Mech>();   
        enemyUnit.SetStats();

        Dialogue.text = enemyUnit.unitName + " approaches...";

        // setup HUD's
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        // set buttons
        RightArmText.text = playerUnit.rightWeapon.weaponName + " " + playerUnit.rightWeapon.cost + "E";
        LeftArmText.text = playerUnit.leftWeapon.weaponName + " " + playerUnit.leftWeapon.cost + "E";
        BackText.text = playerUnit.backWeapon.weaponName + " " + playerUnit.backWeapon.cost + "E";
        GuardText.text = "Guard " + playerUnit.currEnergy + "E";

        // set sprites
        playerPortrait.sprite = playerUnit.frame.portrait;
        playerFrontLegs.sprite = playerUnit.legs.itemImageFront;
        playerBackLegs.sprite = playerUnit.legs.itemImageBack;
        playerFrame.sprite = playerUnit.frame.itemImageFront;
        playerFrontArm.sprite = playerUnit.rightWeapon.itemImageFront;
        playerBackArm.sprite = playerUnit.leftWeapon.itemImageBack;
        playerShoulder.sprite = playerUnit.backWeapon.itemImageFront;

        enemyPortrait.sprite = enemyUnit.frame.portrait;
        enemyFrontLegs.sprite = enemyUnit.legs.itemImageFront;
        enemyBackLegs.sprite = enemyUnit.legs.itemImageBack;
        enemyFrame.sprite = enemyUnit.frame.itemImageFront;
        enemyFrontArm.sprite = enemyUnit.leftWeapon.itemImageFront;
        enemyBackArm.sprite = enemyUnit.rightWeapon.itemImageBack;
        enemyShoulder.sprite = enemyUnit.backWeapon.itemImageFront;

        yield return new WaitForSeconds(2f);

        PlayerTurn();
    }

    // Player turn Logic

    // wait for player to pick an action
    void PlayerTurn()
    {
        state = BattleState.PLAYERTURN;
        Dialogue.text = "Choose an action " + playerUnit.unitName;
        GuardText.text = "Guard " + playerUnit.currEnergy + "E";
        
        
    }

    // top button for right arm weapon
    public void OnRightArm()
    {
        // check that it is the player's turn
        if (state == BattleState.PLAYERTURN)
        {
            // if they have enough energy to attack
            if (playerUnit.currEnergy >= playerUnit.rightWeapon.cost && !playerUnit.rightDisabled)
            {
                state = BattleState.ATTACKING;
                StartCoroutine(attack("right", playerUnit, enemyUnit));
            }
            else
            {
                if (playerUnit.rightDisabled)
                    Dialogue.text = "Disabled, choose another action";
                else
                    Dialogue.text = "Not Enough Energy, choose another action";
            }
        }
    }

    public void OnLeftArm()
    {
        // check that it is the player's turn
        if (state == BattleState.PLAYERTURN)
        {
            // if they have enough energy to attack
            if (playerUnit.currEnergy >= playerUnit.leftWeapon.cost)
            {
                state = BattleState.ATTACKING;
                StartCoroutine(attack("left", playerUnit, enemyUnit));
            }
            else
            {
                Dialogue.text = "Not Enough Energy, choose another action";
            }
        }
    }

    public void OnBack()
    {
        // check that it is the player's turn
        if (state == BattleState.PLAYERTURN)
        {
            // if they have not used the back weapon this turn
            if (!playerBackWeaponUsed && !playerUnit.shoulderDisabled)
            {
                state = BattleState.ATTACKING;
                playerBackWeaponUsed = true;
                StartCoroutine(attack("back", playerUnit, enemyUnit));
            }
            else
            {
                if (playerUnit.shoulderDisabled)
                    Dialogue.text = "Disabled, choose another action";
                else
                    Dialogue.text = "Already used, choose another action";
            }
        }
    }

    // give player shield, 5x energy used 
    public void OnGuard()
    {
        // check that it is the player's turn
        if (state == BattleState.PLAYERTURN)
        {
            // if they have energy left
            if (playerUnit.currEnergy > 0 && !playerUnit.guardDisabled)
            {
                state = BattleState.ATTACKING;
                playerUnit.GainShield(playerUnit.currEnergy * 5);
                playerUnit.SpendEnergy(playerUnit.currEnergy);
                playerHUD.SetHUD(playerUnit);
                PlayerTurn();
            }
            else
            {
                if (playerUnit.guardDisabled)
                    Dialogue.text = "Disabled, choose another action";
                else
                    Dialogue.text = "Not Enough Energy, choose another action";
            }
        }
    }

    // player ends turn go to enemy turn
    public void OnPass()
    {
        if (state == BattleState.PLAYERTURN)
        {
            state = BattleState.ENEMYTURN;
            enemyBackWeaponUsed = false;
            playerUnit.Charge(playerUnit.frame.regen + playerUnit.legs.regenBonus);
            playerHUD.SetHUD(playerUnit);
            enemyUnit.ResetShield();
            enemyHUD.SetHUD(enemyUnit);
            enemyUnit.currHP -= enemyUnit.burn;
            if (enemyUnit.currHP < 0)
                enemyUnit.currHP = 0;
            enemyHUD.SetHUD(enemyUnit);
            StartCoroutine("checkEnd");
            EnemyTurn();
        }
    }

    //function to attack used by enemy or player
    //pass in weapon type, the attacking mech, and the target mech
    IEnumerator attack(string weapon, Mech attacker, Mech target)
    {
        int damage;
        int accuracy;
        int hitRoll = Random.Range(1, 101);
        int evasion = target.frame.evasion + target.legs.evasionBonus;
        int armour = target.frame.armour + target.legs.armourBonus;
        

        //determine damage and accuracy for weapon used
        if (weapon == "right")
        {
            Dialogue.text = attacker.frame.frameName + " used " + attacker.rightWeapon.weaponName;

            if (ability != "WeaponCharge") // unless ability for free attack is used spend energy
                attacker.SpendEnergy(playerUnit.rightWeapon.cost);

            accuracy = attacker.rightWeapon.accuracy + attacker.legs.accuracyBonus;
            damage = Random.Range(attacker.rightWeapon.minDmg, attacker.rightWeapon.maxDmg + 1);
            ability = attacker.rightWeapon.ability;
        }
        else if (weapon == "left")
        {
            Dialogue.text = attacker.frame.frameName + " used " + attacker.leftWeapon.weaponName;

            if (ability != "WeaponCharge") // unless ability for free attack is used spend energy
                attacker.SpendEnergy(playerUnit.leftWeapon.cost);

            accuracy = attacker.leftWeapon.accuracy + attacker.legs.accuracyBonus;
            damage = Random.Range(attacker.leftWeapon.minDmg, attacker.leftWeapon.maxDmg + 1);
            ability = attacker.leftWeapon.ability;
        }
        else
        {
            Dialogue.text = attacker.frame.frameName + " used " + attacker.backWeapon.weaponName;

            accuracy = attacker.backWeapon.accuracy + attacker.legs.accuracyBonus;
            damage = Random.Range(attacker.backWeapon.minDmg, attacker.backWeapon.maxDmg + 1);
            ability = attacker.backWeapon.ability;
        }

        // before attack abilities
        if (ability == "WeaponCharge")
        {
            yield return new WaitForSeconds(2f);
            Dialogue.text = "Next attack costs 0 Energy";
        }
        else if (ability == "energyLoss")
        {
            target.currEnergy--;
            yield return new WaitForSeconds(2f);
            Dialogue.text = "Target energy reduced";
        }
        else if (ability == "ShoulderDisable")
        {
            target.shoulderDisabled = true;
            yield return new WaitForSeconds(2f);
            Dialogue.text = "Target shoulder disabled";
        }
        else if (ability == "rightWeaponDisable")
        {
            target.rightDisabled = true;
            yield return new WaitForSeconds(2f);
            Dialogue.text = "Target right arm disabled";
        }
        else if (ability == "guardDisable")
        {
            target.guardDisabled = true;
            yield return new WaitForSeconds(2f);
            Dialogue.text = "Target guard disabled";
        }


        yield return new WaitForSeconds(2f);

        //determine if it hits
        accuracy -= evasion;
        damage += attacker.level - 1;

        if (ability != "ignoreArmor" && ability != "ignoreArmourLowHPEvasionDown10" && ability != "ignoreArmorLowHPEvasionDown20") // if ignore armour ability armour wont be subtracted from damage
            damage -= armour;

        if (hitRoll <= accuracy) // on hit
        {
            //ability to raise evasion if enemy is below half hp
            if (ability == "lowHPEvasionUp")
            {
                if (target.currHP < target.maxHP / 2)
                {
                    attacker.frame.evasion += 10;
                    yield return new WaitForSeconds(2f);
                    Dialogue.text = "Evasion increased";
                }
            }

            // cripple special effect -1 to target's armour
            else if (ability == "armourPierce")
            {
                target.frame.armour -= 3;
                if (target.frame.armour < 0)
                {
                    target.frame.armour = 0;
                    if (target.legs.armourBonus > 3)
                    {
                        target.legs.armourBonus -= 3;
                    }
                    else
                    {
                        target.frame.armour = 0;
                    }
                }
                yield return new WaitForSeconds(2f);
                Dialogue.text = "Armour vastly decreased";
            }

            // ability to lower enemy evasion if enemy is below half hp
            else if (ability == "ignoreArmourLowHPEvasionDown10")
            {
                if (target.currHP < target.maxHP / 2)
                {
                    target.frame.evasion -= 10;
                    if (target.frame.evasion < 0)
                        target.frame.evasion = 0;

                    yield return new WaitForSeconds(2f);
                    Dialogue.text = "Target evasion decreased";
                }
            }
            else if (ability == "ignoreArmourLowHPEvasionDown20")
            {
                if (target.currHP < target.maxHP / 2)
                {
                    target.frame.evasion -= 20;
                    if (target.frame.evasion < 0)
                        target.frame.evasion = 0;

                    yield return new WaitForSeconds(2f);
                    Dialogue.text = "Target evasion greatly decreased";
                }
            }

            //reduce target's shield abilities
            else if (ability == "ShieldDamage10")
            {
                if (target.shield >= 10) 
                {
                    target.LoseShield(10);
                }
                else // reduce shield to 0
                {
                    target.ResetShield();
                }
                yield return new WaitForSeconds(2f);
                Dialogue.text = "Target shield drained";
            }
            else if (ability == "ShieldDamage15")
            {
                if (target.shield >= 15)
                {
                    target.LoseShield(15);
                }
                else // reduce shield to 0
                {
                    target.ResetShield();
                }
                yield return new WaitForSeconds(2f);
                Dialogue.text = "Target shield drained";
            }

            if (damage < 0) // cant deal negative damage
            {
                damage = 0;
            }
            if (target.shield > 0) // if shields take damage on them
            {
                if (target.shield >= damage) // if eneough take all damage on shields
                {
                    target.LoseShield(damage);
                }
                else // take as much on shields then the rest as damage
                {
                    damage -= target.shield;
                    target.ResetShield();
                    target.TakeDamage(damage);
                }
            }
            else
            {
                target.TakeDamage(damage); // if no shields just take damage
            }

            Dialogue.text = damage + " damage was dealt";

            // cripple special effect -1 to target's armour
            if (ability == "cripple")
            {
                target.frame.armour--;
                if (target.frame.armour < 0)
                {
                    target.frame.armour = 0;
                    if (target.legs.armourBonus > 0)
                    {
                        target.legs.armourBonus--;
                    }
                }
                yield return new WaitForSeconds(2f);
                Dialogue.text = "Armour decreased";
            }
            // burn types
            else if (ability == "burn1")
            {
                target.burn++;
                yield return new WaitForSeconds(2f);
                Dialogue.text = "Target burned";
            }
            else if (ability == "burn2")
            {
                target.burn += 2;
                yield return new WaitForSeconds(2f);
                Dialogue.text = "Target very burned";
            }
            else if (ability == "burnDouble")
            {
                target.burn *= 2;
                yield return new WaitForSeconds(2f);
                Dialogue.text = "Target burn doubled";
            }
            else if (ability == "burn6")
            {
                target.burn++;
                yield return new WaitForSeconds(2f);
                Dialogue.text = "Target heavily burned";
            }

            //update HUDs
            playerHUD.SetHUD(playerUnit);
            enemyHUD.SetHUD(enemyUnit);

            yield return new WaitForSeconds(2f);
            StartCoroutine("checkEnd");
        }
        else // on miss
        {
            Dialogue.text = "Miss!";
            playerHUD.SetHUD(playerUnit);
            enemyHUD.SetHUD(enemyUnit);
            yield return new WaitForSeconds(2f);
        }
        
        if (ability != "multihit4")
        {
            // if player turn they go again until they pass
            if (state == BattleState.ATTACKING)
            {
                PlayerTurn();
            }
            // if enemy turn they go until they pass
            else if (state == BattleState.ENEMYTURN)
            {
                EnemyTurn();
            }
        }
        else
        {
            if (multihit == 4)
            {
                multihit = 0;
                // if player turn they go again until they pass
                if (state == BattleState.ATTACKING)
                {
                    PlayerTurn();
                }
                // if enemy turn they go until they pass
                else if (state == BattleState.ENEMYTURN)
                {
                    EnemyTurn();
                }
            }
            else
            {
                multihit++;
                attack(weapon, attacker, target);
            }
        }
    }

    //enemy turn AI
    public void EnemyTurn()
    {
        if (state == BattleState.ENEMYTURN)
        {
            // enemy does a random action
            int randomAction = Random.Range(0, 5);

            // if they are out of energy end turn
            if (enemyUnit.currEnergy == 0)
            {
                enemyUnit.Charge(enemyUnit.frame.regen + enemyUnit.legs.regenBonus);
                enemyHUD.SetHUD(enemyUnit);
                playerBackWeaponUsed = false;
                playerUnit.ResetShield();
                playerHUD.SetHUD(playerUnit);
                playerUnit.currHP -= playerUnit.burn;
                if (playerUnit.currHP < 0)
                    playerUnit.currHP = 0;
                playerHUD.SetHUD(playerUnit);
                StartCoroutine("checkEnd");
                PlayerTurn();
            }
            // right weapon attack
            if (randomAction == 0)
            {
                if (enemyUnit.currEnergy >= enemyUnit.rightWeapon.cost && !enemyUnit.rightDisabled)
                {

                    enemyHUD.SetHUD(enemyUnit);
                    StartCoroutine("checkEnd");

                    StartCoroutine(attack("right", enemyUnit, playerUnit));
                }
                else
                    EnemyTurn();
            }
            // left weapon attack
            else if (randomAction == 1)
            {
                if (enemyUnit.currEnergy >= enemyUnit.leftWeapon.cost)
                {

                    enemyHUD.SetHUD(enemyUnit);
                    StartCoroutine("checkEnd");

                    StartCoroutine(attack("left", enemyUnit, playerUnit));
                }
                else
                    EnemyTurn();
            }
            // shoulder weapon attack
            else if (randomAction == 2)
            {
                if (enemyBackWeaponUsed == false && !enemyUnit.shoulderDisabled)
                {

                    enemyHUD.SetHUD(enemyUnit);
                    StartCoroutine("checkEnd");

                    enemyBackWeaponUsed = true;
                    StartCoroutine(attack("back", enemyUnit, playerUnit));
                }
                else
                {
                    EnemyTurn();
                }
            }
            // guard
            else if (randomAction == 3 && enemyUnit.currEnergy > 0 && !enemyUnit.guardDisabled)
            {

                enemyHUD.SetHUD(enemyUnit);
                StartCoroutine("checkEnd");

                enemyUnit.GainShield(enemyUnit.currEnergy * 5);
                enemyUnit.SpendEnergy(enemyUnit.currEnergy);
                enemyHUD.SetHUD(enemyUnit);
                enemyUnit.Charge(enemyUnit.frame.regen + enemyUnit.legs.regenBonus);
                enemyHUD.SetHUD(enemyUnit);
                playerBackWeaponUsed = false;
                playerUnit.ResetShield();
                playerHUD.SetHUD(playerUnit);
                playerUnit.currHP -= playerUnit.burn;
                if (playerUnit.currHP < 0)
                    playerUnit.currHP = 0;
                playerHUD.SetHUD(playerUnit);
                StartCoroutine("checkEnd");
                PlayerTurn();
            }
            // pass turn to player
            else
            {
                enemyUnit.Charge(enemyUnit.frame.regen + enemyUnit.legs.regenBonus);
                enemyHUD.SetHUD(enemyUnit);
                playerBackWeaponUsed = false;
                playerUnit.ResetShield();
                playerHUD.SetHUD(playerUnit);
                playerUnit.currHP -= playerUnit.burn;
                if (playerUnit.currHP < 0)
                    playerUnit.currHP = 0;
                playerHUD.SetHUD(playerUnit);
                StartCoroutine("checkEnd");
                PlayerTurn();
            }
        }
    }

    // reward screen display and gain rewards then go to menu
    IEnumerator rewards()
    {
        // randomize rewards and gain them
        int tier1;
        int tier2;
        int tier3;
        int exp;

        if (globalController.GetComponent<GlobalController>().week == 1)
        {
            tier1 = Random.Range(50, 151);
            tier2 = Random.Range(25, 76);
            tier3 = Random.Range(0, 31);
        }
        else if (globalController.GetComponent<GlobalController>().week == 2)
        {
            tier1 = Random.Range(75, 181);
            tier2 = Random.Range(50, 101);
            tier3 = Random.Range(10, 51);
        }
        else
        {
            tier1 = Random.Range(75, 181);
            tier2 = Random.Range(65, 151);
            tier3 = Random.Range(45, 70);
        }
        if (state == BattleState.LOST)
        {
            tier1 = tier1 / 2;
            tier2 = tier2 / 2;
            tier3 = tier3 / 2;
            exp = 0;
        }
        else
        {
            exp = enemyUnit.exp;
        }

        thePlayer.GetComponent<Player>().tier1Scrap += tier1;
        thePlayer.GetComponent<Player>().tier2Scrap += tier2;
        thePlayer.GetComponent<Player>().tier3Scrap += tier3;

        thePlayer.GetComponent<Player>().mech.exp += exp;

        // display rewards
        rewardScreen.SetActive(true);
        scrapRewards.text = tier1 + "\n\n" + tier2 + "\n\n" + tier3;
        expRewards.text = "You gained " + exp + " EXP";
        
        //check for level up
        if (thePlayer.GetComponent<Player>().mech.exp >= thePlayer.GetComponent<Player>().mech.expToNext)
        {
            //level up
            thePlayer.GetComponent<Player>().mech.level++;
            yield return new WaitForSeconds(2f);
            expRewards.text = "You gained a level!";
            thePlayer.GetComponent<Player>().mech.expToNext *= 2;
            thePlayer.GetComponent<Player>().mech.exp = 0;
        }

        yield return new WaitForSeconds(4f);

        // if tutorial fight go back to story
        if (globalController.GetComponent<GlobalController>().tutorial)
        {
            globalController.GetComponent<GlobalController>().story++;
            SceneManager.LoadScene("VisualNovel");
        }
        // if boss fight progress story
        else if (globalController.GetComponent<GlobalController>().bossFight)
        {
            globalController.GetComponent<GlobalController>().bossFight = false;
            globalController.GetComponent<GlobalController>().story++;
            SceneManager.LoadScene("VisualNovel");
        }
        // otherwise go back to menu
        else
            SceneManager.LoadScene("Menu");
    }

    // if someone has died it will handle the end of battle sequence, otherwise it will do nothing
    IEnumerator checkEnd()
    {
        //if player dies
        if (playerUnit.currHP <= 0)
        {
            state = BattleState.LOST;

            if (globalController.GetComponent<GlobalController>().bossFight) // lose a story fight you lose the game
            {
                Dialogue.text = "You Died...";

                yield return new WaitForSeconds(2f);

                //CHANGE SCENE TO GAME OVER SCREEN
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                Dialogue.text = "Some rewards lost";

                StartCoroutine("rewards");


            }
        }
        if (enemyUnit.currHP <= 0)
        {
            state = BattleState.WON;
            Dialogue.text = "You Win!";

            yield return new WaitForSeconds(2f);

            StartCoroutine("rewards");
        }
    }
}