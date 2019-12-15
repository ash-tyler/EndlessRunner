using UnityEngine;

public class Platform : MonoBehaviour
{
    #region Inspector Fields
    public GameObject leftColumnPrefab;
    public GameObject rightColumnPrefab;
    public GameObject centrePrefab;
    #endregion

    public SpriteRenderer LeftColumn { get; private set; }
    public SpriteRenderer RightColumn { get; private set; }
    public SpriteRenderer Centre { get; private set; }
    public SpriteRenderer CentreFence { get; private set; }

    private int _tilesWide = 1;
    public int TilesWide
    {
        get
        {
            return _tilesWide;
        }
        set
        {
            if (_tilesWide == value)
            {
                return;
            }

            _tilesWide = value;
            SetCentreTileWidth();
            SetCollider();
        }
    }
    
    private readonly float centreSize = 2f;

    //For collider offset configuration
    private readonly float columnSize = 4.5f;
    private readonly float leftColumnOffset = 0.75f;
    private readonly float rightColumnOffset = 2.25f;


    public void CreatePlatform()
    {
        if (leftColumnPrefab.GetComponent<SpriteRenderer>() == null || rightColumnPrefab.GetComponent<SpriteRenderer>() == null || centrePrefab.GetComponent<SpriteRenderer>() == null)
        {
            this.enabled = false;
            return;
        }

        LeftColumn = Instantiate(leftColumnPrefab, transform).GetComponent<SpriteRenderer>();
        RightColumn = Instantiate(rightColumnPrefab, transform).GetComponent<SpriteRenderer>();
        Centre = Instantiate(centrePrefab, transform).GetComponent<SpriteRenderer>();

        Transform fence = Centre.transform.Find("Fence");
        CentreFence = fence.GetComponent<SpriteRenderer>();

        SetLeftColumnPos();
    }


    private void SetCentreTileWidth()
    {
        Centre.size = new Vector2(_tilesWide * centreSize, Centre.size.y);
        CentreFence.size = new Vector2(_tilesWide * centreSize, CentreFence.size.y);

        SetCentreTilePos();
    }

    public Vector3 GetPlatformEndWorldPos()
    {
        Vector3 endPos = LeftColumn.transform.position + (Vector3.right * centreSize);
        endPos += Vector3.right * (centreSize * _tilesWide);
        endPos.z = 0;

        return endPos;
    }


    #region Move Position Functions
    private void SetLeftColumnPos()
    {
        LeftColumn.transform.localPosition = Vector3.zero;
    }

    private void SetCentreTilePos()
    {
        Vector3 newPos = LeftColumn.transform.localPosition + (Vector3.right * centreSize);
        //Reset Centre position to be adjacent to LeftColumn

        if (_tilesWide > 1)
        {
            //Send the tile right to adjust for length
            newPos += Vector3.right * _tilesWide;
        }

        Centre.transform.localPosition = newPos;
        SetRightColumnPos();
    }

    private void SetRightColumnPos()
    {
        Vector3 newPos = LeftColumn.transform.localPosition + (Vector3.right * centreSize);
        newPos += Vector3.right * ((centreSize * _tilesWide) + 1);

        RightColumn.transform.localPosition = newPos;
    }
    #endregion

    private void SetCollider()
    {
        BoxCollider2D bc = LeftColumn.GetComponent<BoxCollider2D>();

        if (bc == null)
        {
            Debug.LogWarning("Platform has no BoxCollider2D");
            return;
        }

        bc.offset = new Vector2(GetColliderOffsetX(), bc.offset.y);
        bc.size = new Vector2(GetColliderSizeX(), bc.size.y);
    }

    private float GetColliderSizeX()
    {
        return (columnSize * 2) + (centreSize * _tilesWide);
    }

    private float GetColliderOffsetX()
    {
        return (leftColumnOffset + rightColumnOffset) + (_tilesWide - 1);
    }
}