using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderForce : MonoBehaviour {
	TextMeshProUGUI targetName, vectorValue, xValue, yValue;

	Slider xSlider, ySlider;

	[SerializeField]
	GameObject target;
	[SerializeField]
	KinematicArrow.ArrowProperties arrowProperties;
	[SerializeField]
	Material arrowMaterial;

	InterfaceArrow sliderArrow;
	Rigidbody2D targetRigidbody;

	private void Start() {
		TextMeshProUGUI[] textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();

		targetName = textMeshPros[0];
		vectorValue = textMeshPros[1];
		xValue = textMeshPros[3];
		yValue = textMeshPros[5];

		Slider[] sliders = GetComponentsInChildren<Slider>();
		xSlider = sliders[0];
		ySlider = sliders[1];

		targetName.text = Translate(target.name);

		xSlider.onValueChanged.AddListener(delegate { UpdateTexts(); });
		ySlider.onValueChanged.AddListener(delegate { UpdateTexts(); });

		sliderArrow = KinematicArrow.CreateArrow<InterfaceArrow>(targetName.text, 
			target.transform, arrowProperties, arrowMaterial);
		sliderArrow.decompositeThreshold = 0.1f;
		targetRigidbody = target.GetComponent<Rigidbody2D>();
	}

	private void Update() {
		Vector3 playerVelocity = new Vector2(xSlider.value, ySlider.value);

		sliderArrow.SetInterfaceArrow(playerVelocity);

		targetRigidbody.velocity = playerVelocity;
	}

	void UpdateTexts() {
		string xText = xSlider.value.ToString("0.#");
		string yText = ySlider.value.ToString("0.#");

		xValue.text = xText;
		yValue.text = yText;

		vectorValue.text = string.Format("({0}, {1})", xText, yText);
	}

	string Translate(string text) => text.ToLower() switch {
		"ball" => "Pelota",
		"box" => "Caja",
		_ => text
	};
}
