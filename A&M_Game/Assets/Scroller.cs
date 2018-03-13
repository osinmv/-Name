using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour {

	private int lvl;
	private bool upd;
	private int x;
	private int y;
	// Use this for initialization

	public bool UPD
	{
		get{return upd; }
		set{upd = value; }
	}

	public int LVL
	{
		get{return lvl;}

	}
	public int X
	{
		get{ return x;}
	}
	public int Y
	{
		get{ return y;}
	}

	void Start ()
	{
		lvl = 0;
		x = 0;
		y = 0;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			if (lvl < 34) {
				lvl++;
			} 
			upd = true;
		} else if(Input.GetAxis ("Mouse ScrollWheel") < 0) {
			if (lvl >0) {
				lvl--;
			} 
			upd = true;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			y--;
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			y++;
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			x++;
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			x--;
		}
	}
}
