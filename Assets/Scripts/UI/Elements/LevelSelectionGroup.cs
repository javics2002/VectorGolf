using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Elements
{
	public sealed class LevelSelectionGroup : VisualElement
	{
		public static readonly GameSceneGroup[] GameSceneGroups =
		{
			Resources.Load<GameSceneGroup>("Data/Vector"),
            Resources.Load<GameSceneGroup>("Data/Kinematic"),
			Resources.Load<GameSceneGroup>("Data/Friction"),
			Resources.Load<GameSceneGroup>("Data/LeaningSlope")
		};

		public const int Columns = 3;

		public const float PanelAspectRatio = 16f / 9f;
		public const float GridAspectRatio = PanelAspectRatio * Columns;

		private GameSceneGroup.GroupId _index = GameSceneGroup.GroupId.None;

		public GameSceneGroup.GroupId Index
		{
			get => _index;
			set => SetIndex(value);
		}

		private string _title;
		private Label _labelTitle;

		public string Title
		{
			get => _title;
			private set => SetTitle(value);
		}

		public int Size { get; private set; }

		public override VisualElement contentContainer => null;

		public LevelSelectionGroup() : this(GameSceneGroup.GroupId.None)
		{
		}

		public LevelSelectionGroup(GameSceneGroup.GroupId index)
		{
			usageHints = UsageHints.GroupTransform;

			// Load the stylesheet
			styleSheets.Add(Resources.Load<StyleSheet>("UI/LevelSelectionGroup"));
			styleSheets.Add(Resources.Load<StyleSheet>("UI/Shared"));
			AddToClassList("level-root");

			_labelTitle = new Label { name = "title" };
			_labelTitle.AddToClassList("title");
			hierarchy.Add(_labelTitle);

			RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);

			SetIndex(index);
		}

		private void SetIndex(GameSceneGroup.GroupId index)
		{
			if (_index == index) return;
			_index = index;

			// Remove all the entries
			while (hierarchy.childCount > 1)
			{
				hierarchy.RemoveAt(1);
			}

			Size = 0;

			if (index == GameSceneGroup.GroupId.None) return;

			var group = GameSceneGroups[(int)index - 1];
			Title = group.Name;

			var progress = GameManager.Instance is not null ? GameManager.Instance.progress : null;
			foreach (var id in group.Scenes)
			{
				var levelId = GameScene.Level(id);
				var status = progress?[levelId - 1].Status ?? GameManager.LevelCompletionStatus.Uncompleted;
				AddEntry(new LevelSelectionGroupEntry(levelId, status));
			}
		}

		public void AddEntry(LevelSelectionGroupEntry entry)
		{
			Grid<VisualElement> container;
			var gridIndex = Size % Columns;
			if (gridIndex == 0)
			{
				container = new Grid<VisualElement>(Columns) { name = $"container-{Size / Columns}" };
				container.AddToClassList("level-row");
				hierarchy.Add(container);
			}
			else
			{
				container = (Grid<VisualElement>)hierarchy[hierarchy.childCount - 1];
			}

			container.Insert(gridIndex, entry);
			Size++;
		}

		private void SetTitle(string title)
		{
			_title = title;
			_labelTitle.text = title;
		}

		private void OnGeometryChanged(GeometryChangedEvent evt)
		{
			var width = evt.newRect.width - 40f;
			var height = width / GridAspectRatio;
			foreach (var grid in hierarchy.Children().Skip(1))
			{
				grid.style.height = height;
			}
		}

		/// <summary>
		/// A UxmlFactory for creating <see cref="LevelSelectionGroup"/> instances.
		/// </summary>
		public new class UxmlFactory : UxmlFactory<LevelSelectionGroup, UxmlTraits>
		{
		}

		/// <summary>
		/// A UxmlTraits for adding custom attributes to <see cref="LevelSelectionGroup"/>.
		/// </summary>
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			private readonly UxmlEnumAttributeDescription<GameSceneGroup.GroupId> _group = new()
				{ name = "group", defaultValue = GameSceneGroup.GroupId.Kinematic };

			/// <summary>
			/// Initializes a <see cref="LevelSelectionGroup"/> from a UXML bag of attributes.
			/// </summary>
			/// <param name="element">The VisualElement to initialize.</param>
			/// <param name="attributes">The UXML bag of attributes.</param>
			/// <param name="cc">The UXML creation context.</param>
			public override void Init(VisualElement element, IUxmlAttributes attributes, CreationContext cc)
			{
				base.Init(element, attributes, cc);

				if (element is not LevelSelectionGroup entry) return;
				entry.Index = _group.GetValueFromBag(attributes, cc);
			}
		}
	}
}