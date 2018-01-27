using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinGame : MonoBehaviour
{

	[SerializeField]
	GameObject[] player = new GameObject[2];

	PlayerInput[] pI = new PlayerInput[2];

	RectTransform rect;
	public float speed = 30;
	public Vector3 Left = new Vector3 (-1255, 0, 0);
	public Vector3 Right = new Vector3 (1255, 0, 0);

	// Use this for initialization
	void Start ()
	{
		player[0].GetComponent<SpriteRenderer> ().sortingOrder = -5;
		player[1].GetComponent<SpriteRenderer> ().sortingOrder = -5;
		rect = gameObject.GetComponent<RectTransform> ();
		pI[0] = player[0].GetComponent<PlayerInput> ();
		pI[1] = player[1].GetComponent<PlayerInput> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (pI[0].ButtonDown (Button.A))
		{
			StartCoroutine (slideL ());
		}

		if (pI[1].ButtonDown (Button.A))
		{
			StartCoroutine (slideR ());
		}
	}

	IEnumerator slideL ()
	{
		while (rect.position != Left)
		{
			float before = rect.position.y;

			rect.position = Vector3.MoveTowards (rect.position, Left, speed);
			player[0].GetComponent<SpriteRenderer> ().sortingOrder = 1;
			pI[0].gameObject.transform.position = new Vector3 (-2.5f, 0.5f, 0);
			yield return null;
		}
	}

	IEnumerator slideR ()
	{
		while (rect.position != Right)
		{
			rect.position = Vector3.MoveTowards (rect.position, Right, speed);
			player[1].GetComponent<SpriteRenderer> ().sortingOrder = 1;
			pI[1].gameObject.transform.position = new Vector3 (2.5f, 0.5f, 0);
			yield return null;
		}
	}
}