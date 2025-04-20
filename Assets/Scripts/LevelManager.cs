using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject tubePrefab;
    public Transform tubesParent;

    public List<Color> availableColors;
    public List<List<Color>> levelData;

    void Start()
    {
        GenerateLevel();
    }

    void Awake()
    {
        availableColors = new List<Color> { Color.red, Color.blue, Color.green };

        levelData = new List<List<Color>> {
        new List<Color> { Color.red, Color.blue, Color.red, Color.green },
        new List<Color> { Color.green, Color.blue, Color.red, Color.blue },
        new List<Color>(),
        new List<Color>()
    };
    }


    void GenerateLevel()
    {
        for (int i = 0; i < levelData.Count; i++)
        {
            GameObject tubeObj = Instantiate(tubePrefab, tubesParent);
            tubeObj.transform.localPosition = new Vector3(i * 2.0f, 0, 0); // Spaced layout

            TubeController tube = tubeObj.GetComponent<TubeController>();
            foreach (Color c in levelData[i])
            {
                tube.colors.Add(c); // Just add to the list directly
            }
            tube.UpdateVisuals(); // Only call this once, after setup
        }
    }
}
