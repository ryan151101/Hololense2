using MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeasureMenu : MonoBehaviour
{
    public static GameObject mainMenu, canvas, selectedBar;

    public static string selectedWeight;
    public static string selectedColor;

    private string inputColor;

    public TextMeshProUGUI scanAgainText;

    private Dictionary<string, Weight> WeightList;

    // Progress bar vars
    public PressableButton selectedWeightBtn;
    public TextMeshProUGUI selectedWeightText;

    public class Weight
    {
        public string[] Ranges { get; private set; }

        public Weight(string[] ranges)
        {
            Ranges = ranges;
        }
    }

    void Start()
    {

        inputColor = "";

        WeightList = new Dictionary<string, Weight>();
        WeightList.Add("GREY", new Weight(new string[] { "6-12" }));
        WeightList.Add("PINK", new Weight(new string[] { "13-16" }));
        WeightList.Add("RED", new Weight(new string[] { "17-20" }));
        WeightList.Add("PURPLE", new Weight(new string[] { "21-25" }));
        WeightList.Add("YELLOW", new Weight(new string[] { "26-31" }));
        WeightList.Add("WHITE", new Weight(new string[] { "32-40" }));
        WeightList.Add("BLUE", new Weight(new string[] { "41-51" }));
        WeightList.Add("ORANGE", new Weight(new string[] { "52-64" }));
        WeightList.Add("GREEN", new Weight(new string[] { "65-79" }));
        WeightList.Add("BLACK", new Weight(new string[] { "80+" }));
    }

    void Update()
    {
        // Accumulate color name input from the user
        foreach (char c in Input.inputString)
        {
            if (c != '\r' && c != '\n') // Ignore carriage returns and new lines
            {
                inputColor += c;
            }
        }

        // Check if the Enter key was pressed, indicating the end of input
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("inputColor: " + inputColor);

            // Check if the input color exists in the WeightList dictionary
            if (WeightList.ContainsKey(inputColor))
            {
                CorrectColorScanned(inputColor);
            }
            else
            {
                InvalidColorScanned();
            }
            inputColor = ""; // Reset the input color for the next scan
        } 
    }

    void CorrectColorScanned(string color)
    {
        // Store the scanned color
        selectedColor = color;
        Debug.Log("Selected Color: " + selectedColor);

        // Retrieve and store the weight range for the scanned color
        if (WeightList.TryGetValue(color, out Weight weightInfo))
        {
            // Assuming you want to use the first range in the array as the selected weight
            selectedWeight = weightInfo.Ranges.Length > 0 ? weightInfo.Ranges[0] : "Weight not found";
            Debug.Log("Selected Weight: " + selectedWeight);
        }

        selectedWeightText.text = selectedWeight + " lbs." + "\n" + selectedColor;
        selectedWeightBtn.gameObject.SetActive(true);
        
        SwitchMenus.ChangeMenu(Menu.MEDICATION_MENU, gameObject);
    }

    void InvalidColorScanned()
    {
        scanAgainText.gameObject.SetActive(true);
    }

}