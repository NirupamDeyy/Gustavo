using UnityEngine;

public class BoxController : MonoBehaviour
{
    public Transform cubePrefab;
    public Transform mainCube;

    public int width = 8;
    public int height = 8;
    public float scale = 2;
    public float offsetX = 100f;
    public float offsetY = 100f;

    private Transform[,] cubeArray; // 2D array to store references to the cubes

    void Start()
    {
        cubeArray = new Transform[width, height];
        InitiateArray();
        ManupulateColors();

    }
    private void FixedUpdate()
    {
    
        ManipulateCubeHeights();
        offsetX = offsetX - 0.001f;
        offsetY = offsetY - 0.0001f;

    }
    void InitiateArray()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float cordx = CalculateHeightofCube(x, y);
                float r = Random.Range(0f, 10f);
                Vector3 pos = new Vector3(x, r * cordx, y);
                Transform newCube = Instantiate(cubePrefab, pos, Quaternion.identity);
               
                
                cubeArray[x, y] = newCube; // Store the reference to the instantiated cube

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
               
                float newHeight = CalculateHeightofCube(x, y) * scale;
                
                Vector3 newPosition = cubeArray[x, y].position;
                newPosition.y = newHeight;
                cubeArray[x, y].position = newPosition;
               
            }
        }
    }

    void ManupulateColors()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float cordx = CalculateHeightofCube(x, y)  * scale* 20;
                Renderer renderer = cubeArray[x,y].GetComponent<Renderer>();
                Material mat = renderer.material;
                float r = Random.Range(3f, 10f);
                mat.color = new Color(10, y / cordx, x / cordx);  // R G B cordx * r, y / cordx,x / cordx

                //Debug.Log("x is:" + r*cordx * r + " y is: " + y / cordx + " z is: " + x / cordx);



            }
        }
    }
}
