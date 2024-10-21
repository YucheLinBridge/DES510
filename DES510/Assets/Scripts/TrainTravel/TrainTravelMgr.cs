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
    [SerializeField] private LoopMode loopmode;

    [Header("Train")]
    [SerializeField] private List<Animator> animators;
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onStop;


    [Header("Track")]
    [SerializeField] private bool generateTracks;
    [SerializeField] private Transform trackParent;
    [SerializeField] private float spacing = 13.5f,start_pending,end_pending;
    [SerializeField] private GameObject trackPrefab;
    [SerializeField] private TrackDirection track_direction;

    [Header("Environment Block")]
    [SerializeField] private List<GameObject> blocks=new List<GameObject>();
    [SerializeField] private GameObject station_block;
    [SerializeField] private Transform blocks_parent;
    [SerializeField] private float x_offset,x_pedding;

    private Queue<Transform> block_transforms = new Queue<Transform>();

    private Vector3 dir => (end.position-start.position).normalized;

    private float time;
    private float t;
    private bool moving;
    private int times=0;

    private float thelastblock=0;
    private float disfromlastblock = 0;


    public void StartMoving() {
        foreach (var animator in animators)
        {
            animator.SetTrigger("MOVE");
        }

        
        onStart?.Invoke();
        time =(end.position-start.position).magnitude/speed;
        moving=true;
    }

    private void Start()
    {
        if (generateTracks)
        {
            createTracks();
        }

        if (autoStart)
        {
            StartMoving();
        }

        if (loopmode==LoopMode.MoveEnvironment)
        {
            createBlocks();
        }
    }

    private void Update()
    {
        if (moving)
        {
            switch (loopmode)
            {
                case LoopMode.Transport:
                    move_transport();
                    break;
                case LoopMode.MoveEnvironment:
                    move_MoveEnvironment();
                    break;
                default:
                    break;
            }
            
        }
    }

    private void move_transport()
    {
        t += Time.deltaTime;
        train.position = Vector3.Lerp(start.position, end.position, t / time);
        if (t >= time)
        {
            times++;
            t = 0;
            if (looptimes == -1)
            {

            }
            else if (times >= looptimes)
            {
                moving = false;
                onStop?.Invoke();
            }
        }
    }

    private void move_MoveEnvironment()
    {
        train.position += dir *Time.deltaTime * speed;
        disfromlastblock+= Time.deltaTime * speed;
        if (disfromlastblock>=x_offset*0.5f)
        {
            var block_transform= block_transforms.Dequeue();
            block_transform.position = start.position + dir * (thelastblock * .5f * x_offset + x_pedding);
            block_transforms.Enqueue(block_transform);
            disfromlastblock = 0;
            thelastblock++;
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

    private void createBlocks()
    {
        for (int i=0;i<blocks.Count;i++)
        {
            thelastblock++;
            var go = Instantiate(blocks[i],start.position+ dir * (i*.5f*x_offset+x_pedding),Quaternion.identity, blocks_parent);
            block_transforms.Enqueue(go.transform);
        }

        disfromlastblock = -x_pedding;
    }

    public void ArriveStation()
    {

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

    public enum LoopMode {
        Transport,
        MoveEnvironment
    }
}
