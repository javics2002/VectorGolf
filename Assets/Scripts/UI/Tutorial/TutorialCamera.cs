using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TutorialCamera : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;

    private int _index;
    private Vector3 _initialCameraPosition;
    private float _initialCameraSize;

    private float _speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _initialCameraPosition = _mainCamera.transform.position;
        _initialCameraSize = _mainCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = _initialCameraPosition;
        float cameraSize = _initialCameraSize;

        switch(_index)
        {
            case 1: // FOCUSED ON BALL
                newPosition = new Vector3(14.5f, 6, -10);
                cameraSize = 2f;
                break;
            case 2: // FOCUSED ON FLAG
                newPosition = new Vector3(14.5f, 1.5f, -10);
                cameraSize = 3f;
                break;
            case 3: // FOCUSED ON CANVAS
                newPosition = new Vector3(25, 8.2f, -10);
                cameraSize = 3.5f;
                break;
            case 4: // START DRAGGING
            case 5:
                newPosition = new Vector3(20, 7, -10);
                cameraSize = 4.5f;
                break;
            case 6:
                newPosition = new Vector3(14.5f, 4.5f, -10);
                cameraSize = 4.8f;
                break;
        }

        ChangeCameraFocusPositionAndSize(newPosition, cameraSize);
    }

    public void ChangeCameraFocusPositionAndSize(Vector3 newPosition, float newSize)
    {
        _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, newSize, Time.deltaTime);
        _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, newPosition, Time.deltaTime);
    }

    public void UpdateTutorialCameraPosition()
    {
        _index++;
    }
}
