using UnityEngine;
using DG.Tweening;

public class CameraFollowMap : MonoBehaviour
{
    [Header("Speed Settings")]
    public float keyboardSpeed = 50f;
    public float edgeSpeed = 70f;
    public float mouseEdgeThreshold = 20f;

    [Header("Vertical Clamping")]
    public float minY =0f;
    public float maxY = 32f;

    [Header("DoTween Settings")]

    public float tweenDuration = 3f; // Duration of the tween

    private float screenHeight;
    private bool canMove = false;
    private void OnEnable()
    {
        MapSceneManager.mapSceneLoaded += AnimateCamera;
    }
    private void OnDisable()
    {
        MapSceneManager.mapSceneLoaded -= AnimateCamera;
    }

    private void AnimateCamera()
    {
        screenHeight = Screen.height;
        MapNode mapNode = MapManager.instance.GetHighestUnlockedNode();
        if (mapNode == null)
        {
            Debug.LogError("No map node found");
            return;
        }
        if (mapNode.level>1)//not level 1
        {
            Vector3 startPos = transform.position;
            startPos.y = mapNode.transform.position.y;
            transform.position = startPos;
            return;
        }
        else // start of game
        {
            Vector3 startPos = transform.position;
            startPos.y = maxY;
            transform.position = startPos;

            
            transform.DOMoveY(minY, tweenDuration).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                canMove = true;
            });
        }

    }

    void Update()
    {
        if (!canMove) return;

        float moveY = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            moveY += keyboardSpeed;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            moveY -= keyboardSpeed;

        if (Input.mousePosition.y >= screenHeight - mouseEdgeThreshold)
            moveY += edgeSpeed;
        if (Input.mousePosition.y <= mouseEdgeThreshold)
            moveY -= edgeSpeed;

        Vector3 newPos = transform.position + new Vector3(0, moveY * Time.deltaTime, 0);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
        transform.position = newPos;
    }
}
