using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointOnMap : MonoBehaviour, IPointerUpHandler
{
    public LayerMask LayerMask;

    public UnityEvent<Vector3> onPointOnMap;

    private List<Ray> rays = new List<Ray>(); 

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Clicked");
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        if (Physics.Linecast(ray.origin, ray.origin+ ray.direction*1000, out var hitinfo, LayerMask)){
            //Debug.Log($"click at {hitinfo.collider.name}");
            onPointOnMap?.Invoke(hitinfo.point);
        }

        //rays.Add(ray);
        //Debug.DrawLine(ray.origin, ray.direction);
        //Debug.Log($"ray origin: {ray.origin}\ndirection:{ray.direction}");
        //Debug.LogError($"Clicked at {eventData.position}");
    }

    /*private void OnDrawGizmosSelected()
    {
        for (int i=0;i<rays.Count;i++)
        {
            if (i==rays.Count-1)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color=Color.red;
            }
            Gizmos.DrawLine(rays[i].origin, rays[i].origin+ rays[i].direction*100);
        }

    }*/

}
