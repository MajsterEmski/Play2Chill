using System;
using UnityEngine;
using UnityEngine.Pool;
using Pathfinding;
using DG.Tweening;

public class Agent : MonoBehaviour
{
    private Seeker seeker;
    public GridGraph gridGraph;
    public Guid agentID;
    private ObjectPool<Agent> pool;
    public int radius = 5;
    private bool isMoving;
    private float speed = 1;
    private Vector3 target;
    public AgentManager agentManager;

    private void Update()
    {
        if (!isMoving)
        {
            isMoving = true;
            target = GetRandomDestination();
            CalculatePath(target);
        }
        if (target == transform.position)
        {
            agentManager.LogDestinationReached(agentID);
            isMoving = false;
        }
    }

    private void OnEnable()
    {
        ITickService.onPauseToggle += TogglePause;
    }

    private void OnDisable()
    {
        ITickService.onPauseToggle -= TogglePause;
    }

    public void SetPool(ObjectPool<Agent> targetPool)
    {
        pool = targetPool;
    }

    public void RemoveThisAgent()
    {
        transform.DOKill();
        isMoving = false;
        pool.Release(this);
    }

    public Vector3 GetRandomDestination()
    {
        Vector3 target = UnityEngine.Random.insideUnitSphere * radius;
        target.y = 0;
        target += transform.position;
        NNConstraint constraint = NNConstraint.None;
        constraint.constrainWalkability = true;
        constraint.walkable = true;
        target = AstarPath.active.GetNearest(target, constraint).position;
        return target;
    }

    public void CalculatePath(Vector3 target)
    {
        if (!seeker) seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, target, OnPathComplete);
    }

    void OnPathComplete(Path path)
    {
        transform.DOPath(path.vectorPath.ToArray(), (path.GetTotalLength() * speed) / ITickService.currentTickRate, PathType.Linear, PathMode.Full3D, 10, null).SetLookAt(0.01f);
    }

    void TogglePause()
    {
        transform.DOTogglePause();
    }
}
