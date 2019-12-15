using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestruction : MonoBehaviour
{
    #region Inspector Fields
    [Range(1, 60)]
    public float timeUntilDestroy = 20f;
    #endregion

    private void Update()
    {
        if (Time.timeSinceLevelLoad > timeUntilDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
