#if UNITY_AI_MODULE

using UnityEngine.AI;

public static class AgentExtensions
{
    public static bool DestinationReached(this NavMeshAgent agent)
    {
        const float zero = 0f;

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == zero || agent.path.status.Equals(NavMeshPathStatus.PathComplete))
                {
                    return true;
                }
            }
        }

        return false;
    }
}

#endif