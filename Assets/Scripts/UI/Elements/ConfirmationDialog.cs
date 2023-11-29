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
		public event Action OnConfirm;

		/// <summary>
		/// The event to be invoked when the cancel button is clicked.
		/// </summary>
		public event Action OnCancel;

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

			var cancelButton = new Button(() => OnCancel?.Invoke()) { name = "cancel-button", text = "Cancel" };
			cancelButton.AddToClassList("button");
			buttons.Add(cancelButton);

			var confirmButton = new Button(() => OnConfirm?.Invoke()) { name = "confirm-button", text = "Confirm" };
			confirmButton.AddToClassList("button");
			confirmButton.AddToClassList("danger");
			buttons.Add(confirmButton);
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