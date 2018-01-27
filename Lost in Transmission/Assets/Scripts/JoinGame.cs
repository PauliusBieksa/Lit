using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinGame : MonoBehaviour
{

	[SerializeField]
	PlayerInput pI;

	RectTransform rect;
	public float speed = 30;
	public Vector3 Left = new Vector3 (-1255, 0, 0);
	public Vector3 Right = new Vector3 (1255, 0, 0);

	// Use this for initialization
	void Start ()
	{
		rect = gameObject.GetComponent<RectTransform> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (pI.ButtonDown (Button.A))
		{
			StartCoroutine (slide ());
		}
	}

	IEnumerator slide ()
	{
		while (rect.position != Left || rect.position != Right)
		{
			float before = rect.position.y;

			if (gameObject.name.Contains ("Red"))
			{
				rect.position = Vector3.MoveTowards (rect.position, Left, speed);
				Instantiate (pI.gameObject);
				pI.gameObject.transform.position = new Vector3 (-2.5f, 0.5f, 0);
			}
			else
			{
				rect.position = Vector3.MoveTowards (rect.position, Right, speed);
				Instantiate (pI.gameObject);
				pI.gameObject.transform.position = new Vector3 (2.5f, 0.5f, 0);
			}
			yield return null;
		}
	}
}