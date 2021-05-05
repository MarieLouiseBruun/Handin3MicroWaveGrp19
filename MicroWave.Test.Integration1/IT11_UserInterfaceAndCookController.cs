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
    public class IT11_UserInterfaceAndCookController
    {
        private UserInterface userInterface;
        private ILight light;
        private ICookController cookController;
        private IDoor door;
        private IDisplay display;
        private IOutput output;
        private StringWriter stringWriter;
        private IButton pButton;
        private IButton tButton;
        private IButton sButton;
        private ITimer timer;
        private IPowerTube powerTube;

        [SetUp]
        public void SetUp()
        {
            timer = new Timer();
            output = new Output();
            powerTube = new PowerTube(output);
            light = new Light(output);
            display = new Display(output);
            door = new Door();
            pButton = new Button();
            tButton = new Button();
            sButton = new Button();
            cookController = new CookController(timer, display, powerTube);
            userInterface = new UserInterface(pButton, tButton, sButton, door, display, light, cookController);

            stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
        }
        [Test]
        public void DoorOpen_LightIsTurnedOn()
        {
            door.Open();
            Assert.That(stringWriter.ToString().Contains("Light is turned on"));
        }

        [Test]
        public void DoorClose_LightIsTurnedOff()
        {
            door.Open();
            door.Close();
            Assert.That(stringWriter.ToString().Contains("Light is turned off"));
        }

        [Test]
        public void PowerButtonPressed_DisplayShowsPower()
        {
            pButton.Press();
            Assert.That(stringWriter.ToString().Contains("50 W"));
        }

        [Test]
        public void TimeButtonPressed_DisplayShowsTime()
        {
            pButton.Press();
            tButton.Press();
            Assert.That(stringWriter.ToString().Contains("1:00"));
        }

        [Test]
        public void StartCancelButtonPressed_PowerTubeIsTurnedOn()
        {
            pButton.Press();
            tButton.Press();
            sButton.Press();
            Assert.That(stringWriter.ToString().Contains("PowerTube works with 50"));
        }
    }
}
