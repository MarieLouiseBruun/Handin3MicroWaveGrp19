using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.CompilerServices;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace MicroWave.Test.Integration
{
    public class IT8_CookControllerAndPowerTube
    {
        private CookController cookController;
        private IDisplay display;
        private ITimer timer;
        private IPowerTube powerTube;
        private IOutput output;
        private IUserInterface userInterface;
        private StringWriter stringWriter;

        [SetUp]
        public void SetUp()
        {
            timer = Substitute.For<ITimer>();
            display = Substitute.For<IDisplay>();
            userInterface = Substitute.For<IUserInterface>();
            output = new Output();
            powerTube = new PowerTube(output);
            cookController = new CookController(timer, display, powerTube);

            stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
        }

        [TestCase(300,100)]
        [TestCase(50,100)]
        [TestCase(700,100)]
        public void StartCooking_PowerTubeOn(int power, int time)
        {
            cookController.StartCooking(power,time);
            Assert.That(stringWriter.ToString().Contains("PowerTube works with"));
        }

        [TestCase(49, 100)]
        [TestCase(701, 100)]
        public void StartCooking_PowerTubeOn_PowerOutOfRange(int power, int time)
        {
            Assert.That(() => cookController.StartCooking(power, time), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void OnTimerExpired_PowerTubeOff()
        {
            cookController.StartCooking(300,100);
            timer.Expired += Raise.EventWith(this, EventArgs.Empty);
            Assert.That(stringWriter.ToString().Contains("PowerTube turned off"));
        }

        [Test]
        public void Stop_PowerTubeOff()
        {
            cookController.StartCooking(300,100);
            cookController.Stop();
            Assert.That(stringWriter.ToString().Contains("PowerTube turned off"));
        }

    }
}
