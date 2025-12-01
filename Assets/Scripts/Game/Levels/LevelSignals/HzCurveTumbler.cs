namespace Assets.Scripts.LevelSignals
{
    public class HzCurveTumbler : CurveTumbler
    {
        protected override void ChangeCurve(Curve curve, float percent) => curve.SetHz(percent);
    }
}