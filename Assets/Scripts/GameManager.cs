using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TubeController selectedTube;

    public void OnTubeClicked(TubeController clickedTube)
    {
        if (selectedTube == null)
        {
            selectedTube = clickedTube;
            // Highlight it
        }
        else
        {
            TryPour(selectedTube, clickedTube);
            selectedTube = null;
        }
    }

    void TryPour(TubeController from, TubeController to)
    {
        if (from == to) return;

        Color colorToPour = from.TopColor;
        int amount = from.GetPourableAmount();

        if (amount == 0 || to.IsFull) return;

        if (to.IsEmpty || to.TopColor == colorToPour)
        {
            for (int i = 0; i < amount; i++)
            {
                if (to.IsFull) break;

                Color c = from.RemoveTopColor();
                to.AddColor(c);
            }

            // Check win condition here
        }
    }
}
