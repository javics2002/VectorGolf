using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueBox : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textComponent;

    [SerializeField]
    private Dialogue _tutorialDialogue;

    [SerializeField]
    private TutorialCamera _tutorialCamera;

    [SerializeField]
    private CursorAnimation _tutorialCursor;

    public int dialogueIndex;

    public bool _textCompleted = false;

    void Start()
    {
        // Disable canvas to prevent player from using forces
        foreach (DraggableItem item in FindObjectsOfType<DraggableItem>())
        {
            item.canDrag = false;
        }

        _textComponent.text = "";
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_textComponent.text == _tutorialDialogue.lines[dialogueIndex])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                _textComponent.text = _tutorialDialogue.lines[dialogueIndex];
                _textCompleted = true;
            }
        }   
    }

    private void StartDialogue()
    {
        dialogueIndex = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in _tutorialDialogue.lines[dialogueIndex].ToCharArray())
        {
            _textComponent.text += c;
            yield return new WaitForSeconds(_tutorialDialogue.textSpeed);
        }
    }

    public void NextLine()
    {
        if (dialogueIndex < _tutorialDialogue.lines.Length - 1) {
            dialogueIndex++;
            _textComponent.text = "";

            _tutorialCamera.UpdateTutorialCameraPosition();
            _tutorialCursor.UpdateCursorTutorialPosition();

            // test
            if (dialogueIndex == 5)
                _tutorialCursor.StartDragging();

            _textCompleted = false;

            StartCoroutine(TypeLine());
        }
        else
        {
            GameManager.Instance.firstTimeEnteringLevel = false;
            GameManager.Instance.ChangeScene(GameScene.Id.Level4);
        }
    }
}
