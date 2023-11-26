using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

internal class UIColor255
{
	private int _red;
	private int _green;
	private int _blue;

	[CanBeNull]
	private VisualElement _element;

	public int Red
	{
		get => _red;
		set
		{
			_red = value;
			Update();
		}
	}

	public int Green
	{
		get => _green;
		set
		{
			_green = value;
			Update();
		}
	}

	public int Blue
	{
		get => _blue;
		set
		{
			_blue = value;
			Update();
		}
	}

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

	public UIColor255(Color color)
	{
		Red = (int)(color.r * 255);
		Green = (int)(color.g * 255);
		Blue = (int)(color.b * 255);
	}

	public Color Color => new(Red / 255f, Green / 255f, Blue / 255f);

	private void Update()
	{
		if (Element is not null) Element.style.backgroundColor = Color;
	}
}