using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PathFinder : MonoBehaviour {

	public GameManager gm;
	private bool[,,] rawGraph;
	private List<Point3D> waiting = new List<Point3D>();
	private HashSet<Point3D> watched = new HashSet<Point3D> ();
	Point3D begining;
	Point3D end;
	private int x;
	private int y;
	private int z;
	private int x1;
	private int y1;
	private int z1;



	void Start()
	{	
		rawGraph = gm.get_rawGraph;
		begining = new Point3D (x, y, z);
		end = new Point3D (x1, y1, z1);
		waiting.Add (begining);

		while (waiting.Count > 0) {

			Point3D point = waiting [0];
			for (int i = 0; i < waiting.Count; i++) {
				if (waiting [i].F_cost < point.F_cost || waiting [i].F_cost == point.F_cost) {
					if (waiting [i].H_cost < point.H_cost) {
						point = waiting [i];
					}
				}
			}
			waiting.Remove (point);
			watched.Add (point);

			//if (point == target) {
			//	Function For revercing path
			//}

			Check_Neigbours(point);

		}

	}
	private void Check_Neigbours(Point3D point)
	{
		for (int i = -1; i <2; i++) {
			for (int j = -1; j < 2; j++) {
				for (int d = 0; d < 2; d++) {
					
					if (i == 0 && j == 0 && d == 0) {
						continue;
					}

					if (rawGraph [point.X + i, point.Y + j, point.Z + d]) {
						Point3D a = new Point3D (point,point.X + i, point.Y + j, point.Z + d);
						a.G_cost = Distance (a, begining);
						a.H_cost = Distance (a, end);

						if (!waiting.Contains (a)&&!watched.Contains(a)) {
							waiting.Add (a);
						}
											
					}

				}
			}
		}
	}
	private float Distance(Point3D pointA, Point3D pointB)
	{
		return Mathf.Sqrt (Mathf.Pow(pointB.X-pointA.X,2)+Mathf.Pow(pointB.Y-pointA.Y,2)+Mathf.Pow(pointB.Z-pointA.Z,2));
	}

}
