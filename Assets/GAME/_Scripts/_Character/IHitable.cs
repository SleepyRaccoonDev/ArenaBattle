using System;
using UnityEngine;

public interface IHitable
{
    event Action<SkinnedMeshRenderer> IsHited;
}