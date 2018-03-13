using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {


	public Material ground;
	public Material deepwater;
	public Material water;
	public Material sand;
	public Material rock;

	public int width = 100;
	public int height = 100;
	private int depth = 10;
	public float scale = 3;
	public GameObject TilesHolder;
	public GameObject plane;
	private int moveX;
	private int moveY;
	private float[,,] HeightMaps;
	//private int LVL = 0;
	public Scroller scroll ;
	private GameObject obj;
	private GameObject[,] Planes;
	private MeshRenderer[,] PlanesMaterial;
	// Use this for initialization
	void Awake()
	{
		PlanesMaterial = new MeshRenderer[100, 100];
		Create10000planes ();


		Generate3DNoise ();
	}

	void Start() {
		ShoWorld_Obj ();

	
	}



	private void Create10000planes()
	{
		for (int i = 0; i < 100; i++) {
			for (int j = 0; j < 100; j++) {
				obj = Instantiate<GameObject> (plane);
				obj.transform.SetParent (TilesHolder.transform);
				obj.transform.position = new Vector2 (i*10-400, j*10-500);
				PlanesMaterial [i, j] = obj.GetComponent<MeshRenderer> ();
			}
		}
	}
	void ShoWorld_Obj() {
		
		int level = scroll.LVL;

		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (0 <= HeightMaps[i, j, scroll.LVL] && HeightMaps[i, j, level] < 0.2) {
					PlanesMaterial [i, j].material = deepwater;
				} else if (0.2 <= HeightMaps[i, j, level] && HeightMaps[i, j, level] < 0.4) {
					PlanesMaterial [i, j].material = water;
				    
				} else if (0.4 <= HeightMaps[i, j, level] && HeightMaps[i, j, level] < 0.6) {
					PlanesMaterial [i, j].material = sand;
				} else if (0.6 <= HeightMaps[i, j, level] && HeightMaps[i, j, level] < 0.8) {
					PlanesMaterial [i, j].material = ground;
				    
				} else if (0.8 <= HeightMaps[i, j, level]) {
					PlanesMaterial [i, j].material = rock;
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



}