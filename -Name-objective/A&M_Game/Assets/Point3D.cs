using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point3D
{
	private Point3D previous;
	private int x;
	private int y;
	private int z;
	private float g_cost;
	private float h_cost;
	private float f_cost;

	public Point3D(Point3D previous, int x, int y, int z)
	{
		this.previous = previous;
		this.x = x;
		this.y = y;
		this.z = z;


	}
	public Point3D(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}
	public Point3D Previous
	{
		get{return previous; }
	}
	public int X
	{
		get{return x;}
	}
	public int Y
	{
		get{return y;}
	}
	public int Z
	{
		get{return z;}
	}
	public float H_cost
	{
		get{return h_cost; }
		set{this.h_cost = value;}
	}
	public float G_cost
	{	
		get{return g_cost;}
		set{this.g_cost = value;}
	}
	public float F_cost
	{
		get{return g_cost+h_cost; }
	}

}
