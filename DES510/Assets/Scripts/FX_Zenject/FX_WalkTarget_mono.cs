using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FX_WalkTarget_mono : MonoBehaviour, IPoolable<IMemoryPool>
{
    [SerializeField] private ParticleSystem particle;


    private IMemoryPool pool;

    public Vector3 POS {
        get => transform.position;
        set {
            transform.position = value;
        }
    }
    public void OnDespawned()
    {
        gameObject.SetActive(false);
        pool = null;
    }

    public void OnSpawned(IMemoryPool p1)
    {
        pool = p1;
        gameObject.SetActive(true);
    }

    private void OnParticleSystemStopped()
    {
        pool.Despawn(this);
    }

    public void Stop()
    {
        particle.Stop();
    }

    public class Factory : PlaceholderFactory<FX_WalkTarget_mono> {
        
    }
}
