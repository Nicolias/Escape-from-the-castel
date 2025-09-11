public class AmplitudeCurveTumbler : CurveTumbler
{
    protected override void ChangeCurve(Curve curve, float percent)
    {
        curve.SetAmplitude(percent);
    }
}
