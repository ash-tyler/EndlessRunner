using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollManager : MonoBehaviour
{
    #region Inspector Fields
    public GameObject objPrefab;
    public Vector3 startPos;
    public int numberToSpawn = 4;
    [Tooltip("Multiplies how many backgroundWidths each object should be behind Player on the X pos before moving to front")]
    public float bufferMultiplier = 2f;
    #endregion

    private Queue<SpriteRenderer> backgrounds = new Queue<SpriteRenderer>();
    private Vector3 nextPosition;

    private bool pause = false;


    private void Start()
    {
        if (objPrefab == null || !objPrefab.GetComponent<SpriteRenderer>())
        {
            return;
        }

        nextPosition = startPos;

        for (int i = numberToSpawn; i > 0 ; i--)
        {
            backgrounds.Enqueue(Instantiate(objPrefab, transform).GetComponent<SpriteRenderer>());
        }
        for (int i = numberToSpawn; i > 0; i--)
        {
            MoveNextInQueueToFront();
        }
    }

    private void Update()
    {
        if (pause)
        {
            return;
        }

        if (NextQueuedIsBehindBuffer())
        {
            MoveNextInQueueToFront();
        }
    }

    //Toggle pause
    public void Pause()
    {
        pause = !pause;
    }

    private void MoveNextInQueueToFront()
    {
        SpriteRenderer objToMove = backgrounds.Dequeue();
        objToMove.transform.localPosition = nextPosition;
        backgrounds.Enqueue(objToMove);

        nextPosition.x += objToMove.bounds.size.x;
    }

    private bool NextQueuedIsBehindBuffer()
    {
        if (Player.Instance == null || backgrounds.Count == 0)
        {
            return false;
        }

        float buffer = backgrounds.Peek().bounds.size.x * bufferMultiplier;

        return backgrounds.Peek().transform.position.x < (Player.Instance.GetWorldPosX() - buffer);
    }
}