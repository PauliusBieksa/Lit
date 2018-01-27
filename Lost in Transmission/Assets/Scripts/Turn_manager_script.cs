using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_manager_script : MonoBehaviour
{
    List<Move> moves;
    int starting_index = 0;

    [SerializeField] Sprite[] onCooldown;
    [SerializeField] Sprite[] offCooldown;

    int[] cooldowns;
    // True when an ability goes off cooldown
    List<int[]> cHistory = new List<int[]>();
    SpriteRenderer[] sr;

    // Use this for initialization
    void Start()
    {
        moves = new List<Move>();
        cooldowns = new int[6];
        sr = new SpriteRenderer[6];
        for (int i = 0; i < 6; i++)
        {
            sr[i] = GameObject.Find("ability_card_" + i).GetComponent<SpriteRenderer>();
            sr[i].sprite = offCooldown[i];
        }
    }

    // Returns whether an ability is on cooldown
    public bool Cooldown(MoveTypes m)
    {
        return cooldowns[(int)m] > 0 ? false : true;
    }

    // Moves the move selector up the queue
    public void QueueUp()
    {
        if (starting_index > 0)
            starting_index--;
    }

    // Moves the move selector down the queue
    public void QueueDown()
    {
        if (starting_index < moves.Count - 1)
            starting_index++;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Adds move to the moves queue, appends starting index if required ** Check for cooldown before using **
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
        cHistory.Add(new int[6]);

        for (int i = 0; i < 6; i++)
        {
            // Some jank (shouldn't be 6 times)
            if (cHistory.Count == 1)
                cHistory[cHistory.Count - 1][i] = 0;
            else
            {
                if (cHistory[cHistory.Count - 1][i] < 3 && cHistory[cHistory.Count - 1][i] != 0)
                    cHistory[cHistory.Count - 1][i]--;
            }
        }
        moves.Add(m);
        cooldowns[(int)m.type] = staticObjects.cooldowns[(int)m.type];

    }
}
