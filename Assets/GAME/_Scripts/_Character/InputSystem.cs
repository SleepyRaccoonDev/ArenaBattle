using System;
using UnityEngine;

public class InputSystem
{
    private const string HorizontalKey = "Horizontal";
    private const string VerticalKey = "Vertical";

    public event Action DashPressed;

    private Camera _camera;

    private LayerMask _layerMask;

    public InputSystem(Camera camera, LayerMask layerMask)
    {
        _camera = camera;
        _layerMask = layerMask;
    }

    public Vector3 GetAxes()
    {
        float x = Input.GetAxisRaw(HorizontalKey);
        float y = Input.GetAxisRaw(VerticalKey);

        return new Vector3(x, 0, y);
    }

    public bool IsAnyKey()
    {
        return Input.anyKey;
    }

    public bool GetKeyDownMouseLeft()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    public Vector3 GetMousePosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);

        Physics.Raycast(ray, out RaycastHit hit, 100, _layerMask.value);

        return hit.point;
    }

    public void Poll()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            DashPressed?.Invoke();
    }
}