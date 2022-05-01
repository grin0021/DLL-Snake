using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject TilePrefab;
    public Color CellColor = Color.grey;
    public Color SnakeColor = Color.green;
    public Color AppleColor = Color.red;
    public int GridSize = 20;
    public int CellSize = 5;
    public static Grid Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        m_GridSpan = GridSize * CellSize;
        m_Tiles = new GameObject[GridSize, GridSize];
        m_TileCache = new List<IntVector2>();

        for (int y = 0; y < m_Tiles.GetLength(1); ++y)
        {
            for (int x = 0; x < m_Tiles.GetLength(0); ++x)
            {
                int xPos = x * CellSize;
                int yPos = y * CellSize;
                GameObject gridPiece = Instantiate(
                    TilePrefab, 
                    new Vector3(xPos + CellSize / 2.0f, yPos + CellSize / 2.0f, 0), 
                    Quaternion.identity
                    );
                gridPiece.GetComponent<SpriteRenderer>().color = CellColor;
                m_Tiles[x, y] = gridPiece;
                gridPiece.transform.parent = transform;
            }
        }

        m_AppleIndex = new IntVector2(0, 0);
        SpawnApple();
    }

    void ClearGrid()
    {
        foreach (IntVector2 index in m_TileCache)
        {
            m_Tiles[index.x, index.y].GetComponent<SpriteRenderer>().color = CellColor;
        }

        m_TileCache.Clear();
    }

    public void DrawSnake(IntVector2[] bodyPieces, int numberOfPieces)
    {
        ClearGrid();

        for (int i = 0; i < numberOfPieces; ++i)
        {
            if(IsIndexInAGrid(bodyPieces[i]))
            {
                m_Tiles[bodyPieces[i].x, bodyPieces[i].y].GetComponent<SpriteRenderer>().color = SnakeColor;
                m_TileCache.Add(bodyPieces[i]);
            }
        }
    }

    bool IsIndexInAGrid(IntVector2 index)
    {
        return (index.x >= 0 && index.x < GridSize && index.y >= 0 && index.y < GridSize);
    }

    public void SpawnApple()
    {
        m_Tiles[m_AppleIndex.x, m_AppleIndex.y].GetComponent<SpriteRenderer>().color = CellColor;
        m_AppleIndex = new IntVector2(UnityEngine.Random.Range(0, GridSize), UnityEngine.Random.Range(0, GridSize));
        m_Tiles[m_AppleIndex.x, m_AppleIndex.y].GetComponent<SpriteRenderer>().color = AppleColor;
    }

    public IntVector2 GetAppleIndex()
    {
        return m_AppleIndex;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < GridSize; i++)
        {
            Gizmos.DrawLine(new Vector2(i * CellSize, 0), new Vector2(i * CellSize, m_GridSpan));
            Gizmos.DrawLine(new Vector2(0, i * CellSize), new Vector2(m_GridSpan, i * CellSize));
        }
    }

    GameObject[,] m_Tiles;
    int m_GridSpan;
    IntVector2 m_AppleIndex;
    List <IntVector2> m_TileCache;
}
