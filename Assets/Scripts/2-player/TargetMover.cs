using System.Collections; // Allows using coroutines
using System.Collections.Generic; // Provides support for generic collections like List
using UnityEngine; // Core Unity engine functionalities
using UnityEngine.Tilemaps; // Tilemap-related functionalities

/**
 * This component moves its object towards a given target position.
 */
public class TargetMover : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null; // The tilemap the object moves on
    [SerializeField] AllowedTiles allowedTiles = null; // Reference to allowed tiles for movement

    [Tooltip("The speed by which the object moves towards the target, in meters (=grid units) per second")]
    [SerializeField] float speed = 2f; // Movement speed in grid units per second

    [Tooltip("Maximum number of iterations before BFS algorithm gives up on finding a path")]
    [SerializeField] int maxIterations = 1000; // BFS iteration limit for pathfinding

    [Tooltip("The target position in world coordinates")]
    [SerializeField] Vector3 targetInWorld; // The target position in world coordinates

    [Tooltip("The target position in grid coordinates")]
    [SerializeField] Vector3Int targetInGrid; // The target position in grid coordinates

    private int one = 1;
    protected bool atTarget; // Tracks whether the object has reached the target

    public void SetTarget(Vector3 newTarget)
    { // Updates the target position
        if (targetInWorld != newTarget)
        { // Checks if the target has changed
            targetInWorld = newTarget; // Updates the target in world coordinates
            targetInGrid = tilemap.WorldToCell(targetInWorld); // Converts world to grid coordinates
            atTarget = false; // Resets the target status
        }
    }

    public Vector3 GetTarget()
    { // Returns the current target in world coordinates
        return targetInWorld; // Returns the target
    }

    private TilemapGraph tilemapGraph = null; // Graph representation of the tilemap
    private float timeBetweenSteps; // Time delay between movement steps

    protected virtual void Start()
    { // Initialization of the component
        tilemapGraph = new TilemapGraph(tilemap, allowedTiles.Get()); // Creates the tilemap graph
        timeBetweenSteps = one / speed; // Calculates delay based on speed
        StartCoroutine(MoveTowardsTheTarget()); // Starts the movement coroutine
    }

    IEnumerator MoveTowardsTheTarget()
    { // Coroutine for continuous movement
        for (; ; )
        { // Infinite loop for the coroutine
            yield return new WaitForSeconds(timeBetweenSteps); // Waits for the next movement step
            if (enabled && !atTarget) // Checks if movement is allowed
                MakeOneStepTowardsTheTarget(); // Makes one step towards the target
        }
    }

    private void MakeOneStepTowardsTheTarget()
    { // Executes a single step towards the target
        Vector3Int startNode = tilemap.WorldToCell(transform.position); // Gets the current grid position
        Vector3Int endNode = targetInGrid; // Gets the target grid position
        List<Vector3Int> shortestPath = BFS.GetPath(tilemapGraph, startNode, endNode, maxIterations); // Finds the shortest path
        Debug.Log("shortestPath = " + string.Join(" , ", shortestPath)); // Logs the path for debugging
        if (shortestPath.Count >= one + one)
        { // Checks if a valid path exists
            Vector3Int nextNode = shortestPath[1]; // Gets the next step in the path
            transform.position = tilemap.GetCellCenterWorld(nextNode); // Moves to the next tile
        }
        else
        {
            if (shortestPath.Count == one - one)
            { // Checks if no path is found
                // Uncomment for error logging:
                // Debug.LogError($"No path found between {startNode} and {endNode}");
            }
            atTarget = true; // Marks the target as reached
        }
    }
}
