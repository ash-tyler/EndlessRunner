using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    #region Inspector Fields
    public Platform platformPrefab;
    [Space]
    public int startPosX = 0;
    public int startPosY = 0;
    [Space]
    public int numberToSpawn = 5;
    [Space]
    public int maxPlatformHeight = 30;
    public int minPlatformHeight = 13;
    [Space]
    public int minGap = 1;
    public int maxGap = 10;
    [Space]
    [Tooltip("Length excluding columns- maximum amount of centre tiles per platform")]
    public int maxLength = 10;
    [Tooltip("Length excluding columns- maximum amount of centre tiles per platform")]
    public int minLength = 1;
    [Space]
    [Tooltip("Multiplies how many far each Platform should be behind Player on the X pos before moving to front")]
    public float bufferMultiplier = 2f;
    #endregion

    private Queue<Platform> platforms = new Queue<Platform>();
    private Vector3 nextPosition;

    void Start()
    {
        nextPosition = new Vector3(startPosX, startPosY, 0);

        if (platformPrefab == null)
        {
            return;
        }

        for (int i = numberToSpawn; i > 0; i--)
        {
            Platform plat = Instantiate(platformPrefab, transform);
            plat.CreatePlatform();
            platforms.Enqueue(plat);
        }
        for (int i = numberToSpawn; i > 0; i--)
        {
            MoveNextPlatformInQueue();
        }
    }


    private void Update()
    {
        if (NextQueuedIsBehindBuffer())
        {
            MoveNextPlatformInQueue();
        }
    }


    private void MoveNextPlatformInQueue()
    {
        Platform plat = platforms.Dequeue();
        plat.transform.localPosition = nextPosition;
        plat.TilesWide = Random.Range(minLength, maxLength);
        platforms.Enqueue(plat);

        nextPosition.x = plat.GetPlatformEndWorldPos().x + GetNewGap();
        nextPosition.y = Random.Range(minPlatformHeight, maxPlatformHeight);
    }

    private bool NextQueuedIsBehindBuffer()
    {
        if (Player.Instance == null || platforms.Count == 0)
        {
            return false;
        }

        float buffer = maxLength * bufferMultiplier;
        return platforms.Peek().GetPlatformEndWorldPos().x < (Player.Instance.GetWorldPosX() - buffer);
    }

    private int GetNewGap()
    {
        return Random.Range(minGap, maxGap);
    }
}