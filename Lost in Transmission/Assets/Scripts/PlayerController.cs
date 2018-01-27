using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	Vector3 pos;
	Quaternion rot;
	Transform trans;
	public float smooth = 2.0f;

	Dictionary<int, string> values;

	void Start ()
	{
		pos = gameObject.transform.position;
		rot = gameObject.transform.rotation;
		trans = gameObject.transform;

		List<Move> m = new List<Move> ();

		Move t;
		t.dir = Dirs.E;
		t.type = MoveTypes.MOVE;
		m.Add (t);

		Debug.Log ("Started");
		StartCoroutine (Execute (m));
	}

	// Update is called once per frame
	void Update ()
	{

	}

	IEnumerator Execute (List<Move> moves)
	{
		foreach (Move mov in moves)
		{

			if (mov.type == MoveTypes.MOVE)
			{
				StartCoroutine (Turn (mov));

				StartCoroutine (Translate (mov));
				StartCoroutine (Hold ());

			}

			StartCoroutine (Hold ());
		}
		yield return null;
	}

	IEnumerator Translate (Move mov)
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
		Debug.Log ("transform by " + transform.forward * end * Time.deltaTime);
		yield return new WaitForSeconds (0.5f);
	}

	IEnumerator Turn (Move mov)
	{
		Quaternion target = Quaternion.Euler (0, 0, (float) mov.dir);
		do
		{
			transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
			Debug.Log ("rotate to " + (float) mov.dir);
			StartCoroutine (Hold ());
		} while (rot != target);
		yield return new WaitForSeconds (0.5f);
	}
	IEnumerator Hold ()
	{
		yield return new WaitForSeconds (0.5f);
	}
}