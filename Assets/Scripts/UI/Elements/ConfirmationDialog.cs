using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Elements
{
	public class ConfirmationDialog : ToggleableVisualElement
	{
		/// <summary>
		/// The label for displaying the dialog title
		/// </summary>
		private readonly Label _title;

		/// <summary>
		/// The label for displaying the dialog text
		/// </summary>
		private readonly Label _message;

		/// <summary>
		/// The button for cancelling the dialog
		/// </summary>
		private readonly Button _cancelButton;

		/// <summary>
		/// The button for confirming the dialog
		/// </summary>
		private readonly Button _confirmButton;

		/// <summary>
		/// The text to be displayed in the dialog's title field.
		/// </summary>
		public string Title
		{
			get => _title.text;
			set => _title.text = value;
		}

		/// <summary>
		/// The text to be displayed in the dialog's text field.
		/// </summary>
		public string Message
		{
			get => _message.text;
			set => _message.text = value;
		}

		/// <summary>
		/// The event to be invoked when the confirm button is clicked.
		/// </summary>
		public event Action OnConfirm
		{
			add => _confirmButton.clicked += value;
			remove => _confirmButton.clicked -= value;
		}

		/// <summary>
		/// The event to be invoked when the cancel button is clicked.
		/// </summary>
		public event Action OnCancel
		{
			add => _cancelButton.clicked += value;
			remove => _cancelButton.clicked -= value;
		}

		/// <summary>
		/// Constructor for the ConfirmationDialog class.
		/// </summary>
		public ConfirmationDialog()
		{
			// Load the stylesheet
			styleSheets.Add(Resources.Load<StyleSheet>("UI/ConfirmationDialog"));
			this.AddManipulator(new ClickInterceptor());

			// Add the styles to the elements
			AddToClassList("confirmation-dialog");

			_title = new Label { name = "header" };
			_title.AddToClassList("title");
			Add(_title);

			_message = new Label { name = "text" };
			_message.AddToClassList("dialog-text");
			Add(_message);

			var buttons = new GroupBox { name = "buttons" };
			buttons.AddToClassList("dialog-buttons");
			Add(buttons);

			_cancelButton = new Button { name = "cancel-button", text = "Cancel" };
			_cancelButton.AddToClassList("button");
			buttons.Add(_cancelButton);

			_confirmButton = new Button { name = "confirm-button", text = "Confirm" };
			_confirmButton.AddToClassList("button");
			_confirmButton.AddToClassList("danger");
			buttons.Add(_confirmButton);
		}

		/// <summary>
		/// A UxmlFactory for creating <see cref="ConfirmationDialog"/> instances.
		/// </summary>
		public new class UxmlFactory : UxmlFactory<ConfirmationDialog, UxmlTraits>
		{
		}

		/// <summary>
		/// A UxmlTraits for adding custom attributes to <see cref="ConfirmationDialog"/>.
		/// </summary>
		public new class UxmlTraits : ToggleableVisualElement.UxmlTraits
		{
			private readonly UxmlStringAttributeDescription _title = new()
				{ name = "title", defaultValue = "Warning" };

			private readonly UxmlStringAttributeDescription _message = new()
				{ name = "message", defaultValue = "This action is irreversible, are you sure you want to do this?" };

			/// <summary>
			/// Initializes a <see cref="ConfirmationDialog"/> from a UXML bag of attributes.
			/// </summary>
			/// <param name="element">The VisualElement to initialize.</param>
			/// <param name="attributes">The UXML bag of attributes.</param>
			/// <param name="cc">The UXML creation context.</param>
			public override void Init(VisualElement element, IUxmlAttributes attributes, CreationContext cc)
			{
				base.Init(element, attributes, cc);

				if (element is not ConfirmationDialog confirmationDialog) return;
				confirmationDialog.Title = _title.GetValueFromBag(attributes, cc);
				confirmationDialog.Message = _message.GetValueFromBag(attributes, cc);
			}
		}
	}
}