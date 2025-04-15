using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour
{
    // List to store the colour layers
    public List<Color> colors = new List<Color>();  
    // Maximum number of colour layers the bottle can hold (e.g., 4)
    public int capacity = 4;    

    // Reference to the GameManager to notify when this bottle is clicked
    public GameManager gameManager;     

    // This function is called when the bottle is clicked
    private void OnMouseDown()
    {
        // Send this bottle to the GameManager's click handler
        if (gameManager != null)
        {
            gameManager.BottleClicked(this);
        }
    }

    // Check if the bottle is empty
    public bool IsEmpty()
    {
        return colors.Count == 0;
    }

    // Check if the bottle is full
    public bool IsFull()
    {
        return colors.Count >= capacity;
    }

    // Get the top colour (last element), or clear if empty
    public Color GetTopColor()
    {
        if (!IsEmpty())
            return colors[colors.Count - 1];
        else
            return Color.clear;  // represents "no colour"
    }

    // Pour from this bottle into the target bottle
    // Returns the number of layers successfully poured
    public int PourInto(BottleController targetBottle)
    {
        if (IsEmpty())
            return 0; // nothing to pour

        // Get the top colour to pour
        Color topColor = GetTopColor();

        // Count how many same-colour layers on top can be poured
        int countToPour = 0;
        for (int i = colors.Count - 1; i >= 0; i--)
        {
            if (colors[i] == topColor)
                countToPour++;
            else
                break;
        }

        // Calculate available space in the target bottle
        int availableSpace = targetBottle.capacity - targetBottle.colors.Count;
        int amountToPour = Mathf.Min(countToPour, availableSpace);

        // If the target is not empty, check if its top colour matches the topColor
        if (!targetBottle.IsEmpty() && targetBottle.GetTopColor() != topColor)
        {
            return 0; // invalid pour
        }

        // Pour the allowed amount of colour layers
        for (int i = 0; i < amountToPour; i++)
        {
            targetBottle.colors.Add(topColor);
            colors.RemoveAt(colors.Count - 1);
        }

        // (Optional) Trigger animations for a smooth visual transition here

        return amountToPour;
    }

    // (Optional) This function could highlight the bottle when selected.
    public void Highlight(bool flag)
    {
        // A simple way could be to change the sprite's colour tint to show selection.
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = flag ? Color.yellow : Color.white;
        }
    }
}