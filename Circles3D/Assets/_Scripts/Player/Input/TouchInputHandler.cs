using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInputHandler : MonoBehaviour
{
    public static TouchInputHandler Instance;

    private PlayerControls _playerControls;
    private InputAction _touchPosition;
    private InputAction _screenTouch;

    public Action<bool> OnDirectionChanged;
    public Action OnDoubleTap;

    public enum MoveDirection
    {
        ToCenter,
        FromCenter
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.TouchControls.Enable();
        _touchPosition = _playerControls.TouchControls.ScreenTouchPosition;
        _screenTouch = _playerControls.TouchControls.ScreenTouch;

        _screenTouch.started += MoveToCenter;
        _screenTouch.canceled += MoveFromCenter;

        _playerControls.TouchControls.DoubleTap.performed += ((InputAction.CallbackContext obj) => { OnDoubleTap?.Invoke(); });
    }

    private void MoveToCenter(InputAction.CallbackContext obj)
    {
        OnDirectionChanged.Invoke(true);//Как-то нелогично, что отправляем бул и небул
    }

    private void MoveFromCenter(InputAction.CallbackContext obj)
    {
        OnDirectionChanged.Invoke(false);
    }

    private void OnDisable()
    {
        _playerControls.TouchControls.Disable();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //if (_screenTouch.IsInProgress())
        //{
        //    if (_touchPosition.ReadValue<Vector2>().x < Screen.width / 2)
        //    {
        //        Debug.Log("Лево");
        //    }
        //    else
        //    {
        //        Debug.Log("Право");
        //    }
        //}
    }
}