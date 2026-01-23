using System;
using System.Collections;
using UnityEngine;

public abstract class Controller : IDisposable
{
    private MonoBehaviour _mono;

    private Coroutine _currentUpdateCoroutine;

    public Controller(MonoBehaviour mono)
    {
        _mono = mono;
    }

    public void Start()
    {
        if (_mono == null)
            return;

        Stop();

        _currentUpdateCoroutine = _mono.StartCoroutine(ControllingProcessInUpdate());
    }

    public void Stop()
    {
        if (_mono == null)
            return;

        if (_currentUpdateCoroutine != null)
        {
            _mono.StopCoroutine(_currentUpdateCoroutine);
            _currentUpdateCoroutine = null;
        }
    }

    private IEnumerator ControllingProcessInUpdate()
    {
        while (true)
        {
            ControlLogicInUpdate();
            yield return null;
        }    
    }

    protected abstract void ControlLogicInUpdate();

    public abstract void Dispose();
}