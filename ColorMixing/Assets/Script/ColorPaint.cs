using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPaint : MonoBehaviour
{
    Texture2D tex2D;
    public RawImage ri;

    int TexPixelLength = 256;
    Color[,] arrayColor;
    public Texture Texture;

    // Start is called before the first frame update
    void Start()
    {
        arrayColor = new Color[TexPixelLength,TexPixelLength];//256*256
        tex2D = new Texture2D(TexPixelLength,TexPixelLength,TextureFormat.RGBA32, true);
        //unity官網   public Texture2D(int width, int height, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipChain = true, bool linear = false);

 
        
    }   

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().material.mainTexture = tex2D;
        
        float i = 200;
        for (int y = 0; y < tex2D.height; y++)
        {
            for (int x = 0; x < tex2D.width; x++)
            {
                //Color c = new Color(1f / 255, 100f / 255, 1f / 255);
    
                //arrayColor[x, y] = new Color(100f / 255, 100f / 255, 100f / 255);

                Color color = ((x & y) != 0 ? new Color(255 / 255, 200 / 255, 20 / 255) : new Color(1f / 255, 100f / 255, 1f / 255));
                tex2D.SetPixel(x, y, color);
            }
        }
        tex2D.Apply();
        ri.texture = tex2D;

        Debug.Log(ri.transform.position);
        Vector3 p = ri.transform.position;

 
        if (Input.GetMouseButtonDown(0)) 
        {

            Vector3 mos = Camera.main.ScreenToWorldPoint(new Vector3(-Input.mousePosition.x, -Input.mousePosition.y, -10));
            Debug.Log(mos);
            if (mos.x < 230 + 100 && mos.x > 230 - 100 && mos.y < 240 + 100 && mos.y > 240 - 100)
            {
                float x1 = (mos.x - p.x) / 256;
                float y1 = (mos.y - p.y) / 256;

                Color c = tex2D.GetPixel((int)x1, (int)y1);

                Debug.Log(c);
            }

        }

   
    }
}
