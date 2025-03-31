using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class MapNode:SerializedMonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler
{
    public MapNodeStateEnum mapNodeStateEnum;
    public int nodeID;
    public List<MapNode> incomingConnections;
    public List<MapNode> outgoingConnections;
    public int difficulty;
    public MapNodeTypeEnum nodeTypeEnum;
    public int level;
    public int leftRight;
    public List<GameObject> outgoingLines;
    public bool isBossNode;
    public Color baseColor;
    public Color mouseOverColor;
    public Color lockedColor;
    public LevelBase levelBase;
    public Dictionary<MapNodeTypeEnum, Sprite> displaySprites;

    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        incomingConnections = new List<MapNode>();
        outgoingConnections = new List<MapNode>();
        outgoingLines = new List<GameObject>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }
    public void InitSpriteAndType()
    {
        spriteRenderer.sprite = displaySprites[nodeTypeEnum];
    }

    public void Cleared()
    {
        levelBase = LevelListerManager.instance.GetRandomLevel(difficulty);
        mapNodeStateEnum = MapNodeStateEnum.Completed;
        spriteRenderer.color = Color.green;



        foreach (MapNode node in outgoingConnections)
        {
            node.Unlock();
        }
        UnlockLines();
        foreach (MapNode node in incomingConnections)
        {
            node.LockLines();
        }
        GlobalActions.OnNodeCleared(this);
    }

    private void UnlockLines()
    {
        foreach (GameObject line in outgoingLines)
        {
            line.GetComponent<LineRenderer>().startColor = Color.green;
            line.GetComponent<LineRenderer>().endColor = Color.green;
        }
    }
    private void LockLines()
    {
        foreach (GameObject line in outgoingLines)
        {
            line.GetComponent<LineRenderer>().startColor = Color.white;
            line.GetComponent<LineRenderer>().endColor = Color.white;
        }
    }


    public void Unlock()
    {

        mapNodeStateEnum = MapNodeStateEnum.Unlocked;
        spriteRenderer.color = baseColor;
    }
    public void Lock()
    {
        mapNodeStateEnum = MapNodeStateEnum.Locked;
        spriteRenderer.color = lockedColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mapNodeStateEnum == MapNodeStateEnum.Unlocked)
        {
            Cleared();
        }
       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mapNodeStateEnum == MapNodeStateEnum.Unlocked)
        {
            spriteRenderer.color = mouseOverColor;
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mapNodeStateEnum == MapNodeStateEnum.Unlocked)
        {
            spriteRenderer.color = baseColor;
        }
    }
}




public enum MapNodeStateEnum
{
    Locked,
    Unlocked,
    Completed
}   
public enum MapNodeTypeEnum
{
    Battle,
    Shop,
    Rest,
    elite,
    randomEvent,
    Boss,
    Tresure
}