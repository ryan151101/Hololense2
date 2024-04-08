using MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes

public class ROSC : MonoBehaviour
{
    public PressableButton RoscButton;
    public TextMeshProUGUI RoscText;

    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Register the click event
        RoscButton.OnClicked.AddListener(() => EndCaseButtonClicked(RoscButton));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndCaseButtonClicked(PressableButton button)
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


}
