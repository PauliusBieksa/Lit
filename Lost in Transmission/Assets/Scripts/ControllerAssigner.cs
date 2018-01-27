using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAssigner : MonoBehaviour {

    private List<int> usedControllers = new List<int>();
    [SerializeField]
    private GameObject[] players = new GameObject[2];
    private PlayerInput[] inputs = new PlayerInput[2];

	// Use this for initialization
	void Start () {
        inputs[0] = players[0].GetComponent<PlayerInput>();
        inputs[1] = players[1].GetComponent<PlayerInput>();
    }
	
	// Update is called once per frame
	void Update () {
		for(int i = 1; i < 3; ++i)
        {
            if (!usedControllers.Contains(i))
			{
				if (Input.GetButton(i + "_A"))
				{
					AssignController(i);
				}
            }

        }
	}

    private void AssignController(int conNum)
    {
        for (int i = 0; i < 2; ++i)
        {
            if (!inputs[i].hasController())
            {
                usedControllers.Add(conNum);
                inputs[i].setControllerNumber(conNum);
                return;
            }
        }
    }
}
