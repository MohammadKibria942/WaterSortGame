using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Reference for the selected source bottle; null means no bottle is selected.
    public BottleController selectedBottle;  

    // Method called by a BottleController when it's clicked
    public void BottleClicked(BottleController bottle)
    {
        // If no bottle is currently selected, select this one if it's not empty
        if (selectedBottle == null)
        {
            if (!bottle.IsEmpty())
            {
                selectedBottle = bottle;
                bottle.Highlight(true); // highlight source selection
            }
        }
        else
        {
            // If the same bottle is clicked again, deselect it
            if (selectedBottle == bottle)
            {
                selectedBottle.Highlight(false);
                selectedBottle = null;
                return;
            }

            // Treat the new bottle as the target for pouring
            if (IsPourValid(selectedBottle, bottle))
            {
                int pouredCount = selectedBottle.PourInto(bottle);
                Debug.Log("Poured " + pouredCount + " layers.");
                // Update visuals, animations, etc. here if required.

                // (Optional) Check for win condition after the pour.
                CheckWinCondition();
            }
            else
            {
                Debug.Log("Invalid pour. Try another bottle.");
                // Optionally add a shake animation or sound effect
            }

            // Clear the selected bottle for the next move
            selectedBottle.Highlight(false);
            selectedBottle = null;
        }
    }

    // Validate if pouring from source to target is allowed.
    public bool IsPourValid(BottleController source, BottleController target)
    {
        if (source.IsEmpty())
            return false;
        if (target.IsFull())
            return false;
        // If target bottle is empty, any pour is allowed.
        if (target.IsEmpty())
            return true;
        // Otherwise, ensure the top colour matches.
        return source.GetTopColor() == target.GetTopColor();
    }

    // A simple check to see if the level is complete.
    private void CheckWinCondition()
    {
        // You might have a LevelGenerator or maintain a list of all bottles.
        // For simplicity, suppose you call GameObject.FindObjectsOfType to get all BottleControllers.
        BottleController[] bottles = FindObjectsOfType<BottleController>();

        foreach (BottleController bottle in bottles)
        {
            // A complete bottle is either empty or full with only one colour.
            if (!bottle.IsEmpty())
            {
                // Check if all layers are the same
                Color topColor = bottle.GetTopColor();
                foreach (Color color in bottle.colors)
                {
                    if (color != topColor || bottle.colors.Count != bottle.capacity)
                    {
                        return; // Found a bottle that's not complete
                    }
                }
            }
        }
        // If all bottles pass, the level is complete.
        Debug.Log("Level Complete!");
        // Trigger level complete UI or transition to next level here.
    }
}