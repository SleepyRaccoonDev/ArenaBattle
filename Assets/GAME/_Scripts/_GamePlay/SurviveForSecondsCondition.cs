using System;
using UnityEngine;

public sealed class SurviveForSecondsCondition : IGameCondition
{
    public event Action Triggered;

    private TimerService _timerService;

    private float _seconds;

    public SurviveForSecondsCondition(MonoBehaviour context, float seconds)
    {
        _timerService = new TimerService(context);
        _seconds = seconds;
    }

    public void Activate()
    {
        _timerService.TimesUp += OnTriggered;
        _timerService.Start(_seconds);
    }

    private void OnTriggered() => Triggered?.Invoke();

    public void Dispose() => _timerService.TimesUp -= OnTriggered;
}