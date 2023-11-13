namespace Waves.Sandbox.ViewModels.UI.Color;

public class GeneratorColor
{
    public int Tint { get; set; }
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }

    public String ToHex()
    {
        return "#" + R.ToString("X2") + G.ToString("X2") + B.ToString("X2");
    }
}