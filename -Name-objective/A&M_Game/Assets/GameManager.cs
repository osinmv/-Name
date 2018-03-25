using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {


    public Sprite air;

    public int width = 100;
	public int height = 100;
	public int depth = 25;
    public int Rdepth = 20;
	public float scale = 1;
	public GameObject TilesHolder;
	public GameObject plane;
	private int moveX;
	private int moveY;

	private float[,,] HeightMaps;
    private int[,,] HeightMap;
    //private int LVL = 0;
	private int indexAmount = 7;
	private Sprite[] sprites;
    public Scroller scroll ;
	private GameObject obj;
	private GameObject[,] Planes;
	private SpriteRenderer[,] PlanesMaterial, PlanesSideL, PlanesSideR;
	// Use this for initialization
	void Awake()
	{
		LoadSprites ();
		PlanesMaterial = new SpriteRenderer[width + depth-1, height+height+width-1];
        PlanesSideL = new SpriteRenderer[height, 30];
        PlanesSideR = new SpriteRenderer[width, 30];
		HeightMap = new int[width,height,depth];
        Create10000planes ();
		generateLand (Generate2DNoise ());
		grassing ();
        HeightMap  = rocking();
        sloping();
    }

	void Start()
    {
		ShoWorld_Obj ();
	}

	private void LoadSprites()
	{
		sprites = new Sprite[indexAmount+1];
		for (int i = 0; i < indexAmount; i++) {
			sprites [i+1] = Resources.Load ("Sprites/" + (i+1).ToString (),typeof(Sprite)) as Sprite;
			Debug.Log (sprites [i]);
		}	
	}

	private void Create10000planes()
	{
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				obj = Instantiate<GameObject> (plane);
				obj.transform.SetParent (TilesHolder.transform);
				obj.transform.position = new Vector2 ((i-j)*2, (j+i));
				PlanesMaterial [i, j] = obj.GetComponent<SpriteRenderer> ();
				PlanesMaterial [i, j].sortingOrder = (width - 1 - i) + (height - 1 - j) * width;

			}
		}

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < Rdepth; j++)
            {
                obj = Instantiate<GameObject>(plane);
                obj.transform.SetParent(TilesHolder.transform);
                obj.transform.position = new Vector2(-2*i, i-2*j-2);
				PlanesSideL[i, j] = obj.GetComponent<SpriteRenderer>();
				PlanesSideL[i,j].sortingOrder = height - i- height * j;
               
            }
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < Rdepth; j++)
            {
                obj = Instantiate<GameObject>(plane);
                obj.transform.SetParent(TilesHolder.transform);
                obj.transform.position = new Vector2(2 * i, i - 2 * j - 2);
				PlanesSideR[i, j] = obj.GetComponent<SpriteRenderer>();
				PlanesSideR[i, j].sortingOrder = width - i - width * j;
                
            }
        }
    }

	void ShoWorld_Obj() {
		
		int level = scroll.LVL;

		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
                PlanesMaterial[i, j].GetComponent<Renderer>().sortingLayerName = System.Convert.ToString(102);
                if (HeightMap[i, j , level] != 0)
                {
						PlanesMaterial[i, j].sprite = sprites[HeightMap[i,j,level]];
                }
                else
                {
                    for (int l = 0; i > -1; l++)
                    {
                        if(i+l< width && j+l< height && level - l >= 0)
                        {                     
                            if (HeightMap[i+l, j+l, level-l] != 0)
                            {
                                PlanesMaterial[i, j].GetComponent<Renderer>().sortingLayerName = System.Convert.ToString(102-2*l);
                                PlanesMaterial[i, j].sprite = sprites[HeightMap[i+l, j+l, level-l]];
                                break;
                            }
                        }
                        else
                        {
                            PlanesMaterial[i, j].sprite = air;
                            break;
                        }
                    }     
                }
            }
		}

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < Rdepth; j++)
            {
                PlanesSideL[i, j].sprite = air;
                if (level - 1 - j >= 0)
                {
                PlanesSideL[i, j].GetComponent<Renderer>().sortingLayerName = System.Convert.ToString(101);

                    if (HeightMap[0, i, level - 1 - j] != 0)
                    {
                        PlanesSideL[i, j].sprite = sprites[HeightMap[0, i, level - 1 - j]];
                    }
                    else
                    {
                        for (int l = 0; i > -1; l++)
                        {
                            if (0 + l < width && i + l < height && level - 1 - j - l >= 0)
                            {
                                if (HeightMap[0 + l, i + l, level - 1 - j - l] != 0)
                                {
                                    PlanesSideL[i, j].GetComponent<Renderer>().sortingLayerName = System.Convert.ToString(101 - 2 * l);
                                    PlanesSideL[i, j].sprite = sprites[HeightMap[0 + l, i + l, level - 1 - j - l]];
                                    break;
                                }
                            }
                            else
                            {
                                PlanesSideL[i, j].sprite = air;
                                break;
                            }
                        }
                    }
                }

            }
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < Rdepth; j++)
            {
                PlanesSideR[i, j].sprite = air;
                if (level - 1 - j >= 0)
                {
                    PlanesSideR[i, j].GetComponent<Renderer>().sortingLayerName = System.Convert.ToString(101);
                    if (HeightMap[i, 0, level - 1 - j] != 0)
                    {

                            PlanesSideR[i, j].sprite = sprites[HeightMap[i, 0, level-1-j]];

                    }
                    else
                    {
                        for (int l = 0; i > -1; l++)
                        {
                            if (i + l < width && 0 + l < height && level - 1 - j - l >= 0)
                            {
                                if (HeightMap[i + l, 0 + l, level - 1 - j - l] != 0)
                                {
                                    PlanesSideR[i, j].GetComponent<Renderer>().sortingLayerName = System.Convert.ToString(101 - 2 * l);
                                    PlanesSideR[i, j].sprite = sprites[HeightMap[i+l, 0+l, level-1-j-l]];
                                    break;
                                }
                            }
                            else
                            {
                                PlanesSideR[i, j].sprite = air;
                                break;
                            }
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
	private void generateLand(float[,] noise)
    {
        

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
      
    }

	private void  grassing()
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

        
    }


    private int[,,] rocking()
    {
        int[,,] HeightMap2 = new int[width, height, depth + 30];
        for (int l = 0; l < depth; l++)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    HeightMap2[i, j, l + 28] = HeightMap[i, j, l];
                }
            }
        }

        for (int l = 0; l < 29; l++)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    HeightMap2[i, j, l] = 3;
                }
            }
        }

        return HeightMap2;
    }

	public bool[,,] get_rawGraph
	{
		get
		{
			bool[,,] empty_space = new bool[width,height,depth];
			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++) {
					for (int d = 0; d < depth; d++) {
						if (HeightMap [i, j, d] == 0||HeightMap [i, j, d] == 3||HeightMap [i, j, d] == 4||HeightMap [i, j, d] == 5) {
							empty_space [i, j, d] = true;
						} else {
							empty_space [i, j, d] = false;
						}
					}
				}
			}





			return empty_space;
		}
	}

    public void sloping()
    {
        for (int l = 0; l < HeightMap.GetLength(2); l++)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (HeightMap[i, j, l] == 2 && i + 1 < width && j + 1 < height && j - 1 >= 0 && i - 1 >= 0)
                    {
                        if (HeightMap[i + 1, j, l] == 2 && HeightMap[i - 1, j, l] == 0 || HeightMap[i + 1, j, l] == 1 && HeightMap[i - 1, j, l] == 0)
                        {
                            HeightMap[i, j, l] = 5;
                        }
                        else
                        {
                            if (HeightMap[i - 1, j, l] == 2 && HeightMap[i + 1, j, l] == 0 || HeightMap[i - 1, j, l] == 1 && HeightMap[i + 1, j, l] == 0)
                            {
                                HeightMap[i, j, l] = 6;
                            }
                            else
                            {
                                if (HeightMap[i, j + 1, l] == 2 && HeightMap[i, j - 1, l] == 0 || HeightMap[i, j + 1, l] == 1 && HeightMap[i, j - 1, l] == 0)
                                {
                                    HeightMap[i, j, l] = 4;
                                }
                                else
                                {
                                    if (HeightMap[i, j - 1, l] == 2 && HeightMap[i, j + 1, l] == 0 || HeightMap[i, j - 1, l] == 1 && HeightMap[i, j + 1, l] == 0)
                                    {
                                        HeightMap[i, j, l] = 7;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        for (int l = 0; l < HeightMap.GetLength(2); l++)
        {
            for (int i = 1; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if(HeightMap[i, j, l]==4 && HeightMap[i-1, j, l] == 5)
                    {
                        HeightMap[i-1, j , l] = 4;
                    }
                }
            }
        }
    }
}