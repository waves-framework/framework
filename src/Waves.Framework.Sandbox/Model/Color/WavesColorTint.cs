namespace Waves.Sandbox.Model.Color;

public class WavesColorTint
{
    public WavesColorTint(WavesColor color, int tint)
    {
        Color = color;
        Tint = tint;
    }
    
    public int Tint { get; }
    
    public WavesColor Color { get; }
}