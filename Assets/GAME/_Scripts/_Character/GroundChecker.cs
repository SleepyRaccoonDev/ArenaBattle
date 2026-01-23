using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private CapsuleCollider _collider;

    [SerializeField] private Vector3 _direction; 

    [SerializeField, Min(0f)] private float _distanceToCheck = 0.15f;

    private RaycastHit[] _hits = new RaycastHit[1];

    public bool IsTouches() =>
        Physics.CapsuleCastNonAlloc(
            _collider.bounds.center + transform.forward * (_collider.height * 0.5f - _collider.radius),
            _collider.bounds.center - transform.forward * (_collider.height * 0.5f - _collider.radius),
            _collider.radius,
            Vector3.down,
            _hits,
            _distanceToCheck,
            _layerMask
        ) > 0;
}