using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using MixedReality.Toolkit.UX;
using System;
using MixedReality.Toolkit;
using MedClass;
using System.Runtime.CompilerServices;

public class MedicationMenu : MonoBehaviour
{
    public string symptom;
    public string weight;
    public List<List<string>> medicineFromCSV = new List<List<string>>();

    public List<PressableButton> BtnGroup;
    public List<TextMeshProUGUI> BtnText;
    private Dictionary<PressableButton, TextMeshProUGUI> buttonTextMap = new Dictionary<PressableButton, TextMeshProUGUI>();


    // Progress bar variables
    public PressableButton selectedMed;
    public TextMeshProUGUI selectedMedText;

    public static string correctMed, correctDose, correctVolume;


    // Info. pulled from the CSV
    public string condition, weightClass;
    public List<string> med = new List<string>();
    public List<string> dose = new List<string>();
    public List<string> volume = new List<string>();
    public List<string> colorClass = new List<string>();


    void OnEnable()
    {
        ResetMenu();
        Init();
    }


    // Init function
    public void Init()
    {
        // Change the color of "medication" to orange on the progress bar
        ProgressBarController.changeProgressColor("Medication");

        symptom = MainMenu.getSymptomName();
        weight = WeightMenu.getWeightGroup();

        ReadFromCSV(Directory.GetCurrentDirectory() + "\\Assets\\Scripts\\medicationSheet.csv");


        // Add onClick events to each medication option button. 
        // buttons are defiend as "Btn 1, 2, 3..." and medication names are assigned dynamically to ea button.
        // We need a way to map the name to the buttons
        if (BtnGroup != null && BtnText != null && BtnGroup.Count == BtnText.Count)
        {
            // Populate a dictionary with the name of the medication (Foramt: Btn #: medication name)
            for (int i = 0; i < BtnGroup.Count; i++)
            {
                PressableButton currentButton = BtnGroup[i]; // Create a local variable inside the loop

                buttonTextMap.Add(currentButton, BtnText[i]);
                currentButton.OnClicked.AddListener(() => BtnGroupClicked(currentButton));
            }
        }
    }

    public void ResetMenu()
    {
        // Empty the dictionary and lists that hold the correct information
        buttonTextMap = new Dictionary<PressableButton, TextMeshProUGUI>();
        medicineFromCSV.Clear();
        med.Clear();
        dose.Clear();
        volume.Clear();
        colorClass.Clear();

        // Remove onclick events for the buttons
        for (int i = 0; i < BtnGroup.Count; i++)
        {
            PressableButton currentButton = BtnGroup[i]; // Create a local variable inside the loop
            currentButton.OnClicked.RemoveAllListeners();
            currentButton.gameObject.SetActive(false);
        }

    }


    // A medication was clicked, set the name of the option and go to the next menu.
    private void BtnGroupClicked(PressableButton button)
    {
      
        if (buttonTextMap.TryGetValue(button, out TextMeshProUGUI buttonText))
        {
            selectedMed.gameObject.SetActive(true);
            string currentMed = buttonText.text;
            selectedMedText.text = currentMed;

            // index is the btn # that was selected.
            int medIndex = med.IndexOf(currentMed);

            // Add the button that was clicked to the global list of buttons that have been clicked
            ButtonsClickedTracker.AddButtonClicked(button);
            ButtonsClickedTracker.UpdateButtonClickedName(button, currentMed);


            setCorrectMed(currentMed, medIndex);
            GoToBarcodeMenu(currentMed);
        }  

    }


    // Read in the CSV file and add to medicineFromCSV list
    private void ReadFromCSV(string filename)
    {
        if (File.Exists(filename))
        {
            using (var reader = new StreamReader(filename))
            {
                // Skip the header line
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    // Check that the CSV row match the condition and weight group selected
                    if ((values[0].ToString() == symptom.ToString()) && (values[1].ToString() == weight.ToString() + " lbs"))
                    {
                        medicineFromCSV.Add(new List<string>(values));
                        condition = values[0];
                        weightClass = values[1];
                    }

                }
            }
        }

        // Add appropriate medications, doses, and volumes for all rows matching the condition and weight from the CSV
        // They are added to a list since some conditions and weight groups have multiple options
        foreach(var val in medicineFromCSV)
        {
            med.Add(val[2].ToString());
            dose.Add(val[3].ToString());
            volume.Add(val[4].ToString());
            colorClass.Add(val[5].ToString());
        }

        updateMedicationMenu();

    }


    // Update the list of appropriate medication options
    public void updateMedicationMenu() {
        int numMeds = med.Count;
        for (int i = 0; i < numMeds; i++)
        {
            BtnGroup[i].gameObject.SetActive(true);
            BtnText[i].text = med[i].ToString();

        }
    }




    // Set the correct medication, dose, and volume
    private void setCorrectMed(string selectedMedication, int selectedIndex)
    {
        correctMed = selectedMedication;
        setCorrectDose(selectedIndex);
        setCorrectVol(selectedIndex);

    }
    private void setCorrectDose(int index)
    {
        correctDose = dose[index];
    }

    private void setCorrectVol(int index)
    {
        correctVolume = volume[index];
    }


    // Getters for the correct medication info.
    public static string getCorrectMed()
    {
        return correctMed;
    }

    public static string getCorrectDose()
    {
        return correctDose;
    }

    public static string getCorrectVol()
    {
        return correctVolume;
    }




    // Go to the next menu
    public void GoToBarcodeMenu(string medicineName)
    {
        
        SwitchMenus.ChangeMenu(Menu.BARCODE_MENU, gameObject, medicineName);
    }

   
}





