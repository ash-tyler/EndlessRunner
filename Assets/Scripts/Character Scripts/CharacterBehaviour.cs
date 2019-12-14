using UnityEngine;

public abstract class CharacterBehaviour : MonoBehaviour
{
    protected Animator anim = null;
    protected PickUp pickUps = null;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    protected virtual void ApplyPickup() { }
}