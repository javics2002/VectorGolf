using System;
using UnityEngine;
using UnityEngine.Assertions;
using static System.IO.File;

[RequireComponent(typeof(Camera))]
public class RenderLevel : MonoBehaviour
{
	public float timeToTakePicture = 1f;

	private enum SaveTextureFileFormat
	{
		EXR,
		JPG,
		PNG,
		TGA
	};

	private void Start()
	{
#if UNITY_EDITOR
		Invoke(nameof(SaveLevelImage), timeToTakePicture);
#else
		Destroy(gameObject);
#endif
	}

	private void SaveLevelImage()
	{
		var level = GameManager.Instance.Level;
		if (level.IsValid())
		{
			var camera = GetComponent<Camera>();
			Assert.IsNotNull(camera);

			SaveRenderTextureToFile(camera.targetTexture, $"Assets/Resources/Levels/Level {level.Current}");
		}

		Destroy(gameObject);
	}

	/// <summary>
	/// Saves a Texture2D to disk with the specified filename and image format
	/// </summary>
	/// <param name="tex"></param>
	/// <param name="filePath"></param>
	/// <param name="fileFormat"></param>
	/// <param name="jpgQuality"></param>
	private void SaveTexture2DToFile(Texture2D tex, string filePath, SaveTextureFileFormat fileFormat,
		int jpgQuality = 95)
	{
		switch (fileFormat)
		{
			case SaveTextureFileFormat.EXR:
				WriteAllBytes($"{filePath}.exr", tex.EncodeToEXR());
				break;
			case SaveTextureFileFormat.JPG:
				WriteAllBytes($"{filePath}.jpg", tex.EncodeToJPG(jpgQuality));
				break;
			case SaveTextureFileFormat.PNG:
				WriteAllBytes($"{filePath}.png", tex.EncodeToPNG());
				break;
			case SaveTextureFileFormat.TGA:
				WriteAllBytes($"{filePath}.tga", tex.EncodeToTGA());
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(fileFormat), fileFormat, null);
		}
	}

	/// <summary>
	/// Saves a RenderTexture to disk with the specified filename and image format
	/// </summary>
	/// <param name="renderTexture"></param>
	/// <param name="filePath"></param>
	/// <param name="fileFormat"></param>
	/// <param name="jpgQuality"></param>
	private void SaveRenderTextureToFile(RenderTexture renderTexture, string filePath,
		SaveTextureFileFormat fileFormat = SaveTextureFileFormat.PNG, int jpgQuality = 95)
	{
		var texture = fileFormat != SaveTextureFileFormat.EXR
			? new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false, false)
			: new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBAFloat, false, true);

		var oldRt = RenderTexture.active;
		RenderTexture.active = renderTexture;
		texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
		texture.Apply();
		RenderTexture.active = oldRt;
		SaveTexture2DToFile(texture, filePath, fileFormat, jpgQuality);
		if (Application.isPlaying)
			Destroy(texture);
		else
			DestroyImmediate(texture);
	}
}