using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelsData")]
public class LevelData : ScriptableObject
{
    public List<float> scalars;
    public List<Vector2> vectors;
}
