using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomMoveController : Controller, IDisposable
{
    private TimerService _timer;

    private Vector3 _direction;

    private EnemyConfig _enemyConfig;

    private IMovable _movable;
    private IRotatable _rotatable;
    private IDashable _dashable;
    private IGravitable _gravitable;

    public RandomMoveController(
        MonoBehaviour mono,
        IMovable movable,
        IRotatable rotatable,
        IDashable dashable,
        IGravitable gravitable,
        EnemyConfig enemyConfig) : base(mono)
    {
        _movable = movable;
        _rotatable = rotatable;
        _dashable = dashable;
        _gravitable = gravitable;

        _enemyConfig = enemyConfig;

        _timer = new TimerService(mono);
        _timer.Start(Random.Range(_enemyConfig.MinTimeToChangeDirection, _enemyConfig.MaxTimeToChangeDirection));

        _timer.TimesUp += OnDashEvent;
    }

    protected override void ControlLogicInUpdate()
    {
        if (_movable.MovableTransform.gameObject == null)
            this.Stop();

        if (_movable.IsMoving == false)
            SetDirection();

        _gravitable.HandleGravity(9.8f);

        Move();

        Rotate();
    }

    private void Move() => _movable.Move(_direction, _enemyConfig.MoveSpeed);

    private void Rotate()
    {
        _rotatable.Rotate(_direction, _enemyConfig.RotateSpeed);
    }

    private void OnDashEvent()
    {
        SetDirection();
        _dashable.Dash(_direction, _enemyConfig.DashPower, _enemyConfig.DashTime);
    }

    private void SetDirection()
    {
        Vector2 randomPointAround = Random.insideUnitCircle;

        _direction = (_movable.MovableTransform.position + new Vector3(randomPointAround.x, 0, randomPointAround.y)) -
            _movable.MovableTransform.position;

        _timer.Start(Random.Range(_enemyConfig.MinTimeToChangeDirection, _enemyConfig.MaxTimeToChangeDirection));
    }

    public override void Dispose()
    {
        _timer.TimesUp -= OnDashEvent;
    }
}