using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public static float DistanceRan { get; private set; } = 0;
    public static float Score { get; private set; } = 0;

    public static float PosX { get { return (Instance != null) ? Instance.transform.localPosition.x : 0f; } }
    public static float PosY { get { return (Instance != null) ? Instance.transform.localPosition.y : 0f; } }


    private void Awake()
    {
        //Ensure only one instance of a Player GameObject exists
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}