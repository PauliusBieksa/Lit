using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_manager_script : MonoBehaviour
{
    List<Move> moves;
    int starting_index = 0;

    int [] cooldowns;

	// Use this for initialization
	void Start ()
    {
        moves = new List<Move>();
        cooldowns = new int[6];
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddMove(Move m)
    {
        if (moves.Count > 2)
        {
            while (moves.Count - 2 > starting_index)
            {
                moves.RemoveAt(moves.Count - 1);
            }
            starting_index++;
        }
        moves.Add(m);
    }
}
