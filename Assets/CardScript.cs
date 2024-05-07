using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using Unity.VisualScripting;
public class CardScript : MonoBehaviour
{

    public bool Selected;
    public bool lerped = false;

    //this stores the data
    public string CardName;
    //this line shows the data onto the text
    public TMPro.TextMeshPro CardNameGUI;

    public int PowerStat;
    public TMPro.TextMeshPro PowerStatGUI;

    public int ToughnessStat;
    public TMPro.TextMeshPro ToughnessStatGUI;

    public int CostStat;
    public TMPro.TextMeshPro CostStatGUI;

    //the two floats are used to determine how long a object has been "selected"
    public float LerpSpeed = 0;
    public float Start_Time = 0;
    public Vector3 Cardpos;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = CardName;
        CardNameGUI.text = CardName;
        PowerStatGUI.text = PowerStat.ToString();
        ToughnessStatGUI.text = ToughnessStat.ToString();
        CostStatGUI.text = CostStat.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        if (Selected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
               
            }
        }
       
    }

    //public void LerpCloser()
    //{
    //    transform.position = Vector3.Lerp(Cardpos, Cardpos + new Vector3(0, 0.5f, 0), LerpSpeed * (Time.time - Start_Time));
    //    print(LerpSpeed * (Start_Time - Time.time));
    //    lerped = true;
    //}

    //public void LerpBack()
    //{
    //    transform.position = Vector3.Lerp(Cardpos + new Vector3(0, 0.5f, 0), Cardpos, LerpSpeed * (Time.time - Start_Time));
    //}


    public void Select_Card()
    {
        if (!Selected)
        {
            //Cardpos = transform.position;
            Selected = true;
            Start_Time = Time.time;
        }

    }
}
