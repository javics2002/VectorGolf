/// <summary>
/// Utility class to handle the current level.
/// </summary>
public sealed record GameLevel
{
	/// <summary>
	/// The 1-based level index.
	/// </summary>
	public int Current { get; internal set; }

	/// <summary>
	/// The 0-based level index.
	/// </summary>
	public int CurrentIndex => IsValid() ? Current - 1 : -1;

	/// <summary>
	/// Checks whether or not the current level is the last one.
	/// </summary>
	public bool IsLast() => Current == GameManager.NumberOfLevels;

	/// <summary>
	/// Checks whether or not there is a next level.
	/// </summary>
	public bool HasNext() => IsValid() && !IsLast();

	/// <summary>
	/// Returns whether or not the current level is valid.
	/// </summary>
	/// <returns></returns>
	public bool IsValid() => Current is > 0 and <= GameManager.NumberOfLevels;

	public static GameLevel Invalid => new() { Current = -1 };
}