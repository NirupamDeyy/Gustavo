
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePrint : MonoBehaviour
{
    public Transform[] cubePrefab;
   

    public int width = 8;
    public int height = 8;
    public float scale = 2;
    public float offsetX = 100f;
    public float offsetY = 100f;

    private Transform[,] cubeArray; // 2D array to store references to the cubes
    private int[,] miniCubeArray;
    [Range(0, 50)]
    public int Xvalue;
    [Range(0, 50)]
    public int Yvalue;

    [Range(0, 20)]
    public int depthRange;

    public int gridSize = 10; // Adjust grid size as needed
    public float circleRadius = 3.5f; // Adjust circle radius as needed
    public Transform colliderObject;

    public UIcontrolScript uIcontrolScript;

    bool canInitiate;
    void Start()
    {
        cubeArray = new Transform[width, height];
        canInitiate = true;
        InitiateArray();
        //ManupulateColors();
    }

    private void FixedUpdate()
    {

        ManipulateCubeHeights();
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
        }
        
    }

    float CalculateHeightofCube(int x, int y)
    {
        float xCord = (float)x / width * scale + offsetX;
        float yCord = (float)y / height * scale + offsetY;
        float sample = Mathf.PerlinNoise(xCord, yCord);
        return sample;
    }

    // Example function to manipulate the heights of the cubes
    void ManipulateCubeHeights()
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
    void ManupulateColors()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float cordx = CalculateHeightofCube(x, y) * scale * 20;
                Renderer renderer = cubeArray[x, y].GetComponent<Renderer>();
                Material mat = renderer.material;
                float r = Random.Range(3f, 10f);
                mat.color = new Color(10 / cordx, y / cordx, x / cordx);  // R G B cordx * r, y / cordx,x / cordx

                //Debug.Log("x is:" + r*cordx * r + " y is: " + y / cordx + " z is: " + x / cordx);



            }
        }
    }

}
