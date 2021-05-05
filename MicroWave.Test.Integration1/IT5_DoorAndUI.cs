using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NUnit.Framework;
using NSubstitute;


namespace MicroWave.Test.Integration
{
    public class IT5_DoorAndUI
    {
        private IDoor door;
        private IUserInterface userInterface;
        private IButton tButton;
        private IButton pButton;
        private IButton sButton;
        private IDisplay display;
        private ILight light;
        private ICookController cookController;

        private StringWriter stringWriter;

        [SetUp]

        public void Setup()
        {
            door = new Door();
            tButton = new Button();
            pButton = new Button();
            sButton = new Button();
            display = Substitute.For<IDisplay>();
            light = Substitute.For<ILight>();
            cookController = Substitute.For<ICookController>();
            userInterface = new UserInterface(pButton,tButton,sButton,door,display,light,cookController);
           
        }

        [Test]
        public void ReadyState_Open_TurnOnLightCalled()
        {
           door.Open();
           display.Received(0).Clear();
           light.Received(1).TurnOn();
        }

        [Test]
        public void DoorOpenState_Close_TurnOffLightCalled()
        {
            door.Open();
            door.Close();
            light.Received(1).TurnOff();
        }

        [Test]
        public void SetPowerState_DoorOpen_LightTurnedOnAndDisplayCleared()
        {
            pButton.Press();
            door.Open();

            light.Received(1).TurnOn();
            display.Received(1).Clear();
        }

        [Test]
        public void SetTimeState_DoorOpen_LightTurnedOnAndDisplayCleared()
        {
            pButton.Press();
            tButton.Press();
            door.Open();

            light.Received(1).TurnOn();
            display.Received(1).Clear();
        }

        [Test]
        public void CookingState_DoorOpen_CookingStoppedAndLightTurnedOnAndDisplayCleared()
        {
            pButton.Press();
            tButton.Press();
            sButton.Press();
            door.Open();

            cookController.Received(1).Stop();
            light.Received(1).TurnOn();
            display.Received(1).Clear();
        }

    }
}
