using UnityEngine;

public class RunningBehaviour : ControllableCharacterBehaviour
{
    #region Inspector Fields
    public float speed = 4;
    #endregion


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        canAct = true;
    }

    void Update()
    {
        if (canAct)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }


    public void FreezeMovement()
    {
        anim.SetBool("Idle", true);
        canAct = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    public void UnFreezeMovement()
    {
        anim.SetBool("Idle", false);
        canAct = true;
        rb.isKinematic = false;
    }
}