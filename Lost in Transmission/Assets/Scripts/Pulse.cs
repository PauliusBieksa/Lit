using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pulse : MonoBehaviour
{

	// Use this for initialization
	[SerializeField]
	private RectTransform tRect;

	[SerializeField]
	private Transform transform;

	[SerializeField] private float sizeLimmiter = 2;
	[SerializeField] private float speed = 5;

	void Start ()
	{
		tRect = GetComponent<RectTransform> ();
		if (tRect == null)
		{
			transform = GetComponent<Transform> ();
		}
	}

	// Update is called once per frame
	void Update ()
	{
		//grow and shrink a UI image by a sine wave that takes time#
		float modifier = Mathf.Sin (Time.time * speed) / sizeLimmiter;

		if (tRect != null)
			tRect.localScale = tRect.localScale += new Vector3 (modifier, modifier, 0) * Time.deltaTime;
		else
			transform.localScale = transform.localScale += new Vector3 (modifier, modifier, 0) * Time.deltaTime;
	}
}