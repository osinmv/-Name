using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMAnager : MonoBehaviour {


	private int width = 100;
	private int height = 100;
	private float scale = 7;
	private int moveX;
	private int moveY;

	public GameObject ground;
	public GameObject water;
	public GameObject sand;
	public GameObject deepwater;
	public GameObject rock;
	public Scroll scroll;
	public GameObject parent;
	private float[,,] HeightMaps;
	private GameObject obj; 
	private Texture2D[] LayersText; 


	void Start () {
		HeightMaps = Generate3DNoise ();
		LayersText = new Texture2D[10];
	}
	/*private void TextureGen()
	{
		Texture2D textur = new Texture2D (width, height);
		for (int i =0;i<width;i++)
		{
			for (int j = 0; j < height; j++)
			{

			}
		}
	}*/
	private void DeletCurrentMap()
	{
		for (int i =0;i< parent.transform.childCount;i++)
		{
			Destroy(parent.transform.GetChild (i).gameObject);

		}
	}
	private void ShowMap3D()
	{
		obj= new GameObject ();

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				if (HeightMaps [i, j,scroll.LVL] <= 0.2) {
					obj = Instantiate<GameObject> (deepwater);
				} else if (0.2 < HeightMaps [i, j,scroll.LVL] && HeightMaps [i, j,scroll.LVL] <= 0.4) {
					obj = Instantiate<GameObject> (water);
					;
				} else if (0.4 < HeightMaps [i, j,scroll.LVL] && HeightMaps [i, j,scroll.LVL] <= 0.6) {
					obj = Instantiate<GameObject> (sand);
					; 
				} else if (0.6 < HeightMaps [i, j,scroll.LVL] && HeightMaps [i, j,scroll.LVL] <= 0.8) {
					obj = Instantiate<GameObject> (ground);
					;
				} else if (0.8 < HeightMaps [i, j,scroll.LVL] && HeightMaps [i, j,scroll.LVL] <= 1) {
					obj = Instantiate<GameObject> (rock);
					;
				}
				obj.transform.SetParent (parent.transform);
				obj.transform.position = new Vector3 (i, j, 0);
			}
		}

	}
	private void ShowMap()
	{
		/*GameObject obj = new GameObject ();
		float[,] HeightMaps = GenerateNoise ();
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				if (HeightMaps [i, j] <= 0.2) {
					obj = Instantiate<GameObject> (deepwater);
				} else if (0.2 < HeightMaps [i, j] && HeightMaps [i, j] <= 0.4) {
					obj = Instantiate<GameObject> (water);
					;
				} else if (0.4 < HeightMaps [i, j] && HeightMaps [i, j] <= 0.6) {
					obj = Instantiate<GameObject> (sand);
					; 
				} else if (0.6 < HeightMaps [i, j] && HeightMaps [i, j] <= 0.8) {
					obj = Instantiate<GameObject> (ground);
					;
				} else if (0.8 < HeightMaps [i, j] && HeightMaps [i, j] <= 1) {
					obj = Instantiate<GameObject> (rock);
					;
				}
				obj.transform.position = new Vector3 (i, j, 0);
			}
			}*/
	}

	void Update () {
		if (scroll.UPD==true) {
			DeletCurrentMap ();
			ShowMap3D ();
			scroll.UPD = false;
		}
	}
		

	private float[,,] Generate3DNoise ()
	{
		float [,,] a = new float[width,height,10];
		for (int depth = 0;depth<10;depth++)
		{
			moveX = Random.Range (0, 1000);
			moveY = Random.Range (0, 1000);

			for (int i = 0; i < width; i++) 
			{
				for (int j = 0; j < height; j++)
				{
					a [i, j,depth] = Mathf.PerlinNoise ((float)i*scale/width+moveX, (float)j*scale/height+moveY);

				}
			}
		}
		return a;
	}
}
