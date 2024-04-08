using MixedReality.Toolkit.UX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class UnableToScanMenu : MonoBehaviour
{
    public TextMeshProUGUI medNameText, concentrationAmountText;
    public PressableButton concentrationAmountButton;


    private string correctMedicine, medName, concentration;

    void Awake()
    {
        // Change the color of "Dose" to orange on the progress bar
        ProgressBarController.changeProgressColor("Dose");

        correctMedicine = MedicationMenu.getCorrectMed();
        

        // Use Regex to extract the medicine name and the concentration from the correct medicine var
        string pattern = @"^(.*?)\((.*?)\)";
        
        // Match the pattern in the input string
        Match match = Regex.Match(correctMedicine, pattern);
        if (match.Success)
        {
            // Extract the text before the parentheses
            medName = match.Groups[1].Value.Trim();

            // Extract the text within parentheses
            concentration = match.Groups[2].Value.Trim();
        }


        medNameText.text = medName + " Availability:";
        concentrationAmountText.text = concentration;

        // add onclick event for the concentation button confirmation
        concentrationAmountButton.OnClicked.AddListener(() => concentrationBtnPressed());
    }


    // Concentration amount is confirmed, go to the administer menu next
    private void concentrationBtnPressed()
    {
        // Add button to the list of all btns clicked so far in the program
        ButtonsClickedTracker.AddButtonClicked(concentrationAmountButton);
        SwitchMenus.ChangeMenu(Menu.ADMINISTER_MENU, gameObject);
    }

}
