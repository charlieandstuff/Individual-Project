using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
public class CardScript : MonoBehaviour
{

    public bool Selected;
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
            LerpCloser();
        }
        else
        {
            LerpBack();
        }
    }

    public void LerpCloser()
    {
        transform.position = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(0, 10, 0), LerpSpeed*(Time.time - Start_Time) );
        print(LerpSpeed * (Start_Time - Time.time));
    }

    public void LerpBack()
    {
        
    }
  

    public void Select_Card ()
    {
        if (!Selected)
        {
        Selected = true;
        Start_Time = Time.time;
        }
        
    }
}
