using UnityEngine.UIElements;

namespace UI.Manipulators
{
	/// <summary>
	/// A class that intercepts and stops the propagation of mouse click events.
	/// This class is sealed and cannot be inherited.
	/// </summary>
	public sealed class ClickInterceptor : PointerManipulator
	{
		/// <summary>
		/// Registers the mouse down and mouse up event callbacks on the target.
		/// </summary>
		protected override void RegisterCallbacksOnTarget()
		{
			target.RegisterCallback<MouseDownEvent>(OnMouseDown);
			target.RegisterCallback<MouseUpEvent>(OnMouseUp);
		}

		/// <summary>
		/// Unregisters the mouse down and mouse up event callbacks from the target.
		/// </summary>
		protected override void UnregisterCallbacksFromTarget()
		{
			target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
			target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
		}

		/// <summary>
		/// Handles the mouse down event and stops its propagation.
		/// </summary>
		/// <param name="evt">The mouse down event.</param>
		private void OnMouseDown(MouseDownEvent evt)
		{
			evt.StopPropagation();
		}

		/// <summary>
		/// Handles the mouse up event and stops its propagation.
		/// </summary>
		/// <param name="evt">The mouse up event.</param>
		private void OnMouseUp(MouseUpEvent evt)
		{
			evt.StopPropagation();
		}
	}
}