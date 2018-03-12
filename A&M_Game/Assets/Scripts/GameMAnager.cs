using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMAnager : MonoBehaviour {

	private int width = 100;
	private int height = 100;
	private float scale = 5;
	private int moveX = Random.Range (0, 1000);
	private int moveY = Random.Range (0, 1000);


	void Start () {
		
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
