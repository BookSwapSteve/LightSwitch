using System;
using System.Threading;
using AnalysisUK.LightSwitch.Sensors;
using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch
{
    /// <summary>
    /// Main class for handling LightSwitch functionality.
    /// </summary>
    public class LightSwitchController
    {
        #region Constructors

        public LightSwitchController()
            : this(new SensorsController())
        { }

        public LightSwitchController(ISensorsController sensorController)
        {
            SensorsController = sensorController;
        }

        #endregion

        #region Dependency Injection Properties

        protected ISensorsController SensorsController
        {
            get; set;
        }

        #endregion

        public void Initialize()
        {
            Debug.Print("Initializing LightSwitch Controller");
            SensorsController.Initialise();
        }

        /// <summary>
        /// Start doing the light switch control...
        /// </summary>
        public void Start()
        {
            Debug.Print("Starting LightSwitch Controller");

            SensorsController.Start();

            // Show the LED on to indicate started.
            SensorsController.LedController.On();
            Thread.Sleep(1000);
            SensorsController.LedController.Off();

            while (true)
            {
                if (SensorsController.KeyPad.GetValue() == false)
                {                   
                    SensorsController.LedController.On();
                    Thread.Sleep(500);

                    Debug.Print(System.DateTime.UtcNow.ToString());

                    SensorsController.LedController.Off();
                    Thread.Sleep(200);
                    SensorsController.AudioOut.Beep();                    
                }
            }
        }
    }
}
