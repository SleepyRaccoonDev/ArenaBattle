using UnityEngine;

public class Shooter : MonoBehaviour, IInitializable
{
    private Transform _firePoint;
    private ParticleSystem _particleSystem;

    public void Initialize()
    {
        _firePoint = GetComponentInChildren<FirePoint>().transform;
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void Shoot(BulletConfig bulletConfig)
    {
        _particleSystem?.Play();

        var bullet = GameObject.Instantiate(bulletConfig.BulletPrefab, _firePoint.position, _firePoint.rotation, null);
        bullet?.Initialize(bulletConfig);

        bullet.GetComponent<Rigidbody>().velocity = new Vector3(bullet.transform.right.x, 0, bullet.transform.right.z) * 40f;
    }
}