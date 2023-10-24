using UnityEngine;

[CreateAssetMenu(fileName = "SettingsData", menuName = "ScriptableObjects/SettingsData")]
public class SettingsData : ScriptableObject
{
    public float musicVolume;
    public float soundVolume;

    public Color ballColor;
}
