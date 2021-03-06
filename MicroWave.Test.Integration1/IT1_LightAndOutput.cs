using System;
using System.IO;
using NUnit.Framework;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using Castle.Core.Internal; 

namespace MicroWave.Test.Integration
{
    public class IT1_LightAndOutput
    {
        private ILight light;
        private IOutput output;
        private StringWriter stringWriter;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            light = new Light(output);
            stringWriter = new StringWriter();
            System.Console.SetOut(stringWriter);

        }

        [Test]
        public void NoMethodCall_ConsoleIsEmpty()
        {
            string consoleoutput = stringWriter.ToString();
            Assert.That(consoleoutput.IsNullOrEmpty);
        }

        [Test]
        public void LightIsTurnedOn()
        {
            light.TurnOn();
            Assert.That(stringWriter.ToString().Contains("on"));
        }

        [Test]
        public void LightIsTurnedOff()
        {
            light.TurnOn();
            light.TurnOff();
            Assert.That(stringWriter.ToString().Contains("off"));
        }
    }
}