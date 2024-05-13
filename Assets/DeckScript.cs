
//todo
// make a "playedCards" list
// make a "enemyplayedCards" list
// make it so the cards can attack
// 


using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR;

public class DeckScript : MonoBehaviour
{
    public Camera PlayerCamera;

    float CardPos = -12;

    public GameObject prefab;

    public List<GameObject> Deck = new List<GameObject>();

    public List<GameObject> Hand = new List<GameObject>();

    public List<GameObject> Goats = new List<GameObject>();

    public List<GameObject> PlayedCards = new List<GameObject>();

    public float handxPos = 0;

    public GameObject Selected;

    public bool DrawPhase = true;
    public bool PlayPhase = false;
    public bool AttackPhases = false;

    // Start is called before the first frame update
    void Start() //insratinitaing the cards and givign them their values and stuff onto the card prefabs
    {

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

            //this variabl;e is used to make the illusion of the cards being ontop of each other
            y2pos = y2pos + 0.2f;
        }

        PlayedCards = new List<GameObject> { null, null, null, null };

        PlayedCards.Capacity = 4;

        // these run the functions at the start of scene
        reader.Close();
        ShuffleDeck();
        DrawThreeCards();
        SpawnGoats();
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

    public void SpawnGoats()
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
        if (hit.transform != null && hit.transform.CompareTag("Card") && Input.GetMouseButtonDown(0) && Hand.Contains(hit.transform.gameObject) && PlayPhase == true)
        {
            hit.transform.GetComponent<CardScript>().Select_Card();
            Selected = hit.transform.gameObject;
        }

        if (Input.GetMouseButtonDown(0) && Selected != null && hit.transform != null && hit.transform.CompareTag("Lane"))
        {
            //print (hit.transform.position.ToString());

            // takes the lane youve clicked on and puts the card into the list
            int laneToUse = int.Parse(hit.transform.name);
            if (PlayedCards[laneToUse] == null)
            {
                PlayedCards[laneToUse] = Selected.gameObject;
                Selected.transform.SetParent(hit.transform);
                Selected.transform.localPosition = Vector3.zero;
                Hand.Remove(Selected);
                Selected = null;
            }
            // ????
            

            // print (Selected.transform.position.ToString());
            // print (hit.transform.transform.name.ToString());

            // ????
            
            
        }



    }
}
