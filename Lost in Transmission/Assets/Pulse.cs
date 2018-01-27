using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pulse : MonoBehaviour
{
	RectTransform rect;
	float wave;
	void Start ()
	{
		rect = gameObject.GetComponent<RectTransform> ();
		//wave = 
	}
	// Update is called once per frame
	void Update ()
	{
		rect.localScale = rect.localScale * Mathf.Sin (Time.time);
		Debug.Log (wave);
	}
}