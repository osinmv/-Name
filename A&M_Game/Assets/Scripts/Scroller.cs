using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour {

	private int lvl;
	private bool upd;
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

	void Start ()
	{
		lvl = 5;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			if (lvl < 99) {
				lvl++;
			} 
			upd = true;
		} else if(Input.GetAxis ("Mouse ScrollWheel") < 0) {
			if (lvl >0) {
				lvl--;
			} 
			upd = true;
		}
	}
}
