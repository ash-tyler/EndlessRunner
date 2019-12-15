using UnityEngine;

public class PickUp : MonoBehaviour
{
    #region Inspector Fields
    public int score = 20;
    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.Instance.Score += score;
        Destroy(gameObject);
    }
}