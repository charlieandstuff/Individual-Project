using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
public class CardScript : MonoBehaviour
{


    //this stores the data
    public string CardName;
    //this line shows the data onto the text
    public TMPro.TextMeshPro CardNameGUI;

    public string PowerStat;
    public TMPro.TextMeshPro PowerStatGUI;

    public string ToughnessStat;
    public TMPro.TextMeshPro ToughnessStatGUI;

    public string CostStat;
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
        
    }



    //skin walker,4,7,4
    //cave bear,4,6,8
}
