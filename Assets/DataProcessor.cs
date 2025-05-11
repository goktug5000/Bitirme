using System;
using System.Collections.Generic;

public class DataProcessor
{
    private static readonly Dictionary<string, float> DistanceValueMap = new Dictionary<string, float>
    {
        { "SoClose", 0 },
        { "Close",    0.5f },
        { "Far",      1 }
    };

    private static readonly Dictionary<char, (float x, float y)> DirectionMap = new Dictionary<char, (float, float)>
    {
        { 'A', (1f,  0f) },
        { 'S', (-1f, 0f) },
        { 'D', (0f,  1f) },
        { 'O', (0f, -1f) }
    };

    private float alphaDistance;
    private float alphaDirection;

    private float emaDistance;
    private (float x, float y) emaDirection;

    public DataProcessor(
        float alphaDistance = 0.2f,
        float alphaDirection = 0.2f
        )
    {
        this.alphaDistance = alphaDistance;
        this.alphaDirection = alphaDirection;

        this.emaDistance = 1;
        this.emaDirection = (0, 0);
    }

    public static float GetDistanceValue(string input)
    {
        if (DistanceValueMap.TryGetValue(input, out float value))
        {
            return value;
        }
        return 0f;
    }

    public float UpdateDistanceEma(string input)
    {
        float value = GetDistanceValue(input);
        emaDistance = (value * alphaDistance) + (emaDistance * (1 - alphaDistance));
        return emaDistance;
    }


    public float GetCurrentDistanceEma()
    {
        return emaDistance;
    }

    public (float x, float y) UpdateDirectionEma(string input)
    {
        float decay = 1.0f;
        float decayFactor = 0.7f;

        float sumX = 0f;
        float sumY = 0f;

        foreach (char c in input)
        {
            if (DirectionMap.TryGetValue(c, out var vec))
            {
                sumX += vec.x * decay;
                sumY += vec.y * decay;
                decay *= decayFactor;
            }
        }

        if (sumX == 0f)
            emaDirection.x *= (1 - alphaDirection);
        else
            emaDirection.x = (sumX * alphaDirection) + (emaDirection.x * (1 - alphaDirection));

        if (sumY == 0f)
            emaDirection.y *= (1 - alphaDirection);
        else
            emaDirection.y = (sumY * alphaDirection) + (emaDirection.y * (1 - alphaDirection));

        return emaDirection;
    }
}