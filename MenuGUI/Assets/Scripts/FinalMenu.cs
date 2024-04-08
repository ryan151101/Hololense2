using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MixedReality.Toolkit;
using MixedReality.Toolkit.UX;
using TMPro;

public class FinalMenu : MonoBehaviour
{
    public TextMeshProUGUI btnsClickedText;
    private string listOfBtnsClicked = "";

    public List<ButtonClickedData> allBtnsClicked;

    // For all buttons that were clicked throughout the entire program,
    // Print the name and its timestamp
    void Awake()
    {
        allBtnsClicked = ButtonsClickedTracker.GetAllButtonsClicked();

        for (int i = 0; i < allBtnsClicked.Count; i++)
        {
            listOfBtnsClicked += allBtnsClicked[i].btnName + " Button" +"\t\t" +" Clicked at: " + allBtnsClicked[i].timestamp + "\n\n";
            btnsClickedText.text = listOfBtnsClicked;
        }
    }

    
}
