
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

    // Start is called before the first frame update
    void Start()
    {

        string path = "Assets/cards.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);

        string database = reader.ReadToEnd();


        string[] lines = database.Split('\n');
        float y2pos = 4.5f;



        foreach (string line in lines)
        { 
            //this line splits the string at each comma to get seperated values
            string[] cols = line.Split(',');

            //creates a new card for each seperate card and puts them apart
            GameObject newGO = Instantiate(prefab, new Vector3(8, y2pos, 7.5f), Quaternion.identity);
            CardScript newCard = newGO.GetComponent<CardScript>();

            //each of these make the seperated values into the stats which is used in the "card script"
            newCard.CardName = cols[0];
            newCard.CostStat = int.Parse(cols[1]);
            newCard.PowerStat = int.Parse(cols[2]);
            newCard.ToughnessStat = int.Parse(cols[3]);

            //adds the  new card to the "Deck Script"
            Deck.Add(newGO);

            //this variabl;e is used to make the illusion of the cards being ontop of each other
            y2pos = y2pos + 0.15f;
        }

        // these run the functions at the start of scene
        reader.Close();
        ShuffleDeck();
        DrawThreeCards();
        SpawnGoats();
    }



    public void DrawThreeCards()
    {
        // used to draw cards at start of game
        for (int i = 0; i < 3; i++)
        {
            // adds the card in position Deck[0] into the hand list and removes it from the position of Deck[0]
            Hand.Add(Deck[0]);
            Deck[0].transform.position = new Vector3(CardPos, 6, -4.5f);
            Deck.RemoveAt(0);
            CardPos = CardPos + 3;
        }


    }
    public void DrawGoat()
    {
        //function to draw from an alternate deck and garuntees a palyable card
        Hand.Add(Goats[0]);
        Goats[0].transform.position = new Vector3(CardPos, 6, -4.5f);
        Goats.RemoveAt(0);
        CardPos = CardPos + 3;
    }

    public void SpawnGoats()
    {
        float ypos = 4.5f;
        for (int i = 0; i < 10; i++)
        {
            GameObject newGO = Instantiate(prefab, new Vector3(8, ypos, 2), Quaternion.identity);
            CardScript newCard = newGO.GetComponent<CardScript>();
            newCard.CardName = "goat";
            newCard.CostStat = 0;
            newCard.PowerStat = 0;
            newCard.ToughnessStat = 1;
            Goats.Add(newGO);
            ypos = ypos + 0.15f;
        }
    }


    public void DrawDeckCard()
    {
        // add to hand
        //Instantiate(Deck[0]);
        Hand.Add(Deck[0]);
        Deck[0].transform.position = new Vector3(CardPos, 6, -4.5f);
        Deck.RemoveAt(0);
        CardPos = CardPos + 3;
    }


    //swap function to shuffle the deck
    public void swap(int index1, int index2)
    {
        //this function is used as a shuffle by randomly swapping the cards
        GameObject temp = Deck[index1];
        Deck[index1] = Deck[index2];
        Deck[index2] = temp;
    }


    //
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
        if (Physics.Raycast(ray, out hit)) { print(hit.transform.name); }
        return hit;
    }




    private void Update() // to fix errors when mouse isnt inerating with something on the screen
    {
        RaycastHit hit = GetRayFromScreen();
        if (hit.transform != null && hit.transform.CompareTag("Card"))
        {
            hit.transform.GetComponent<CardScript>().Select_Card();
        }
    }
}
