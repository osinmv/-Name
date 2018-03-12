using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMAnager : MonoBehaviour {

	private int width = 100;
	private int height = 100;
	private float scale = 5;
	private int moveX;
	private int moveY;

	public GameObject ground;
	public GameObject water;
	public GameObject sand;
	public GameObject deepwater;
	public GameObject rock;

		
	void Start () {
	moveX = Random.Range (0, 1000);
	moveY = Random.Range (0, 1000);
	ShowMap ();

	}
	private void ShowMap()
	{
		GameObject obj = new GameObject ();
		float[,] HeightMaps = GenerateNoise ();
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				if (HeightMaps [i, j] <= 0.2) {
					obj = Instantiate<GameObject>(deepwater);
				} else if (0.2 < HeightMaps [i, j] && HeightMaps [i, j] <= 0.4) {
					obj = Instantiate<GameObject>(water);;
				} else if (0.4 < HeightMaps [i, j] && HeightMaps [i, j] <= 0.6) {
					obj = Instantiate<GameObject>(sand);; 
				} else if (0.6 < HeightMaps [i, j] && HeightMaps [i, j] <= 0.8) {
					obj = Instantiate<GameObject>(ground);;
				} else if (0.8 < HeightMaps [i, j] && HeightMaps [i, j] <= 1) {
					obj = Instantiate<GameObject>(rock);;
				}
				obj.transform.position = new Vector3 (i, j, 0);
			}
		}
	}

	void Update () {
		
	}
		

	private float[,] GenerateNoise ()
	{
		float [,] a = new float[width,height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				a [i, j] = Mathf.PerlinNoise ((float)i*scale/width+moveX, (float)j*scale/height+moveY);

			}
		}

		return a;
	}
}
