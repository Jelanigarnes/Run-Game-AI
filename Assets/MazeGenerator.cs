using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int mazeWidth = 10;
    public int mazeDepth = 10;
    // The prefabs for the different maze pieces
    public GameObject CenterMaze;
    public GameObject CornerMaze;
    public GameObject Beginning;
    public GameObject End;

    // The size of the maze
    public int mazeSize = 10;

    // The space between each piece
    public float spacing = 2.0f;

    // The start position of the maze
    private Vector3 startPos;

    // 2D array to keep track of where the maze pieces have been placed
    private int[,] mazeArray;

    // Keeps track of the current position in the maze
    private int currentX;
    private int currentZ;

    // Stack to keep track of the path
    private Stack<Vector2> pathStack;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the start position for the maze
        startPos = transform.position - new Vector3((mazeSize / 2) * spacing, 0, (mazeSize / 2) * spacing);

        // Initialize the maze array
        mazeArray = new int[mazeSize, mazeSize];

        // Initialize the current position
        currentX = Random.Range(0, mazeSize);
        currentZ = Random.Range(0, mazeSize);

        // Initialize the path stack
        pathStack = new Stack<Vector2>();

        // Place the starting piece
        Instantiate(Beginning, startPos + new Vector3(currentX * spacing, 0, currentZ * spacing), Quaternion.identity, transform);

        // Generate the maze
        GenerateMaze();

        // Place the end piece
        Instantiate(End, startPos + new Vector3(currentX * spacing, 0, currentZ * spacing), Quaternion.identity, transform);
    }

    // Generates the maze
    private void GenerateMaze()
    {
        // Add the current position to the stack
        pathStack.Push(new Vector2(currentX, currentZ));

        // Mark the current position in the maze array
        mazeArray[currentX, currentZ] = 1;

        // Generate the maze until the stack is empty
        while (pathStack.Count > 0)
        {
            // Get the next position
            Vector2 nextPos = GetNextPosition();

            // If there is no next position, backtrack
            if (nextPos.x == -1)
            {
                Vector2 previousPos = pathStack.Pop();
                currentX = (int)previousPos.x;
                currentZ = (int)previousPos.y;
                continue;
            }

            // Move to the next position
            currentX = (int)nextPos.x;
            currentZ = (int)nextPos.y;

            // Add the current position to the stack
            pathStack.Push(new Vector2(currentX, currentZ));

            // Mark the current position in the maze array
            mazeArray[currentX, currentZ] = 1;

            // Choose a random direction to go in
            int direction = Random.Range(0, 4);

            // Place the center maze piece in the chosen direction
            switch (direction)
            {
                case 0:
                    Instantiate(CenterMaze, startPos + new Vector3((currentX + 1) * spacing, 0, currentZ * spacing), Quaternion.Euler(0, 90, 0), transform);
                    break;
                case 1:
                    Instantiate(CenterMaze, startPos + new Vector3(currentX * spacing, 0, (currentZ + 1) * spacing), Quaternion.identity, transform);
                    break;
                case 2:
                    Instantiate(CenterMaze, startPos + new Vector3((currentX - 1) * spacing, 0, currentZ * spacing), Quaternion.Euler(0, 90, 0), transform);
                    break;
                case 3:
                    Instantiate(CenterMaze, startPos + new Vector3(currentX * spacing, 0, (currentZ - 1) * spacing), Quaternion.identity, transform);
                    break;
            }

            // Place the corner maze piece in the chosen direction
            if (direction == 0)
            {
                Instantiate(CornerMaze, startPos + new Vector3(currentX * spacing, 0, (currentZ + 1) * spacing), Quaternion.Euler(0, 90, 0), transform);
                Instantiate(CornerMaze, startPos + new Vector3(currentX * spacing, 0, (currentZ - 1) * spacing), Quaternion.Euler(0, 90, 0), transform);
            }
            else if (direction == 1)
            {
                Instantiate(CornerMaze, startPos + new Vector3((currentX + 1) * spacing, 0, currentZ * spacing), Quaternion.Euler(0, 180, 0), transform);
                Instantiate(CornerMaze, startPos + new Vector3((currentX - 1) * spacing, 0, currentZ * spacing), Quaternion.Euler(0, 180, 0), transform);
            }
            else if (direction == 2)
            {
                Instantiate(CornerMaze, startPos + new Vector3(currentX * spacing, 0, (currentZ + 1) * spacing), Quaternion.Euler(0, 270, 0), transform);
                Instantiate(CornerMaze, startPos + new Vector3(currentX * spacing, 0, (currentZ - 1) * spacing), Quaternion.Euler(0, 270, 0), transform);
            }
            else if (direction == 3)
            {
                Instantiate(CornerMaze, startPos + new Vector3((currentX + 1) * spacing, 0, currentZ * spacing), Quaternion.identity, transform);
                Instantiate(CornerMaze, startPos + new Vector3((currentX - 1) * spacing, 0, currentZ * spacing), Quaternion.identity, transform);
            }
        }
    }

    // Gets the next position to move to
    private Vector2 GetNextPosition()
    {
        // Create a list of possible next positions
        List<Vector2> nextPositions = new List<Vector2>();
        if (currentX + 1 < mazeWidth && mazeArray[currentX + 1, currentZ] == 0)
        {
            nextPositions.Add(new Vector2(currentX + 1, currentZ));
        }
        if (currentX - 1 >= 0 && mazeArray[currentX - 1, currentZ] == 0)
        {
            nextPositions.Add(new Vector2(currentX - 1, currentZ));
        }
        if (currentZ + 1 < mazeDepth && mazeArray[currentX, currentZ + 1] == 0)
        {
            nextPositions.Add(new Vector2(currentX, currentZ + 1));
        }
        if (currentZ - 1 >= 0 && mazeArray[currentX, currentZ - 1] == 0)
        {
            nextPositions.Add(new Vector2(currentX, currentZ - 1));
        }

        // If there are no possible next positions, return the current position
        if (nextPositions.Count == 0)
        {
            pathStack.Clear();
        }

        // Choose a random next position from the list
        int nextIndex = Random.Range(0, nextPositions.Count);
        return nextPositions[nextIndex];
    }
}

