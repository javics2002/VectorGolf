using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Elements
{
	/// <summary>
	/// A custom IntegerField that enforces a minimum and maximum value.
	/// </summary>
	public class StrictIntegerField : IntegerField
	{
		/// <summary>
		/// The minimum value that this field can hold.
		/// </summary>
		public int Minimum { get; set; }

		/// <summary>
		/// The maximum value that this field can hold.
		/// </summary>
		public int Maximum { get; set; }

		public StrictIntegerField()
		{
			RegisterCallback<KeyDownEvent>(OnKeyDown, TrickleDown.TrickleDown);
		}

		private void OnKeyDown(KeyDownEvent evt)
		{
			switch (evt.keyCode)
			{
				case KeyCode.UpArrow:
					evt.PreventDefault();
					value = Math.Min(value + (evt.shiftKey ? 10 : 1), Maximum);
					break;
				case KeyCode.DownArrow:
					evt.PreventDefault();
					value = Math.Max(value - (evt.shiftKey ? 10 : 1), Minimum);
					break;
			}
		}

		/// <summary>
		/// Converts a string to an integer and clamps it between the minimum and maximum values.
		/// </summary>
		/// <param name="str">The string to convert.</param>
		/// <returns>The clamped integer.</returns>
		protected override int StringToValue(string str) => Math.Clamp(base.StringToValue(str), Minimum, Maximum);

		/// <summary>
		/// A UxmlFactory for creating <see cref="StrictIntegerField"/> instances.
		/// </summary>
		public new class UxmlFactory : UxmlFactory<StrictIntegerField, UxmlTraits>
		{
		}

		/// <summary>
		/// A UxmlTraits for adding custom attributes to <see cref="StrictIntegerField"/>.
		/// </summary>
		public new class UxmlTraits : IntegerField.UxmlTraits
		{
			private readonly UxmlIntAttributeDescription _minimum = new()
				{ name = "minimum", defaultValue = int.MinValue };

			private readonly UxmlIntAttributeDescription _maximum = new()
				{ name = "maximum", defaultValue = int.MaxValue };

			/// <summary>
			/// Initializes a <see cref="StrictIntegerField"/> from a UXML bag of attributes.
			/// </summary>
			/// <param name="element">The VisualElement to initialize.</param>
			/// <param name="attributes">The UXML bag of attributes.</param>
			/// <param name="cc">The UXML creation context.</param>
			public override void Init(VisualElement element, IUxmlAttributes attributes, CreationContext cc)
			{
				base.Init(element, attributes, cc);

				if (element is not StrictIntegerField field) return;
				field.Minimum = _minimum.GetValueFromBag(attributes, cc);
				field.Maximum = _maximum.GetValueFromBag(attributes, cc);
			}
		}
	}
}