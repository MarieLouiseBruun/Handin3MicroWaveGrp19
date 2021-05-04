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
    public class IT10_UserInterfaceAndDisplay
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
            
            output = new Output();
            light = new Light(output);
            display = new Display(output);
            door = new Door();
            pButton = new Button();
            tButton = new Button();
            sButton = new Button();
            userInterface = new UserInterface(pButton, tButton, sButton, door, display, light, cookController);

            stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
        }

        [TestCase(1,50)]
        [TestCase(6,300)]
        [TestCase(14,700)]
        [TestCase(15,50)]
        public void ReadyState_PowerIsPushed_DisplayShowsPowerLevel(int nbpress,int powerlevel)
        {
            for (int i = 0; i < nbpress; i++)
            {
                pButton.Press();
            }
            
            Assert.That(stringWriter.ToString().Contains(Convert.ToString(powerlevel)));
        }

        

        [TestCase(1,01,00)]
        [TestCase(3,03,00)]
        [TestCase(5,05,00)]
        public void SetPowerState_TimeIsPushed_DisplayShowsTime(int nbpress, int minutes,int seconds)
        {
            pButton.Press();

            for (int i = 0; i < nbpress; i++)
            {
                tButton.Press();
            }
            Assert.That(stringWriter.ToString().Contains(Convert.ToString(minutes+":"+seconds)));
        }

        [Test]
        public void SetPowerState_StartCancelIsPushed_DisplayIsEmpty()
        {
            pButton.Press();
            sButton.Press();
            
            Assert.That(stringWriter.ToString().Contains("Display cleared"));
        }

        [Test]
        public void SetPowerState_DoorIsOpened_DisplayIsEmpty()
        {
            pButton.Press();
            door.Open();

            Assert.That(stringWriter.ToString().Contains("Display cleared"));
        }

        [Test]
        public void CookingState_StartCancelIsPushed_DisplayIsEmpty()
        {
            pButton.Press();
            tButton.Press();

            for (int i = 0; i < 2; i++)
            {
                sButton.Press();
            }

            Assert.That(stringWriter.ToString().Contains("Display cleared"));
        }

    }
}
