using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject tilePrefab;    // Reference to your tile prefab (it could be a simple colored square)
    public int gridWidth = 10;       // Width of the grid
    public int gridHeight = 10;      // Height of the grid
    public float tileSize = 1f;      // Size of each tile

    public GameObject[,] tiles;     // Array to store tile references

    public enum TileState
    {
        Explored,
        Unexplored
    }

    // A 2D array that tracks the state of each tile (explored or unexplored)
    public TileState[,] tileStates;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
	{
		tiles = new GameObject[gridWidth, gridHeight];
		tileStates = new TileState[gridWidth, gridHeight];
	
		for (int x = 0; x < gridWidth; x++)
		{
			for (int y = 0; y < gridHeight; y++)
			{
				// Offset tiles based on TileGenerator's position
				Vector3 tilePosition = transform.position + new Vector3(x * tileSize, y * tileSize, 0f);
	
				GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform); // Parent to TileGenerator
	
				tile.name = "Tile_" + x + "_" + y;
				tiles[x, y] = tile;
				tileStates[x, y] = TileState.Unexplored;
	
				UpdateTileVisualState(x, y);
			}
		}
	}
	

    // Update the tile's visual state (color or other visual feedback)
    public void UpdateTileVisualState(int x, int y)
    {
        GameObject tile = tiles[x, y];

        if (tileStates[x, y] == TileState.Explored)
        {
            // Change the color of the tile to indicate it is explored (e.g., green)
            tile.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            // Change the color of the tile to indicate it is unexplored (e.g., gray)
            tile.GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    // Method to mark a tile as explored (can be triggered by player interaction)
    public void MarkTileAsExplored(int x, int y)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
        {
            tileStates[x, y] = TileState.Explored;
            UpdateTileVisualState(x, y);
        }
    }

    // Method to reset the entire map (mark all tiles as unexplored)
    public void ResetMap()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                tileStates[x, y] = TileState.Unexplored;
                UpdateTileVisualState(x, y);
            }
        }
    }
}
