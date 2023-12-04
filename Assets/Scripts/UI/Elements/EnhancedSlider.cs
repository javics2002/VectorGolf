using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Elements
{
	/// <summary>
	/// A custom Slider that responds to more keyboard events.
	/// </summary>
	public class EnhancedSlider : Slider
	{
		public EnhancedSlider()
		{
			RegisterCallback<KeyDownEvent>(OnKeyDown, TrickleDown.TrickleDown);
		}

		/// <summary>
		/// Handles KeyDownEvent. If any arrow key is pressed, it adjusts the value of the slider.
		/// The direction of the slider and whether it is inverted or not determines which keys are used.
		/// </summary>
		/// <param name="evt">The KeyDownEvent to handle.</param>
		private void OnKeyDown(KeyDownEvent evt)
		{
			// Determine the keys to use for decreasing and increasing the slider value based on the slider's direction
			// and whether it is inverted
			var (previousKey, nextKey) = (direction, inverted) switch
			{
				(SliderDirection.Horizontal, false) => (KeyCode.LeftArrow, KeyCode.RightArrow),
				(SliderDirection.Horizontal, true) => (KeyCode.RightArrow, KeyCode.LeftArrow),
				(SliderDirection.Vertical, false) => (KeyCode.DownArrow, KeyCode.UpArrow),
				(SliderDirection.Vertical, true) => (KeyCode.UpArrow, KeyCode.DownArrow),
				_ => throw new ArgumentOutOfRangeException()
			};

			// If the pressed key is not one of the determined keys, exit the method
			if (evt.keyCode != previousKey && evt.keyCode != nextKey) return;

			var step = evt.shiftKey ? range * 0.1f : range * 0.01f;
			value = Mathf.Clamp(value + (evt.keyCode == previousKey ? -step : step), lowValue, highValue);
			evt.PreventDefault();
		}

		/// <summary>
		/// A UxmlFactory for creating <see cref="EnhancedSlider"/> instances.
		/// </summary>
		public new class UxmlFactory : UxmlFactory<EnhancedSlider, UxmlTraits>
		{
		}
	}
}