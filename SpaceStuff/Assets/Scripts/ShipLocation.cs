using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipLocation : MonoBehaviour
{
    [SerializeField] // Serialized for the inspector but not directly editable
    private List<string> shipLocations; // Private to encapsulate the list

    public TileGenerator tileGenerator;  // Reference to the TileGenerator script (drag the TileGenerator in the editor)

    public List<string> ShipLocations
    {
        get { return shipLocations; } // Getter to allow read access from other classes
    }

    void Start()
    {
        // Only initialize if the list is not already populated (in case it's serialized)
        if (shipLocations == null)
        {
            shipLocations = new List<string>();
        }

        // Ensure tileGenerator is not null before starting the coroutine
        if (tileGenerator != null)
        {
            StartCoroutine(SetInitialTileColorAfterGeneration("Tile_5_5"));
        }
        else
        {
            Debug.LogError("TileGenerator is not assigned in the Inspector.");
        }
    }

    // Coroutine to wait for the TileGenerator to finish creating the map before setting the initial tile color
    IEnumerator SetInitialTileColorAfterGeneration(string tileName)
    {
        // Wait for a frame to allow TileGenerator to finish its Start method and tile instantiation
        yield return new WaitForEndOfFrame();

        // Set the initial tile (Tile_5_5) to black color after the tiles are generated
        SetInitialTileColor(tileName);

        // Add the initial tile to the ShipLocations list
        AddShipLocation(tileName);
    }

    // This method sets the initial tile color to black by looking up the tile name
    void SetInitialTileColor(string tileName)
    {
        // Find the tile by its name
        GameObject tile = GameObject.Find(tileName);

        if (tile != null)
        {
            // Set the tile's color to black (000000)
            tile.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            Debug.LogError("Tile not found: " + tileName);
        }
    }

    // This method adds a new tile to the ShipLocations list when the ship moves
    public void AddShipLocation(string newTileName)
    {
        if (!shipLocations.Contains(newTileName))  // Ensure no duplicates
        {
            shipLocations.Add(newTileName);
            Debug.Log("New tile added to ShipLocations: " + newTileName);
        }
    }
}
