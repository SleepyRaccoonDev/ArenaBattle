using UnityEngine;

public class Bullet : DamageDealer
{
    private BulletConfig _bulletConfig;
    private LayerMask _layerMask;
    private ParticleSystem _particleSystem;

    public void Initialize(BulletConfig bulletConfig)
    {
        _bulletConfig = bulletConfig;
        _particleSystem = GetComponentInChildren<ParticleSystem>(true);
        _particleSystem?.gameObject.SetActive(false);

        _layerMask = bulletConfig.LayerMask;
    }

    protected override void AdditionalLogic()
    {
        _particleSystem?.transform.SetParent(null, true);
        _particleSystem?.gameObject.SetActive(true);
        _particleSystem?.Play(true);

        GameObject.Destroy(this.gameObject);
    }

    protected override Collider[] GetHits(Collision collision)
    {
        Vector3 hitPoint = collision.contacts[0].point;

        return Physics.OverlapSphere(hitPoint, _bulletConfig.Radius, _layerMask, QueryTriggerInteraction.Ignore);
    }

    protected override void DealDamage(IDamagable damagable)
    {
        damagable.TakeDamage(_bulletConfig.Damage);
    }
}
