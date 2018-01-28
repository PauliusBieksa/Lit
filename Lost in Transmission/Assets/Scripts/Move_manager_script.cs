using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Move_manager_script : MonoBehaviour
{
    [SerializeField] PlayerController pc1;
    [SerializeField] PlayerController pc2;

    public GameObject pew;

    List<Transform> projectiles;
    Transform player1t;
    Transform player2t;

    Move m1;
    Move m2;

    bool currentlyMoving = false;

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


    Dirs RotToEnum(Quaternion q)////////???????????????????????????????????????????????
    {
        float z = q.eulerAngles.z;
        while (z < 0.0f)
            z += 360.0f;
        while (z > 360.0f)
            z -= 360.0f;

        if (z == 0.0f)
            return Dirs.N;
        if (z == 45.0f)
            return Dirs.NW;
        if (z == 90.0f)
            return Dirs.W;
        if (z == 135.0f)
            return Dirs.SW;
        if (z == 180.0f)
            return Dirs.S;
        if (z == 225.0f)
            return Dirs.SE;
        if (z == 270.0f)
            return Dirs.E;
        if (z == 315.0f)
            return Dirs.NE;
        return Dirs.NONE;
    }


    Vector3 DirLookup(Dirs d)
    {
        switch (d)
        {
            case Dirs.N:
                return new Vector3(0.0f, 1.0f, 0.0f);
            case Dirs.NE:
                return new Vector3(1.0f, 1.0f, 0.0f);
            case Dirs.E:
                return new Vector3(1.0f, 0.0f, 0.0f);
            case Dirs.SE:
                return new Vector3(1.0f, -1.0f, 0.0f);
            case Dirs.S:
                return new Vector3(0.0f, -1.0f, 0.0f);
            case Dirs.SW:
                return new Vector3(-1.0f, -1.0f, 0.0f);
            case Dirs.W:
                return new Vector3(-1.0f, 0.0f, 0.0f);
            case Dirs.NW:
                return new Vector3(-1.0f, 1.0f, 0.0f);
            default:
                return new Vector3(-1.0f, 1.0f, 0.0f);
        }
    }


    Dirs ReverseDir(Dirs d)
    {
        switch (d)
        {
            case Dirs.N:
                return Dirs.S;
            case Dirs.NE:
                return Dirs.SW;
            case Dirs.E:
                return Dirs.W;
            case Dirs.SE:
                return Dirs.NW;
            case Dirs.S:
                return Dirs.N;
            case Dirs.SW:
                return Dirs.NE;
            case Dirs.W:
                return Dirs.E;
            case Dirs.NW:
                return Dirs.SE;
            default:
                return Dirs.NONE;
        }
    }


    IEnumerator Execute()
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
            if (RotToEnum(t.rotation) == Dirs.N)
            {
                if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.N) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.S)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p1pushed = true;
                    p1dir = Dirs.N;
                }
                else if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.N) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.S)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p2pushed = true;
                    p2dir = Dirs.N;
                }
            }
            // NE
            else if (RotToEnum(t.rotation) == Dirs.NE)
            {
                if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.NE) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.SW)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p1pushed = true;
                    p1dir = Dirs.NE;
                }
                else if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.NE) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.SW)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p2pushed = true;
                    p2dir = Dirs.NE;
                }
            }
            // E
            else if (RotToEnum(t.rotation) == Dirs.E)
            {
                if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.E) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.W)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p1pushed = true;
                    p1dir = Dirs.E;
                }
                else if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.E) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.W)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p2pushed = true;
                    p2dir = Dirs.E;
                }
            }
            // SE
            else if (RotToEnum(t.rotation) == Dirs.SE)
            {
                if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.SE) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.NW)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p1pushed = true;
                    p1dir = Dirs.SE;
                }
                else if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.SE) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.NW)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p2pushed = true;
                    p2dir = Dirs.SE;
                }
            }
            // S
            else if (RotToEnum(t.rotation) == Dirs.S)
            {
                if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.S) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.N)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p1pushed = true;
                    p1dir = Dirs.S;
                }
                else if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.S) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.N)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p2pushed = true;
                    p2dir = Dirs.S;
                }
            }
            // SW
            else if (RotToEnum(t.rotation) == Dirs.SW)
            {
                if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.SW) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.NE)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p1pushed = true;
                    p1dir = Dirs.SW;
                }
                else if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.SW) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.NE)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p2pushed = true;
                    p2dir = Dirs.SW;
                }
            }
            // W
            else if (RotToEnum(t.rotation) == Dirs.W)
            {
                if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.W) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.E)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p1pushed = true;
                    p1dir = Dirs.W;
                }
                else if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.W) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.E)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p2pushed = true;
                    p2dir = Dirs.W;
                }
            }
            // NW
            else if (RotToEnum(t.rotation) == Dirs.NW)
            {
                if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.NW) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.SE)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p1pushed = true;
                    p1dir = Dirs.NW;
                }
                else if (Vector3.SqrMagnitude(t.position + DirLookup(Dirs.NW) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.SE)
                    {
                        t.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 180.0f);
                    }
                    p2pushed = true;
                    p2dir = Dirs.NW;
                }
            }
        }

        // Pushed by projectile
        if (p1pushed)
        {
            StartCoroutine(Lerp(player1t, p1dir, 1));
        }
        if (p2pushed)
        {
            StartCoroutine(Lerp(player1t, p1dir, 1));
        }
        yield return new WaitForSeconds(0.22f);

        // Charge checks
        Vector3 tmp = new Vector3(0.0f, 0.0f, 0.0f);
        if (m1.type == MoveTypes.CHARGE && m2.type == MoveTypes.CHARGE)
        {
            // Charging straight into each other
            if (DirLookup(m1.dir) + DirLookup(m2.dir) == tmp)
            {
                // SPECIAL HAM /////////////////////////////////////////////////////////
                if (Vector3.SqrMagnitude(player1t.position + DirLookup(m1.dir) - player2t.position) < 0.09f)
                {
                    StartCoroutine(Lerp(player1t, ReverseDir(m1.dir), 3));
                    StartCoroutine(Lerp(player2t, ReverseDir(m2.dir), 3));
                    yield return new WaitForSeconds(0.22f);
                }
                // One square in the middle
                else if (Vector3.SqrMagnitude(player1t.position + DirLookup(m1.dir) - player2t.position - DirLookup(m2.dir)) < 0.09f)
                {
                    StartCoroutine(Lerp(player1t, ReverseDir(m1.dir), 2));
                    StartCoroutine(Lerp(player2t, ReverseDir(m2.dir), 2));
                    yield return new WaitForSeconds(0.22f);
                }
                // Two squares in the middle
                else if (Vector3.SqrMagnitude(player1t.position + (DirLookup(m1.dir) * 1.5f) - player2t.position - (DirLookup(m2.dir) * 1.5f)) < 0.09f)
                {
                    StartCoroutine(Lerp(player1t, m1.dir, 1));
                    StartCoroutine(Lerp(player2t, m2.dir, 1));
                    yield return new WaitForSeconds(0.22f);
                    StartCoroutine(Lerp(player1t, ReverseDir(m1.dir), 2));
                    StartCoroutine(Lerp(player2t, ReverseDir(m2.dir), 2));
                    yield return new WaitForSeconds(0.22f);
                }
                // Three squares in the middle
                else if (Vector3.SqrMagnitude(player1t.position + (DirLookup(m1.dir) * 2.0f) - player2t.position - (DirLookup(m2.dir) * 2.0f)) < 0.09f)
                {
                    StartCoroutine(Lerp(player1t, m1.dir, 1));
                    StartCoroutine(Lerp(player2t, m2.dir, 1));
                    yield return new WaitForSeconds(0.22f);
                    StartCoroutine(Lerp(player1t, ReverseDir(m1.dir), 2));
                    StartCoroutine(Lerp(player2t, ReverseDir(m2.dir), 2));
                    yield return new WaitForSeconds(0.22f);
                }
                // No contact
                else
                {
                    StartCoroutine(Lerp(player1t, m1.dir, 2));
                    StartCoroutine(Lerp(player2t, m2.dir, 2));
                    yield return new WaitForSeconds(0.22f);
                }
            }
            else
            {
                if (Vector3.SqrMagnitude(player1t.position + DirLookup(m1.dir) - player2t.position - DirLookup(m2.dir)) < 0.09f)
                {
                    StartCoroutine(Lerp(player1t, m1.dir, 1));
                    StartCoroutine(Lerp(player2t, m2.dir, 1));
                    yield return new WaitForSeconds(0.22f);
                    StartCoroutine(Lerp(player1t, m2.dir, 2));
                    StartCoroutine(Lerp(player2t, m1.dir, 2));
                    yield return new WaitForSeconds(0.22f);
                }
                else if (Vector3.SqrMagnitude(player1t.position + (DirLookup(m1.dir) * 2.0f) - player2t.position - (DirLookup(m2.dir) * 2.0f)) < 0.09f)
                {
                    StartCoroutine(Lerp(player1t, m1.dir, 2));
                    StartCoroutine(Lerp(player2t, m2.dir, 2));
                    yield return new WaitForSeconds(0.22f);
                    StartCoroutine(Lerp(player1t, m2.dir, 2));
                    StartCoroutine(Lerp(player2t, m1.dir, 2));
                    yield return new WaitForSeconds(0.22f);
                }
                // No contact
                else
                {
                    StartCoroutine(Lerp(player1t, m1.dir, 2));
                    StartCoroutine(Lerp(player2t, m2.dir, 2));
                    yield return new WaitForSeconds(0.22f);
                }
            }
        }
        else if (m1.type == MoveTypes.CHARGE)
        {
            if (Vector3.SqrMagnitude(player1t.position + DirLookup(m1.dir) - player2t.position) < 0.09f)
            {
                StartCoroutine(Lerp(player1t, m1.dir, 1));
                yield return new WaitForSeconds(0.22f);
                if (m2.type == MoveTypes.BLOCK)
                {
                    StartCoroutine(Lerp(player1t, ReverseDir(m1.dir), 3));
                    yield return new WaitForSeconds(0.22f);
                }
                else
                {
                    StartCoroutine(Lerp(player2t, m1.dir, 2));
                    yield return new WaitForSeconds(0.22f);
                }
            }
            else if (Vector3.SqrMagnitude(player1t.position + (DirLookup(m1.dir) * 2.0f) - player2t.position) < 0.09f)
            {
                StartCoroutine(Lerp(player1t, m1.dir, 2));
                yield return new WaitForSeconds(0.22f);
                if (m2.type == MoveTypes.BLOCK)
                {
                    StartCoroutine(Lerp(player1t, ReverseDir(m1.dir), 3));
                    yield return new WaitForSeconds(0.22f);
                }
                else
                {
                    StartCoroutine(Lerp(player2t, m1.dir, 2));
                    yield return new WaitForSeconds(0.22f);
                }
            }
            // No contact
            else
            {
                bool hit = false;
                foreach (Transform p in projectiles)
                {
                    if (p.position == player1t.position + DirLookup(m1.dir))
                    {
                        hit = true;
                        StartCoroutine(Lerp(player1t, m1.dir, 1));
                        yield return new WaitForSeconds(0.22f);

                        StartCoroutine(Lerp(player1t, RotToEnum(p.rotation), 1));
                        Destroy(p.gameObject);
                        yield return new WaitForSeconds(0.22f);
                    }
                }
                if (!hit)
                {
                    foreach (Transform p in projectiles)
                    {
                        if (p.position == player1t.position + DirLookup(m1.dir) * 2.0f)
                        {
                            hit = true;
                            StartCoroutine(Lerp(player1t, m1.dir, 2));
                            yield return new WaitForSeconds(0.22f);

                            StartCoroutine(Lerp(player1t, RotToEnum(p.rotation), 1));
                            Destroy(p.gameObject);
                            yield return new WaitForSeconds(0.22f);
                        }
                    }
                }
                if (!hit)
                {
                    StartCoroutine(Lerp(player1t, m1.dir, 2));
                    // StartCoroutine(Lerp(player2t, p2dir, 2));
                    yield return new WaitForSeconds(0.22f);
                }
            }
        }
        else if (m2.type == MoveTypes.CHARGE)
        {
            if (Vector3.SqrMagnitude(player2t.position + DirLookup(m2.dir) - player1t.position) < 0.09f)
            {
                StartCoroutine(Lerp(player2t, m2.dir, 1));
                yield return new WaitForSeconds(0.22f);
                if (m1.type == MoveTypes.BLOCK)
                {
                    StartCoroutine(Lerp(player2t, ReverseDir(m2.dir), 3));
                    yield return new WaitForSeconds(0.22f);
                }
                else
                {
                    StartCoroutine(Lerp(player1t, m2.dir, 2));
                    yield return new WaitForSeconds(0.22f);
                }
            }
            else if (Vector3.SqrMagnitude(player2t.position + (DirLookup(m2.dir) * 2.0f) - player1t.position) < 0.09f)
            {
                StartCoroutine(Lerp(player2t, m2.dir, 2));
                yield return new WaitForSeconds(0.22f);
                if (m1.type == MoveTypes.BLOCK)
                {
                    StartCoroutine(Lerp(player2t, ReverseDir(m2.dir), 3));
                    yield return new WaitForSeconds(0.22f);
                }
                else
                {
                    StartCoroutine(Lerp(player1t, m2.dir, 2));
                    yield return new WaitForSeconds(0.22f);
                }
            }
            // No contact
            else
            {
                bool hit = false;
                foreach (Transform p in projectiles)
                {
                    if (p.position == player2t.position + DirLookup(m2.dir))
                    {
                        hit = true;
                        StartCoroutine(Lerp(player2t, m2.dir, 1));
                        yield return new WaitForSeconds(0.22f);

                        StartCoroutine(Lerp(player2t, RotToEnum(p.rotation), 1));
                        Destroy(p.gameObject);
                        yield return new WaitForSeconds(0.22f);
                    }
                }
                if (!hit)
                {
                    foreach (Transform p in projectiles)
                    {
                        if (p.position == player2t.position + DirLookup(m2.dir) * 2.0f)
                        {
                            hit = true;
                            StartCoroutine(Lerp(player2t, m2.dir, 2));
                            yield return new WaitForSeconds(0.22f);

                            StartCoroutine(Lerp(player2t, RotToEnum(p.rotation), 1));
                            Destroy(p.gameObject);
                            yield return new WaitForSeconds(0.22f);
                        }
                    }
                }
                if (!hit)
                {
                    //StartCoroutine(Lerp(player1t, p1dir, 2));
                    StartCoroutine(Lerp(player2t, m2.dir, 2));
                    yield return new WaitForSeconds(0.22f);
                }
            }
        }
        else if (m1.type == MoveTypes.MOVE && m2.type == MoveTypes.MOVE)
        {
            if (!(Vector3.SqrMagnitude(player1t.position + DirLookup(m1.dir) - player2t.position - DirLookup(m2.dir)) < 0.09f))
            {
                StartCoroutine(Turn(player1t, m1.dir, 2.0f));
                StartCoroutine(Turn(player2t, m2.dir, 2.0f));
                yield return new WaitForSeconds(0.22f);

                StartCoroutine(Lerp(player1t, m1.dir, 1));
                StartCoroutine(Lerp(player2t, m2.dir, 1));
                yield return new WaitForSeconds(0.22f);
                // Projectile check
                foreach (Transform p in projectiles)
                {
                    if (p.position == player1t.position)
                    {
                        StartCoroutine(Lerp(player1t, RotToEnum(p.rotation), 1));
                        Destroy(p.gameObject);
                        yield return new WaitForSeconds(0.22f);
                    }
                    if (p.position == player2t.position)
                    {
                        StartCoroutine(Lerp(player2t, RotToEnum(p.rotation), 1));
                        Destroy(p.gameObject);
                        yield return new WaitForSeconds(0.22f);
                    }
                }
            }
        }
        else if (m1.type == MoveTypes.MOVE)
        {
            if (!(Vector3.SqrMagnitude(player1t.position + DirLookup(m1.dir) - player2t.position) < 0.09f))
            {
                StartCoroutine(Turn(player1t, m1.dir, 2.0f));
                yield return new WaitForSeconds(0.22f);

                StartCoroutine(Lerp(player1t, m1.dir, 1));
                yield return new WaitForSeconds(0.22f);
                foreach (Transform p in projectiles)
                {
                    if (p.position == player1t.position)
                    {
                        StartCoroutine(Lerp(player1t, RotToEnum(p.rotation), 1));
                        Destroy(p.gameObject);
                        yield return new WaitForSeconds(0.22f);
                    }
                }
            }
        }
        else if (m2.type == MoveTypes.MOVE)
        {
            if (!(Vector3.SqrMagnitude(player2t.position + DirLookup(m2.dir) - player1t.position) < 0.09f))
            {
                StartCoroutine(Turn(player2t, m2.dir, 2.0f));
                yield return new WaitForSeconds(0.22f);

                StartCoroutine(Lerp(player2t, m2.dir, 1));
                yield return new WaitForSeconds(0.22f);
                foreach (Transform p in projectiles)
                {
                    if (p.position == player2t.position)
                    {
                        StartCoroutine(Lerp(player2t, RotToEnum(p.rotation), 1));
                        Destroy(p.gameObject);
                        yield return new WaitForSeconds(0.22f);
                    }
                }
            }
        }
        else if (m1.type == MoveTypes.MELEE && m2.type == MoveTypes.MELEE)
        {
            bool p1hit = false;
            bool p2hit = false;
            if (Vector3.SqrMagnitude(player1t.position + DirLookup(m1.dir) - player2t.position) < 0.09f)
            {
                p2hit = true;
            }
            if (Vector3.SqrMagnitude(player2t.position + DirLookup(m2.dir) - player1t.position) < 0.09f)
            {
                p1hit = true;
            }
            if (p1hit)
            {
                StartCoroutine(Lerp(player1t, m2.dir, 3));
                yield return new WaitForSeconds(0.22f);
            }
            if (p2hit)
            {
                StartCoroutine(Lerp(player2t, m1.dir, 3));
                yield return new WaitForSeconds(0.22f);
            }
        }
        else if (m1.type == MoveTypes.MELEE)
        {
            if (Vector3.SqrMagnitude(player1t.position + DirLookup(m1.dir) - player2t.position) < 0.09f)
            {
                if (m2.type == MoveTypes.BLOCK)
                {
                    StartCoroutine(Lerp(player1t, ReverseDir(m1.dir), 3));
                    yield return new WaitForSeconds(0.22f);
                }
                else
                {
                    StartCoroutine(Lerp(player2t, m1.dir, 3));
                    yield return new WaitForSeconds(0.22f);
                }
            }
        }
        else if (m2.type == MoveTypes.MELEE)
        {
            if (Vector3.SqrMagnitude(player2t.position + DirLookup(m2.dir) - player1t.position) < 0.09f)
            {
                if (m1.type == MoveTypes.BLOCK)
                {
                    StartCoroutine(Lerp(player2t, ReverseDir(m2.dir), 3));
                    yield return new WaitForSeconds(0.22f);
                }
                else
                {
                    StartCoroutine(Lerp(player1t, m2.dir, 3));
                    yield return new WaitForSeconds(0.22f);
                }
            }
        }
        else
        {
            if (m1.type == MoveTypes.RANGE)
                if (Vector3.SqrMagnitude(player1t.position + DirLookup(m1.dir) - player2t.position) < 0.09f)
                {
                    // animating goes here*************************
                    StartCoroutine(Lerp(player2t, m1.dir, 1));
                    yield return new WaitForSeconds(0.22f);
                }
                else
                    Instantiate(pew, (player1t.position + DirLookup(m1.dir)), Quaternion.Euler(0.0f, 0.0f, (float)m1.dir));
            if (m2.type == MoveTypes.RANGE)
                if (Vector3.SqrMagnitude(player2t.position + DirLookup(m2.dir) - player1t.position) < 0.09f)
                {
                    // animating goes here*************************
                    StartCoroutine(Lerp(player1t, m2.dir, 1));
                    yield return new WaitForSeconds(0.22f);
                }
                else
                    Instantiate(pew, (player2t.position + DirLookup(m2.dir)), Quaternion.Euler(0.0f, 0.0f, (float)m2.dir));
        }
        yield return null;
    }

    // May be usefull to have the duration as parameter
    public IEnumerator Lerp(Transform t, Dirs d, int dist)
    {
        // May be required ///////////////////////////////////
        //while (currentlyMoving)
        //    yield return null;

        Vector3 startingPosition = t.position;
        currentlyMoving = true;
        float startingTime = Time.unscaledTime;
        float timeRemaining = staticObjects.moveTime;
        float scalar = 1.0f / timeRemaining;
        while (timeRemaining > 0.0f)
        {
            timeRemaining -= Time.unscaledTime - startingTime;
            if (timeRemaining < 0.0f)
                timeRemaining = 0.0f;
            float alpha = 1.0f - (timeRemaining * scalar);
            t.position = startingPosition + (DirLookup(d) * (float)dist * alpha);
            yield return null;
        }
        currentlyMoving = false;
    }


    IEnumerator Turn(Transform t, Dirs target, float step)
    {
        Quaternion qtarget = Quaternion.Euler(0, 0, (float)target);
        Quaternion rot = t.rotation;
        while (rot != qtarget)
        {
            rot = Quaternion.RotateTowards(rot, qtarget, step);
            t.rotation = rot;
            yield return null;
        }
    }
}
