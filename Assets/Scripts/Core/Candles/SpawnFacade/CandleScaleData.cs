namespace Core.Candles.SpawnFacade
{
    public class CandleScaleData
    {
        public readonly float FirstTarget;
        public readonly float SecondTarget;
        public readonly float ThirdTarget;
        public readonly float FourthTarget;

        public CandleScaleData(float firstTarget, float secondTarget, float thirdTarget, float fourthTarget)
        {
            FirstTarget = firstTarget;
            SecondTarget = secondTarget;
            ThirdTarget = thirdTarget;
            FourthTarget = fourthTarget;
        }
    }
}