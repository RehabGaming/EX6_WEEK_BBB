using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component manages a list of allowed tiles.
 * The list is used for pathfinding and movement.
 */
public class AllowedTiles : MonoBehaviour
{
    // A list of tiles that are allowed for movement or pathfinding.
    [SerializeField] private List<TileBase> allowedTiles = new List<TileBase>();

    // Checks if the given tile is in the list of allowed tiles.
    public bool Contains(TileBase tile)
    {
        return allowedTiles.Contains(tile);
    }

    // Adds a tile to the list if it's not already in the list.
    public void Add(TileBase tile)
    {
        if (!allowedTiles.Contains(tile))
        {
            allowedTiles.Add(tile);
        }
    }

    // Returns all allowed tiles as an array.
    public TileBase[] Get()
    {
        return allowedTiles.ToArray();
    }
}
