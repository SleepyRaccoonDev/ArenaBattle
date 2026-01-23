using UnityEngine;

public abstract class DamageDealer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hits = GetHits(collision);

        foreach (var hit in hits)
            if (hit.TryGetComponent<IDamagable>(out IDamagable damagable))
                DealDamage(damagable);

        AdditionalLogic();
    }

    protected abstract void DealDamage(IDamagable damagable);

    protected abstract Collider[] GetHits(Collision hitPoint);

    protected virtual void AdditionalLogic()
    {

    }
}