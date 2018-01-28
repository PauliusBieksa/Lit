using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinGame : MonoBehaviour
{

	[SerializeField]
	GameObject[] player = new GameObject[2];
    [SerializeField]
	PlayerInput[] pI = new PlayerInput[2];

	RectTransform rect;
	public float speed = 30;
	private Vector3 Left = new Vector3 (-1255, 0, 0);
	private Vector3 Right = new Vector3 (1255, 0, 0);

	// Use this for initialization
	void Start ()
	{
		rect = GetComponent<RectTransform> ();
		Debug.Log (pI[0].name);
	}

	// Update is called once per frame
	void Update ()
	{
		if (pI[0].ButtonDown (Button.A))
		{
			Debug.Log ("p1 button");
			StartCoroutine (slideL ());
			Debug.Log ("done 1");
		}

		if (pI[1].ButtonDown (Button.A))
		{
			Debug.Log ("p1 button");
			StartCoroutine (slideR ());
			Debug.Log ("done 2");
		}
	}

	IEnumerator slideL ()
	{
		Debug.Log ("Red--------------------------------------------------");
		if (rect.gameObject.name.Contains ("Red"))
		{
			Debug.Log ("Red");
			while (rect.position != Left)
			{
				Debug.Log ("player 1 join");
				float before = rect.position.y;
				rect.localPosition = Vector3.MoveTowards (rect.localPosition, Left, speed);
				player[0].transform.position = new Vector3 (player[0].transform.position.x, player[0].transform.position.y, 1);
                player[0].transform.position = new Vector3 (-2.5f, 0.5f, 0);
				yield return null;
			}
		}
	}

	IEnumerator slideR ()
	{
		if (rect.gameObject.name.Contains ("Blue"))
		{
			Debug.Log ("Blue");
			while (rect.position != Right)
			{
				Debug.Log ("player 2 join");

				rect.localPosition = Vector3.MoveTowards (rect.localPosition, Right, speed);
				player[1].transform.position = new Vector3 (player[1].transform.position.x, player[1].transform.position.y, 1);
				player[1].transform.position = new Vector3 (2.5f, 0.5f, 0);
				yield return null;
			}
		}
	}
}