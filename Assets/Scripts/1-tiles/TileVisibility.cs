using UnityEngine;
using UnityEngine.Tilemaps;

public class TileVisibility : MonoBehaviour
{
    [SerializeField] private Tilemap gameTilemap; // Represents the tilemap of the game
    [SerializeField] private Transform player; // The player's position (Transform of the player character)
    [SerializeField] private TileBase[] mountainTiles; // Array of tiles defined as "mountains"
    [SerializeField] private float proximityDistance = 2f; // The distance within which the player is considered "close" to the mountain

    private Vector3 tileOffset = new Vector3(0.5f, 0.5f, 0); // Offset to center the tile in the world position
    private float angleThreshold = -0.5f; // Threshold for determining if a tile is "behind" the mountain

    private void Update()
    {
        // Iterate through all tiles in the tilemap
        foreach (Vector3Int position in gameTilemap.cellBounds.allPositionsWithin)
        {
            // Calculate the world position of the current tile (based on the game's coordinate system)
            Vector3 worldPosition = gameTilemap.CellToWorld(position) + tileOffset;

            // Get the current tile at this position
            TileBase currentTile = gameTilemap.GetTile(position);

            // Check if the tile exists and is a "mountain" tile
            if (currentTile != null && IsMountainTile(currentTile))
            {
                // Check if the player is close to the current tile
                if (Vector3.Distance(player.position, worldPosition) < proximityDistance)
                {
                    // Call the function to hide tiles behind the mountain
                    HideTilesBehindMountain(position);
                }
            }
        }
    }

    // Function to hide all tiles "behind" the mountain relative to the player's position
    private void HideTilesBehindMountain(Vector3Int mountainPosition)
    {
        // Calculate the world position of the mountain tile
        Vector3 mountainWorldPosition = gameTilemap.CellToWorld(mountainPosition) + tileOffset;

        // Get the player's world position
        Vector3 playerWorldPosition = player.position;

        // Calculate the direction from the mountain to the player
        Vector3 direction = (playerWorldPosition - mountainWorldPosition).normalized;

        // Iterate through all tiles in the tilemap
        foreach (Vector3Int pos in gameTilemap.cellBounds.allPositionsWithin)
        {
            // Calculate the world position of the current tile
            Vector3 worldPos = gameTilemap.CellToWorld(pos) + tileOffset;

            // Check if the tile is "behind" the mountain relative to the player's direction
            if (IsBehindMountain(worldPos, mountainWorldPosition, direction))
            {
                // Remove tile flags and set its color to black
                gameTilemap.SetTileFlags(pos, TileFlags.None);
                gameTilemap.SetColor(pos, Color.black);
            }
            else
            {
                // If the tile is not hidden, restore its original color (white)
                ShowTile(pos);
            }
        }
    }

    // Function that returns true if the tile is "behind" the mountain
    private bool IsBehindMountain(Vector3 tilePosition, Vector3 mountainPosition, Vector3 direction)
    {
        // Calculate the direction from the mountain to the tile
        Vector3 toTile = (tilePosition - mountainPosition).normalized;

        // Calculate the angle between the player's direction and the direction to the tile using the Dot Product
        float angle = Vector3.Dot(direction, toTile);

        // Return true if the angle is below the threshold
        return angle < angleThreshold;
    }

    // Function to show the tile (restore its original color)
    private void ShowTile(Vector3Int position)
    {
        // Remove tile flags and set its color to white
        gameTilemap.SetTileFlags(position, TileFlags.None);
        gameTilemap.SetColor(position, Color.white);
    }

    // Function to check if a specific tile belongs to the "mountain" type
    private bool IsMountainTile(TileBase tile)
    {
        // Iterate through all tiles defined as "mountains" and check if the tile matches
        foreach (TileBase mountainTile in mountainTiles)
        {
            if (tile == mountainTile)
            {
                return true;
            }
        }
        return false; // Return false if the tile does not belong to the "mountain" type
    }
}
