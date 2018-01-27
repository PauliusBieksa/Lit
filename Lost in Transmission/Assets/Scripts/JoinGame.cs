using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinGame : MonoBehaviour
{

	[SerializeField]
	PlayerInput pI;

	RectTransform rect;
	public float speed = 10;
	public Vector3 Left;
	public Vector3 Right;
	public Vector3 BNorm;
	public Vector3 RNorm;

	// Use this for initialization
	void Start ()
	{
		rect = GetComponent<RectTransform> ();
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
		while (rect.position != Left && rect.position != Right)
		{

			if (gameObject.name.Contains ("Blue"))
			{
				rect.position = Vector3.MoveTowards (BNorm, Left, speed);
			}
			else
			{
				rect.position = Vector3.MoveTowards (RNorm, Right, speed);
			}
			yield return null;
		}
	}
}