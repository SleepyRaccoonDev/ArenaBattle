using UnityEngine;

public interface IMovable
{
    bool IsMoving { get; }

    Transform MovableTransform { get; }

    void Move(Vector3 direction, float speed);
}