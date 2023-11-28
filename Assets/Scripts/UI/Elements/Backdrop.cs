using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Elements
{
	public class Backdrop : ToggleableVisualElement
	{
		/// <summary>
		/// The clickable mouse manipulator for this backdrop.
		/// </summary>
		private readonly Clickable _clickable;

		/// <summary>
		/// The event to be invoked when the confirm button is clicked.
		/// </summary>
		public event Action OnClick
		{
			add => _clickable.clicked += value;
			remove => _clickable.clicked -= value;
		}

		public Backdrop() : this(null)
		{
		}

		public Backdrop(Action clickEvent)
		{
			_clickable = new Clickable(clickEvent);
			this.AddManipulator(_clickable);

			// Load the stylesheet
			styleSheets.Add(Resources.Load<StyleSheet>("UI/Backdrop"));

			// Add the styles to the elements
			AddToClassList("backdrop");
		}

		/// <summary>
		/// A UxmlFactory for creating <see cref="Backdrop"/> instances.
		/// </summary>
		public new class UxmlFactory : UxmlFactory<Backdrop, UxmlTraits>
		{
		}
	}
}