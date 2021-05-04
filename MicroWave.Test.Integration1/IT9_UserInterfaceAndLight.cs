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
    public class IT9_UserInterfaceAndLight
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

        [SetUp]
        public void SetUp()
        {
            cookController = Substitute.For<ICookController>();
            display = Substitute.For<IDisplay>();

            output = new Output();
            light = new Light(output);
            door = new Door();
            pButton = new Button();
            tButton = new Button();
            sButton = new Button();
            userInterface = new UserInterface(pButton, tButton, sButton, door, display, light, cookController);

            stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
        }

        [Test]
        public void SetTimeState_startCancelIsPressed_LightIsTurnedOn()
        {
            pButton.Press();
            tButton.Press();
            sButton.Press();

            Assert.That(stringWriter.ToString().Contains("Light is turned on"));
        }

        [Test]
        public void CookingState_startCancelIsPressed_LightIsTurnedOff()
        {
            pButton.Press();
            tButton.Press();
            for (int i = 0; i < 2; i++) { sButton.Press(); }
            
            Assert.That(stringWriter.ToString().Contains("Light is turned off"));

        }

        [Test]
        public void ReadyState_DoorIsOpened_LightIsTurnedOn()
        {
            door.Open();

            Assert.That(stringWriter.ToString().Contains("Light is turned on"));

        }

        [Test]
        public void SetPowerState_DoorIsOpened_LightIsTurnedOn()
        {
            pButton.Press();
            door.Open();

            Assert.That(stringWriter.ToString().Contains("Light is turned on"));

        }


        [Test]
        public void SetTimeState_DoorIsOpened_LightIsTurnedOn()
        {
            pButton.Press();
            tButton.Press();
            door.Open();

            Assert.That(stringWriter.ToString().Contains("Light is turned on"));

        }

        [Test]
        public void DoorOpenState_DoorIsClosed_LightIsTurnedOff()
        {
            door.Open();
            door.Close();

            Assert.That(stringWriter.ToString().Contains("Light is turned off"));

        }


        [Test]
        public void CookingState_CookingIsDone_LightIsTurnedOff()
        {
            pButton.Press();
            tButton.Press();
            sButton.Press();
            userInterface.CookingIsDone();

            Assert.That(stringWriter.ToString().Contains("Light is turned off"));

        }
    }
}
