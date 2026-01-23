using System.Collections;
using UnityEngine;

public class CharacterViewer : MonoBehaviour, IInitializable
{
    private const float BlinkTime = .15f;

    private IKillable _killable;
    private IHitable _hitable;
    private ParticleSystem _particleSystem;
    private Coroutine _coroutine;
    private Color _defaultColor;
    private SkinnedMeshRenderer _skinnedMesh;

    public void Initialize()
    {
        _killable = GetComponentInParent<IKillable>();
        _killable.IsKilled += Kill;

        _hitable = GetComponentInParent<IHitable>();
        _hitable.IsHited += Hit;

        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _particleSystem?.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _killable.IsKilled -= Kill;
        _hitable.IsHited -= Hit;
    }

    private void Hit(SkinnedMeshRenderer skinnedMesh)
    {
        _skinnedMesh = skinnedMesh;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            ReturnToDefault();
        }
            
        _coroutine = StartCoroutine(HitProcess(_skinnedMesh));
    }

    private void ReturnToDefault()
    {
        _skinnedMesh.material.color = _defaultColor;
    }

    private IEnumerator HitProcess(SkinnedMeshRenderer _skinnedMesh)
    {
        _defaultColor = _skinnedMesh.material.color;
        _skinnedMesh.material.color = Color.red;
        yield return new WaitForSeconds(BlinkTime);
        ReturnToDefault();
    }

    private void Kill(Character character)
    {
        _particleSystem?.gameObject.SetActive(true);
        _particleSystem?.transform.SetParent(null);
        _particleSystem?.Play();
    }
}