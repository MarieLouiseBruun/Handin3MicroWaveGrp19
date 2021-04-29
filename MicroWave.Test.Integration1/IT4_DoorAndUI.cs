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
    public class IT4_DoorAndUI
    {
        private IDoor door;
        private IUserInterface userInterface;
        private IButton tButton;
        private IButton pButton;
        private IButton sButton;
        private IDisplay display;
        private ILight light;
        private ICookController cookController;

        //private StringReader stringReader;
        private StringWriter stringWriter;

        [SetUp]

        public void Setup()
        {
            door = new Door();
            tButton = Substitute.For<IButton>();
            pButton = Substitute.For<IButton>();
            sButton = Substitute.For<IButton>();
            display = Substitute.For<IDisplay>();
            light = Substitute.For<ILight>();
            cookController = Substitute.For<ICookController>();
            userInterface = new UserInterface(pButton,tButton,sButton,door,display,light,cookController);
           
        }

        [Test]
        public void NoMethodCall_ConsoleIsEmpty()
        {
           
        }

    }
}
