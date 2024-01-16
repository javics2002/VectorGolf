using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Elements
{
	public sealed class LevelSelectionGroupEntry : Button
	{
		private const string FlagClassNameCompleted = "red";
		private const string FlagClassNamePar = "gold";
		private const string FlagClassNameHoleInOne = "platinum";

		private static readonly string[] FlagClassNames =
			{ FlagClassNameCompleted, FlagClassNamePar, FlagClassNameHoleInOne };

		private static readonly Sprite[] LevelSprites = new Sprite[GameManager.NumberOfLevels];

		static LevelSelectionGroupEntry()
		{
			for (var i = 0; i < GameManager.NumberOfLevels; i++)
			{
				LevelSprites[i] = Resources.Load<Sprite>($"Levels/Level {i + 1}");
			}
		}

		private int _level = 1;
		private GameManager.LevelCompletionStatus _status = GameManager.LevelCompletionStatus.Locked;

		public int Level
		{
			get => _level;
			private set
			{
				_level = value;
				UpdateLevel();
			}
		}

		public int Index => Level - 1;

		public GameManager.LevelCompletionStatus Status
		{
			get => _status;
			private set
			{
				_status = value;
				UpdateStatus();
			}
		}

		private readonly VisualElement _thumbnail;
		private readonly VisualElement _flagContainer;
		private readonly VisualElement _flagImage;
		private readonly Label _numberLabel;

		public LevelSelectionGroupEntry()
		{
			// Load the stylesheets
			styleSheets.Add(Resources.Load<StyleSheet>("UI/LevelSelectionGroupEntry"));
			styleSheets.Add(Resources.Load<StyleSheet>("UI/Shared"));

			// Add the styles to the elements
			AddToClassList("level-button");
			AddToClassList("animate-scale");

			_thumbnail = new VisualElement
				{ name = "thumbnail", style = { backgroundImage = LevelSprites[Index].texture } };
			_thumbnail.AddToClassList("button-icon");
			_thumbnail.AddToClassList("level-thumbnail");
			Add(_thumbnail);

			_flagContainer = new VisualElement { name = "flag-fg" };
			_flagContainer.AddToClassList("level-flag-container");
			Add(_flagContainer);

			_flagImage = new VisualElement { name = "image" };
			_flagImage.AddToClassList("level-flag-image");
			_flagContainer.Add(_flagImage);

			_numberLabel = new Label { name = "number", text = Level.ToString() };
			_numberLabel.AddToClassList("level-number");
			Add(_numberLabel);

			clicked += OnLevelSelected();
		}

		public LevelSelectionGroupEntry(int level, GameManager.LevelCompletionStatus status) : this()
		{
			Level = level;
			Status = status;
		}

		private Action OnLevelSelected()
		{
			var gm = GameManager.Instance;
			if (gm is null) return () => { };

			return gm.firstTimeEnteringLevel && GameScene.Level(Level) == GameScene.Id.Level4
                ? () => gm.ChangeScene(GameScene.LevelTutorial())
				: () => gm.ChangeScene(GameScene.Level(Level));
		}

		private void UpdateLevel()
		{
			name = $"level-{_level}";
			_numberLabel.text = _level.ToString();
			_thumbnail.style.backgroundImage = LevelSprites[_level - 1].texture;
		}

		private void UpdateStatus()
		{
			var (enabled, className) = Status switch
			{
				GameManager.LevelCompletionStatus.Locked => (false, null),
				GameManager.LevelCompletionStatus.Uncompleted => (true, null),
				GameManager.LevelCompletionStatus.Completed => (true, FlagClassNameCompleted),
				GameManager.LevelCompletionStatus.Par => (true, FlagClassNamePar),
				GameManager.LevelCompletionStatus.HoleInOne => (true, FlagClassNameHoleInOne),
				_ => throw new ArgumentOutOfRangeException(nameof(Status), Status, "Invalid level status")
			};

			SetEnabled(enabled);
			foreach (var flagClassName in FlagClassNames)
			{
				if (flagClassName == className) AddToClassList(flagClassName);
				else RemoveFromClassList(flagClassName);
			}
		}

		/// <summary>
		/// A UxmlFactory for creating <see cref="LevelSelectionGroupEntry"/> instances.
		/// </summary>
		public new class UxmlFactory : UxmlFactory<LevelSelectionGroupEntry, UxmlTraits>
		{
		}

		/// <summary>
		/// A UxmlTraits for adding custom attributes to <see cref="LevelSelectionGroupEntry"/>.
		/// </summary>
		public new class UxmlTraits : Button.UxmlTraits
		{
			private readonly UxmlIntAttributeDescription _level = new()
				{ name = "level", defaultValue = 1 };

			private readonly UxmlEnumAttributeDescription<GameManager.LevelCompletionStatus> _status = new()
				{ name = "status", defaultValue = GameManager.LevelCompletionStatus.Locked };

			/// <summary>
			/// Initializes a <see cref="LevelSelectionGroupEntry"/> from a UXML bag of attributes.
			/// </summary>
			/// <param name="element">The VisualElement to initialize.</param>
			/// <param name="attributes">The UXML bag of attributes.</param>
			/// <param name="cc">The UXML creation context.</param>
			public override void Init(VisualElement element, IUxmlAttributes attributes, CreationContext cc)
			{
				base.Init(element, attributes, cc);

				if (element is not LevelSelectionGroupEntry entry) return;
				entry.Level = _level.GetValueFromBag(attributes, cc);
				entry.Status = _status.GetValueFromBag(attributes, cc);
			}
		}
	}
}