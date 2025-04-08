namespace MonoEight;

public class MinMax
{
    private float _min;
    private float _max;

    public float Min
    {
        get => _min;
        set => _min = value;
    }

    public float Max
    {
        get => _max;
        set => _max = value;
    }

    public MinMax(float min, float max)
    {
        _min = min;
        _max = max;
    }

    public float Contrain(float value)
    {
        if (value < _min)
            return _min;
        if (value > _max)
            return _max;

        return value;
    }
}