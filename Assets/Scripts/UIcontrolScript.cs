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

    public Slider colorRed;
    public Slider colorGreen;
    public Slider colorBlue;

    public Slider SizeX;
    public Slider SizeY;

    public Slider DepthScale;

    public Slider OffsetX;
    public Slider OffsetY;

    public Button menuButon;
    public Image expand;
    public Image collapse;

    public Button quit;

    public CreatePrint createPrint;
    public BallMovementScript ballMovementScript;
    public CinemachineSwitch cinemachineSwitch;

    // Start is called before the first frame update
    void Start()
    {
      

        pauseMenu.gameObject.SetActive(true);
        pauseMenuOn = true;
        List<string> options = new List<string> { "Cube", "Cylinder", "Sphere" };
        selectPrefabDropDown.AddOptions(options);
        selectPrefabDropDown.onValueChanged.AddListener(OnDropdownValueChanged);

        colorRed.onValueChanged.AddListener(OnChangeColorRed);
        colorGreen.onValueChanged.AddListener(OnChangeColorGreen);
        colorBlue.onValueChanged.AddListener(OnChangeColorBlue);

        SizeX.onValueChanged.AddListener(OnSizeXChanged);
        SizeY.onValueChanged.AddListener(OnSizeYChanged);

        DepthScale.onValueChanged.AddListener(OnScaleChanged);

        OffsetX.onValueChanged.AddListener(OnOffsetXChanged);
        OffsetY.onValueChanged.AddListener(OnOffsetYChanged);

        menuButon.onClick.AddListener(menuButtonFunc);
        quit.onClick.AddListener(QuitApplication);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuButtonFunc();
        }
    }

    private void menuButtonFunc()
    {
        cinemachineSwitch.SwitchPriority();
        if (!pauseMenuOn)
        {
            
            pauseMenu.gameObject.SetActive(true);
            pauseMenuOn = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            expand.gameObject.SetActive(true);
            collapse.gameObject.SetActive(false);
        }
        else if (pauseMenuOn)
        {
            
            pauseMenu.gameObject.SetActive(false);
            pauseMenuOn = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            expand.gameObject.SetActive(false);
            collapse.gameObject.SetActive(true);
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
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
    void OnChangeColorRed(float index)
    {
        createPrint.colorConstant = (int)index;
        createPrint.ManupulateColors();
    }

    void OnChangeColorGreen(float index)
    {
        createPrint.colorConstanty = (int)index;
        createPrint.ManupulateColors();
    }

    void OnChangeColorBlue(float index)
    {
        createPrint.colorConstantx = (int)index;
        createPrint.ManupulateColors();
    }
    
    void OnSizeXChanged(float index)
    {
        
        createPrint.DestroyArray();
        createPrint.width = (int)index;
        createPrint.InitiateArray();
        //createPrint.ManupulateColors();
    }

    void OnSizeYChanged(float index)
    {
        
        createPrint.DestroyArray();

        createPrint.height = (int)index;
        createPrint.InitiateArray();
        //createPrint.ManupulateColors();
    }

    void OnScaleChanged(float index)
    {

        createPrint.scale = index;
        createPrint.ManupulateColors();
    }

    void OnOffsetXChanged(float index)
    {

        createPrint.offsetX = index;
        createPrint.ManupulateColors();
    }

    void OnOffsetYChanged(float index)
    {

        createPrint.offsetY = index;
        createPrint.ManupulateColors();
    }




}
