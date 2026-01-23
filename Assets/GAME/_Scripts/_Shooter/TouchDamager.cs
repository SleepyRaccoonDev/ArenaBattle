using UnityEngine;

public class TouchDamager : DamageDealer
{
    private TouchConfig _touchConfig;
    private LayerMask _layerMask;

    public void Initialize(TouchConfig touchConfig)
    {
        _layerMask = touchConfig.LayerMask;
        _touchConfig = touchConfig;
    }

    protected override void DealDamage(IDamagable damagable)
    {
        damagable.TakeDamage(_touchConfig.Damage);
    }

    protected override Collider[] GetHits(Collision collision)
    {
        Vector3 point = collision.GetContact(0).point;

        return Physics.OverlapSphere(point, 0, _layerMask, QueryTriggerInteraction.Ignore);
    }
}