using UnityEngine;
using UnityEngine.UI;

public class PerlinNoise : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public float scale = 20f;
    new Renderer renderer;
    public float offsetX = 100f;
    public float offsetY = 100f;
    public Image uiImage;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        //offsetX = Random.Range(0f, 99999f);
        //offsetY = Random.Range(0f, 99999f);
    }
    private void Update()
    {
        
       renderer.material.mainTexture = GenerateTexture();

        
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        //GENERATE A PERLIN N M FOR TEXTURE

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y)
    {
        float xCord = (float)x / width * scale + offsetX; //why? because perlin noise repeats at whole numbers, so ints cannot be used
        float yCord = (float)y / height * scale + offsetY; // (float) because it would provide the result in decimals  
        float sample = Mathf.PerlinNoise(xCord, yCord);
        return new Color(sample, sample, sample);
    }
}
