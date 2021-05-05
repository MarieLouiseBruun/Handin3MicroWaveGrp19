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
    public class IT4_ButtonAndUI
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
            door = Substitute.For<IDoor>();
            tButton = new Button();
            pButton = new Button();
            sButton = new Button();
            display = Substitute.For<IDisplay>();
            light = Substitute.For<ILight>();
            cookController = Substitute.For<ICookController>();
            userInterface = new UserInterface(pButton,tButton,sButton,door,display,light,cookController);
           
        }

        #region PowerButtonPress

        [Test]
        public void ReadyState_PressPower_powerLevel50()
        {
           pButton.Press();
           display.Received(1).ShowPower(50);
        }

        [Test]
        public void ReadyState_PressPower14Times_powerlevel700()
        {
            for (int i = 0; i < 14; i++){pButton.Press(); }
            display.Received(1).ShowPower(700);
        }

        [Test]
        public void ReadyState_PressPower15Times_powerLevel50()
        {
            for (int i = 0; i < 15; i++) { pButton.Press(); }
            display.Received(2).ShowPower(50);
        }
        #endregion

        #region TimeButtonPress

        

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(7)]
        public void SetPowerState_PressTimeXtimes_DisplayXmin(int count)
        {
            pButton.Press();
            for (int i = 0; i < count; i++)
            {
                tButton.Press();
            }

            display.Received(1).ShowTime(count, 0);
        }


        #endregion

        #region StartbuttonPress

        [Test]
        public void SetTimeState_StartPress_LightAndCookerOn()
        {
            pButton.Press();
            tButton.Press();
            sButton.Press();

            light.Received(1).TurnOn();
            cookController.Received(1).StartCooking(50, 60);
        }


        [Test]
        public void CookingState_StartPress_LightAndCookerAndDisplayRecived()
        {
            pButton.Press();
            tButton.Press();
            sButton.Press();

            sButton.Press();

            display.Received(1).Clear();
            light.Received(1).TurnOff();
            cookController.Received(1).Stop();
        }



        #endregion

    }
}
