using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Inspector Fields
    public float cameraMaxY = 0;
    public float cameraMinY = 0;
    public float maxDistanceDelta = 2f;
    #endregion

    void Start()
    {
        //Ensure values are not negative to avoid clamping errors
        if (cameraMaxY < 0)
        {
            cameraMaxY = 0;
            Debug.LogWarning("CameraMaxY has been set to 0 due to a negative value.");
        }
        if (cameraMinY < 0)
        {
            cameraMinY = 0;
            Debug.LogWarning("CameraMinY has been set to 0 due to a negative value.");
        }
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 newPosition = new Vector3(Player.PosX, GetClampedYPos(), transform.position.z);
        transform.position += Vector3.MoveTowards(transform.position, newPosition, maxDistanceDelta); 
    }

    private float GetClampedYPos()
    {
        return Mathf.Clamp(Player.PosY, cameraMinY, cameraMaxY);
    }
}