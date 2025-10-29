public class HzCurveTumbler : CurveTumbler
{
    protected override void ChangeCurve(Curve curve, float percent)
    {
        curve.SetHz(percent);
    }
}
