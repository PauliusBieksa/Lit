using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

	void CountdownUpdate (float maxSecs, float secondsRemaining)
	{
		float percent = secondsRemaining / maxSecs;
		gameObject.transform.localScale = new Vector3 (percent, percent, percent);
	}

}