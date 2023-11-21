using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelsData")]
public class LevelData : ScriptableObject
{
	[FormerlySerializedAs("scalars")]
	public List<float> Scalars;

	[FormerlySerializedAs("vectors")]
	public List<Vector2> Vectors;

	public int Platinum;
	public int Gold;
}