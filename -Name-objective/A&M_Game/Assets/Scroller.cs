using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scroller : MonoBehaviour {

	private int lvl;
	private bool upd;
	private int x;
	private int y;
    public Camera cam;

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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                cam.orthographicSize-=5;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                cam.orthographicSize+=5;
            }
        }
        else
        {
		    if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			    if (lvl < 54) {
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


        

		if (Input.GetKey (KeyCode.DownArrow)) {
			y--;
            cam.transform.position = new Vector3(x, y, -1);
        }
		if (Input.GetKey (KeyCode.UpArrow)) {
			y++;
            cam.transform.position = new Vector3(x, y, -1);
        }
		if (Input.GetKey (KeyCode.RightArrow)) {
			x++;
            cam.transform.position = new Vector3(x, y, -1);
        }
		if (Input.GetKey (KeyCode.LeftArrow)) {
			x--;
            cam.transform.position = new Vector3(x, y, -1);
        }
	}
}
