using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FXController_mono : MonoBehaviour, IPoolable<IMemoryPool>
{
    private IMemoryPool pool;

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

    public class Factory : PlaceholderFactory<FXController_mono> {
        
    }
}
