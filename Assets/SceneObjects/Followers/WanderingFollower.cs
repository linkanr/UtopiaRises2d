using UnityEngine;
using Pathfinding;

public class WanderingFollower : MonoBehaviour
{

    IAstarAI ai;
    public int searchLength;

    void Start()
    {
        ai = GetComponent<IAstarAI>();
    }

    public void GetNewPath()
    {
        Vector3 goalPos = GameSceneRef.instance.followerBirthPlace.position;
        float dist = Vector3.Distance(transform.position, goalPos);
        float mix = GeneralUtils.fit(dist, 0f, 30f, 0f, .9f); 
        Vector3 endPos = Vector3.Lerp(transform.position, goalPos, mix);
        ConstantPath path = ConstantPath.Construct(endPos, searchLength);

        AstarPath.StartPath(path);
        path.BlockUntilCalculated();

        ai.destination = PathUtilities.GetPointsOnNodes(path.allNodes, 1)[0];
        ai.SearchPath();

    }
    private void Update()
    {
        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            GetNewPath();
        }
    }
}
