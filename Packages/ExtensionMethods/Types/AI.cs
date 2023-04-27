#if UNITY_AI_MODULE

using System;
using UnityEngine.AI;

public static class AIExtensions
{
    /// <summary>
    /// Checks if the NavMeshAgent is at its destination with the given tolerance.
    /// </summary>
    /// <param name="agent">The NavMeshAgent to check.</param>
    /// <param name="tolerance">The tolerance value to use for comparing the agent's velocity magnitude to zero.</param>
    /// <returns>True if the agent is at its destination within the specified tolerance, false otherwise.</returns>
    public static bool IsAtDestination(this NavMeshAgent agent, float tolerance = 0.01f)
    {
        const float zero = 0f;

        if (agent.pathPending) return false;

        if (!(agent.remainingDistance <= agent.stoppingDistance)) return false;

        return !agent.hasPath || Math.Abs(agent.velocity.sqrMagnitude - zero) < tolerance || agent.path.status.Equals(NavMeshPathStatus.PathComplete);
    }
}

#endif