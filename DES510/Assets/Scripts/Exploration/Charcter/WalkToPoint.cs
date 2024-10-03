using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkToPoint : MonoBehaviour
{
    [SerializeField]private NavMeshAgent agent;

   public void Walk(Vector3 des)
   {
        agent.SetDestination(des);
        Debug.Log($"{name} walk to {des}");
   }
}
