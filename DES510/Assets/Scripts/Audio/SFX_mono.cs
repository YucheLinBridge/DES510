using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SFX_mono : MonoBehaviour,IPoolable<AudioClip,IMemoryPool>
{
    [SerializeField] private AudioSource source;

    private IMemoryPool pool;



    public void OnDespawned()
    {
        gameObject.SetActive(false);
    }

    public void OnSpawned(AudioClip p1, IMemoryPool p2)
    {
        gameObject.SetActive(true);
        source.clip = p1;
        pool = p2;
        source.Play();
        StartCoroutine(waitToRecollectItself(p1.length));
    }


    /// <summary>
    /// Wait and recollect this object after time.
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator waitToRecollectItself(float time)
    {
        yield return new WaitForSeconds(time);
        pool.Despawn(this);
    }


    public class Factory : PlaceholderFactory<AudioClip, SFX_mono> { }
}
