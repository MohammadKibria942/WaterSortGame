using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TubeController : MonoBehaviour
{
    public List<Color> colors = new();
    public int maxLayers = 4;

    private Image[] layerImages;

    void Awake()
    {
        // Automatically get all child Images (excluding the tube's own Image)
        layerImages = GetComponentsInChildren<Image>()
            .Where(img => img.gameObject != this.gameObject)
            .OrderBy(img => img.rectTransform.anchoredPosition.y)
            .ToArray();
    }

    public bool IsFull => colors.Count >= maxLayers;
    public bool IsEmpty => colors.Count == 0;
    public Color TopColor => IsEmpty ? Color.clear : colors[^1];

    public void AddColor(Color color)
    {
        if (IsFull) return;
        colors.Add(color);
        UpdateVisuals();
    }

    public Color RemoveTopColor()
    {
        if (IsEmpty) return Color.clear;
        Color top = colors[^1];
        colors.RemoveAt(colors.Count - 1);
        UpdateVisuals();
        return top;
    }

    public int GetPourableAmount()
    {
        if (IsEmpty) return 0;
        Color top = TopColor;
        int count = 0;
        for (int i = colors.Count - 1; i >= 0 && colors[i] == top; i--)
            count++;
        return count;
    }

    public void UpdateVisuals()
    {
        for (int i = 0; i < layerImages.Length; i++)
        {
            if (i < colors.Count)
            {
                layerImages[i].gameObject.SetActive(true);
                layerImages[i].color = colors[i];
            }
            else
            {
                layerImages[i].gameObject.SetActive(false);
            }
        }
    }
}
