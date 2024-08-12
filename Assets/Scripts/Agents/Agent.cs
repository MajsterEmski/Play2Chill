using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Agent : MonoBehaviour
{
    public Guid agentID;
    private ObjectPool<Agent> pool;

    public void SetPool(ObjectPool<Agent> targetPool)
    {
        pool = targetPool;
    }

    public void RemoveThisAgent()
    {
        pool.Release(this);
    }
}
