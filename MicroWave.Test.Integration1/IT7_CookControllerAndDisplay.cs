using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace MicroWave.Test.Integration
{
    public class IT7_CookControllerAndDisplay
    {
        private CookController cookController;
        private IDisplay display;
        private ITimer timer;
        private IPowerTube powerTube;
        private IOutput output;
        private StringWriter stringWriter; 

        [SetUp]
        public void SetUp()
        {
            powerTube = Substitute.For<IPowerTube>();
            timer = Substitute.For<ITimer>();
            output = new Output();
            display = new Display(output);
            cookController = new CookController(timer,display,powerTube);

            stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
        }

        [TestCase("00","20")]
        [TestCase("01","55")]
        [TestCase("10","30")]
        //OnTimerTick method from CookController.
        public void TimerEventThroughCookController_DisplayShowsTimer(string minutes, string seconds)
        {
            cookController.StartCooking(50,Convert.ToInt32(minutes)*60+Convert.ToInt32(seconds));
            timer.TimeRemaining.Returns(Convert.ToInt32(minutes) * 60 + Convert.ToInt32(seconds));
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);
            Assert.That(stringWriter.ToString().Contains(minutes + ":" + seconds));
        }


    }
}
