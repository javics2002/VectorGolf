using UnityEngine.UIElements;

/// <summary>
/// A custom VisualElement that can be enabled or disabled.
/// </summary>
public class ToggleableVisualElement : VisualElement
{
	private bool _enabled = true;

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="ToggleableVisualElement"/> is enabled.
	/// </summary>
	/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
	public bool Enabled
	{
		get => _enabled;
		set
		{
			_enabled = value;
			SetEnabled(value);
		}
	}

	/// <summary>
	/// A UxmlFactory for creating <see cref="ToggleableVisualElement"/> instances.
	/// </summary>
	public new class UxmlFactory : UxmlFactory<ToggleableVisualElement, UxmlTraits>
	{
	}

	/// <summary>
	/// A UxmlTraits for adding custom attributes to <see cref="ToggleableVisualElement"/>.
	/// </summary>
	public new class UxmlTraits : VisualElement.UxmlTraits
	{
		private readonly UxmlBoolAttributeDescription _enabled = new() { name = "enabled", defaultValue = true };

		/// <summary>
		/// Initializes a <see cref="ToggleableVisualElement"/> from a UXML bag of attributes.
		/// </summary>
		/// <param name="element">The VisualElement to initialize.</param>
		/// <param name="attributes">The UXML bag of attributes.</param>
		/// <param name="cc">The UXML creation context.</param>
		public override void Init(VisualElement element, IUxmlAttributes attributes, CreationContext cc)
		{
			base.Init(element, attributes, cc);

			if (element is ToggleableVisualElement toggleableElement)
			{
				toggleableElement.Enabled = _enabled.GetValueFromBag(attributes, cc);
			}
		}
	}
}