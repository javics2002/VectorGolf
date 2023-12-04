using UnityEngine.UIElements;

public static class VisualElementExtensions
{
	public static void RemoveFromClassList(this VisualElement element, params string[] classNames)
	{
		foreach (var className in classNames)
		{
			element.RemoveFromClassList(className);
		}
	}
}