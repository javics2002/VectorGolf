using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Elements
{
	public class Grid<TElement> : VisualElement where TElement : VisualElement, new()
	{
		private int _columns;

		public int Columns
		{
			get => _columns;
			set => UpdateColumns(value);
		}

		public override VisualElement contentContainer => null;

		public Grid() : this(1)
		{
		}

		public Grid(int columns)
		{
			_columns = columns;
			usageHints = UsageHints.GroupTransform;

			// Load the stylesheet
			styleSheets.Add(Resources.Load<StyleSheet>("UI/Grid"));
			AddToClassList("grid");

			var percent = 100f / _columns;
			for (var i = 0; i < _columns; i++)
			{
				hierarchy.Add(new TElement { name = $"column-{i}", style = { width = Length.Percent(percent) } });
			}
		}

		public new void Insert(int index, VisualElement element)
		{
			if (index < 0 || index >= Columns)
			{
				throw new ArgumentOutOfRangeException(nameof(index));
			}

			hierarchy[index].Clear();
			hierarchy[index].Add(element);
		}

		private void UpdateColumns(int columns)
		{
			if (columns < 0) throw new ArgumentOutOfRangeException(nameof(columns));
			if (columns == _columns) return;

			if (columns > _columns)
			{
				for (var i = _columns; i < columns; i++)
				{
					hierarchy.Add(new VisualElement { name = $"column-{i}" });
				}
			}
			else
			{
				for (var i = _columns - 1; i >= columns; i--)
				{
					hierarchy.RemoveAt(i);
				}
			}

			_columns = columns;

			var percent = 100f / _columns;
			for (var i = 0; i < _columns; i++)
			{
				hierarchy[i].style.width = Length.Percent(percent);
			}
		}

		/// <summary>
		/// A UxmlFactory for creating <see cref="Grid"/> instances.
		/// </summary>
		public new class UxmlFactory : UxmlFactory<Grid<TElement>, UxmlTraits>
		{
		}

		/// <summary>
		/// A UxmlTraits for adding custom attributes to <see cref="Grid"/>.
		/// </summary>
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			private readonly UxmlIntAttributeDescription _title = new()
				{ name = "columns", defaultValue = 1 };

			/// <summary>
			/// Initializes a <see cref="Grid"/> from a UXML bag of attributes.
			/// </summary>
			/// <param name="element">The VisualElement to initialize.</param>
			/// <param name="attributes">The UXML bag of attributes.</param>
			/// <param name="cc">The UXML creation context.</param>
			public override void Init(VisualElement element, IUxmlAttributes attributes, CreationContext cc)
			{
				base.Init(element, attributes, cc);

				if (element is not Grid<TElement> entry) return;
				entry.Columns = _title.GetValueFromBag(attributes, cc);
			}
		}
	}
}