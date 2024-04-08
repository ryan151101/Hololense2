using JetBrains.Annotations;
using UnityEngine;

public static class SwitchMenus
{
    public static bool Initialised { get; private set; }
    public static GameObject mainMenu, weightMenu, measureMenu, ageMenu, medicationMenu, 
                            barcodeMenu, unableToScanMenu, administerMenu, bloodSugarLevelMenu, finalMenu;


    public static BarcodeMenu barcodeScript;

    // Initalize the objects
    public static void Init()
    {
        GameObject canvas = GameObject.Find("Canvas");
        mainMenu = canvas.transform.Find("Main Menu").gameObject;
        weightMenu = canvas.transform.Find("Weight Menu").gameObject;
        measureMenu = canvas.transform.Find("Measure Menu").gameObject;
        ageMenu = canvas.transform.Find("Age Menu").gameObject;
        medicationMenu = canvas.transform.Find("Medication Menu").gameObject;
        barcodeMenu = canvas.transform.Find("Barcode Menu").gameObject;
        unableToScanMenu = canvas.transform.Find("Unable To Scan Menu").gameObject;
        administerMenu = canvas.transform.Find("Administer Menu").gameObject;
        bloodSugarLevelMenu = canvas.transform.Find("Blood Sugar Level Menu").gameObject;
        finalMenu = canvas.transform.Find("Final Menu").gameObject;
    }

    // Change visibility of the menus
    public static void ChangeMenu(Menu menu, GameObject menuToClose, string var = "")
    {
        if (!Initialised)
        {
            Init();
        }

        // Control which menu to open
        switch(menu)
        {
            case Menu.MAIN_MENU:
                mainMenu.SetActive(true);
                break;

            case Menu.WEIGHT_MENU:
                weightMenu.SetActive(true); 
                break;

            case Menu.MEASURE_MENU:
                measureMenu.SetActive(true);
                break;

            case Menu.AGE_MENU:
                ageMenu.SetActive(true);
                break;

            case Menu.MEDICATION_MENU:
                medicationMenu.SetActive(true);
                break;

            case Menu.BARCODE_MENU:
                barcodeMenu.SetActive(true);
                barcodeScript = barcodeMenu.GetComponent<BarcodeMenu>();
                barcodeScript.medicineName = var;
                break;

            case Menu.UNABLE_TO_SCAN_MENU:
                unableToScanMenu.SetActive(true);
                break;


            case Menu.ADMINISTER_MENU:
                administerMenu.SetActive(true);
                break;

            case Menu.BLOOD_SUGAR_LEVEL_MENU:
                bloodSugarLevelMenu.SetActive(true);
                break;

            case Menu.FINAL_MENU:
                finalMenu.SetActive(true); 
                break;
        }

        // Close the current active menu after opening the new one
        menuToClose.SetActive(false);
       
    }
}

