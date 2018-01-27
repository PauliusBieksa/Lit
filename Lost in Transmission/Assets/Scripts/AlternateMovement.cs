using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateMovement : MonoBehaviour
{

	public bool command;
	public bool rotating;
	public float step;

	Vector3 pos;
	Quaternion rot;
	Transform trans;

	public List<Move> M = new List<Move> ();

	public Move m1;
	public Move m2;
	public Move m3;

	void Start ()
	{

		pos = gameObject.transform.position;
		rot = gameObject.transform.rotation;
		trans = gameObject.transform;

		m1.dir = Dirs.E;
		m1.type = MoveTypes.MOVE;

		m2.dir = Dirs.S;
		m2.type = MoveTypes.MOVE;

		m3.dir = Dirs.NE;
		m3.type = MoveTypes.MOVE;

		M.Add (m1);
		M.Add (m2);
		M.Add (m3);

		Debug.Log ("Started");
	}

	// Update is called once per frame
	void Update ()
	{
		if (command)
		{
			foreach (Move m in M)
			{
				Quaternion target = Quaternion.Euler (0, 0, (float) m.dir);

				if (m.type == MoveTypes.MOVE)
				{
					if (rot != target)
					{
						rotating = true;
						rot = Quaternion.RotateTowards (rot, target, step * Time.deltaTime);
						gameObject.transform.rotation = rot;
					}

				}
			}
		}
	}
}