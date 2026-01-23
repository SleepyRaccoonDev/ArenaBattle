using UnityEngine;

public class Rotator
{
    private Rigidbody _rigidbody;

    public Rotator(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void Rotate(Vector3 directin, float rotateSpeed)
    {
        if (directin == Vector3.zero)
            return;

        Quaternion rotation = Quaternion.LookRotation(directin);

        _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, rotation, rotateSpeed * Time.deltaTime);
    }
}