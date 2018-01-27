using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	Vector3 pos;
	Quaternion rot;
	Transform trans;
	public float step = 2.0f;
	public float waitTime = 1.0f;

	public Move m1;
	public Move m2;
	List<Move> m = new List<Move> ();

	void Start ()
	{
		pos = gameObject.transform.position;
		rot = gameObject.transform.rotation;
		trans = gameObject.transform;

		m1.dir = Dirs.E;
		m1.type = MoveTypes.MOVE;

		m2.dir = Dirs.S;
		m2.type = MoveTypes.MOVE;

		m.Add (m1);
		m.Add (m2);

		Debug.Log ("Started");
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			Debug.Log ("got space");
			StartCoroutine (ExecuteMoves (m));
		}
	}

	IEnumerator ExecuteMoves (List<Move> moves)
	{
		Debug.Log ("Executing");
		int i = 0;
		foreach (Move mov in moves)
		{
			i++;
			Debug.Log ("loop " + i);

			if (mov.type == MoveTypes.MOVE)
			{
				StartCoroutine (Turn (mov));
				yield return new WaitForSeconds (waitTime);
				Debug.Log ("turned");
				Translate (mov);
				yield return new WaitForSeconds (waitTime);
				Debug.Log ("moved");

			}
		}
	}

	void Translate (Move mov)
	{
		float end;
		if (mov.dir == Dirs.N || mov.dir == Dirs.E || mov.dir == Dirs.S || mov.dir == Dirs.W)
		{
			end = 1.0f;
		}
		else
		{
			end = Mathf.Sqrt (2);
		}
		pos += transform.forward * end * Time.deltaTime;
		gameObject.transform.position = pos;
		Debug.Log ("transform by " + transform.forward * end * Time.deltaTime);
	}

	IEnumerator Turn (Move mov)
	{
		Debug.Log ("Rotating");
		Debug.Log ("rotate to " + (float) mov.dir + " " + mov.dir);
		Quaternion target = Quaternion.Euler (0, 0, (float) mov.dir);
		while (rot != target)
		{
			rot = Quaternion.RotateTowards (rot, target, step);
			gameObject.transform.rotation = rot;
			yield return null;
		}

	}
}