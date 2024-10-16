using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrainTravelMgr : MonoBehaviour
{
    [SerializeField] private Transform start, end;
    [SerializeField] private Transform train;
    [Header("Setting")]
    [SerializeField] private bool autoStart;
    [SerializeField] private float speed=10;
    [Tooltip("looptimes=-1 means infinetly loop")]
    [SerializeField] private int looptimes = 1;

    [Header("Train")]
    [SerializeField] private List<Animator> animators;
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onStop;


    [Header("Rail")]
    [SerializeField] private Transform trackParent;
    [SerializeField] private float spacing = 13.5f,start_pending,end_pending;
    [SerializeField] private GameObject trackPrefab;
    [SerializeField] private TrackDirection track_direction;


    private float time;
    private float t;
    private bool moving;
    private int times=0;

    public void StartMoving() {
        foreach (var animator in animators)
        {
            animator.SetTrigger("MOVE");
        }

        
        onStart?.Invoke();
        time =(end.position-start.position).magnitude/speed;
        moving=true;
        //Debug.Log($"start={start.position}\nend={end.position}");
    }

    private void Start()
    {
        createTracks();

        if (autoStart)
        {
            StartMoving();
        }
    }

    private void Update()
    {
        if (moving)
        {

            t += Time.deltaTime;
            train.position = Vector3.Lerp(start.position,end.position,t/time);
            if (t>=time)
            {
                times++;
                t = 0;
                if (looptimes==-1)
                {

                }else if (times>=looptimes)
                {
                    moving = false;
                    onStop?.Invoke();
                }
            }
        }
    }

    private void createTracks()
    {
        float trackLength = -start_pending;
        Vector3 dir= end.position - start.position;
        float distance = dir.magnitude;
        //Debug.Log($"start={start.position}\nend={end.position}");
        while (true)
        {
            trackLength += Mathf.Abs(spacing);
            var go=Instantiate(trackPrefab,trackParent);
            go.transform.position = Vector3.LerpUnclamped(start.position, end.position, trackLength / distance);
            switch (track_direction)
            {
                case TrackDirection.Right:
                    go.transform.right = dir;
                    break;
                case TrackDirection.Forward:
                    go.transform.forward = dir;
                    break;
                case TrackDirection.Up:
                    go.transform.up = dir;

                    break;
                default:
                    break;
            }

            if (trackLength>=distance+end_pending)
            {
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(start.position, end.position);
    }


    public enum TrackDirection {
        Right,
        Forward,
        Up
    }
}
