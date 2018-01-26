using System.Collections;

public enum Dirs
{
	N = 0,
	NE = 45,
	E = 90,
	SE = 135,
	S = 180,
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

public struct Move
{
	public string Type;
	public float Dir;
}