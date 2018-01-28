using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_manager_script : MonoBehaviour
{
    [SerializeField] PlayerController pc1;
    [SerializeField] PlayerController pc2;

    [SerializeField] Turn_manager_script tms1;
    [SerializeField] Turn_manager_script tms2;

    [SerializeField] GameObject BlueChargeGFX;
    [SerializeField] GameObject RedChargeGFX;

    [SerializeField] GameObject RedBlock;
    [SerializeField] GameObject BlueBlock;

    [SerializeField] GameObject RedMelee;
    [SerializeField] GameObject BlueMelee;

    public GameObject RedRanged;
    public GameObject BlueRanged;

    List<Transform> projectiles = new List<Transform>();
    Transform player1t;
    Transform player2t;

    Move m1;
    Move m2;

    public float stepSpeed = 2.0f;

    bool currentlyMoving1 = false;
    bool currentlyMoving2 = false;

    List<bool> movingProjs = new List<bool>();

    // Use this for initialization
    void Start ()
    {
        player1t = GameObject.Find ("player1").GetComponent<Transform> ();
        player2t = GameObject.Find ("player2").GetComponent<Transform> ();

        BlueChargeGFX.SetActive (false);
        RedChargeGFX.SetActive (false);

        RedBlock.SetActive (false);
        BlueBlock.SetActive (false);
        for (int i = 0; i < movingProjs.Count; i++)
            movingProjs[i] = false;
    }

    // Update is called once per frame
    void Update ()
    {
        if (pc1.Executed && pc2.Executed)
        {
            m1 = pc1.Passthrough[0];
            pc1.Passthrough.RemoveAt (0);
            m2 = pc2.Passthrough[0];
            pc2.Passthrough.RemoveAt (0);
            pc1.Executed = false;
            pc2.Executed = false;
            StartCoroutine(Execute ());
        }
    }

    Dirs RotToEnum (Quaternion q) ////////???????????????????????????????????????????????
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

    Vector3 DirLookup (Dirs d)
    {
        switch (d)
        {
            case Dirs.N:
                return new Vector3 (0.0f, 1.0f, 0.0f);
            case Dirs.NE:
                return new Vector3 (1.0f, 1.0f, 0.0f);
            case Dirs.E:
                return new Vector3 (1.0f, 0.0f, 0.0f);
            case Dirs.SE:
                return new Vector3 (1.0f, -1.0f, 0.0f);
            case Dirs.S:
                return new Vector3 (0.0f, -1.0f, 0.0f);
            case Dirs.SW:
                return new Vector3 (-1.0f, -1.0f, 0.0f);
            case Dirs.W:
                return new Vector3 (-1.0f, 0.0f, 0.0f);
            case Dirs.NW:
                return new Vector3 (-1.0f, 1.0f, 0.0f);
            default:
                return new Vector3 (-1.0f, 1.0f, 0.0f);
        }
    }

    Dirs ReverseDir (Dirs d)
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

    IEnumerator Execute ()
    {
        bool p1pushed = false;
        Dirs p1dir = Dirs.NONE;
        bool p2pushed = false;
        Dirs p2dir = Dirs.NONE;
        projectiles.Clear ();
        GameObject[] pr = GameObject.FindGameObjectsWithTag ("Projectile");
        foreach (GameObject g in pr)
        {
            projectiles.Add (g.GetComponent<Transform> ());
        }
        // All projectile checks
        foreach (Transform t in projectiles)
        {
            // N
            if (RotToEnum (t.rotation) == Dirs.N)
            {
                if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.N) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.S)
                    {
                        RedBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        RedBlock.SetActive (false);
                    }
                    p1pushed = true;
                    p1dir = Dirs.N;
                }
                else if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.N) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.S)
                    {
                        BlueBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        yield return new WaitForSeconds (1.22f);
                        BlueBlock.SetActive (false);

                    }
                    p2pushed = true;
                    p2dir = Dirs.N;
                }
            }
            // NE
            else if (RotToEnum (t.rotation) == Dirs.NE)
            {
                if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.NE) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.SW)
                    {
                        RedBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        yield return new WaitForSeconds (1.22f);
                        RedBlock.SetActive (false);
                    }
                    p1pushed = true;
                    p1dir = Dirs.NE;
                }
                else if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.NE) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.SW)
                    {
                        BlueBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        yield return new WaitForSeconds (1.22f);
                        BlueBlock.SetActive (false);
                    }
                    p2pushed = true;
                    p2dir = Dirs.NE;
                }
            }
            // E
            else if (RotToEnum (t.rotation) == Dirs.E)
            {
                if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.E) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.W)
                    {
                        RedBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        yield return new WaitForSeconds (1.22f);
                        RedBlock.SetActive (false);
                    }
                    p1pushed = true;
                    p1dir = Dirs.E;
                }
                else if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.E) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.W)
                    {
                        BlueBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        yield return new WaitForSeconds (1.22f);
                        BlueBlock.SetActive (false);
                    }
                    p2pushed = true;
                    p2dir = Dirs.E;
                }
            }
            // SE
            else if (RotToEnum (t.rotation) == Dirs.SE)
            {
                if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.SE) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.NW)
                    {
                        RedBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        yield return new WaitForSeconds (1.22f);
                        RedBlock.SetActive (false);
                    }
                    p1pushed = true;
                    p1dir = Dirs.SE;
                }
                else if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.SE) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.NW)
                    {
                        BlueBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        yield return new WaitForSeconds (1.22f);
                        BlueBlock.SetActive (false);
                    }
                    p2pushed = true;
                    p2dir = Dirs.SE;
                }
            }
            // S
            else if (RotToEnum (t.rotation) == Dirs.S)
            {
                if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.S) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.N)
                    {
                        RedBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        yield return new WaitForSeconds (1.22f);
                        RedBlock.SetActive (false);
                    }
                    p1pushed = true;
                    p1dir = Dirs.S;
                }
                else if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.S) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.N)
                    {
                        BlueBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        yield return new WaitForSeconds (1.22f);
                        BlueBlock.SetActive (false);
                    }
                    p2pushed = true;
                    p2dir = Dirs.S;
                }
            }
            // SW
            else if (RotToEnum (t.rotation) == Dirs.SW)
            {
                if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.SW) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.NE)
                    {
                        RedBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        yield return new WaitForSeconds (1.22f);
                        RedBlock.SetActive (false);
                    }
                    p1pushed = true;
                    p1dir = Dirs.SW;
                }
                else if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.SW) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.NE)
                    {
                        BlueBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        yield return new WaitForSeconds (1.22f);
                        BlueBlock.SetActive (false);
                    }
                    p2pushed = true;
                    p2dir = Dirs.SW;
                }
            }
            // W
            else if (RotToEnum (t.rotation) == Dirs.W)
            {
                if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.W) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.E)
                    {
                        RedBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        yield return new WaitForSeconds (1.22f);
                        RedBlock.SetActive (false);
                    }
                    p1pushed = true;
                    p1dir = Dirs.W;
                }
                else if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.W) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.E)
                    {
                        BlueBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        yield return new WaitForSeconds (1.22f);
                        BlueBlock.SetActive (false);
                    }
                    p2pushed = true;
                    p2dir = Dirs.W;
                }
            }
            // NW
            else if (RotToEnum (t.rotation) == Dirs.NW)
            {
                if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.NW) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK && m1.dir == Dirs.SE)
                    {
                        RedBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        yield return new WaitForSeconds (1.22f);
                        RedBlock.SetActive (false);
                    }
                    p1pushed = true;
                    p1dir = Dirs.NW;
                }
                else if (Vector3.SqrMagnitude (t.position + DirLookup (Dirs.NW) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK && m2.dir == Dirs.SE)
                    {
                        BlueBlock.SetActive (true);
                        //t.Rotate (new Vector3 (0.0f, 0.0f, 1.0f), 180.0f);
                        StartCoroutine (Turn (t, RotToEnum (t.rotation), stepSpeed));
                        yield return new WaitForSeconds (1.22f);
                        BlueBlock.SetActive (false);
                    }
                    p2pushed = true;
                    p2dir = Dirs.NW;
                }
            }
        }

        // Pushed by projectile
        if (p1pushed)
        {
            StartCoroutine (Lerp (player1t, p1dir, 1, 1));
        }
        if (p2pushed)
        {
            StartCoroutine (Lerp (player2t, p2dir, 1, 2));
        }
        yield return new WaitForSeconds (1.22f);

        // Charge checks
        Vector3 tmp = new Vector3 (0.0f, 0.0f, 0.0f);
        if (m1.type == MoveTypes.CHARGE && m2.type == MoveTypes.CHARGE)
        {
            RedChargeGFX.SetActive (true);
            BlueChargeGFX.SetActive (true);
            // Charging straight into each other
            if (DirLookup (m1.dir) + DirLookup (m2.dir) == tmp)
            {

                // SPECIAL HAM /////////////////////////////////////////////////////////
                if (Vector3.SqrMagnitude (player1t.position + DirLookup (m1.dir) - player2t.position) < 0.09f)
                {
                    StartCoroutine (Lerp (player1t, ReverseDir (m1.dir), 3, 1));
                    StartCoroutine (Lerp (player2t, ReverseDir (m2.dir), 3, 2));
                    yield return new WaitForSeconds (1.22f);
                }
                // One square in the middle
                else if (Vector3.SqrMagnitude (player1t.position + DirLookup (m1.dir) - player2t.position - DirLookup (m2.dir)) < 0.09f)
                {
                    StartCoroutine (Lerp (player1t, ReverseDir (m1.dir), 2, 1));
                    StartCoroutine (Lerp (player2t, ReverseDir (m2.dir), 2, 2));
                    yield return new WaitForSeconds (1.22f);
                }
                // Two squares in the middle
                else if (Vector3.SqrMagnitude (player1t.position + (DirLookup (m1.dir) * 1.5f) - player2t.position - (DirLookup (m2.dir) * 1.5f)) < 0.09f)
                {
                    StartCoroutine (Lerp (player1t, m1.dir, 1, 1));
                    StartCoroutine (Lerp (player2t, m2.dir, 1, 2));
                    yield return new WaitForSeconds (1.22f);
                    StartCoroutine (Lerp (player1t, ReverseDir (m1.dir), 2, 1));
                    StartCoroutine (Lerp (player2t, ReverseDir (m2.dir), 2, 2));
                    yield return new WaitForSeconds (1.22f);
                }
                // Three squares in the middle
                else if (Vector3.SqrMagnitude (player1t.position + (DirLookup (m1.dir) * 2.0f) - player2t.position - (DirLookup (m2.dir) * 2.0f)) < 0.09f)
                {
                    StartCoroutine (Lerp (player1t, m1.dir, 1, 1));
                    StartCoroutine (Lerp (player2t, m2.dir, 1, 2));
                    yield return new WaitForSeconds (1.22f);
                    StartCoroutine (Lerp (player1t, ReverseDir (m1.dir), 2, 1));
                    StartCoroutine (Lerp (player2t, ReverseDir (m2.dir), 2, 2));
                    yield return new WaitForSeconds (1.22f);
                    BlueChargeGFX.SetActive (false);
                }
                // No contact
                else
                {
                    StartCoroutine (Lerp (player1t, m1.dir, 2, 1));
                    StartCoroutine (Lerp (player2t, m2.dir, 2, 2));
                    yield return new WaitForSeconds (1.22f);
                    BlueChargeGFX.SetActive (false);
                    BlueChargeGFX.SetActive (false);
                }
            }
            else
            {
                if (Vector3.SqrMagnitude (player1t.position + DirLookup (m1.dir) - player2t.position - DirLookup (m2.dir)) < 0.09f)
                {
                    StartCoroutine (Lerp (player1t, m1.dir, 1, 1));
                    StartCoroutine (Lerp (player2t, m2.dir, 1, 2));
                    yield return new WaitForSeconds (1.22f);
                    StartCoroutine (Lerp (player1t, m2.dir, 2, 1));
                    StartCoroutine (Lerp (player2t, m1.dir, 2, 2));
                    yield return new WaitForSeconds (1.22f);
                }
                else if (Vector3.SqrMagnitude (player1t.position + (DirLookup (m1.dir) * 2.0f) - player2t.position - (DirLookup (m2.dir) * 2.0f)) < 0.09f)
                {
                    StartCoroutine (Lerp (player1t, m1.dir, 2, 1));
                    StartCoroutine (Lerp (player2t, m2.dir, 2, 2));
                    yield return new WaitForSeconds (1.22f);
                    StartCoroutine (Lerp (player1t, m2.dir, 2, 1));
                    StartCoroutine (Lerp (player2t, m1.dir, 2, 2));
                    yield return new WaitForSeconds (1.22f);
                }
                // No contact
                else
                {
                    StartCoroutine (Lerp (player1t, m1.dir, 2, 1));
                    StartCoroutine (Lerp (player2t, m2.dir, 2, 2));
                    yield return new WaitForSeconds (1.22f);
                }
            }
            RedChargeGFX.SetActive (false);
            BlueChargeGFX.SetActive (false);

        }
        else if (m1.type == MoveTypes.CHARGE)
        {
            RedChargeGFX.SetActive (true);
            if (Vector3.SqrMagnitude (player1t.position + DirLookup (m1.dir) - player2t.position) < 0.09f)
            {
                StartCoroutine (Lerp (player1t, m1.dir, 1, 1));
                yield return new WaitForSeconds (1.22f);
                if (m2.type == MoveTypes.BLOCK)
                {
                    BlueBlock.SetActive (true);
                    StartCoroutine (Lerp (player1t, ReverseDir (m1.dir), 3, 1));
                    yield return new WaitForSeconds (1.22f);
                    BlueBlock.SetActive (false);
                }
                else
                {
                    StartCoroutine (Lerp (player2t, m1.dir, 2, 2));
                    yield return new WaitForSeconds (1.22f);
                }
            }
            else if (Vector3.SqrMagnitude (player1t.position + (DirLookup (m1.dir) * 2.0f) - player2t.position) < 0.09f)
            {
                StartCoroutine (Lerp (player1t, m1.dir, 2, 1));
                yield return new WaitForSeconds (1.22f);
                if (m2.type == MoveTypes.BLOCK)
                {
                    BlueBlock.SetActive (true);
                    StartCoroutine (Lerp (player1t, ReverseDir (m1.dir), 3, 1));
                    yield return new WaitForSeconds (1.22f);
                    BlueBlock.SetActive (false);
                }
                else
                {
                    StartCoroutine (Lerp (player2t, m1.dir, 2, 2));
                    yield return new WaitForSeconds (1.22f);
                }
            }
            // No contact
            else
            {
                bool hit = false;
                foreach (Transform p in projectiles)
                {
                    if (p.position == player1t.position + DirLookup (m1.dir))
                    {
                        hit = true;
                        StartCoroutine (Lerp (player1t, m1.dir, 1, 1));
                        yield return new WaitForSeconds (1.22f);
                        RedChargeGFX.SetActive (false);

                        StartCoroutine (Lerp (player1t, RotToEnum (p.rotation), 1, 1));
                        Destroy (p.gameObject);
                        yield return new WaitForSeconds (1.22f);
                    }
                }
                if (!hit)
                {
                    foreach (Transform p in projectiles)
                    {
                        if (p.position == player1t.position + DirLookup (m1.dir) * 2.0f)
                        {
                            hit = true;
                            StartCoroutine (Lerp (player1t, m1.dir, 2, 1));
                            yield return new WaitForSeconds (1.22f);
                            RedChargeGFX.SetActive (false);

                            StartCoroutine (Lerp (player1t, RotToEnum (p.rotation), 1, 1));
                            Destroy (p.gameObject);
                            yield return new WaitForSeconds (1.22f);
                        }
                    }
                }

                if (!hit)
                {
                    StartCoroutine (Lerp (player1t, m1.dir, 2, 1));
                    // StartCoroutine(Lerp(player2t, p2dir, 2));
                    yield return new WaitForSeconds (1.22f);
                }
            }
            RedChargeGFX.SetActive (false);
        }
        else if (m2.type == MoveTypes.CHARGE)
        {
            BlueChargeGFX.SetActive (true);
            if (Vector3.SqrMagnitude (player2t.position + DirLookup (m2.dir) - player1t.position) < 0.09f)
            {
                StartCoroutine (Lerp (player2t, m2.dir, 1, 2));
                yield return new WaitForSeconds (1.22f);
                if (m1.type == MoveTypes.BLOCK)
                {
                    RedBlock.SetActive (true);
                    StartCoroutine (Lerp (player2t, ReverseDir (m2.dir), 3, 2));
                    yield return new WaitForSeconds (1.22f);
                    RedBlock.SetActive (false);
                }
                else
                {
                    StartCoroutine (Lerp (player1t, m2.dir, 2, 1));
                    yield return new WaitForSeconds (1.22f);
                }
            }
            else if (Vector3.SqrMagnitude (player2t.position + (DirLookup (m2.dir) * 2.0f) - player1t.position) < 0.09f)
            {
                StartCoroutine (Lerp (player2t, m2.dir, 2, 2));
                yield return new WaitForSeconds (1.22f);
                if (m1.type == MoveTypes.BLOCK)
                {
                    RedBlock.SetActive (true);
                    StartCoroutine (Lerp (player2t, ReverseDir (m2.dir), 3, 2));
                    yield return new WaitForSeconds (1.22f);
                    RedBlock.SetActive (false);
                }
                else
                {
                    StartCoroutine (Lerp (player1t, m2.dir, 2, 1));
                    yield return new WaitForSeconds (1.22f);
                }
            }
            // No contact
            else
            {
                bool hit = false;
                foreach (Transform p in projectiles)
                {
                    if (p.position == player2t.position + DirLookup (m2.dir))
                    {
                        hit = true;
                        StartCoroutine (Lerp (player2t, m2.dir, 1, 2));
                        yield return new WaitForSeconds (1.22f);
                        BlueChargeGFX.SetActive (false);

                        StartCoroutine (Lerp (player2t, RotToEnum (p.rotation), 1, 2));
                        Destroy (p.gameObject);
                        yield return new WaitForSeconds (1.22f);
                    }
                }
                if (!hit)
                {
                    foreach (Transform p in projectiles)
                    {
                        if (p.position == player2t.position + DirLookup (m2.dir) * 2.0f)
                        {
                            hit = true;
                            StartCoroutine (Lerp (player2t, m2.dir, 2, 2));
                            yield return new WaitForSeconds (1.22f);
                            BlueChargeGFX.SetActive (false);

                            StartCoroutine (Lerp (player2t, RotToEnum (p.rotation), 1, 2));
                            Destroy (p.gameObject);
                            yield return new WaitForSeconds (1.22f);
                        }
                    }
                }

                if (!hit)
                {
                    //StartCoroutine(Lerp(player1t, p1dir, 2));
                    StartCoroutine (Lerp (player2t, m2.dir, 2, 2));
                    yield return new WaitForSeconds (1.22f);
                }
            }
        }
        else if (m1.type == MoveTypes.MOVE && m2.type == MoveTypes.MOVE)
        {
            if (!(Vector3.SqrMagnitude (player1t.position + DirLookup (m1.dir) - player2t.position - DirLookup (m2.dir)) < 0.09f))
            {
                StartCoroutine (Turn (player1t, m1.dir, 2.0f));
                StartCoroutine (Turn (player2t, m2.dir, 2.0f));
                yield return new WaitForSeconds (1.22f);

                StartCoroutine (Lerp (player1t, m1.dir, 1, 1));
                StartCoroutine (Lerp (player2t, m2.dir, 1, 2));
                yield return new WaitForSeconds (1.22f);
                // Projectile check
                foreach (Transform p in projectiles)
                {
                    if (p.position == player1t.position)
                    {
                        StartCoroutine (Lerp (player1t, RotToEnum (p.rotation), 1, 1));
                        Destroy (p.gameObject);
                        yield return new WaitForSeconds (1.22f);
                    }
                    if (p.position == player2t.position)
                    {
                        StartCoroutine (Lerp (player2t, RotToEnum (p.rotation), 1, 2));
                        Destroy (p.gameObject);
                        yield return new WaitForSeconds (1.22f);
                    }
                }
            }
        }
        else if (m1.type == MoveTypes.MOVE)
        {
            if (!(Vector3.SqrMagnitude (player1t.position + DirLookup (m1.dir) - player2t.position) < 0.09f))
            {
                StartCoroutine (Turn (player1t, m1.dir, 2.0f));
                yield return new WaitForSeconds (1.22f);

                StartCoroutine (Lerp (player1t, m1.dir, 1, 1));
                yield return new WaitForSeconds (1.22f);
                foreach (Transform p in projectiles)
                {
                    if (p.position == player1t.position)
                    {
                        StartCoroutine (Lerp (player1t, RotToEnum (p.rotation), 1, 1));
                        Destroy (p.gameObject);
                        yield return new WaitForSeconds (1.22f);
                    }
                }
            }
        }
        else if (m2.type == MoveTypes.MOVE)
        {
            if (!(Vector3.SqrMagnitude (player2t.position + DirLookup (m2.dir) - player1t.position) < 0.09f))
            {
                StartCoroutine (Turn (player2t, m2.dir, 2.0f));
                yield return new WaitForSeconds (1.22f);

                StartCoroutine (Lerp (player2t, m2.dir, 1, 2));
                yield return new WaitForSeconds (1.22f);
                foreach (Transform p in projectiles)
                {
                    if (p.position == player2t.position)
                    {
                        StartCoroutine (Lerp (player2t, RotToEnum (p.rotation), 1, 2));
                        Destroy (p.gameObject);
                        yield return new WaitForSeconds (1.22f);
                    }
                }
            }
        }
        else if (m1.type == MoveTypes.MELEE && m2.type == MoveTypes.MELEE)
        {
            RedMelee.SetActive (true);
            BlueMelee.SetActive (true);
            bool p1hit = false;
            bool p2hit = false;
            if (Vector3.SqrMagnitude (player1t.position + DirLookup (m1.dir) - player2t.position) < 0.09f)
            {
                p2hit = true;
            }
            if (Vector3.SqrMagnitude (player2t.position + DirLookup (m2.dir) - player1t.position) < 0.09f)
            {
                p1hit = true;
            }

            if (p1hit)
            {
                StartCoroutine (Lerp (player1t, m2.dir, 3, 1));
                yield return new WaitForSeconds (1.22f);
            }
            if (p2hit)
            {
                StartCoroutine (Lerp (player2t, m1.dir, 3, 2));
                yield return new WaitForSeconds (1.22f);
            }
            if (!p1hit && !p2hit)
            {
                yield return new WaitForSeconds (1.22f);
            }

            RedMelee.SetActive (false);
            BlueMelee.SetActive (false);

        }
        else if (m1.type == MoveTypes.MELEE)
        {
            RedMelee.SetActive (true);
            if (Vector3.SqrMagnitude (player1t.position + DirLookup (m1.dir) - player2t.position) < 0.09f)
            {
                if (m2.type == MoveTypes.BLOCK)
                {
                    BlueBlock.SetActive (true);
                    StartCoroutine (Lerp (player1t, ReverseDir (m1.dir), 3, 1));
                    yield return new WaitForSeconds (1.22f);
                    BlueBlock.SetActive (false);
                }
                else
                {
                    StartCoroutine (Lerp (player2t, m1.dir, 3, 2));
                    yield return new WaitForSeconds (1.22f);
                }
            }
            else
            {
                yield return new WaitForSeconds (1.22f);
            }
            RedMelee.SetActive (false);
        }
        else if (m2.type == MoveTypes.MELEE)
        {
            BlueMelee.SetActive (true);
            if (Vector3.SqrMagnitude (player2t.position + DirLookup (m2.dir) - player1t.position) < 0.09f)
            {
                if (m1.type == MoveTypes.BLOCK)
                {
                    RedBlock.SetActive (true);
                    StartCoroutine (Lerp (player2t, ReverseDir (m2.dir), 3, 2));
                    yield return new WaitForSeconds (1.22f);
                    RedBlock.SetActive (false);
                }
                else
                {
                    StartCoroutine (Lerp (player1t, m2.dir, 3, 1));
                    yield return new WaitForSeconds (1.22f);
                }
            }
            else
            {
                yield return new WaitForSeconds (1.22f);
            }
            BlueMelee.SetActive (false);
        }
        else
        {
            if (m1.type == MoveTypes.RANGE)
                if (Vector3.SqrMagnitude (player1t.position + DirLookup (m1.dir) - player2t.position) < 0.09f)
                {
                    if (m2.type == MoveTypes.BLOCK)
                    {
                        BlueBlock.SetActive (true);
                        StartCoroutine (Lerp (player1t, ReverseDir (m1.dir), 1, 1));
                        yield return new WaitForSeconds (1.22f);
                        BlueBlock.SetActive (false);
                    }

                    // animating goes here*************************
                    StartCoroutine (Lerp (player2t, m1.dir, 1, 2));
                    yield return new WaitForSeconds (1.22f);
                }
            else
                Instantiate (RedRanged, (player1t.position + DirLookup (m1.dir)), Quaternion.Euler (0.0f, 0.0f, (float) m1.dir));
            if (m2.type == MoveTypes.RANGE)
                if (Vector3.SqrMagnitude (player2t.position + DirLookup (m2.dir) - player1t.position) < 0.09f)
                {
                    if (m1.type == MoveTypes.BLOCK)
                    {
                        RedBlock.SetActive (true);
                        StartCoroutine (Lerp (player2t, ReverseDir (m2.dir), 1, 2));
                        yield return new WaitForSeconds (1.22f);
                        RedBlock.SetActive (false);
                    }
                    // animating goes here*************************
                    StartCoroutine (Lerp (player1t, m2.dir, 1, 1));
                    yield return new WaitForSeconds (1.22f);
                }
            else
                Instantiate (BlueRanged, (player2t.position + DirLookup (m2.dir)), Quaternion.Euler (0.0f, 0.0f, (float) m2.dir));
        }


        if (pc1.Passthrough.Count > 0)
        {
            m1 = pc1.Passthrough[0];
            pc1.Passthrough.RemoveAt(0);
            m2 = pc2.Passthrough[0];
            pc2.Passthrough.RemoveAt(0);
            StartCoroutine(Execute());
        }
        else
        {
            tms1.StartTurn();
            tms2.StartTurn();
        }

        //Execute();
        yield return null;
    }

    // May be usefull to have the duration as parameter
    public IEnumerator Lerp (Transform t, Dirs d, int dist, int whichPlayer)
    {
        // May be required ///////////////////////////////////
        if (whichPlayer == 1)
            while (currentlyMoving1)
                yield return null;
        if (whichPlayer == 2)
            while (currentlyMoving2)
                yield return null;

        Vector3 startingPosition = t.position;
        if (whichPlayer == 1)
            currentlyMoving1 = true;
        if (whichPlayer == 2)
            currentlyMoving2 = true;
        float startingTime = Time.unscaledTime;
        float timeRemaining = staticObjects.moveTime;
        float scalar = 1.0f / timeRemaining;
        while (timeRemaining > 0.0f)
        {
            timeRemaining -= Time.unscaledTime - startingTime;
            if (timeRemaining < 0.0f)
                timeRemaining = 0.0f;
            float alpha = 1.0f - (timeRemaining * scalar);
            t.position = startingPosition + (DirLookup (d) * (float) dist * alpha);
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        if (whichPlayer == 1)
            currentlyMoving1 = false;
        if (whichPlayer == 2)
            currentlyMoving2 = false;
        for (int i = 0; i < movingProjs.Count; i++)
            movingProjs[i] = false;
    }

    IEnumerator Turn (Transform t, Dirs target, float step)
    {
        Quaternion qtarget = Quaternion.Euler (0, 0, (float) target);
        Quaternion rot = t.rotation;
        while (rot != qtarget)
        {
            rot = Quaternion.RotateTowards (rot, qtarget, step);
            t.rotation = rot;
            yield return null;
        }
    }
}