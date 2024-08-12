using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AgentManager : MonoBehaviour
{
    //Setting up a pool to avoid the performance hit of endlessly instantiating and destroying agents :)
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
        agent.transform.position = agentSpawnPos;

        agent.gameObject.SetActive(true);

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
        Debug.Log("Agent Manager attempting to spawn agents!");
        poolAgents.Get();
        //IAgentService.agentsNumber++;
    }

    public void DestroyAgent()
    {
        Debug.Log("Agent Manager attempting to destroy a random agent!");
        if (IAgentService.agents.Count <= 0) return;
        int r = UnityEngine.Random.Range(0, IAgentService.agentsNumber);
        IAgentService.agents[r].GetComponent<Agent>().RemoveThisAgent();
        IAgentService.agents.RemoveAt(r);
        //IAgentService.agentsNumber--;
    }

    public void ClearAgents()
    {
        Debug.Log("Agent Manager attempting to clear agents!");
        foreach (GameObject obj in IAgentService.agents)
        {
            obj.GetComponent<Agent>().RemoveThisAgent();
        }
        IAgentService.agents.Clear();
        //IAgentService.agentsNumber = 0;
    }
    #endregion

    #region Tickrate Management
    public void IncreaseTick()
    {
        Debug.Log("Agent Manager attempting to increase the tick rate!");
        if (ITickService.currentTickRate >= 5) return;
        ITickService.currentTickRate++;
    }

    public void DecreaseTick() 
    {
        Debug.Log("Agent Manager attempting to decrease the tick rate!");
        if (ITickService.currentTickRate <= 1) return;
        ITickService.currentTickRate--;
    }

    public void ResetTick()
    {
        Debug.Log("Agent Manager attempting to reset the tick rate!");
        ITickService.currentTickRate = 1;
    }

    public void TogglePause()
    {
        Debug.Log("Agent Manager attempting to toggle pause!");
        ITickService.isPaused = !ITickService.isPaused;
    }
    #endregion
}
