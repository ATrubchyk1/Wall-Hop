using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ColorProviderExtensions
{
    public static Color GetRandomColor(this ColorProvider colorProvider, Color except)
    {
        var availableColors = colorProvider.Colors.Where(color => color != except).ToList();
        var randomIndex = Random.Range(0, availableColors.Count);
        return availableColors[randomIndex];
    }
    
    public static Color GetRandomColor(this ColorProvider colorProvider)
    {
        var randomIndex = Random.Range(0, colorProvider.Colors.Count);
        var color = colorProvider.Colors[randomIndex];
        return color;
    }
}
