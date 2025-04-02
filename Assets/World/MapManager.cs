
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public List<MapNode> mapNodes;
    public int startingNodesCount;
    public int nodesLevelCount;
    public int leftRightCount = 4;
    public float chanceOfRandomLine = 0.2f;
    public GameObject prefabNode;
    public GameObject prefabLine;

    public static int nodeCount;
    public List<GameObject> lines;
    public float noiseFreq;
    public float noiseAmp;
    public float noiseAmpRand;
    public float noiseFreq2;
    public float noiseAmp2;
    public Transform mapHolder;
    private List<int> possibleLeftRightPos;
    private MapNode bossNode;
    private bool intialized = false;
    public bool Intialized => intialized;
    private int finalLevel => nodesLevelCount + 1;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Debug.LogError("double mapmanger"); }
        mapHolder.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        GlobalActions.OnDebugCreateMap += Generate;
        GlobalActions.OnMapSceneEntered += EnterMapScene;
        GlobalActions.OnMapSceneExited += ExitMapScene;
        GlobalActions.OnNodeCleared += OnNodeDone;
    }
    private void OnDisable()
    {
        GlobalActions.OnDebugCreateMap -= Generate;
        GlobalActions.OnMapSceneEntered -= EnterMapScene;
        GlobalActions.OnMapSceneExited -= ExitMapScene;
        GlobalActions.OnNodeCleared -= OnNodeDone;
    }

    private void EnterMapScene()
    {
        if (!intialized)
        {
            Generate();
            intialized = true;
        }
        mapHolder.gameObject.SetActive(true);
    }
    private void ExitMapScene()
    {
        mapHolder.gameObject.SetActive(false);
    }

    private void OnNodeDone(MapNode node)
    {

        foreach (MapNode mapNode in mapNodes)
        {
            if (mapNode.level == node.level)
            {
                mapNode.Lock();
            }
        }

    }

    public void Clear()
    {
        foreach (var node in mapNodes)
        {
            Destroy(node.gameObject);
        }
        foreach (var line in lines)
        {
            Destroy(line);
        }
        lines.Clear();
        mapNodes.Clear();
        nodeCount = 0;
    }
    public void Generate()
    {
        Clear();
        lines = new List<GameObject>();

        GenerateNodes();
        PositionAllNodes();
        SetNodeTypes();
        GenerateLines();
    }

    private void GenerateNodes()
    {
        possibleLeftRightPos = new List<int>();
        FillListWithPositions(possibleLeftRightPos);

        mapNodes = SetStartingNodes();
        List<MapNode> prevLevel = new List<MapNode>(mapNodes);

        for (int i = 0; i < nodesLevelCount; i++)
        {
            int currentLevel = i + 1;
            List<MapNode> newMapNodes = CreateMapNodes(prevLevel, currentLevel);
            mapNodes.AddRange(newMapNodes);
            prevLevel.Clear();
            prevLevel = new List<MapNode>();
            prevLevel.AddRange(newMapNodes);
        }

        // Add final boss node
        int mid = leftRightCount / 2;
        bossNode = CreateMapNode(finalLevel, mid);
        mapNodes.Add(bossNode);
    }

    private void GenerateLines()
    {
        Dictionary<int, List<MapNode>> nodesByLevel = new Dictionary<int, List<MapNode>>();
        foreach (var node in mapNodes)
        {
            if (!nodesByLevel.ContainsKey(node.level))
            {
                nodesByLevel[node.level] = new List<MapNode>();
            }

            nodesByLevel[node.level].Add(node);
        }
        GenereateStraightLines(nodesByLevel);
        GenereateOutGoingLines(nodesByLevel);
        GenereateIncomingConnections(nodesByLevel);
        GenerateRandomLines(nodesByLevel);
        RemoveExessiveLines(nodesByLevel);
    }

    private void RemoveExessiveLines(Dictionary<int, List<MapNode>> nodesByLevel)
    {
        foreach (var level in nodesByLevel)
        {
            if (nodesByLevel.ContainsKey(level.Key + 1))
            {
                foreach (var node in level.Value)
                {
                    foreach (var nextNode in nodesByLevel[level.Key + 1])
                    {
                        if (node.outgoingConnections.Count > 1 && node.outgoingConnections.Contains(nextNode))
                        {
                            if (nextNode.incomingConnections.Count > 1 && nextNode.incomingConnections.Contains(node))
                            {
                                float chance = .25f;
                                if (node.outgoingConnections.Count > 2)
                                {
                                    chance += .5f;
                                }
                                if (nextNode.incomingConnections.Count > 2)
                                {
                                    chance += .5f;
                                }
                                if (UnityEngine.Random.Range(0f, 1f) < chance)
                                {
                                    node.outgoingConnections.Remove(nextNode);
                                    nextNode.incomingConnections.Remove(node);
                                    if (nextNode.level <= node.level)
                                    {
                                        Debug.LogError($"Node {nextNode.nodeID} is at a lower level than {node.nodeID}");
                                    }
                                    GameObject line = node.outgoingLines.Find(l => l.GetComponent<MapLineConnection>().endNode == nextNode);
                                    node.outgoingLines.Remove(line);
                                    Destroy(line);
                                }



                            }
                        }
                    }
                }
            }
        }
    }


    private void GenerateRandomLines(Dictionary<int, List<MapNode>> nodesByLevel)
    {
        foreach (var level in nodesByLevel)
        {
            if (nodesByLevel.ContainsKey(level.Key + 1))
            {
                foreach (var node in level.Value)
                {

                    foreach (var nextNode in nodesByLevel[level.Key + 1])
                    {
                        float rand = chanceOfRandomLine;
                        if (!node.outgoingConnections.Contains(nextNode) && Mathf.Abs(nextNode.leftRight - node.leftRight) == 1)
                        {
                            if (UnityEngine.Random.Range(0f, 1f) < rand)
                            {
                                rand /= 2;
                                nextNode.incomingConnections.Add(node);
                                node.outgoingConnections.Add(nextNode);
                                if (nextNode.level <= node.level)
                                {
                                    Debug.LogError($"Node {nextNode.nodeID} is at a lower level than {node.nodeID}");
                                }
                                CreateLine(node, nextNode);
                            }
                        }
                    }
                }
            }
        }
    }

    private void GenereateIncomingConnections(Dictionary<int, List<MapNode>> nodesByLevel)
    {
        foreach (var level in nodesByLevel)
        {
            if (nodesByLevel.ContainsKey(level.Key - 1))
            {
                foreach (var node in level.Value)
                {
                    if (node.incomingConnections.Count == 0)
                    {
                        List<MapNode> validTargets = nodesByLevel[level.Key - 1].FindAll(nextNode => Mathf.Abs(nextNode.leftRight - node.leftRight) <= 1);

                        if (validTargets.Count > 0)
                        {
                            MapNode randomTarget = validTargets[UnityEngine.Random.Range(0, validTargets.Count)];
                            randomTarget.outgoingConnections.Add(node);
                            node.incomingConnections.Add(randomTarget);
                            if (node.level <= randomTarget.level)
                            {
                                Debug.LogError($"Node {node.nodeID} is at a lower level than {randomTarget.nodeID}");
                            }
                            CreateLine(node, randomTarget);
                        }
                        else
                        {
                            Debug.LogError($"No valid targets for node {node.nodeID} at level {node.level}");
                        }
                    }
                }
            }
        }
    }

    private void GenereateOutGoingLines(Dictionary<int, List<MapNode>> nodesByLevel)
    {
        foreach (var level in nodesByLevel)
        {
            if (nodesByLevel.ContainsKey(level.Key + 1))
            {
                foreach (var node in level.Value)
                {
                    foreach (var nextNode in nodesByLevel[level.Key + 1])
                    {
                        if (node.outgoingConnections.Count == 0)
                        {
                            if (nextNode.level == finalLevel)
                            {
                                node.outgoingConnections.Add(nextNode);
                                nextNode.incomingConnections.Add(node);
                                if (nextNode.level <= node.level)
                                {
                                    Debug.LogError($"Node {nextNode.nodeID} is at a lower level than {node.nodeID}");
                                }
                                CreateLine(node, nextNode);
                            }
                            else
                            {
                                List<MapNode> validTargets = nodesByLevel[level.Key + 1].FindAll(nextNode => Mathf.Abs(nextNode.leftRight - node.leftRight) <= 1);

                                if (validTargets.Count > 0)
                                {
                                    MapNode randomTarget = validTargets[UnityEngine.Random.Range(0, validTargets.Count)];
                                    randomTarget.incomingConnections.Add(node);
                                    node.outgoingConnections.Add(randomTarget);
                                    if (randomTarget.level <= node.level)
                                    {
                                        Debug.LogError($"Node {randomTarget.nodeID} is at a lower level than {node.nodeID}");
                                    }
                                    CreateLine(node, randomTarget);
                                }
                                else
                                {
                                    Debug.LogError($"No valid targets for node {node.nodeID} at level {node.level}");
                                }
                            }

                        }
                    }
                }
            }

        }
    }

    private void GenereateStraightLines(Dictionary<int, List<MapNode>> nodesByLevel)
    {
        foreach (var level in nodesByLevel)
        {
            if (nodesByLevel.ContainsKey(level.Key + 1))
            {
                foreach (var node in level.Value)
                {
                    foreach (var nextNode in nodesByLevel[level.Key + 1])
                    {
                        if (nextNode.leftRight == node.leftRight)
                        {
                            if (node.outgoingConnections.Contains(nextNode) || nextNode.incomingConnections.Contains(node))
                            {
                                continue;
                            }
                            if (nextNode.level <= node.level)
                            {
                                Debug.LogError($"Node {nextNode.nodeID} is at a lower level than {node.nodeID}");
                            }
                            nextNode.incomingConnections.Add(node);
                            node.outgoingConnections.Add(nextNode);
                            CreateLine(node, nextNode);

                        }

                    }
                }
            }




        }
    }

    private void CreateLine(MapNode from, MapNode to)
    {
        GameObject lineObj = Instantiate(prefabLine);
        lineObj.transform.SetParent(mapHolder);
        MapLineConnection line = lineObj.GetComponent<MapLineConnection>();
        line.startNode = from;
        line.endNode = to;

        int segments = 30; // More segments = smoother line
        line.lineRenderer.positionCount = segments;

        Vector3 start = from.transform.position;
        Vector3 end = to.transform.position;

        for (int i = 0; i < segments; i++)
        {

            float u = i / (float)(segments - 1);
            float u2 = i + 1 / (float)(segments - 1);
            Vector3 point = Vector3.Lerp(start, end, u);
            Vector3 point2 = Vector3.Lerp(start, end, u2);
            Vector3 tangent = point2 - point;
            Vector3 cross = Vector3.Cross(tangent, Vector3.forward);

            float offsetStrength = GeneralUtils.fit01(Mathf.PerlinNoise(point.x * noiseFreq, point.y * noiseFreq), -noiseAmpRand, noiseAmpRand);
            float offsetStrength2 = GeneralUtils.fit01(Mathf.PerlinNoise((point.x + 1515.88f) * noiseFreq, (point.y - 5154.55f) * noiseFreq), -noiseAmp, noiseAmp);
            float offsetStrength3 = GeneralUtils.fit01(Mathf.PerlinNoise((point.x + 15215.818f) * noiseFreq2, (point.y - 51154.554f) * noiseFreq2), -noiseAmp2, noiseAmp2);
            Vector3 offsetRand2 = new Vector3(cross.x * offsetStrength2, cross.y * offsetStrength2, 0f);
            Vector3 offsetRand3 = new Vector3(cross.x * offsetStrength3, cross.y * offsetStrength3, 0f);

            Vector3 offsetRand = new Vector3(
                UnityEngine.Random.Range(-offsetStrength, offsetStrength),
                UnityEngine.Random.Range(-offsetStrength, offsetStrength),
                0
            );

            float falloff = Mathf.Sin(u * Mathf.PI);
            point += offsetRand * falloff;
            point += offsetRand2 * falloff;
            point += offsetRand3 * falloff;
            line.lineRenderer.SetPosition(i, point);
        }
        if (from.level < to.level)
        {
            from.outgoingLines.Add(lineObj);
        }
        else
        {
            to.outgoingLines.Add(lineObj);
        }

        lines.Add(lineObj);



        // Debug.Log($"Line created: {from.nodeID} → {to.nodeID} | Total outgoing lines: {from.outgoingLines.Count }");
    }


    private List<MapNode> SetStartingNodes()
    {
        List<MapNode> startingNodes = new List<MapNode>();
        List<int> leftRightPositions = GetRandomLeftRightPositions(startingNodesCount);

        foreach (int lr in leftRightPositions)
        {
            startingNodes.Add(CreateMapNode(0, lr));
        }

        return startingNodes;
    }

    private List<MapNode> CreateMapNodes(List<MapNode> prevLevel, int level)
    {
        List<MapNode> newNodes = new List<MapNode>();
        List<int> prevPositions = GetLeftRightPositions(prevLevel);

        int rand = UnityEngine.Random.Range(2, 4);
        if (rand == 1 || rand == 5)
        {
            //Debug.LogError("Random number is out of bounds");
        }
        List<int> leftRightPositions = GetRandomLeftRightPositions(rand);
        if (leftRightPositions.Count == 1 || leftRightPositions.Count == 5)
        {
            // Debug.LogError("left right positions out of bounds");
        }

        foreach (int lr in leftRightPositions)
        {
            //   Debug.Log($"Creating node at level {level}, LR {lr}");
            bool checker = false;
            foreach (var node in prevLevel)
            {
                if (Mathf.Abs(node.leftRight - lr) < 2)
                {
                    checker = true;
                }
            }
            if (checker)
            {
                newNodes.Add(CreateMapNode(level, lr));
            }

        }

        AddMoreNodesIfNeeded(newNodes, prevPositions, level);


        return newNodes;
    }

    private void AddMoreNodesIfNeeded(List<MapNode> nodes, List<int> prevLeftRightPos, int level)
    {
        foreach (int lr in prevLeftRightPos)
        {
            if (lr == 0)
            {
                if (nodes.Contains(nodes.Find(n => n.leftRight == 1)))
                {
                    //Debug.Log("node exist ad level " + level + " left right 1 so therfore node at " + (level -1 ) + " position 0 is ok");
                    continue;
                }

                if (nodes.Contains(nodes.Find(n => n.leftRight == 0)))
                {
                    //  Debug.Log("node exist ad level " + level + " left right 0 so therfore node at " + (level - 1) + " position 0 is ok");
                    continue;
                }

                int rand = UnityEngine.Random.Range(0, 2);
                //Debug.Log($"Creating extra  node at level {level}, LR {rand}" + "should be 0 or 1");
                nodes.Add(CreateMapNode(level, rand));
            }
            if (lr == 3)
            {
                if (nodes.Contains(nodes.Find(n => n.leftRight == 2)))
                {
                    //  Debug.Log("node exist ad level " + level + " left right 2 so therfore node at " + (level - 1) + " position 4 is ok");
                    continue;
                }

                if (nodes.Contains(nodes.Find(n => n.leftRight == 3)))
                {
                    //Debug.Log("node exist ad level " + level + " left right 3 so therfore node at " + (level - 1) + " position 4 is ok");
                    continue;
                }
                int rand = UnityEngine.Random.Range(2, 4);
                //Debug.Log($"Creating node at level {level}, LR {rand}" + "should be 2 or 3");
                nodes.Add(CreateMapNode(level, rand));
            }
        }

    }


    private MapNode CreateMapNode(int level, int leftRight)
    {
        GameObject nodeGO = Instantiate(prefabNode);
        nodeGO.transform.SetParent(mapHolder);
        MapNode mapNode = nodeGO.GetComponent<MapNode>();
        mapNode.nodeID = nodeCount++;

        mapNode.difficulty = (level / 4) + 1;
        mapNode.leftRight = leftRight;
        mapNode.level = level;

        if (level == 0)
        {
            mapNode.Unlock();
        }
        else
        {
            mapNode.Lock();
        }
        mapNodes.Add(mapNode);
        return mapNode;
    }

    private void PositionAllNodes()
    {
        Debug.Log($"Positioning {mapNodes.Count} nodes...");
        HashSet<int> seenIds = new HashSet<int>();

        foreach (var node in mapNodes)
        {
            if (node == null)
            {
                Debug.LogWarning("Null node in mapNodes list!");
                continue;
            }

            seenIds.Add(node.nodeID);
            if (node.level == 0)
            {
                node.transform.position = new Vector3(node.leftRight * 3, node.level * 3, 0);

            }
            else
            {
                Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-.85f, .85f), UnityEngine.Random.Range(-.75f, .75f), 0);
                node.transform.position = new Vector3(node.leftRight * 3 + randomOffset.x, node.level * 3 + randomOffset.y, 0);

            }


        }

        // Check if any IDs are missing
        for (int i = 0; i < MapManager.nodeCount; i++)
        {
            if (!seenIds.Contains(i))
            {
                Debug.LogError($"❌ Missing node ID {i} in mapNodes!");
            }
        }
    }

    private void SetNodeTypes()
    {


        int elite = Random.Range(4, 6);
        int shop = Random.Range(2, 4);
        int rest = Random.Range(2, 4);
        int randevent = Random.Range(5, 7);
        if (elite + shop + rest + randevent > mapNodes.Count)
        {
            Debug.LogError("Too many special nodes");
            return;
        }

        foreach (MapNode mapNode in mapNodes)
        {
            mapNode.nodeTypeEnum = MapNodeTypeEnum.Battle;
        }

        for (int i = 0; i < elite; i++)
        {
            MapNode node = mapNodes[Random.Range(0, mapNodes.Count)];
            if (node.nodeTypeEnum == MapNodeTypeEnum.Battle)
            {
                node.nodeTypeEnum = MapNodeTypeEnum.elite;
            }
            else
            {
                i--;
            }
        }
        for (int i = 0; i < shop; i++)
        {
            MapNode node = mapNodes[Random.Range(0, mapNodes.Count)];
            if (node.nodeTypeEnum == MapNodeTypeEnum.Battle)
            {
                node.nodeTypeEnum = MapNodeTypeEnum.Shop;
            }
            else
            {
                i--;
            }
        }

        for (int i = 0; i < rest; i++)
        {
            MapNode node = mapNodes[Random.Range(0, mapNodes.Count)];
            if (node.nodeTypeEnum == MapNodeTypeEnum.Battle)
            {
                node.nodeTypeEnum = MapNodeTypeEnum.Rest;
            }
            else
            {
                i--;
            }
        }
        for (int i = 0; i < randevent; i++)
        {
            MapNode node = mapNodes[Random.Range(0, mapNodes.Count)];
            if (node.nodeTypeEnum == MapNodeTypeEnum.Battle)
            {
                node.nodeTypeEnum = MapNodeTypeEnum.randomEvent;
            }
            else
            {
                i--;
            }
        }


        foreach (var node in mapNodes)
        {
            if (node.level == 0)
            {
                node.nodeTypeEnum = MapNodeTypeEnum.Battle;
                continue;
            }
            if (node.level == finalLevel)
            {
                node.nodeTypeEnum = MapNodeTypeEnum.Boss;
                continue;
            }
            float rand = Random.Range(0f, 1f);
            node.InitSpriteAndType();


        }
    }
    private List<int> GetRandomLeftRightPositions(int count)
    {
        List<int> available = new List<int>(possibleLeftRightPos);

        return GeneralUtils.GetRandomFromList(available, count);
    }

    private List<int> GetLeftRightPositions(List<MapNode> nodes)
    {
        return nodes.ConvertAll(n => n.leftRight);
    }

    private void FillListWithPositions(List<int> list)
    {
        list.Clear();
        for (int i = 0; i < leftRightCount; i++)
            list.Add(i);
    }
    public MapNode GetHighestUnlockedNode()
    {

        Debug.Log("Getting highest unlocked node map count" +  mapNodes.Count);
        MapNode mapNode = null;
        int _level = -1;
        foreach (var node in mapNodes)

        {
            if (node.level > _level && node.mapNodeStateEnum == MapNodeStateEnum.Unlocked)
            {
                Debug.Log($"Found unlocked node at level {node.level}");
                mapNode = node;
                _level = node.level;
            }
        }
        return mapNode;
    }
}
