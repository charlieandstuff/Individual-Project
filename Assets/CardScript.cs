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
        print("closer");
    }

    public void LerpBack()
    {
        print("backerer");
    }
    //skin walker,4,7,4
    //cave bear,4,6,8
}
