using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Inspector Fields
    public float cameraMaxY = 0;
    public float cameraMinY = 0;

    public Vector3 offsetFromPlayer = Vector3.zero;
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
        Vector3 targetPos = new Vector3(Player.PosX, GetClampedYPos(Player.PosY), 0) + offsetFromPlayer;
        targetPos.y = GetClampedYPos(targetPos.y);
        transform.localPosition = targetPos;
    }

    private float GetClampedYPos(float yPos)
    {
        return Mathf.Clamp(yPos, cameraMinY, cameraMaxY);
    }
}