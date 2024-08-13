using DG.Tweening;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AgentManager : MonoBehaviour
{
    //Setting up a pool to avoid the performance hit of endlessly instantiating and destroying agents :)
    //
    //Though, if the project required a significant number of active agents, perhaps it'd be better to use DOTS Entities instead of typical GameObjects?
    public ObjectPool<Agent> poolAgents;
    public Agent agentPrefab;
    public Vector3 agentSpawnPos;

    private void OnEnable()
    {
        IAgentService.onAgentSpawn += SpawnAgent;
        IAgentService.onAgentClear += ClearAgents;
        IAgentService.onAgentDestroy += DestroyAgent;
        ITickService.onTickIncrease += IncreaseTick;
        ITickService.onTickDecrease += DecreaseTick;
        ITickService.onTickReset += ResetTick;
        ITickService.onPauseToggle += TogglePause;
    }

    private void OnDisable()
    {
        IAgentService.onAgentSpawn -= SpawnAgent;
        IAgentService.onAgentClear -= ClearAgents;
        IAgentService.onAgentDestroy -= DestroyAgent;
        ITickService.onTickIncrease -= IncreaseTick;
        ITickService.onTickDecrease -= DecreaseTick;
        ITickService.onTickReset -= ResetTick;
        ITickService.onPauseToggle -= TogglePause;
    }

    private void Start()
    {
        //Setting initial values
        IAgentService.onAgentClear?.Invoke();
        ITickService.onTickReset?.Invoke();
        ITickService.isPaused = false;
        poolAgents = new ObjectPool<Agent>(CreateAgent, OnTakeAgentFromPool, OnReturnAgentToPool, OnDestroyAgent, true, 50, 150);
    }

    #region Pool Setup
    private Agent CreateAgent()
    {
        Agent agent = Instantiate(agentPrefab, agentSpawnPos, transform.rotation);
        agent.SetPool(poolAgents);
        agent.agentID = Guid.NewGuid();
        return agent;
    }

    private void OnTakeAgentFromPool(Agent agent)
    {
        agent.gameObject.SetActive(true);
        agent.DOKill();
        agent.transform.position = agentSpawnPos;
        IAgentService.agents.Add(agent.gameObject);
    }

    private void OnReturnAgentToPool(Agent agent)
    {
        agent.gameObject.SetActive(false);
    }

    private void OnDestroyAgent(Agent agent)
    {
        Destroy(agent.gameObject);
    }

    #endregion

    #region Agent Management
    public void SpawnAgent()
    {
        poolAgents.Get();
    }

    public void DestroyAgent()
    {
        if (IAgentService.agents.Count <= 0) return;
        int r = UnityEngine.Random.Range(0, IAgentService.agents.Count);
        IAgentService.agents[r].GetComponent<Agent>().RemoveThisAgent();
        IAgentService.agents.RemoveAt(r);
    }

    public void ClearAgents()
    {
        foreach (GameObject obj in IAgentService.agents)
        {
            obj.GetComponent<Agent>().RemoveThisAgent();
        }
        IAgentService.agents.Clear();
    }
    #endregion

    #region Tickrate Management
    public void IncreaseTick()
    {
        if (ITickService.currentTickRate >= 5) return;
        ITickService.currentTickRate++;
        DOTween.timeScale = ITickService.currentTickRate;
    }

    public void DecreaseTick() 
    {
        if (ITickService.currentTickRate <= 1) return;
        ITickService.currentTickRate--;
        DOTween.timeScale = ITickService.currentTickRate;
    }

    public void ResetTick()
    {
        ITickService.currentTickRate = 1;
        DOTween.timeScale = ITickService.currentTickRate;
    }

    public void TogglePause()
    {
        ITickService.isPaused = !ITickService.isPaused;
    }
    #endregion
}
