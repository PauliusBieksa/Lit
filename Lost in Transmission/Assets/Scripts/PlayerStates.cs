using System.Collections;
using UnityEngine;

public enum Dirs
{
	N = 0,
	NE = -45,
	E = -90,
	SE = -135,
	S = -180,
	SW = 225,
	W = 270,
	NW = 315
}

public enum MoveTypes
{
	MOVE,
	BLOCK,
	MELEE,
	RANGE
};

public enum Button
{
	A,
	B,
	X,
	Y
};

public struct Move
{
	public MoveTypes type;
	public Dirs dir;
}

public class staticObjects
{
    public static int[] cooldowns = { 0, 1, 3, 2, 4, 0 };
}