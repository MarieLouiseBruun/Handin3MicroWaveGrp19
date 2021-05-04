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

        //Flere testcases her. 
        [TestCase("00","20")]
        public void bla(string minutes, string seconds)
        {
            timer.Start(Convert.ToInt32(minutes) * 60 + Convert.ToInt32(seconds));
            cookController.OnTimerTick(this, EventArgs.Empty);
            Assert.That(stringWriter.ToString().Contains(minutes + ":" + seconds));

            //timer.TimeRemaining.Returns(Convert.ToInt32(minutes) * 60 + Convert.ToInt32(seconds));
            //timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);
            //Assert.That(stringWriter.ToString().Contains(minutes+""+seconds));

            //Der er noget galt med denne test. Vi har prøvet forskellige versioner, der alle fejler. 
        }
    }
}
