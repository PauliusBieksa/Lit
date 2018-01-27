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
	public float dist = 1;
	public float chargePower = 2;

	bool rotating;
	bool moving;
	bool collidable = false;

	public float startTime;
	public float journeyTime = 0.5f;

	[SerializeField] public Move m1;
	[SerializeField] public Move m2;
	[SerializeField] public Move m3;
	List<Move> m = new List<Move> ();
	public static Move mov;

	[SerializeField] Turn_manager_script tm;
	public List<Move> Passthrough = new List<Move> ();
	public bool Executed = false;

	//BoxCollider2D col;

	void Start ()
	{
		pos = gameObject.transform.position;
		rot = gameObject.transform.rotation;
		trans = gameObject.transform;

		m1.dir = Dirs.E;
		m1.type = MoveTypes.MOVE;

		m2.dir = Dirs.S;
		m2.type = MoveTypes.MOVE;

		m3.dir = Dirs.NW;
		m3.type = MoveTypes.MELEE;

		m.Add (m1);
		m.Add (m2);
		m.Add (m3);

		//col = gameObject.GetComponent<BoxCollider2D> ();

		Debug.Log ("Started");
	}

	public void TurnPassthrough (List<Move> p)
	{
		Passthrough = p;
	}

	public void ExecuteMovesSequence (List<Move> m)
	{
		StartCoroutine (ExecuteMoves (m));
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
		Executed = false;
		Debug.Log ("Executing");
		int i = 0;
		foreach (Move m in moves)
		{
			mov = m;
			i++;
			Debug.Log ("loop " + i);

			// if (mov.type == MoveTypes.BLOCK)
			// {
			// 	Vector3 reset = transform.position;
			// 	yield return new WaitForSeconds (0.6f);
			// 	transform.position = reset;
			// }
			if (mov.type == MoveTypes.MOVE)
			{
				StartCoroutine (Turn (mov));
				yield return new WaitForSeconds (waitTime);
				Debug.Log ("turned");
				StartCoroutine (Translate (mov, dist, gameObject));
				yield return new WaitForSeconds (waitTime);
				Debug.Log ("moved");

			}
			// else if (mov.type == MoveTypes.MELEE)
			// {
			// 	StartCoroutine (Turn (mov));
			// 	yield return new WaitForSeconds (waitTime);
			// 	Debug.Log ("turned");
			// 	StartCoroutine (Translate (mov, chargePower, gameObject));
			// 	collidable = true;

			// }
		}

		Executed = true;
	}

	void OnCollisionEnter (Collision other)
	{
		collidable = false;
		if (other.gameObject.name.ToLower ().Contains ("player"))
		{
			StopCoroutine (Translate (mov, chargePower, gameObject));
			Pushback (other.gameObject, mov);
		}
	}

	public void Pushback (GameObject hitPlayer, Move m)
	{
		StartCoroutine (Translate (mov, chargePower, hitPlayer));
	}

	IEnumerator Translate (Move mov, float d, GameObject g)
	{
		Vector3 startPos = transform.position;
		float end;
		if (mov.dir == Dirs.N || mov.dir == Dirs.E || mov.dir == Dirs.S || mov.dir == Dirs.W)
		{
			end = d;
		}
		else
		{
			end = Mathf.Sqrt ((d * d) - (d * d));
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
			float elapsedTime = (Time.time - startTime);
			float localDT = Time.deltaTime / journeyTime;
			if ((elapsedTime + localDT) > journeyTime)
			{
				localDT = journeyTime - elapsedTime;
			}
			Debug.Log ("BIG EASY TO SPOT" + localDT);
			fracComplete = elapsedTime / journeyTime;
			Debug.Log ("frac " + fracComplete.ToString ());
			moving = true;

			Debug.Log ("up is " + transform.up * end);
			pos += (transform.up * end * localDT);
			transform.position = (pos);
			//gameObject.transform.position = pos;
			yield return null;
		}
		pos.x = Mathf.Floor (pos.x) + 0.5f;
		pos.y = Mathf.Floor (pos.y) + 0.5f;
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