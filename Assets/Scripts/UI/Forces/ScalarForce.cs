using TMPro;

public sealed class ScalarForce : BaseForce<float>
{
	/// <inheritdoc />
	public override float Force
	{
		get => force;
		set
		{
			force = value;
			GetComponentInChildren<TextMeshProUGUI>().text = $"{value:0.#}";
		}
	}

	/// <inheritdoc />
	public override ForceKind Kind => ForceKind.Scalar;
}