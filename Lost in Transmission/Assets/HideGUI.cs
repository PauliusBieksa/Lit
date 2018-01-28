using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGUI : MonoBehaviour
{

	RectTransform rt;
	float speed = 10;
	bool hidden = false;
	Vector3 down;

	// Use this for initialization

	void Start ()
	{
		rt = gameObject.GetComponent<RectTransform> ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Transition ();
		}
	}

	//open move list and freeze time
	void Transition ()
	{
		if (rt.localScale == new Vector3 (1, 1, 1))
		{
			rt.localScale = new Vector3 (0, 0, 0);
			Time.timeScale = 1;
		}
		else
		{
			rt.localScale = new Vector3 (1, 1, 1);
			Time.timeScale = 0;
		}
	}
}