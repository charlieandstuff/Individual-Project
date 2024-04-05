



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



            string[] cols = line.Split(',');
            //print(cols[0]);
            //print(cols[1]);
            //print(cols[2]);
            //print(cols[3]);
            //CardScript newCard = new CardScript();
            GameObject newGO = Instantiate(prefab, new Vector3(8, y2pos, 7.5f), Quaternion.identity);
            CardScript newCard = newGO.GetComponent<CardScript>();
            newCard.CardName = cols[0];
            newCard.CostStat = int.Parse(cols[1]);
            newCard.PowerStat = int.Parse(cols[2]);
            newCard.ToughnessStat = int.Parse(cols[3]);
            Deck.Add(newGO);
            y2pos = y2pos + 0.15f;


            // this is so the function runs on the start of the program


        }




        reader.Close();

        ShuffleDeck();
        DrawThreeCards();
        SpawnGoats();
    }

    public void DrawThreeCards()
    {
        for (int i = 0; i < 3; i++)
        {
            Hand.Add(Deck[0]);
            Deck[0].transform.position = new Vector3(CardPos, 6, -4.5f);
            Deck.RemoveAt(0);
            CardPos = CardPos + 3;
        }


    }
    public void DrawGoat()
    {
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
        GameObject temp = Deck[index1];
        Deck[index1] = Deck[index2];
        Deck[index2] = temp;
    }


    //
    public void ShuffleDeck()
    {
        for (int i = 0; i < 100; i++)
        {
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


    private void SelectCard()
        {

        }

    private void Update()
    {
        RaycastHit hit = GetRayFromScreen();
        if (hit.transform != null)
        {
            
        }
    }
}
