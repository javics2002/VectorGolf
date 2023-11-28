using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAnimation : MonoBehaviour
{
    public float speed;
    public float amplitude;

    public Vector3 _position;
    public Vector3 _direction;

    private Sprite defaultSprite;
    public Sprite dragSprite;

    private Transform scalarTransform;
    public Transform ballTransform;

    public DialogueBox dialogueBox;
    private Vector3 destScalarPosition;

    private Vector3 _initialCursorPosition;
    private Vector3 _initialCursorDirection;

    private int _index = 0;

    private SpriteRenderer _cursorSprite;
    private float _showSpeed = 1.5f;
    private float _dragSpeed = 0.5f;

    bool _isDragging = false;

    void Start()
    {
        _cursorSprite = GetComponent<SpriteRenderer>();
        defaultSprite = _cursorSprite.sprite;

        Color color = _cursorSprite.color;
        color.a = 0f;
        _cursorSprite.color = color;

        _initialCursorPosition = _position;
        _initialCursorDirection = _direction;

        destScalarPosition = ballTransform.position;
    }

    void Update()
    {
        Vector3 newPosition = _initialCursorPosition;
        Vector3 newDirection = _initialCursorDirection;

        switch (_index)
        {
            case 1: // FOCUS ON BALL
                newPosition = new Vector3(15, 5.75f, 0);
                newDirection = new Vector3(-1, 1, 0);
                break;
            case 2: // FOCUS ON FLAG
                newPosition = new Vector3(15.5f, 2.5f, 0);
                newDirection = new Vector3(-1, 1, 0);
                break;
            case 3: // FOCUS ON CANVAS
                
                break;
            case 4:
                newPosition = new Vector3(25, 8.7f, 0);
                newDirection = new Vector3(-1, 1, 0);
                break;
            case 5: // START DRAG
                UpdateScalarPosition();
                newPosition = destScalarPosition + new Vector3(.8f, -.5f, 0); // offset
                newDirection = new Vector3(-1, 1, 0);

                if (Vector3.Distance(newPosition, transform.position) < 2f)
                {
                    dialogueBox.NextLine();
                    StopDragging();
                    return;
                }

                break;
            case 6: // STOP DRAGGING
                scalarTransform.GetComponent<CanvasGroup>().alpha = 0f;

                break;
            case 7:

                break;
        } 

        UpdateCursor(newPosition, newDirection);
    }

    private void UpdateCursor(Vector3 position, Vector3 direction)
    {
        // Play animation
        if (!_isDragging)
        {
            // Calcula el desplazamiento en funci�n del tiempo y la amplitud
            float offset = amplitude * Mathf.Sin(Time.time * speed);

            // Aplica el desplazamiento en la direcci�n diagonal
            Vector3 offsetVector = direction.normalized * offset;
            transform.position = position + offsetVector;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * _dragSpeed);
        }
       
        // Show opacity
        Color spriteColor = _cursorSprite.color;

        float currentAlpha = spriteColor.a;
        float newAlpha = Mathf.Lerp(currentAlpha, 1f, Time.deltaTime * _showSpeed);

        spriteColor.a = newAlpha;

        _cursorSprite.color = spriteColor;
    }

    private void UpdateScalarPosition()
    {
        scalarTransform.position = Vector3.Lerp(scalarTransform.position, destScalarPosition, Time.deltaTime * _dragSpeed);
    }

    public void UpdateCursorTutorialPosition()
    {
        _index++;

        scalarTransform = GameObject.Find("VectorForce(Clone)").transform;

        if (_index < 5)
        {
            Color spriteColor = _cursorSprite.color;
            spriteColor.a = 0f;
            _cursorSprite.color = spriteColor;
        }
        else if (_index == 6)
        {
            StopDragging();

            // SHOW PREVIEW ARROW
            ballTransform.GetComponentInChildren<Ball>().ShowTutorialArrow(scalarTransform.GetComponent<VectorForce>().Force);
        }
        else if (_index == 7)
        {
            StartCoroutine(ballTransform.GetComponentInChildren<Ball>().Hit(scalarTransform.GetComponent<VectorForce>().Force));
        }
    }

    public void StartDragging()
    {
        _isDragging = true;
        _cursorSprite.sprite = dragSprite;
    }

    public void StopDragging()
    {
        _isDragging = false;
        _cursorSprite.sprite = defaultSprite;
    }
}
