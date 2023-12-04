using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _panel;

    [SerializeField]
    private UIGame _uiGame;

    [SerializeField]
    private float _speed;

    private void Start()
    {
        _uiGame = GameObject.Find("Game UI").GetComponent<UIGame>();
        _uiGame.EnableUI(false);
        // Disable canvas to prevent player from using forces
        foreach (DraggableItem item in FindObjectsOfType<DraggableItem>())
        {
            item.canDrag = false;
        }
    }

    public void OnClick()
    {
        StartCoroutine(MakePanelInvisible());
    }

    private IEnumerator MakePanelInvisible()
    {
        while (_panel.alpha > 0.01)
        {
            _panel.alpha = Mathf.Lerp(_panel.alpha, 0, Time.deltaTime * _speed);
            yield return null;
        }

        // Disable canvas to prevent player from using forces
        foreach (DraggableItem item in FindObjectsOfType<DraggableItem>())
        {
            item.canDrag = true;
        }

        _uiGame.EnableUI(true);

        _panel.gameObject.SetActive(false);
    }
}
