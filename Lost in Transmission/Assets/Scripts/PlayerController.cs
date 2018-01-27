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

	bool rotating;
	bool moving;

	public float startTime;
	public float journeyTime = 0.5f;

	public Move m1;
	public Move m2;
	public Move m3;
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

		m3.dir = Dirs.NE;
		m3.type = MoveTypes.MOVE;

		m.Add (m1);
		m.Add (m2);
		m.Add (m3);

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

	public IEnumerator ExecuteMoves (List<Move> moves)
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
				StartCoroutine (Translate (mov));
				yield return new WaitForSeconds (waitTime);
				Debug.Log ("moved");

			}
		}
	}

	IEnumerator Translate (Move mov)
	{
		Vector3 startPos = transform.position;
		float end;
		if (mov.dir == Dirs.N || mov.dir == Dirs.E || mov.dir == Dirs.S || mov.dir == Dirs.W)
		{
			end = 1.0f;
		}
		else
		{
			end = Mathf.Sqrt (2);
		}

		Debug.Log ("transform by " + transform.forward * end);

		while (rotating || moving)
		{
			yield return null;
		}

		startTime = Time.time;
		float fracComplete = 0.0f;
		Debug.Log ("BULLSHIT TEXT " + fracComplete);
		while (fracComplete < 1.0f)
		{
			float localDT = Time.deltaTime / journeyTime;
			Debug.Log ("BIG EASY TO SPOT" + localDT);
			fracComplete = (Time.time - startTime) / journeyTime;
			Debug.Log ("frac " + fracComplete);
			moving = true;

			Debug.Log ("up is " + transform.up * end);
			pos += (transform.up * end * localDT);
			transform.position = (pos);
			//gameObject.transform.position = pos;
			yield return null;
		}
		pos -= transform.up * 0.8f * Time.deltaTime / journeyTime;
		pos.x = Mathf.Round (pos.x) - 0.5f;
		pos.y = Mathf.Round (pos.y) - 0.5f;
		transform.position = pos;
		moving = false;
		yield return new WaitForSeconds (waitTime);

	}

	IEnumerator Turn (Move mov)
	{
		Debug.Log ("rotate to " + (float) mov.dir + " " + mov.dir);
		Quaternion target = Quaternion.Euler (0, 0, (float) mov.dir);

		while (rotating || moving)
		{
			yield return null;
		}

		while (rot != target)
		{
			rotating = true;
			rot = Quaternion.RotateTowards (rot, target, step);
			gameObject.transform.rotation = rot;
			yield return null;
		}
		rotating = false;
	}
}