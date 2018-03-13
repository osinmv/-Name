using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteMaker : MonoBehaviour {

    SpriteRenderer rend;

    private int width = 1600;
    private int height = 1600;
    private int depth = 35;
    private float scale = 3;
    private int moveX;
    private int moveY;
    private float[,,] HeightMaps;
    private int[,,] HeightMap;
    private int LVL = 0;
    public Scroller scroll = new Scroller();

    // Use this for initialization
    void Start() {
        HeightMap = generateLand(Generate2DNoise());
        rend = GetComponent<SpriteRenderer>();
        renderist();

    }

    void renderist() {
        Texture2D tex = new Texture2D(width, height);
        Color[] colorArray = new Color[tex.width * tex.height];

        for (int i = 0; i < tex.width; i++)
        {
            for (int j = 0; j < tex.height; j++)
            {
                //if (0 <= HeightMaps[i, j, scroll.LVL] && HeightMaps[i, j, scroll.LVL] <= 0.2) {
                //    colorArray[i + tex.width * j] = Color.blue;
                //} else if (0.2 < HeightMaps[i, j, scroll.LVL] && HeightMaps[i, j, scroll.LVL] <= 0.4) {
                //    colorArray[i + tex.width * j] = Color.blue;
                //    ;
                //} else if (0.4 < HeightMaps[i, j, scroll.LVL] && HeightMaps[i, j, scroll.LVL] <= 0.6) {
                //    colorArray[i + tex.width * j] = Color.yellow;
                //    ;
                //} else if (0.6 < HeightMaps[i, j, scroll.LVL] && HeightMaps[i, j, scroll.LVL] <= 0.8) {
                //    colorArray[i + tex.width * j] = Color.green;
                //    ;
                //} else if (0.8 < HeightMaps[i, j, scroll.LVL]) {
                //    colorArray[i + tex.width * j] = Color.gray;
                //    ;
                //}

                if (HeightMap[i, j, scroll.LVL] == 1)
                {
                    colorArray[i + tex.width * j] = Color.green;
                }
                    else
                {
                    colorArray[i + tex.width * j] = Color.white;
                }


            }
        }

        tex.SetPixels(colorArray);
        tex.Apply();

        tex.anisoLevel = 0;
        tex.wrapMode = TextureWrapMode.Clamp;
        tex.filterMode = FilterMode.Point;
        //sprite

        Sprite newSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

        //assign sprite to ingame sprite

        rend.sprite = newSprite;
    }


    // Update is called once per frame
    void Update() {
        if (scroll.UPD == true) {
            renderist();
            scroll.UPD = false;
        }
    }

    //генератор шума
    private float[,,] Generate3DNoise()
    {
        float[,,] a = new float[width, height, depth];

        for (int l = 0; l < depth; l++)
        {
            moveX = Random.Range(0, 1000);
            moveY = Random.Range(0, 1000);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    a[i, j, l] = Mathf.PerlinNoise((float)i * scale / width + moveX, (float)j * scale / height + moveY);
                }
            }
        }
        return a;
    }

    //генератор шума
    private float[,] Generate2DNoise()
    {
        float[,] a = new float[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                a[i, j] = Mathf.PerlinNoise((float)i * scale / width + moveX, (float)j * scale / height + moveY);
            }
        }
        return a;
    }

    //генератор высот
    private int[,,] generateLand(float[,] noise)
    {
          int[,,] HeightMap = new int[width,height,depth];

        for (int l = 0; l < depth; l++)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if(noise[i, j] <= (float)(l + 1) / (float)(depth + 1))
                    {
                        HeightMap[i, j, l] = 0;
                    }
                    else
                    {
                        HeightMap[i, j, l] = 1;
                    }
                     
                }
            }
        }
        return HeightMap;
    }



}
