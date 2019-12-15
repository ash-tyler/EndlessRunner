using UnityEngine;

public class PlayerGroundDetection : MonoBehaviour
{
    public static PlayerGroundDetection Instance { get; private set; }

    public delegate void OnGroundedChangeDelegate(bool value);
    public event OnGroundedChangeDelegate OnGroundedChange;

    private bool _isGrounded;
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            if (_isGrounded != value)
            {
                _isGrounded = value;
            }

            OnGroundedChange?.Invoke(value);
        }
    }


    private void Awake()
    {
        //Ensure only one instance of a PlayerGroundDetection GameObject exists
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


    //Sets IsGrounded when a collider in the Platform layer leaves or enters the attached trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            EndState.Instance.OnEndState();
        }
        else
        {
            IsGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGrounded = false;
    }
}