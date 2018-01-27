using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Move_manager_script : MonoBehaviour
{
    [SerializeField] PlayerController pc1;
    [SerializeField] PlayerController pc2;

    List<Transform> projectiles;
    Transform player1t;
    Transform player2t;

    Move m1;
    Move m2;

    // Use this for initialization
    void Start()
    {
        player1t = GameObject.Find("Player1").GetComponent<Transform>();
        player2t = GameObject.Find("Player2").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc1.Executed && pc2.Executed)
        {
            m1 = pc1.Passthrough[0];
            pc1.Passthrough.RemoveAt(0);
            m2 = pc2.Passthrough[0];
            pc2.Passthrough.RemoveAt(0);
            pc1.Executed = false;
            pc2.Executed = false;
            Execute();
        }
    }


    void Execute()
    {
        bool p1pushed = false;
        Dirs p1dir = Dirs.NONE;
        bool p2pushed = false;
        Dirs p2dir = Dirs.NONE;
        projectiles.Clear();
        GameObject[] pr = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject g in pr)
        {
            projectiles.Add(g.GetComponent<Transform>());
        }
        // All projectile checks
        foreach (Transform t in projectiles)
        {
            // N
            if (transform.rotation.z == 0.0f)
            {
                if (Vector3.SqrMagnitude(transform.position + (new Vector3(0.0f, 1.0f, 0.0f)) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.S)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p1pushed = true;
                    p1dir = Dirs.N;
                }
                else if (Vector3.SqrMagnitude(transform.position + (new Vector3(0.0f, 1.0f, 0.0f)) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.S)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p2pushed = true;
                    p2dir = Dirs.N;
                }
            }
            // NE
            else if (transform.rotation.z == -45.0f || transform.rotation.z == 315.0f)
            {
                if (Vector3.SqrMagnitude(transform.position + (new Vector3(1.0f, 1.0f, 0.0f)) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.SW)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p1pushed = true;
                    p1dir = Dirs.NE;
                }
                else if (Vector3.SqrMagnitude(transform.position + (new Vector3(1.0f, 1.0f, 0.0f)) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.SW)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p2pushed = true;
                    p2dir = Dirs.NE;
                }
            }
            // E
            else if (transform.rotation.z == -90.0f || transform.rotation.z == 270.0f)
            {
                if (Vector3.SqrMagnitude(transform.position + (new Vector3(1.0f, 0.0f, 0.0f)) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.W)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p1pushed = true;
                    p1dir = Dirs.E;
                }
                else if (Vector3.SqrMagnitude(transform.position + (new Vector3(1.0f, 0.0f, 0.0f)) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.W)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p2pushed = true;
                    p2dir = Dirs.E;
                }
            }
            // SE
            else if (transform.rotation.z == -135.0f || transform.rotation.z == 225.0f)
            {
                if (Vector3.SqrMagnitude(transform.position + (new Vector3(1.0f, -1.0f, 0.0f)) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.NW)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p1pushed = true;
                    p1dir = Dirs.SE;
                }
                else if (Vector3.SqrMagnitude(transform.position + (new Vector3(1.0f, -1.0f, 0.0f)) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.NW)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p2pushed = true;
                    p2dir = Dirs.SE;
                }
            }
            // S
            else if (transform.rotation.z == -180.0f || transform.rotation.z == 180.0f)
            {
                if (Vector3.SqrMagnitude(transform.position + (new Vector3(0.0f, -1.0f, 0.0f)) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.N)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p1pushed = true;
                    p1dir = Dirs.S;
                }
                else if (Vector3.SqrMagnitude(transform.position + (new Vector3(0.0f, -1.0f, 0.0f)) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.N)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p2pushed = true;
                    p2dir = Dirs.S;
                }
            }
            // SW
            else if (transform.rotation.z == -225.0f || transform.rotation.z == 125.0f)
            {
                if (Vector3.SqrMagnitude(transform.position + (new Vector3(-1.0f, -1.0f, 0.0f)) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.NE)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p1pushed = true;
                    p1dir = Dirs.SW;
                }
                else if (Vector3.SqrMagnitude(transform.position + (new Vector3(-1.0f, -1.0f, 0.0f)) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.NE)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p2pushed = true;
                    p2dir = Dirs.SW;
                }
            }
            // W
            else if (transform.rotation.z == -270.0f || transform.rotation.z == 125.0f)
            {
                if (Vector3.SqrMagnitude(transform.position + (new Vector3(-1.0f, 0.0f, 0.0f)) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.E)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p1pushed = true;
                    p1dir = Dirs.W;
                }
                else if (Vector3.SqrMagnitude(transform.position + (new Vector3(-1.0f, 0.0f, 0.0f)) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.E)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p2pushed = true;
                    p2dir = Dirs.W;
                }
            }
            // NW
            else if (transform.rotation.z == -315.0f || transform.rotation.z == 90.0f)
            {
                if (Vector3.SqrMagnitude(transform.position + (new Vector3(-1.0f, 1.0f, 0.0f)) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.SE)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p1pushed = true;
                    p1dir = Dirs.NW;
                }
                else if (Vector3.SqrMagnitude(transform.position + (new Vector3(-1.0f, 1.0f, 0.0f)) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.SE)
                    {
                        if (transform.rotation.z < 180.0f)
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                        else
                            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -180.0f);
                        continue;
                    }
                    p2pushed = true;
                    p2dir = Dirs.NW;
                }
            }
        }


    }
}
