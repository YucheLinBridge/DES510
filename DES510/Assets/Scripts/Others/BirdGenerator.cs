using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGenerator : MonoBehaviour
{
    [SerializeField] private GameObject Prefab_BridEffect;
    [SerializeField] private float Interval = 10;
    [SerializeField] private float IntervalRange = 3;
    [SerializeField] private Transform SpawnPoint;

    private GameObject birdFx;

    private void Start()
    {
        StartCoroutine(WaitForARandomDelay());
    }

    IEnumerator WaitForARandomDelay() {
        yield return new WaitForSeconds(Random.Range(Interval-IntervalRange,Interval+IntervalRange));
        spawn();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void spawn()
    {
        if (birdFx==null)
        {
            birdFx = Instantiate(Prefab_BridEffect,SpawnPoint.position,Quaternion.Euler(new Vector3(-90,0,0)),transform);
        }
        else
        {
            birdFx.transform.position = SpawnPoint.transform.position;
            birdFx.GetComponent<ParticleSystem>().Play();
        }
        StartCoroutine(WaitForARandomDelay());
    }

}
