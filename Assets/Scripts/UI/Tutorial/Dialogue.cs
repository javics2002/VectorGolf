using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/DialogueData")]
public class Dialogue : ScriptableObject
{
    [TextArea(5, 5)]
    public string[] lines;

    public float textSpeed;
}
