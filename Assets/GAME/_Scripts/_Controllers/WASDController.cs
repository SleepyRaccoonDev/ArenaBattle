using System;
using UnityEngine;

public class WASDController : Controller
{
    private InputSystem _inputSystem;

    private Vector3 _direction;

    private PlayerConfig _playerConfig;

    private Shooter _shooter;

    private BulletConfig _bulletConfig;

    private IMovable _movable;
    private IRotatable _rotatable;
    private IDashable _dashable;
    private IGravitable _gravitable;

    public WASDController(
        MonoBehaviour mono,
        InputSystem inputSystem,
        IMovable movable,
        IRotatable rotatable,
        IDashable dashable,
        IGravitable gravitable,
        PlayerConfig playerConfig,
        BulletConfig bulletConfig,
        Shooter shooter) : base(mono)
    {
        _movable = movable;
        _rotatable = rotatable;
        _dashable = dashable;
        _gravitable = gravitable;

        _inputSystem = inputSystem;
        _playerConfig = playerConfig;

        _bulletConfig = bulletConfig;

        _shooter = shooter;

        inputSystem.DashPressed += OnDashPressed;
    }

    protected override void ControlLogicInUpdate()
    {
        if (_movable.MovableTransform.gameObject == null)
            this.Stop();

        _gravitable?.HandleGravity(9.8f);

        Move();

        Rotate();

        Dash();

        Shoot();
    }

    private void Move()
    {
        _direction = _inputSystem.GetAxes();

        float lookOffset = Vector3.Dot(_movable.MovableTransform.forward, _direction);

        lookOffset = lookOffset > _playerConfig.Degrees ? 1 : lookOffset;

        float coef = Mathf.Clamp(lookOffset, _playerConfig.ClampBackSpeed, 1);

        _movable?.Move(_direction, _playerConfig.MoveSpeed * coef);
    }

    private void Rotate()
    {
        Vector3 mousePosition = _inputSystem.GetMousePosition();
        Vector3 rotateDirection = mousePosition - _movable.MovableTransform.position;
        rotateDirection.y = 0;

        _rotatable?.Rotate(rotateDirection.normalized, _playerConfig.RotateSpeed);
    }

    private void Dash()
    {
        _inputSystem?.Poll();
    }

    private void Shoot()
    {
        if (_inputSystem.GetKeyDownMouseLeft())
            _shooter.Shoot(_bulletConfig);
    }

    private void OnDashPressed()
    {
        _dashable.Dash(_direction.normalized, _playerConfig.DashPower, _playerConfig.DashTime);
    }

    public override void Dispose()
    {
        _inputSystem.DashPressed -= OnDashPressed;
    }
}