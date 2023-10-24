
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreatePrint : MonoBehaviour
{

    public TMP_Text infoText;
    public Transform[] cubePrefab;
   

    public int width = 8;
    public int height = 8;

  
    public float scale = 2;
    public float offsetX = 0f;
    public float offsetY = 0f;

    private Transform[,] cubeArray; // 2D array to store references to the cubes
    private int[,] miniCubeArray;
    [Range(0, 50)]
    public int Xvalue;
    [Range(0, 50)]
    public int Yvalue;

    [Range(0, 20)]
    public int depthRange;

    [Range(0,100)]
    public int colorConstant =34;
    [Range(0, 100)]
    public int colorConstanty = 2;
    [Range(0, 100)]
    public int colorConstantx = 2;

    public int gridSize = 10; // Adjust grid size as needed
    public float circleRadius = 3.5f; // Adjust circle radius as needed
    public Transform colliderObject;

    public UIcontrolScript uIcontrolScript;

    bool canInitiate;
    public bool arraylimitCrossed =false;

    void Start()
    {
        cubeArray = new Transform[width, height];
        canInitiate = true;
        width = 50;
        height = 50;
        InitiateArray();
        infoText.text = "W A S D for movement";


    }

    private void FixedUpdate()
    {
        ManipulateCubeHeights();

        if (width <= 50 && height <= 50) 
        {
            //ManipulateCubeHeights();
        }
       
        /*offsetX = offsetX - 0.001f;
        offsetY = offsetY - 0.0001f;
*/
    }
    public void DestroyArray()
    {
        
        if (canInitiate)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Destroy(cubeArray[x, y].gameObject);

                }
            }
        }
    }
    public void InitiateArray()
    {
        if(canInitiate) 
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    

                    float cordx = CalculateHeightofCube(x, y);
                    float r = Random.Range(0f, 10f);
                    Vector3 pos = new Vector3(x, r * cordx, y);
                    Transform newCube = Instantiate(cubePrefab[uIcontrolScript.selectedPrefabNumber], pos, Quaternion.identity);


                    cubeArray[x, y] = newCube; // Store the reference to the instantiated cube

                }
            }
            if (width + height <= 100)
            {
                infoText.text = "";
                ManupulateColors();
            }
            else if(width + height > 100)
            {
                infoText.text = "More than 2500 cells. Colours are disabled for faster processing.";
            }
            ManipulateCubeHeights();
            
        }
        
    }

    float CalculateHeightofCube(int x, int y)
    {
        
        float xCord = (float)x / width * scale + offsetX/100;
        float yCord = (float)y / height * scale + offsetY/100;
        float sample = Mathf.PerlinNoise(xCord, yCord);
        return sample;
    }

    // Example function to manipulate the heights of the cubes
    public void ManipulateCubeHeights()
    {
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // You can adjust the height of each cube using cubeArray
               

                if (cubeArray[x,y].tag == "cellsMoving")
                {
                    Vector3 newPosition = cubeArray[x, y].position;
                    newPosition.y -= 0.5f;
                  
                    
                    cubeArray[x, y].position = newPosition;
                   
                }
                else if (cubeArray[x, y].tag == "cellsStopMoving")
                {
                    
                }
                else 
                {
                    float newHeight = CalculateHeightofCube(x, y) * scale;
                    Vector3 newPosition = cubeArray[x, y].position;
                    newPosition.y = newHeight;
                    cubeArray[x, y].position = newPosition;
                }
            }
        }
    }

   /* public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cells"))
        {
            other.tag = "cellsMoving";
        }
    }*/

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("cellsMoving"))
        {
            other.tag = "cellsStopMoving";
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("cells") || other.CompareTag("cellsStopMoving"))
        {
         other.tag = "cellsMoving";
        }
    }
    public void ManupulateColors()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++) 
            {
                float cordx = CalculateHeightofCube(x, y) * scale * 20;
                Renderer renderer = cubeArray[x, y].GetComponent<Renderer>();
                Material mat = renderer.material;
                float r = Random.Range(3f, 10f);
                mat.color = new Color(colorConstant/ cordx, 0.1f * colorConstanty *y / cordx, 0.2f * colorConstantx * x / cordx);  // R G B cordx * r, y / cordx,x / cordx

                //Debug.Log("x is:" + r*cordx * r + " y is: " + y / cordx + " z is: " + x / cordx);



            }
        }
    }

}
