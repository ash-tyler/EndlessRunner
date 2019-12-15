using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Inspector Fields
    public Text scoreBox = null;
    #endregion

    public static Player Instance { get; private set; }

    public static float DistanceRan { get; private set; } = 0;
    public float Score { get; set; } = 0;

    public RunningBehaviour Running { get; private set; }
    public JumpBehaviour Jumping { get; private set; }

    public static float PosX { get { return (Instance != null) ? Instance.transform.localPosition.x : 0f; } }
    public static float PosY { get { return (Instance != null) ? Instance.transform.localPosition.y : 0f; } }


    private void Start()
    {
        Running = GetComponent<RunningBehaviour>();
        Jumping = GetComponent<JumpBehaviour>();
    }

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

    private void Update()
    {
        scoreBox.text = Score.ToString();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public float GetWorldPosX()
    {
        return transform.position.x;
    }
}