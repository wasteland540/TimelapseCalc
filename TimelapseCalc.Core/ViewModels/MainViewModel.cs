using System;
using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using TimelapseCalc.Core.Services;

namespace TimelapseCalc.Core.ViewModels
{
    public class MainViewModel 
		: MvxViewModel
    {
        private readonly ICalculationService _calculationService;
        private const string Operation1 = "Calc Duration";
        private const string Operation2 = "Calc Count";

        public MainViewModel(ICalculationService calculationService)
        {
            _calculationService = calculationService;
        }

        #region PictureCount

        private int _pictureCount;

        public int PictureCount
        {
            get { return _pictureCount; }
            set
            {
                _pictureCount = value;
                RaisePropertyChanged(() => PictureCount);
            }
        }

        #endregion

        #region ExposionTime

        private string _exposionTime;

        public string ExposionTime
        {
            get { return _exposionTime; }
            set
            {
                _exposionTime = value;
                RaisePropertyChanged(() => ExposionTime);
            }
        }

        #endregion

        #region TakePicEveryXSec

        private int _takePicEveryXSec;

        public int TakePicEveryXSec
        {
            get { return _takePicEveryXSec; }
            set
            {
                _takePicEveryXSec = value;
                RaisePropertyChanged(() => TakePicEveryXSec);
            }
        }

        #endregion

        #region Duration

        private double _duration;

        public double Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                RaisePropertyChanged(() => Duration);
            }
        }

        #endregion

        #region OperationList

        private List<string> _operationList = new List<string> { Operation1, Operation2 };

        public List<string> OperationList
        {
            get { return _operationList; }
            set
            {
                _operationList = value;
                RaisePropertyChanged(() => OperationList);
            }
        }

        #endregion

        #region SelectedOperation

        private string _selectedOperation;

        public string SelectedOperation
        {
            get { return _selectedOperation; }
            set
            {
                _selectedOperation = value;
                RaisePropertyChanged(() => SelectedOperation);
            }
        }

        #endregion

        #region Calculation command

        private MvxCommand _calculationCommand;
        
        public ICommand CalculationCommand
        {
            get
            {
                _calculationCommand = _calculationCommand ?? new MvxCommand(DoCalculationCommand);
                return _calculationCommand;
            }
        }

        private void DoCalculationCommand()
        {
            if (SelectedOperation == Operation1)
            {
                if (!string.IsNullOrEmpty(ExposionTime) && TakePicEveryXSec > 0 && PictureCount > 0)
                {
                    var expositionTime = DetermineExpositionTime();

                    if (!double.IsNaN(expositionTime))
                    {
                        var duration = _calculationService.CalculateDuration(expositionTime, TakePicEveryXSec,
                            PictureCount);

                        Duration = duration;
                    }
                }
            }
            else if (SelectedOperation == Operation2)
            {
                if (!string.IsNullOrEmpty(ExposionTime) && TakePicEveryXSec > 0 && Duration > 0)
                {
                    var expositionTime = DetermineExpositionTime();

                    if (!double.IsNaN(expositionTime))
                    {
                        var count = _calculationService.CalculatePictureCount(expositionTime, TakePicEveryXSec, Duration);

                        PictureCount = count;
                    }
                }
            }
        }

        #endregion

        private double DetermineExpositionTime()
        {
            double expositionTime = Double.NaN;

            if (ExposionTime.EndsWith("\""))
            {
                var buff = ExposionTime.Replace("\"", "");
                expositionTime = double.Parse(buff);
            }
            else if (ExposionTime.Contains("/"))
            {
                var dividendBuff = ExposionTime.Substring(0, ExposionTime.IndexOf("/", StringComparison.Ordinal));
                var dividend = double.Parse(dividendBuff);

                var divisorBuff = ExposionTime.Substring(ExposionTime.IndexOf("/", StringComparison.Ordinal)+1);
                var divisor = double.Parse(divisorBuff);

                // ReSharper disable once PossibleLossOfFraction
                expositionTime = dividend/divisor;
            }

            return expositionTime;
        }
    }
}
