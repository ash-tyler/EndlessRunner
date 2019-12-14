using UnityEngine;

public class TouchArcadeButton : MonoBehaviour
{
    #region Inspector Fields
    public RectTransform buttonRect = null;
    public bool holdHasFunctionality = false;
    #endregion


    private void Start()
    {
        Input.simulateMouseWithTouches = true;

        if (buttonRect == null)
        {
            Debug.LogWarning("TouchArcadeButtons cannot function without a non-null RectTransform. Please set it in the inspector to use this script");
            this.enabled = false;
        }
    }


    //Returns whether a touch is being held within the Button Area, but only when holdHasFunctionality is true. Otherwise returns GetButtonDown
    public bool GetButton
    {
        get
        {
            if (holdHasFunctionality)
            {
                return (Input.GetMouseButton(0) && TouchIsWithinRect());
            }
            else
            {
                return GetButtonDown;
            }
        }
    }

    //Returns true during the frame the user touched the screen within the Button Area
    public bool GetButtonDown
    {
        get
        {
            return (Input.GetMouseButtonDown(0) && TouchIsWithinRect());
        }
    }

    //Returns true during the frame the user releases a touch within the Button Area
    public bool GetButtonUp
    {
        get
        {
            return (Input.GetMouseButtonUp(0) && TouchIsWithinRect());
        }
    }


    //Returns whether the most recent touch (if any) was within the Button Area
    private bool TouchIsWithinRect()
    {
        //if (Input.touchCount <= 0)
        //{
        //    return false;
        //}

        Vector2 localTouchPosition = buttonRect.InverseTransformPoint(Input.mousePosition);
        return buttonRect.rect.Contains(localTouchPosition);
    }

    //Note: GetMouseButton and variations are compatible with touchscreen input so long as Input.simulateMouseWithTouches is enabled
}