using UnityEngine;

public class Duster : MonoBehaviour, IInitializable
{
    private Character _character;
    private ParticleSystem _particleSystem;

    public void Initialize()
    {
        _character = GetComponentInParent<Character>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _particleSystem.Stop();

        _character.IsDashed += StartDusting;
        _character.IsStopDashing += Stopusting;
    }

    private void OnDisable()
    {
        _character.IsDashed -= StartDusting;
        _character.IsStopDashing -= Stopusting;
    }

    private void StartDusting()
    {
        _particleSystem?.Play();
    }

    private void Stopusting()
    {
        _particleSystem?.Stop();
    }
}