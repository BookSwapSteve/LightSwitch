using System;
using System.Threading;
using AnalysisUK.LightSwitch.Network;
using AnalysisUK.LightSwitch.Sensors;
using AnalysisUK.LightSwitch.Utilities;
using AnalysisUK.LightSwitch.Utilities.Logger;
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
            : this(new NetworkController(), new SensorsController())
        { }

        public LightSwitchController(INetworkController networkController, ISensorsController sensorController)
        {
            NetworkController = networkController;
            SensorsController = sensorController;

            // Store these away so we can get them back in other objects (i.e. network handler).
            IoCContainer.Store(NetworkController);
            IoCContainer.Store(SensorsController);
        }

        #endregion

        #region Dependency Injection Properties

        protected INetworkController NetworkController
        {
            get; set;
        }

        protected ISensorsController SensorsController
        {
            get; set;
        }

        #endregion

        public void Initialize()
        {
            Log.Message("Initializing LightSwitch Controller");
            SensorsController.Initialise();
            NetworkController.Initialise();
        }

        /// <summary>
        /// Start doing the light switch control...
        /// </summary>
        public void Start()
        {
            Log.Message("Starting LightSwitch Controller");

            SensorsController.Start();
            NetworkController.Start();

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

                    Log.Message(System.DateTime.UtcNow.ToString());

                    // Sync the timeservice when prompted - no timeout! - crashes!
                    //NetworkController.TimeSyncService.Syncronise();

                    NetworkController.TimeSyncService.Start();

                    SensorsController.LedController.Off();
                    Thread.Sleep(200);
                    SensorsController.AudioOut.Beep();                    
                }
            }
        }
    }
}
