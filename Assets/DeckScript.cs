
//todo
// make a "enemy' (script that reacts to players moves)
// Enemy will not follow rules like costs but to level the playing feild you can see their moves a turn ahead
// make a list for enemy pre played cards
// make enemy be able to instatiate cards
// makde enemy kill visoli 
// make enemy be able to end its turn
// add card abilities
// add a "difficulty" to the enemy that can scale
//add customisability to deck



using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class DeckScript : MonoBehaviour
{
    public Camera PlayerCamera;

    float CardPos = -12;

    // prefab for the cards
    public GameObject prefab;

    // list for the deck of cards which player can draw
    public List<GameObject> Deck = new List<GameObject>();

    public List<GameObject> Hand = new List<GameObject>();

    public List<GameObject> Goats = new List<GameObject>();

    public List<GameObject> PlayedCards = new List<GameObject>();

    public List<GameObject> EnemyCards = new List<GameObject>();

    public List<GameObject> EnemySelectable = new List<GameObject>();

    public float handxPos = 0;


    // the object selected to be played
    public GameObject Selected;

    public int PlayerHealth = 15;
    public TextMeshProUGUI EnemyHealthGUI;
    public TextMeshProUGUI PlayerHealthGUI;
    public TextMeshProUGUI BloodMoneyGUI;
    public int EnemyHealth = 15;

    public int AttackLanes = 0;

    public int BloodMoney = 0;

    public bool DrawPhase = true;
    public bool PlayPhase = false;
    public bool AttackPhase = false;
    public bool EnemyPhase = false;
    public bool SacrificePhase = false;
    public bool DebugMode = false;

    // Start is called before the first frame update
    void Start() //insratinitaing the cards and givign them their values and stuff onto the card prefabs
    {
        EnemyHealthGUI.text = EnemyHealth.ToString();
        BloodMoneyGUI.text = BloodMoney.ToString();
        string path = "Assets/cards.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);

        string database = reader.ReadToEnd();


        string[] lines = database.Split('\n');
        float y2pos = 0f;



        foreach (string line in lines)
        {
            //this line splits the string at each comma to get seperated values
            string[] cols = line.Split(',');

            //creates a new card for each seperate card and puts them apart
            GameObject newGO = Instantiate(prefab, new Vector3(10.5f, y2pos, 1.5f), Quaternion.identity);
            CardScript newCard = newGO.GetComponent<CardScript>();

            //each of these make the seperated values into the stats which is used in the "card script"
            newCard.CardName = cols[0];
            newCard.CostStat = int.Parse(cols[1]);
            newCard.PowerStat = int.Parse(cols[2]);
            newCard.ToughnessStat = int.Parse(cols[3]);

            //adds the  new card to the "Deck Script"
            Deck.Add(newGO);
            EnemySelectable.Add(newGO);
            //this variabl;e is used to make the illusion of the cards being ontop of each other
            y2pos = y2pos + 0.2f;
        }

        PlayedCards = new List<GameObject> { null, null, null, null };
        EnemyCards = new List<GameObject> { null, null, null, null };
        //PlayedCards.Capacity = 4;

        // these run the functions at the start of scene
        reader.Close();
        ShuffleDeck();
        DrawThreeCards();
        SpawnGoats();
    }


    // used to make temperary print statements
    void Debug(string msg)
    {
        if (DebugMode)
        {
            print(msg);
        }
    }

    public void DrawThreeCards() // how the starting cards are taken from the deck
    {
        // used to draw cards at start of game
        for (int i = 0; i < 3; i++)
        {
            // adds the card in position Deck[0] into the hand list and removes it from the position of Deck[0]
            Hand.Add(Deck[0]);
            Deck[0].transform.position = new Vector3(CardPos, 0.5f, -10);
            Deck.RemoveAt(0);
            CardPos = CardPos + 3;
        }
    }
    public void DrawGoat()
    {
        //function to draw from an alternate deck and garuntees a palyable card
        if (DrawPhase == true)
        {
            Hand.Add(Goats[0]);
            Goats[0].transform.position = new Vector3(CardPos, 0.5f, -10f);
            Goats.RemoveAt(0);
            CardPos = CardPos + 3;
            DrawPhase = false;
            PlayPhase = true;
        }
    }

    public void SpawnGoats() // used to create the deck of goats
    {
        float ypos = 0f;
        for (int i = 0; i < 10; i++)
        {
            GameObject newGO = Instantiate(prefab, new Vector3(10.5f, ypos, -4.5f), Quaternion.identity);
            CardScript newCard = newGO.GetComponent<CardScript>();
            newCard.CardName = "goat";
            newCard.CostStat = 0;
            newCard.PowerStat = 0;
            newCard.ToughnessStat = 1;
            Goats.Add(newGO);
            ypos = ypos + 0.2f;
        }
    }

    public void DrawDeckCard()
    {
        if (DrawPhase == true)
        {
            Hand.Add(Deck[0]);
            Deck[0].transform.position = new Vector3(CardPos, 0.5f, -10f);
            Deck.RemoveAt(0);
            CardPos = CardPos + 3;
            DrawPhase = false;
            PlayPhase = true;
        }
        // add to hand
        //Instantiate(Deck[0]);

    }

    //swap function to shuffle the deck
    public void swap(int index1, int index2)
    {
        //this function is used as a shuffle by randomly swapping the cards
        GameObject temp = Deck[index1];
        Deck[index1] = Deck[index2];
        Deck[index2] = temp;
    }

    public void ShuffleDeck()
    {
        for (int i = 0; i < 100; i++)
        {
            //selects a random card to swap deck[0] with
            int random = Random.Range(0, Deck.Count - 1);
            // grab a random card
            // swap it with card 0
            swap(0, random);

        }
    }

    public void SacrificeCard()
    {
        if (PlayPhase == true)
        {
            PlayPhase = false;
            SacrificePhase = true;
        }
    }

    public void EndTurn()
    {
        if (PlayPhase == true)
        {
            PlayPhase = false;
            AttackPhase = true;
        }

    }
    private RaycastHit GetRayFromScreen() //returns the point of the screen the mouse is interacting with
    {
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

        }
        return hit;

    }

    private void Update() // This is the playing card code it checks all the contions and whether a player can play the card they've selected
    {
        RaycastHit hit = GetRayFromScreen();
        if (hit.transform != null && hit.transform.CompareTag("Card") && Input.GetMouseButtonDown(0) && Hand.Contains(hit.transform.gameObject) && PlayPhase)
        {
            hit.transform.GetComponent<CardScript>().Select_Card();
            Selected = hit.transform.gameObject;
            Debug(Selected.name);
        }

        // Sacrifice Phase


        if (SacrificePhase == true && Input.GetMouseButtonDown(0) && hit.transform != null && hit.transform.CompareTag("Card") && PlayedCards.Contains(hit.transform.gameObject))
        {
            print(hit.transform.gameObject);
            int index = PlayedCards.IndexOf(hit.transform.gameObject);
            PlayedCards[index] = null;
            Destroy(hit.transform.gameObject);
            BloodMoney = BloodMoney + 1;
            BloodMoneyGUI.text = BloodMoney.ToString();
            SacrificePhase = false;
            PlayPhase = true;
        }

        if (Input.GetMouseButtonDown(0) && Selected != null && hit.transform != null && hit.transform.CompareTag("Lane") && Selected.GetComponent<CardScript>().CostStat >= BloodMoney)
        {
            //print (hit.transform.position.ToString());

            // takes the lane youve clicked on and puts the card into the list
            int laneToUse = int.Parse(hit.transform.name);
            if (PlayedCards[laneToUse] == null)
            {
                BloodMoney = BloodMoney - Selected.GetComponent<CardScript>().CostStat;
                PlayedCards[laneToUse] = Selected.gameObject;
                Selected.transform.SetParent(hit.transform);
                Selected.transform.localPosition = Vector3.zero;
                Hand.Remove(Selected);
                Selected = null;
                BloodMoneyGUI.text = BloodMoney.ToString();
            }
            // ????


            // print (Selected.transform.position.ToString());
            // print (hit.transform.transform.name.ToString());

            // ????


        }

        if (AttackPhase == true)
        {
            // function which takes a played cards power and reduces the enemyhealth down by the played cards power stat if there is no card opposing it
            if (PlayedCards[AttackLanes] != null && EnemyCards[AttackLanes] == null)
            {
                // this is the part where it references the cardscript to obtain the "power" stat of the played card
                EnemyHealth = EnemyHealth - PlayedCards[AttackLanes].GetComponent<CardScript>().PowerStat;

                // this is to move the "attackLane" to go to the next "lane" in the played card script
                AttackLanes = AttackLanes + 1;

                // this is to show the affect the played cards power had on the enemy players health
                EnemyHealthGUI.text = EnemyHealth.ToString();
            }

            // this function runs if there is a card opposing the played card and instead reduces the enemy cards health by the played cards power
            // this is to check if a card opposes the played card
            else if (PlayedCards[AttackLanes] != null && EnemyCards[AttackLanes] != null)
            {
                //get the enemycards health stat and reduces it by the played cards power stat
                EnemyCards[AttackLanes].GetComponent<CardScript>().ToughnessStat = EnemyCards[AttackLanes].GetComponent<CardScript>().ToughnessStat - PlayedCards[AttackLanes].GetComponent<CardScript>().PowerStat;
                EnemyCards[AttackLanes].GetComponent<CardScript>().UpdateText();

                AttackLanes = AttackLanes + 1;

            }

            // function to skip empty lanes
            else if (PlayedCards[AttackLanes] == null)
            {

                AttackLanes = AttackLanes + 1;

            }

            // function made to make the attack phase end after all lanes have been accounted for 
            if (AttackLanes == 4)
            {
                AttackPhase = false;
                AttackLanes = 0;
                EnemyPhase = true;
            }

        }

        if (EnemyPhase == true)
        {
            // Choose a random card from EnemySelectable
            int chosen = Random.Range(0, EnemySelectable.Count);

            // Find an empty lane
            int laneToUse = -1;
            for (int i = 0; i < EnemyCards.Count; i++)
            {
                if (EnemyCards[i] == null)
                {
                    laneToUse = i;
                    break;
                }
            }

            // If an empty lane is found, instantiate the card and place it in the lane
            if (laneToUse != -1)
            {
                GameObject newCard = Instantiate(EnemySelectable[chosen], new Vector3(10.5f, 100, 1.5f), Quaternion.identity);
                EnemyCards[laneToUse] = newCard;

                // Set the position of the card to the lane position
                // Assuming you have predefined positions for enemy lanes
                Vector3 lanePosition = GetEnemyLanePosition(laneToUse);
                newCard.transform.position = lanePosition;
            }

            // End the enemy phase and start the draw phase
            EnemyPhase = false;
            DrawPhase = true;
        }
        // Helper method to get the position of the enemy lane
        Vector3 GetEnemyLanePosition(int laneIndex)
        {
            // Define your lane positions here
            // Example positions, you should replace these with your actual lane positions
            Vector3[] lanePositions = new Vector3[]
            {
        new Vector3(-11, 1, 4),
        new Vector3(-6, 1, 4),
        new Vector3(-1, 1, 4),
        new Vector3(4, 1, 4)
            };

            return lanePositions[laneIndex];
        }
    }
}
