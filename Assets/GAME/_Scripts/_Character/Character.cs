using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour, IDamagable, IKillable, IHitable, IMovable, IRotatable, IDashable, IGravitable
{
    public event Action IsDashed;
    public event Action IsStopDashing;

    public event Action<SkinnedMeshRenderer> IsHited;
    public event Action<Character> IsKilled;

    private GroundChecker _groundChecker;

    private Rigidbody _rigidbody;

    private Mover _mover;
    private Rotator _rotator;
    private Dasher _dasher;

    private Vector3 _velocity;

    private ReactiveVariable<float> _health;

    private SkinnedMeshRenderer _skinned;

    public bool IsMoving => _rigidbody.velocity.magnitude > 5f;
    public bool IsDashing => _dasher.IsDashing;
    public IReadOnlyVariable<float> Health => _health;
    public Transform MovableTransform => transform;

    public void Initialize(
        Rigidbody rigidbody,
        Mover mover,
        Rotator rotator,
        Dasher dasher,
        GroundChecker groundChecker,
        SkinnedMeshRenderer skinned)
    {
        _rigidbody = rigidbody;

        _mover = mover;
        _rotator = rotator;
        _dasher = dasher;
        _groundChecker = groundChecker;
        _skinned = skinned;

        _health = new ReactiveVariable<float>(100);

        foreach (IInitializable initializable in GetComponentsInChildren<IInitializable>())
            initializable.Initialize();
    }

    public void Move(Vector3 direction, float speed)
    {
        if (IsDashing)
            return;

        IsStopDashing?.Invoke();

        float controlPercent = _groundChecker.IsTouches() ? 1f : 0;
        Vector3 newDirection = direction.normalized * controlPercent;

        _velocity = new Vector3(newDirection.x, _velocity.y, newDirection.z);
        _mover?.Move(_velocity, speed);
    }

    public void Rotate(Vector3 direction, float speed)
    {
        _rotator?.Rotate(direction, speed);
    }

    public void Dash(Vector3 direction, float force, float dashTime)
    {
        IsDashed?.Invoke();
        _dasher?.Dash(direction, force, dashTime);
    }

    public void HandleGravity(float gravity = 9.8f)
    {
        if (_groundChecker.IsTouches())
            _velocity.y = 0;
        else 
            _velocity.y -= gravity * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new Exception($"Отрицательный урон - {damage}");

        _health.Value -= damage;
        IsHited?.Invoke(_skinned);

        if (_health.Value < 0) {
            _health.Value = 0;
            IsKilled?.Invoke(this);
            Kill();
        }
    }

    private void Kill()
    {
        GameObject.Destroy(this.gameObject);
    }
}