namespace TimelapseCalc.Core.Services
{
    public interface ICalculationService
    {
        int CalculatePictureCount(double exposureTime, int takePicEveryXSec, double duration);

        double CalculateDuration(double exposureTime, int takePicEveryXSec, int pictureCount);
    }
}