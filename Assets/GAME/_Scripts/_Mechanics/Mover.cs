using UnityEngine;

public class Mover
{
    private Rigidbody _rigidbody;

    public Mover(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void Move(Vector3 direction, float speed)
    {
        _rigidbody.velocity = direction * speed;
    }
}