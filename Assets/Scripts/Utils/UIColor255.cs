using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Represents a color with red, green, and blue components, each ranging from 0 to 255.
/// </summary>
internal class UIColor255
{
	/// <summary>
	/// The red component of the color.
	/// </summary>
	private int _red;

	/// <summary>
	/// The green component of the color.
	/// </summary>
	private int _green;

	/// <summary>
	/// The blue component of the color.
	/// </summary>
	private int _blue;

	/// <summary>
	/// The UI element to which the color is applied.
	/// </summary>
	[CanBeNull]
	private VisualElement _element;

	/// <summary>
	/// Gets or sets the red component of the color.
	/// </summary>
	public int Red
	{
		get => _red;
		set
		{
			_red = value;
			Update();
		}
	}

	/// <summary>
	/// Gets or sets the green component of the color.
	/// </summary>
	public int Green
	{
		get => _green;
		set
		{
			_green = value;
			Update();
		}
	}

	/// <summary>
	/// Gets or sets the blue component of the color.
	/// </summary>
	public int Blue
	{
		get => _blue;
		set
		{
			_blue = value;
			Update();
		}
	}

	/// <summary>
	/// Gets or sets the UI element to which the color is applied.
	/// </summary>
	[CanBeNull]
	public VisualElement Element
	{
		get => _element;
		set
		{
			_element = value;
			Update();
		}
	}

	/// <summary>
	/// Initializes a new instance of the UIColor255 class with the specified color.
	/// </summary>
	/// <param name="color">The color to initialize with.</param>
	public UIColor255(Color color)
	{
		Red = (int)(color.r * 255);
		Green = (int)(color.g * 255);
		Blue = (int)(color.b * 255);
	}

	/// <summary>
	/// Gets the color represented by this instance.
	/// </summary>
	public Color Color => new(Red / 255f, Green / 255f, Blue / 255f);

	/// <summary>
	/// Updates the background color of the UI element.
	/// </summary>
	private void Update()
	{
		if (Element is not null) Element.style.backgroundColor = Color;
	}
}