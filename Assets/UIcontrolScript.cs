using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIcontrolScript : MonoBehaviour
{

    bool pauseMenuOn;

    public Transform pauseMenu;
    public TMP_Dropdown selectPrefabDropDown;

    public int selectedPrefabNumber;

    public CreatePrint createPrint;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.gameObject.SetActive(true);
        List<string> options = new List<string> { "Cube", "Cylinder", "Sphere" };
        selectPrefabDropDown.AddOptions(options);
        selectPrefabDropDown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pauseMenuOn)
            {
                pauseMenu.gameObject.SetActive(true);
                pauseMenuOn = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if(pauseMenuOn)
            {
                pauseMenu.gameObject.SetActive(false);
                pauseMenuOn = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                
            }
        }
    }

    void OnDropdownValueChanged(int index)
    {
        // Do something with the selected option, e.g., get the text
        selectedPrefabNumber = index;
        string selectedOption = selectPrefabDropDown.options[index].text;
        Debug.Log("Selected option: " + selectedOption);
        createPrint.DestroyArray();
        createPrint.InitiateArray();
    }




}
