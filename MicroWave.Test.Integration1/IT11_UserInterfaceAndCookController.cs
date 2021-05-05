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
    }
}
