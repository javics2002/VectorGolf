/// <summary>
/// A utility class to handle the current game level.
/// </summary>
public sealed record GameLevel
{
	/// <summary>
	/// Gets or sets the 1-based index of the current level.
	/// </summary>
	public int Current { get; internal set; }

	/// <summary>
	/// Gets the 0-based index of the current level.
	/// </summary>
	public int CurrentIndex => IsValid() ? Current - 1 : -1;

	/// <summary>
	/// Checks whether the current level is the last one.
	/// </summary>
	/// <returns><c>true</c> if the current level is the last one, <c>false</c> otherwise.</returns>
	public bool IsLast() => Current == GameManager.NumberOfLevels;

	/// <summary>
	/// Checks whether there is a next level.
	/// </summary>
	/// <returns><c>true</c> if there is a next level, <c>false</c> otherwise.</returns>
	public bool HasNext() => IsValid() && !IsLast();

	/// <summary>
	/// Checks whether the current level is valid.
	/// </summary>
	/// <returns><c>true</c> if the current level is valid, <c>false</c> otherwise.</returns>
	public bool IsValid() => Current is > 0 and <= GameManager.NumberOfLevels;

	/// <summary>
	/// Gets an instance of GameLevel that represents an invalid level.
	/// </summary>
	public static GameLevel Invalid => new() { Current = -1 };
}