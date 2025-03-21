using UnityEngine;
using System;

public class InputHandler
{
    private const string Fire1 = "Fire1";
    
    public event Action OnUpTouch;
    public event Action OnDownTouch;
    public event Action OnTouch;

    public void UpdateHandler()
    {
        if (Input.GetButtonDown(Fire1))
        {
            OnDownTouch?.Invoke();
        }

        if (Input.GetButtonUp(Fire1))
        {
            OnUpTouch?.Invoke();
        }

        if (Input.GetButton(Fire1))
        {
            OnTouch?.Invoke();
        }
    }
}
