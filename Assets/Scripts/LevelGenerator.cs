using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [Header("Bottle Settings")]
    public GameObject bottlePrefab;         // Assign your Bottle prefab via the Inspector.
    public Transform bottlesParent;         // A parent container for the bottles (helps keep the hierarchy tidy).
    public int bottleCapacity = 4;          // Number of colour layers each bottle can hold.

    [Header("Level Settings")]
    public int numColors = 6;               // Number of distinct colours to use in the level.
    public int extraEmptyBottles = 2;       // Extra bottles that start empty (common in water sort puzzles).

    [Header("Colour Options")]
    public List<Color> availableColors;     // Populate this list in the Inspector with at least 'numColors' colours.

    // (Optional) List to keep references to all instantiated bottles
    private List<BottleController> allBottles = new List<BottleController>();

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        // Clear any existing bottles (e.g. if restarting the level)
        foreach (Transform child in bottlesParent)
        {
            Destroy(child.gameObject);
        }
        allBottles.Clear();
        
        // Calculate the number of bottles that will be filled
        int filledBottleCount = numColors;  // One bottle per distinct colour block
        
        // Total bottles in the level (filled bottles + extra empty ones)
        int totalBottles = filledBottleCount + extraEmptyBottles;

        // Create the list of colour blocks
        List<Color> colorBlocks = new List<Color>();
        for (int i = 0; i < numColors; i++)
        {
            // Each colour repeats 'bottleCapacity' times
            for (int j = 0; j < bottleCapacity; j++)
            {
                colorBlocks.Add(availableColors[i]);
            }
        }
        
        // Shuffle the colour blocks to randomize the starting layout
        Shuffle(colorBlocks);
        
        // Create filled bottles and assign colour blocks to them
        for (int i = 0; i < filledBottleCount; i++)
        {
            GameObject bottleObj = Instantiate(bottlePrefab, bottlesParent);
            BottleController controller = bottleObj.GetComponent<BottleController>();
            controller.capacity = bottleCapacity;
            controller.colors = new List<Color>();
            
            // Each bottle receives exactly 'bottleCapacity' colour blocks.
            for (int j = 0; j < bottleCapacity; j++)
            {
                int index = i * bottleCapacity + j;
                controller.colors.Add(colorBlocks[index]);
            }
            allBottles.Add(controller);
        }
        
        // Create extra empty bottles
        for (int i = 0; i < extraEmptyBottles; i++)
        {
            GameObject bottleObj = Instantiate(bottlePrefab, bottlesParent);
            BottleController controller = bottleObj.GetComponent<BottleController>();
            controller.capacity = bottleCapacity;
            controller.colors = new List<Color>();  // Start with an empty list
            allBottles.Add(controller);
        }
    }
    
    // Fisher-Yates shuffle implementation to randomize the colour blocks
    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
