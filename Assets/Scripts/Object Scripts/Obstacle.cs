using UnityEngine;

public class Obstacle : MonoBehaviour
{
    #region Inspector Fields
    public int scoreReduction = 20;
    public bool isEndStateObject = false;
    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    { 
        Player.Instance.Score -= scoreReduction;

        if (isEndStateObject)
        {
            EndState.Instance.OnEndState();
        }

        Destroy(gameObject);
    }
}