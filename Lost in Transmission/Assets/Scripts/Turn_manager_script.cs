using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_manager_script : MonoBehaviour
{
    List<Move> moves = new List<Move>();
    public int count = 0;
    int starting_index = 0;

    SpriteLibrary sL;

    [SerializeField] float vertOffset;

    [SerializeField] Sprite[] onCooldown;
    [SerializeField] Sprite[] offCooldown;
    [SerializeField] PlayerInput pI;
    [SerializeField] PlayerController pc;

    [SerializeField] Transform[] queuedTran = new Transform[4];
    bool[] locked = new bool[4];

    int[] cooldowns = new int[6];
    List<int[]> cHistory = new List<int[]>();

    bool validTurn = false;
    [SerializeField] float turnTimer = 15.0f;
    float loopTimer = 0.0f;

    AbilityMaster abMaster;

    // Use this for initialization
    void Start()
    {
        sL = FindObjectOfType<SpriteLibrary>();
        GameObject[] q = GameObject.FindGameObjectsWithTag("Queued");
        for (int i = 0; i < 4; i++)
        {
            //   queuedTran[i] = q[i].GetComponent<Transform>();
        }
        abMaster = GetComponentInChildren<AbilityMaster>();
        StartTurn();
    }

    void StartTurn()
    {
        loopTimer += turnTimer;
        cHistory.Add(cooldowns);
        starting_index = 0;
        MakeLocks();
    }

    void EndTurn()
    {
        queuedTran[0].GetComponent<SpriteRenderer>().sprite = sL.arrow;
        if (!validTurn)
        {
            starting_index = 0;
        }

        List<Move> turn = new List<Move>();
        for (int i = 0; i < (moves.Count < 3 ? moves.Count : 3); i++)
        {
            turn.Add(moves[starting_index + i]);
        }
        while (turn.Count < 3)
        {
            Move m = new Move();
            m.dir = Dirs.E;
            m.type = MoveTypes.MOVE;
            turn.Add(m);
        }

        for (int i = 0; i < 4; i++)
        {
            // make invisible
        }
        // execute moves
        StartCoroutine(pc.ExecuteMoves(turn));
        cHistory.Clear();
        moves.Clear();
        for (int i = 0; i < cooldowns.Length; ++i)
        {
            if (cooldowns[i] >= 3)
            {
                cooldowns[i] -= 3;
            }
        }
        StartTurn();
    }

    // Returns whether an ability is on cooldown(at the start of turn) (true is available to use)
    public bool Cooldown(MoveTypes m)
    {
        return cooldowns[(int)m] > 0 ? false : true;
    }

    // Returns whether an ability is on cooldown(now) (true is available to use)
    public bool CurrCooldown(MoveTypes m)
    {
        return cHistory[cHistory.Count - 1][(int)m] > 0 ? false : true;
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
                queuedTran[i].position = new Vector3(queuedTran[i].position.x, queuedTran[i].position.y - vertOffset, queuedTran[i].position.z);
            }
            queuedTran[lowestIndex].position = new Vector3(queuedTran[lowestIndex].position.x, queuedTran[lowestIndex].position.y + (vertOffset * 4.0f), queuedTran[lowestIndex].position.z);

            QueueValidations();
        }
    }

    // Moves the move selector down the queue
    public void QueueDown()
    {
        if (moves.Count < 3)
            return;
        if (starting_index < moves.Count - 3)
        {
            starting_index++;

            // Move the cards
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
                queuedTran[i].position = new Vector3(queuedTran[i].position.x, queuedTran[i].position.y + vertOffset, queuedTran[i].position.z);
            }
            queuedTran[highestIndex].position = new Vector3(queuedTran[highestIndex].position.x, queuedTran[highestIndex].position.y - (vertOffset * 4.0f), queuedTran[highestIndex].position.z);

            QueueValidations();
        }
    }

    void QueueValidations()
    {
        validTurn = true;
        // First queued check
        if (cHistory[starting_index][(int)moves[starting_index].type] > 0)
        {
            validTurn = false;
            locked[(int)moves[starting_index].type] = true;
            // put a lock on
        }
        else
        {
            locked[(int)moves[starting_index].type] = false;
            // remove lock
        }
        // Second queued check
        if (cHistory[starting_index][(int)moves[starting_index + 1].type] > 0)
        {
            validTurn = false;
            locked[(int)moves[starting_index + 1].type] = true;
            // Put a lock on
        }
        else
        {
            locked[(int)moves[starting_index + 1].type] = false;
            // remove lock
        }
        MakeLocks();
    }

    // Update is called once per frame
    void Update()
    {
        count = moves.Count;
        loopTimer -= Time.deltaTime;
        if (loopTimer <= 0.0f)
        {
            Debug.Log("Time's up!!!");
            EndTurn();
        }
    }

    // Adds move to the moves queue, appends starting index if required ** Check for cooldown before using **
    public void AddMove(Move m)
    {

        if (moves.Count > 2)
        {
            while (moves.Count > 3 + starting_index)
            {
                moves.RemoveAt(moves.Count - 1);
            }
            starting_index++;
        }
        cHistory.Add(new int[6]);

        for (int i = 1; i < 6; i++)
        {
            // Some jank (shouldn't be 6 times)
            cHistory[cHistory.Count - 1][i] = cHistory[cHistory.Count - 2][i];
            if (cHistory[cHistory.Count - 1][i] < 3 && cHistory[cHistory.Count - 1][i] != 0)
            {
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
            queuedTran[i].position = new Vector3(queuedTran[i].position.x, queuedTran[i].position.y + vertOffset, queuedTran[i].position.z);
        }
        queuedTran[highestIndex].position = new Vector3(queuedTran[highestIndex].position.x, queuedTran[highestIndex].position.y - (vertOffset * 4.0f), queuedTran[highestIndex].position.z);
        if (moves.Count < 3)
        {
            // make sprite appear
        }
        MakeLocks();
    }

    private void MakeLocks()
    {
        MakeLock(Button.A);
        MakeLock(Button.B);
        MakeLock(Button.X);
        MakeLock(Button.Y);
        MakeLock(Button.RB);
    }

    private void MakeLock(Button b)
    {
        int bIndex = IndexButton(b);
        int cd;
        Locks l = Locks.OPEN;
        if (moves.Count > 3)
        {
            cd = cHistory[starting_index + 2][bIndex];
        }
        else
        {
            cd = cHistory[starting_index + (cHistory.Count - 1)][bIndex];
        }
        if (cd < 3 && cd != 0)
        {
            l = Locks.CLOSED;
        }
        else if (cd >= 3)
        {
            l = Locks.HECKA;
        }
        abMaster.UpdateAbility(b, cd, l);
    }

    private int IndexButton(Button b)
    {
        if((int)b < 4)
        {
            return (int)b;
        }
        else if((int)b < 6)
        {
            return (int)b + 2;
        }
        else if((int)b < 8)
        {
            return (int)b - 2;
        }
        else
        {
            return (int)b;
        }
    }
}
