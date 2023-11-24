using TMPro;
using UnityEngine;

public sealed class VectorForce : BaseForce<Vector2>
{
	/// <inheritdoc />
	public override Vector2 Force
	{
		get => force;
		set
		{
			force = value;
			GetComponentInChildren<TextMeshProUGUI>().text = $"({value.x:0.#}, {value.y:0.#})";
		}
	}

	/// <inheritdoc />
	public override ForceKind Kind => ForceKind.Vector;
}