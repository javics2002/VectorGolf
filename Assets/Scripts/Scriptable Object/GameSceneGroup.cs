using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Game Scene Group")]
public sealed class GameSceneGroup : ScriptableObject
{
	/// <summary>
	/// Enum for the group ID. It can be None, Kinematic, Friction, or LeaningSlope.
	/// </summary>
	public enum GroupId
	{
		None,
		Kinematic,
		Friction,
		LeaningSlope
	}

	/// <summary>
	/// The name of the game scene group.
	/// </summary>
	public string Name;

	/// <summary>
	/// The ID of the game scene group, represented by the GroupId enum.
	/// </summary>
	public GroupId Id;

	/// <summary>
	/// The list of scenes that belong to this game scene group.
	/// </summary>
	public List<GameScene.Id> Scenes;
}