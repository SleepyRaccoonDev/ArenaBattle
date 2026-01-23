using System.Collections;
using UnityEngine;

public class Dasher
{
    private MonoBehaviour _mono;

    private Rigidbody _rigidbody;

    public Dasher(Rigidbody rigidbody, MonoBehaviour mono)
    {
        _rigidbody = rigidbody;
        _mono = mono;
    }

    public bool IsDashing { get; private set; }

    public void Dash(Vector3 direction, float force, float dashTime)
    {
        if (IsDashing)
            return;

        IsDashing = true;

        _mono.StartCoroutine(DashProcess(direction.normalized, force, dashTime));
    }

    private IEnumerator DashProcess(Vector3 direction, float force, float dashTime)
    {
        float timeSpend = 0f;

        do
        {
            timeSpend += Time.deltaTime;

            float t = Mathf.Clamp01(timeSpend / dashTime);
            float forceConf = Mathf.SmoothStep(1f, 0f, t);

            Vector3 newVector = direction * (force * forceConf);
            _rigidbody.velocity = new Vector3(newVector.x, 0, newVector.z);

            yield return null;

            if (_rigidbody.velocity.magnitude < force / 2)
                break;

        } while (timeSpend < dashTime);

        _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);

        IsDashing = false;
    }
}