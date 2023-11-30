using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup panel;

    [SerializeField]
    private float _speed;

    public void OnClick()
    {
        StartCoroutine(MakePanelInvisible());
    }

    private IEnumerator MakePanelInvisible()
    {
        while (panel.alpha > 0.01)
        {
            panel.alpha = Mathf.Lerp(panel.alpha, 0, Time.deltaTime * _speed);
            yield return null;
        }

        panel.gameObject.SetActive(false);
    }
}
