using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_manager_script : MonoBehaviour
{
    List<Move> moves;
    int starting_index = 0;

    [SerializeField] Sprite[] onCooldown;
    [SerializeField] Sprite[] offCooldown;
    [SerializeField] GameObject player;
    PlayerInput pI;

    SpriteRenderer[] queuedSr;
    Transform[] queuedTran;

    int[] cooldowns;
    List<int[]> cHistory = new List<int[]>();
    SpriteRenderer[] abilitySr;

    bool validTurn = false;
    float turnTimer = 10.0f;

    // Use this for initialization
    void Start()
    {
        moves = new List<Move>();
        cooldowns = new int[6];
        abilitySr = new SpriteRenderer[6];
        for (int i = 0; i < 6; i++)
        {
            abilitySr[i] = GameObject.Find("ability_card_" + i).GetComponent<SpriteRenderer>();
            abilitySr[i].sprite = offCooldown[i];
        }
        GameObject[] q = GameObject.FindGameObjectsWithTag("Queued");
        queuedSr = new SpriteRenderer[4];
        queuedTran = new Transform[4];
        for (int i = 0; i < 4; i++)
        {
            queuedSr[i] = q[i].GetComponent<SpriteRenderer>();
            queuedTran[i] = q[i].GetComponent<Transform>();
        }
        pI = player.GetComponent<PlayerInput>();
    }

    void EndTurn()
    {
        if (!validTurn)
            starting_index = 0;

        List<Move> turn = new List<Move>();
        for (int i = 0; i < (moves.Count < 3 ? moves.Count : 3); i++)
            turn.Add(moves[starting_index + i]);

        moves.Clear();
        for (int i = 0; i < 4; i++)
        {
            // make invisible
        }
        // execute moves
    }

    void StartTurn()
    {

    }

    // Returns whether an ability is on cooldown
    public bool Cooldown(MoveTypes m)
    {
        return cooldowns[(int)m] > 0 ? false : true;
    }

    // Moves the move selector up the queue
    public void QueueUp()
    {
        if (moves.Count < 3)
            return;
        if (starting_index > 0)
        {
            starting_index--;

            // Move the cards
            float lowest = 50.0f;
            int lowestIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                if (queuedTran[i].position.y < lowest)
                {
                    lowestIndex = i;
                    lowest = queuedTran[i].position.y;
                }
                // move each card after it has been checked
                queuedTran[i].position = new Vector3(queuedTran[i].position.x, queuedTran[i].position.y - 1.3f, queuedTran[i].position.z);
            }
            queuedTran[lowestIndex].position = new Vector3(queuedTran[lowestIndex].position.x, queuedTran[lowestIndex].position.y + (1.3f * 4.0f), queuedTran[lowestIndex].position.z);

            validTurn = true;
            // First queued check
            if (cooldowns[(int)moves[starting_index].type] > 0)
            {
                validTurn = false;
                // put a lock on
            }
            else
            {
                // remove lock
            }
            // Second queued check
            if (cooldowns[(int)moves[starting_index + 1].type] > 0)
            {
                validTurn = false;
                // Put a lock on
            }
            else
            {
                // remove lock
            }
        }
    }

    // Moves the move selector down the queue
    public void QueueDown()
    {
        if (moves.Count < 3)
            return;
        if (starting_index < moves.Count - 1)
        {
            starting_index++;

            // Move the cards
            float highest = 50.0f;
            int highestIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                if (queuedTran[i].position.y < highest)
                {
                    highestIndex = i;
                    highest = queuedTran[i].position.y;
                }
                // move each card after it has been checked
                queuedTran[i].position = new Vector3(queuedTran[i].position.x, queuedTran[i].position.y - 1.3f, queuedTran[i].position.z);
            }
            queuedTran[highestIndex].position = new Vector3(queuedTran[highestIndex].position.x, queuedTran[highestIndex].position.y + (1.3f * 4.0f), queuedTran[highestIndex].position.z);

            validTurn = true;
            // First queued check
            if (cooldowns[(int)moves[starting_index].type] > 0)
            {
                validTurn = false;
                // put a lock on
            }
            else
            {
                // remove lock
            }
            // Second queued check
            if (cooldowns[(int)moves[starting_index + 1].type] > 0)
            {
                validTurn = false;
                // Put a lock on
            }
            else
            {
                // remove lock
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        turnTimer -= Time.deltaTime;
        if (turnTimer <= 0.0f)
            EndTurn();
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
        float highest = -50.0f;
        int highestIndex = 0;
        for (int i = 0; i < 4; i++)
        {
            if (queuedTran[i].position.y > highest)
            {
                highestIndex = i;
                highest = queuedTran[i].position.y;
            }
            // move each card after it has been checked
            queuedTran[i].position = new Vector3(queuedTran[i].position.x, queuedTran[i].position.y + 1.3f, queuedTran[i].position.z);
        }
        queuedTran[highestIndex].position = new Vector3(queuedTran[highestIndex].position.x, queuedTran[highestIndex].position.y - (1.3f * 4.0f), queuedTran[highestIndex].position.z);
        if (moves.Count < 3)
        {
            // make sprite appear
        }
    }
}
