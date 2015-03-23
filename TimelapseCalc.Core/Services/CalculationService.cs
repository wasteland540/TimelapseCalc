namespace TimelapseCalc.Core.Services
{
    public class CalculationService : ICalculationService
    {
        public int CalculatePictureCount(double exposureTime, int takePicEveryXSec, double duration)
        {
            int count;

            count = (int) ((duration*60)/(exposureTime + takePicEveryXSec));

            return count;
        }

        public double CalculateDuration(double exposureTime, int takePicEveryXSec, int pictureCount)
        {
            double duration;

            duration = (exposureTime + takePicEveryXSec)*pictureCount;
            duration /= 60;

            return duration;
        }
    }
}