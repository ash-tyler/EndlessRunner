using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int NumberInStock { get; private set; } = 0;


    public void AddAmount(int amountToAdd)
    {
        NumberInStock += amountToAdd;
    }

    public void DropAll()
    {
        NumberInStock = 0;
    }

    public void DropAmount(int amountToDrop)
    {
        NumberInStock -= amountToDrop;

        if (NumberInStock < 0)
        {
            NumberInStock = 0;
        }
    }
}