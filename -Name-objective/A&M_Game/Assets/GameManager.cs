using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {


	public Sprite water;
	public Sprite ground;
	public Sprite sand;
	public Sprite rock;
    public Sprite dirt;

    public int width = 100;
	public int height = 100;
	public int depth = 10;
	public float scale = 1;
	public GameObject TilesHolder;
	public GameObject plane;
	private int moveX;
	private int moveY;
	private float[,,] HeightMaps;
    private int[,,] HeightMap;
    //private int LVL = 0;
    public Scroller scroll ;
	private GameObject obj;
	private GameObject[,] Planes;
	private SpriteRenderer[,] PlanesMaterial;
	// Use this for initialization
	void Awake()
	{
		PlanesMaterial = new SpriteRenderer[width + depth-1, height+height+width-1];
		Create10000planes ();
        HeightMap = grassing(generateLand(Generate2DNoise()));
    }

	void Start()
    {
		ShoWorld_Obj ();
	}



	private void Create10000planes()
	{
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				obj = Instantiate<GameObject> (plane);
				obj.transform.SetParent (TilesHolder.transform);
				obj.transform.position = new Vector2 ((i-j)*2, (j+i));//- height/2
                obj.GetComponent<Renderer>().sortingOrder = (width - 1 - i) + (height - 1 - j) * width;
				PlanesMaterial [i, j] = obj.GetComponent<SpriteRenderer> ();
			}
		}



	}

	void ShoWorld_Obj() {
		
		int level = scroll.LVL;

		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
                PlanesMaterial[i, j].GetComponent<Renderer>().sortingLayerName = System.Convert.ToString(10);
                if (HeightMap[i, j , level] != 0)
                {
                    if(HeightMap[i, j, level] == 1)
                    {
                        PlanesMaterial[i, j].sprite = dirt;
                    }
                    else
                    {
                         PlanesMaterial[i, j].sprite = ground;
                    }
                }
                else
                {
                    for (int l = 0; i > -1; l++)
                    {
                        if(i+l< width && j+l< height && level - l >= 0)
                        {                     
                            if (HeightMap[i+l, j+l, level-l] != 0)
                            {
                                PlanesMaterial[i, j].GetComponent<Renderer>().sortingLayerName = System.Convert.ToString(10-l);

                                if (HeightMap[i, j, level] == 1)
                                {
                                    PlanesMaterial[i, j].sprite = dirt;
                                }
                                else
                                {
                                    PlanesMaterial[i, j].sprite = ground;
                                }
                                break;
                            }
                        }
                        else
                        {
                            PlanesMaterial[i, j].sprite = rock;
                            break;
                        }
                    }     
                }
            }
		}
	}


	// Update is called once per frame
	void Update() {
		if (scroll.UPD == true) {
			ShoWorld_Obj ();
			scroll.UPD = false;
		}
	}

	//генератор шума
	private void Generate3DNoise()
	{
		HeightMaps = new float[width, height, depth];

		for (int l = 0; l < depth; l++)
		{
			moveX = Random.Range(0, 1000);
			moveY = Random.Range(0, 1000);
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					HeightMaps[i, j, l] = Mathf.PerlinNoise((float)i * scale / width + moveX, (float)j * scale / height + moveY);
				}
			}
		}

	}

    //генератор шума
    private float[,] Generate2DNoise()
    {
        float[,] a = new float[width, height];
        moveX = Random.Range(0, 1000);
        moveY = Random.Range(0, 1000);
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
        int[,,] HeightMap = new int[width, height, depth];

        for (int l = 0; l < depth; l++)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (noise[i, j] <= (float)(l + 1) / (float)(depth + 1))
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

    private int[,,] grassing(int[,,] HeightMap)
    {
        for (int l = 0; l < depth; l++)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (l + 1 < depth)
                    {
                        if(HeightMap[i,j,l] == 1 && HeightMap[i, j, l+1] == 0)
                         {
                            HeightMap[i, j, l] = 2;
                         }
                    }
                    else
                    {
                        if (HeightMap[i, j, l] == 1 )
                        {
                            HeightMap[i, j, l] = 2;
                        }
                    }
                }
            }
        }

        return HeightMap;
    }

}