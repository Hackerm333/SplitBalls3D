

using System;

public enum LerpType
{
    Liner,
    EaseInQuad,
    EaseOutQuad,
    EaseInOutQuad,
    EaseInCubic,
    EaseOutCubic,
    EaseInOutCubic,
    EaseInQuart,
    EaseOutQuart,
    EaseInOutQuart,
    EaseInQuint,
    EaseOutQuint,
    EaseInOutQuint,
    EaseInElastic,
    EaseOutElastic,
    EaseInOutElastic,
}



public class EasyType
{

    public static float Liner(float t)
    {
        return t;
    }

    public static float EaseInQuad(float t)
    {
        return t * t;
    }
    public static float EaseOutQuad(float t)
    {
        return t * (2 - t);
    }
    public static float EaseInOutQuad(float t)
    {
        return t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;
    }
    public static float EaseInCubic(float t)
    {
        return t * t * t;
    }
    public static float EaseOutCubic(float t)
    {
        return (--t) * t * t + 1;
    }
    public static float EaseInOutCubic(float t)
    {
        return t < 0.5f ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
    }
    public static float EaseInQuart(float t)
    {
        return t * t * t * t;
    }
    public static float EaseOutQuart(float t)
    {
        return 1 - (--t) * t * t * t;
    }
    public static float EaseInOutQuart(float t)
    {
        return t < 0.5f ? 8 * t * t * t * t : 1 - 8 * (--t) * t * t * t;
    }
    public static float EaseInQuint(float t)
    {
        return t * t * t * t * t;
    }
    public static float EaseOutQuint(float t)
    {
        return 1 + (--t) * t * t * t * t;
    }

    public static float EaseInOutQuint(float t)
    {
        return t < 0.5f ? 16 * t * t * t * t * t : 1 + 16 * (--t) * t * t * t * t;
    }

    public static float EaseInElastic(float t)
    {
        return (float)((0.04f - 0.04f / t) * Math.Sin(25 * t) + 1);
    }

    public static float EaseOutElastic(float t)
    {
        return (float)(0.04f * t / (--t) * Math.Sin(25 * t));
    }

    public static float EaseInOutElastic(float t)
    {
        return (float)((t -= 0.5f) < 0 ? (0.02f + 0.01f / t) * Math.Sin(50 * t) : (0.02f - 0.01f / t) * Math.Sin(50 * t) + 1);
    }

    public static float MatchedLerpType(LerpType lerpType, float t)
    {
        switch (lerpType)
        {
            case LerpType.Liner:
                return Liner(t);
            case LerpType.EaseInQuad:
                return EaseInQuad(t);
            case LerpType.EaseOutQuad:
                return EaseOutQuad(t);
            case LerpType.EaseInOutQuad:
                return EaseInOutQuad(t);
            case LerpType.EaseInCubic:
                return EaseInCubic(t);
            case LerpType.EaseOutCubic:
                return EaseOutCubic(t);
            case LerpType.EaseInOutCubic:
                return EaseInOutCubic(t);
            case LerpType.EaseInQuart:
                return EaseInQuart(t);
            case LerpType.EaseOutQuart:
                return EaseOutQuart(t);
            case LerpType.EaseInOutQuart:
                return EaseInOutQuart(t);
            case LerpType.EaseInQuint:
                return EaseInQuint(t);
            case LerpType.EaseOutQuint:
                return EaseOutQuint(t);
            case LerpType.EaseInOutQuint:
                return EaseInOutQuint(t);
            case LerpType.EaseInElastic:
                return EaseInElastic(t);
            case LerpType.EaseOutElastic:
                return EaseOutElastic(t);
            case LerpType.EaseInOutElastic:
                return EaseInOutElastic(t);
            default:
                return t;
        }
    }
}
