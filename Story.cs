using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    public GameObject globalController;
    public Text speech;
    public GameObject background;
    public GameObject left;
    public GameObject right;
    public GameObject speechBox;
    public GameObject credits;

    //sprite array
    public Sprite[] characterSprites;
    public Sprite[] backgroundSprites;

    public void Awake()
    {
        globalController = GameObject.Find("GlobalController");
        left.SetActive(false);
        right.SetActive(false);
        if (globalController.GetComponent<GlobalController>().story == 0)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[0]; // omega sprite
            speech.text = "It has been a few centuries since humans migrated to space from Earth. " +
                "The planet became uninhabitable from unchecked pollution, war and strife. " +
                "The only option to avoid extinction was to leave, and thus mankind colonized numerous planets that were livable throughout the cosmos. ";
        }
        else if (globalController.GetComponent<GlobalController>().story == 9)
        {
            globalController.GetComponent<GlobalController>().tutorial = false;
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[1]; // battle background
            left.SetActive(true);
            left.GetComponent<Image>().sprite = characterSprites[0]; // widget
            speech.text = "Widget: \nDamn, this thing has fire power! With this we can for sure get registered!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 19)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[0]; // omega sprite
            left.SetActive(true);
            right.SetActive(false);
            left.GetComponent<Image>().sprite = characterSprites[0]; // widget
            speech.text = "Widget: \nHere it is… Time to get registered and enter in this tournament!" +
                " I wonder what the lineup will be this year! Think there will be any fun Mechs?";
        }
        else if (globalController.GetComponent<GlobalController>().story == 43)
        {
            globalController.GetComponent<GlobalController>().bossNumber++;
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[4]; // collosseum sprite
            left.SetActive(true);
            right.SetActive(true);
            left.GetComponent<Image>().sprite = characterSprites[0]; // widget
            right.GetComponent<Image>().sprite = characterSprites[3]; // junk monger
            speech.text = "Widget: \nNot bad old man…";
        }
        else if (globalController.GetComponent<GlobalController>().story == 54)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[3]; // reception sprite
            left.SetActive(true);
            right.SetActive(true);
            left.GetComponent<Image>().sprite = characterSprites[0]; // widget
            right.GetComponent<Image>().sprite = characterSprites[1]; // screw
            speech.text = "Screw: \nAlright, this is round two… If we can pull off this match, we’ll be right toward the finals.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 62)
        {
            globalController.GetComponent<GlobalController>().bossNumber++;
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[4]; // collosseum sprite
            left.SetActive(true);
            right.SetActive(true);
            left.GetComponent<Image>().sprite = characterSprites[0]; // widget
            right.GetComponent<Image>().sprite = characterSprites[4]; // corrigan
            speech.text = "Corrigan: \nNgh… An unexpected variable…";
        }
        else if (globalController.GetComponent<GlobalController>().story == 69)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[3]; // reception background
            left.SetActive(true);
            right.SetActive(true);
            left.GetComponent<Image>().sprite = characterSprites[0]; // widget
            right.GetComponent<Image>().sprite = characterSprites[2]; // bolt
            speech.text = "Bolt: \nThis is it… The final match against Marlowe… He has no real strong weaknesses and is a deadly pilot.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 76)
        {
            globalController.GetComponent<GlobalController>().bossNumber++;
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[4]; // collosseum sprite
            left.SetActive(true);
            right.SetActive(true);
            left.GetComponent<Image>().sprite = characterSprites[0]; // widget
            right.GetComponent<Image>().sprite = characterSprites[5]; // marlowe
            speech.text = "Marlowe: \nHow… To think that I could lose to the likes of you…";
        }
    }


    //displays the story in the visual novel section
    public void AdvanceStory()
    {
        globalController.GetComponent<GlobalController>().story++;

        if (globalController.GetComponent<GlobalController>().story == 1)
        {
            speech.text = "Through space travel, precious metals became abundant with the large amount of planets taken over by small populations. " +
                "So much so, that what were once created as giant robot war machines known as Mecha, were now seen as commodities and something used for sport.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 2)
        {
            speech.text = "A luxury on many planets was to battle large mechanized robots in massive tournaments for fame and wealth, with many Mecha pilots going on to earn acclaim on their respective planets.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 3)
        {
            speech.text = " Here on Omega 9, it is the only way to escape the segregated deserts used to house those unfortunate enough to be born into them, " +
                "and be accepted into Omega City, by registering to be a pilot to escape living in the city’s glorified junkyard…";
        }
        else if (globalController.GetComponent<GlobalController>().story == 4)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[1]; // battle background
            left.SetActive(true);
            left.GetComponent<Image>().sprite = characterSprites[0]; // widget
            speech.text = "Widget: \nWe finally did it… Here it is! A Mecha! With this we can get registered as pilots! Way to go little guy!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 5)
        {
            right.SetActive(true);
            right.GetComponent<Image>().sprite = characterSprites[1]; //screw
            speech.text = "Screw: \nC’mon, it was a group effort, if not for you guys I couldn’t have made the parts, and who knows if they would even work together without Bolt.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 6)
        {
            right.GetComponent<Image>().sprite = characterSprites[2]; //bolt
            speech.text = "Bolt: \nHey, as long as it all works, I’m happy. Though, I do want to make sure its functioning as intended. You wanna to give it a test drive?";
        }
        else if (globalController.GetComponent<GlobalController>().story == 7)
        {
            speech.text = "Widget: \nI’m on it! I think we should be able to find something to fight in the Junkyard!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 8)
        {
            globalController.GetComponent<GlobalController>().tutorial = true;
            globalController.GetComponent<GlobalController>().LoadBattleScene();
        }        
        else if (globalController.GetComponent<GlobalController>().story == 10)
        {
            right.SetActive(true);
            right.GetComponent<Image>().sprite = characterSprites[1]; //screw
            speech.text = "Screw: \nI think the next Iron Gauntlet Tournament is doing preliminaries at the end of the week…";
        }
        else if (globalController.GetComponent<GlobalController>().story == 11)
        {
            right.GetComponent<Image>().sprite = characterSprites[2]; //bolt
            speech.text = "Bolt: \nIf we want to stand a chance, I think we might want to make some adjustments to our old mech here.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 12)
        {
            right.GetComponent<Image>().sprite = characterSprites[1]; //screw
            speech.text = "Screw: \nIf you scavenge for scrap, I can fabricate more parts.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 13)
        {
            right.GetComponent<Image>().sprite = characterSprites[2]; //bolt
            speech.text = "Bolt: \nAnd then I can put them together! The better our mech, the better our chance of coming out on top!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 14)
        {
            speech.text = "Widget: \nRight, I guess just coming in with a basic mecha like this won’t be the best idea.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 15)
        {
            right.GetComponent<Image>().sprite = characterSprites[1]; //screw
            speech.text = "Screw: You can also train against monsters in the scrap yard. " +
                "You’ll probably take a bit to get used to the controls in the cockpit, and the better you are, the better the mech will run.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 16)
        {
            right.GetComponent<Image>().sprite = characterSprites[2]; //bolt
            speech.text = "Bolt: \nWe won’t get as much scrap from scavenging the junkyard, but the experience would be worth it.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 17)
        {
            speech.text = "Widget: \nJust you watch, we’ll take that tournament down no problem!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 18)
        {
            globalController.GetComponent<GlobalController>().LoadMenu();
        }
        else if (globalController.GetComponent<GlobalController>().story == 20)
        {
            right.SetActive(true);
            right.GetComponent<Image>().sprite = characterSprites[2]; // bolt
            speech.text = "Bolt: \nOh big time, this kinda tournament people bring out all the stops to come out on top, we’ll see some big designs for sure!";
        }
        else if (globalController.GetComponent <GlobalController>().story == 21)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[2]; // omega city background
            speech.text = "Widget: \nWow it… It worked! They actually let us get registered and get in! " +
                "Wow… Look at all the sights and sounds, the air here is so clean and not dusty at all!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 22)
        {
            right.GetComponent<Image>().sprite = characterSprites[1]; // screw
            speech.text = "Screw: \nThere’s a part shop… I want to go check it out…";
        }
        else if (globalController.GetComponent<GlobalController>().story == 23)
        {
            speech.text = "Widget: \nAnd look look look! They have one of those flying cars! Nothing like the dirt rovers we have in the wastes!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 24)
        {
            right.GetComponent<Image>().sprite = characterSprites[2]; // bolt
            speech.text = "Bolt: \nC’mon… Let’s get signed up for the tournament and then we can check out the rest of the city. Ok?";
        }
        else if (globalController.GetComponent<GlobalController>().story == 25)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[3]; // reception background
            speech.text = "Widget: \nThere we go! They registered us as Team Junk. We got put into… The West bracket?";
        }
        else if (globalController.GetComponent<GlobalController>().story == 26)
        {
            right.GetComponent<Image>().sprite = characterSprites[1]; // screw
            speech.text = "Screw: \nAh, I think they divide it by region. " +
                "Since we’re in the wastes, technically we get grouped up with the West Ward since that bracket has less people in it.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 27)
        {
            speech.text = "*A rude man walks past, bumping into Screw and nearly knocking him over*";
        }
        else if (globalController.GetComponent<GlobalController>().story == 28)
        {
            speech.text = "Widget: \nHey, watch where you’re going!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 29)
        {
            right.GetComponent<Image>().sprite = characterSprites[5]; // marlowe
            speech.text = "Marlowe: \nOh… Sorry, my apologies. You wasteland bumpkins always get in the way, not knowing where you’re going… ";
        }
        else if (globalController.GetComponent<GlobalController>().story == 30)
        {
            speech.text = "Widget: \nWhat was that!? You want to go!?";
        }
        else if (globalController.GetComponent<GlobalController>().story == 31)
        {
            speech.text = "Marlowe: \nHah… Resorting to violence. Fear not, we will never ‘Go’ because you’ll never get out of the West bracket… " +
                "Like all wasteland bumpkins, you’ll lose there. Toodles.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 32)
        {
            right.SetActive(false);
            speech.text = "Widget: \nHey, get back here!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 33)
        {
            right.SetActive(true);
            right.GetComponent<Image>().sprite = characterSprites[1]; // screw
            speech.text = "Screw: \nIts fine Widget, really…";
        }
        else if (globalController.GetComponent<GlobalController>().story == 34)
        {
            right.GetComponent<Image>().sprite = characterSprites[2]; // bolt
            speech.text = "Bolt: \nI mean, now we know the competition, and more than anything, I want to beat him…";
        }
        else if (globalController.GetComponent<GlobalController>().story == 35)
        {
            speech.text = "*An announcer’s voice rings out* \nWould Team Junk please proceed to the arena, your starting match is to begin.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 36)
        {
            speech.text = "Bolt: \nAh, we better get going!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 37)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[4]; // collosseum background
            right.GetComponent<Image>().sprite = characterSprites[3]; // junk monger
            speech.text = "Widget: \nWait, you’re our matchup this time old man?";
        }
        else if (globalController.GetComponent<GlobalController>().story == 38)
        {
            speech.text = "Junk Monger: \nOh damn, the kiddies from the junkyard! Never thought you’d get a functioning mech no matter how hard you tried," +
                " but… Sorry to say, that hunk of junk won’t stand a chance compared to a pro like me!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 39)
        {
            speech.text = "Widget: \nWhat was that!?";
        }
        else if (globalController.GetComponent<GlobalController>().story == 40)
        {
            speech.text = "Junk Monger: \nYou kids don’t appreciate the scrap, you don’t LOVE the garbage like ME! " +
                "You couldn’t possibly build a Mech worthy of winning this tournament!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 41)
        {
            speech.text = "Junk Monger: \nI’ll be winning this all, showing these city shits a thing or two, and send you kids back to the junkyard, with your Mech added to the heap!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 42)
        {
            globalController.GetComponent<GlobalController>().bossFight = true;
            globalController.GetComponent<GlobalController>().LoadBattleScene();
        }
        else if (globalController.GetComponent<GlobalController>().story == 44)
        {
            speech.text = "Junk Monger: \nHah… You’re actually pretty good kid… You might actually have a shot at winning this thing, but you still got a long way to go with your Mecha." +
                " Keep fixing her up real good, I’ll help, and maybe you can make a name for us Junkyard Dogs.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 45)
        {
            right.GetComponent<Image>().sprite = characterSprites[2]; // bolt
            speech.text = "Bolt: \nWait, Junk Monger!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 46)
        {
            right.GetComponent<Image>().sprite = characterSprites[3]; // junk monger
            speech.text = "Junk Monger: \nHuh? What is it…?";
        }
        else if (globalController.GetComponent<GlobalController>().story == 47)
        {
            right.GetComponent<Image>().sprite = characterSprites[2]; // bolt
            speech.text = "Bolt: \nI… I always really respected what you did with Mecha. By the end of this all," +
                " would you mind if I interned under you? I always wanted to learn from a pro.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 48)
        {
            right.GetComponent<Image>().sprite = characterSprites[3]; // junk monger
            speech.text = "Junk Monger: \nI don’t train losers kiddo, so… Win this tournament and I’ll take you under my wing.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 49)
        {
            right.GetComponent<Image>().sprite = characterSprites[2]; // bolt
            speech.text = "Bolt: \nYou got it!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 50)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[3]; // reception background
            right.GetComponent<Image>().sprite = characterSprites[1]; // screw
            speech.text = "Screw: \nWell, there we have it, we won preliminaries. I think our next match in a week is against a guy named Corrigan… " +
                "He won his last match without taking a single hit. I think its safe to say his Mecha is very evasive. " +
                "We’ll want something that can actually land a hit on him.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 51)
        {
            right.GetComponent<Image>().sprite = characterSprites[2]; // bolt
            speech.text = "Bolt: \nI can think of a few things we can do.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 52)
        {
            speech.text = "Widget: \nI’m all pumped up now, I know we can do it!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 53)
        {
            globalController.GetComponent<GlobalController>().week = 2;
            globalController.GetComponent<GlobalController>().day = 1;
            globalController.GetComponent<GlobalController>().LoadMenu();
        }
        else if (globalController.GetComponent<GlobalController>().story == 55)
        {
            right.GetComponent<Image>().sprite = characterSprites[2]; // bolt
            speech.text = "Bolt: \nI hear that prissy guy from before is moving up along with us. Marlowe is his name, he’s actually doing really well.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 56)
        {
            speech.text = "Widget: \nMore reason for us to come out on top and beat him.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 57)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[4]; // collosseum background
            right.GetComponent<Image>().sprite = characterSprites[4]; // corrigan
            speech.text = "Corrigan: \nAh, I see you’re coming at me… With your poor tactics you won’t stand a chance. I ran the calculations, you zeros are going to lose.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 58)
        {
            speech.text = "Widget: \nSays you. We’re going to win this whole tournament!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 59)
        {
            speech.text = "Corrigan: \nHah… You really think you have a chance? Marlowe’s already won his match and going to finals," +
                " you all don’t have what it takes to take him down, and I’ll show you that grim reminder now.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 60)
        {
            speech.text = "Widget: \nBring it on!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 61)
        {
            globalController.GetComponent<GlobalController>().bossFight = true;
            globalController.GetComponent<GlobalController>().LoadBattleScene();
        }
        else if (globalController.GetComponent<GlobalController>().story == 63)
        {
            speech.text = "Corrigan: \nHmm… Fine, you win. You might actually have a chance against Marlowe. So, don’t disappoint me. " +
                "Beat that rich smug asshole and teach him a lesson. Show him that talent and money aren’t all that’s needed to win the Iron Gauntlet tournament.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 64)
        {
            speech.text = "Widget: \nHuh… You aren’t such a bad guy, huh?";
        }
        else if (globalController.GetComponent<GlobalController>().story == 65)
        {
            speech.text = "Corrigan: \nI just want to prove something.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 66)
        {
            right.GetComponent<Image>().sprite = characterSprites[1]; // screw
            speech.text = "Screw: \nYour calibrations on your parts were incredible… You are a good pilot Corrigan. Mind if I ask you some questions?";
        }
        else if (globalController.GetComponent<GlobalController>().story == 67)
        {
            right.GetComponent<Image>().sprite = characterSprites[4]; // corrigan
            speech.text = "Corrigan: \nI suppose I can squeeze in time for a rival.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 68)
        {
            globalController.GetComponent<GlobalController>().week = 3;
            globalController.GetComponent<GlobalController>().day = 1;
            globalController.GetComponent<GlobalController>().LoadMenu();
        }
        else if (globalController.GetComponent<GlobalController>().story == 70)
        {
            right.GetComponent<Image>().sprite = characterSprites[1]; // screw
            speech.text = "Screw: \nWe’re going to have to pull out all the stops…";
        }
        else if (globalController.GetComponent<GlobalController>().story == 71)
        {
            speech.text = "Widget: \nDon’t worry, we’ve got this…";
        }
        else if (globalController.GetComponent<GlobalController>().story == 72)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[4]; // collosseum background
            right.GetComponent<Image>().sprite = characterSprites[5]; // marlowe
            speech.text = "Marlowe: \nAh, so you actually decided to desecrate this hallowed arena with your sand caked boots. You junkyard hounds never cease to amaze me…";
        }
        else if (globalController.GetComponent<GlobalController>().story == 73)
        {
            speech.text = "Widget: \nPut a sock in it… This is just about you and me right now, pilot vs. pilot…";
        }
        else if (globalController.GetComponent<GlobalController>().story == 74)
        {
            speech.text = "Marlowe: \nOh, I’m painfully aware… And I plan to drive you back to the desert to show you your place… Now… Perish among the sands!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 75)
        {
            globalController.GetComponent<GlobalController>().bossFight = true;
            globalController.GetComponent<GlobalController>().LoadBattleScene();
        }
        else if (globalController.GetComponent<GlobalController>().story == 77)
        {
            speech.text = "Widget: \nIt's because you weren’t even really trying… You’re good, you have the best Mecha money can buy but… " +
                "In the end, you don’t have the same drive. You don’t have the same fire I have.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 78)
        {
            speech.text = "Marlowe: \nAccept your victory while you can… I will take my place again as the Iron Gauntlet champion… But for now… " +
                "Accept your win. Enjoy the spotlight, I suppose you’ve earned that much, whelp.";
        }
        else if (globalController.GetComponent<GlobalController>().story == 79)
        {
            right.SetActive(false);
            speech.text = "*The audience cheers*, Widget, Widget, Widget!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 80)
        {
            speech.text = "Widget: \nWow… I… Actually did it… We actually did it!";
        }
        else if (globalController.GetComponent<GlobalController>().story == 81)
        {
            right.SetActive(false);
            speech.text = "Team Junk succeeded, they had won the Iron Gauntlet Tournament and stand as the champions in Omega City… ";
        }
        else if (globalController.GetComponent<GlobalController>().story == 82)
        {
            right.SetActive(false);
            speech.text = "Team Junk succeeded, they had won the Iron Gauntlet Tournament and stand as the champions in Omega City… ";
        }
        else 
        {
            speechBox.SetActive(false);
            credits.SetActive(true);
            credits.GetComponent<Text>().text = "Junk King" +
                "\nA game by Team Scrap" +
                "\nLead Programmer: Joshua Kelln\nWriting: Catherine Grant" +
                "\nArt: Catherine Grant\nUI Design: Catherine Grant" +
                "\nGameplay: Catherine Grant and Joshua Kelln" +
                "\nTesting: Catherine Grant and Joshua Kelln" +
                "\nMusic: Sakamoto, Hiroshi. 13 Sentinels: Aegis Rim (Original Soundtrack), Basiscape. 2020";
        }
    }
}
