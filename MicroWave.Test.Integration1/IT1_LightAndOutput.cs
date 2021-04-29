using System;
using System.IO;
using NUnit.Framework;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;

namespace MicroWave.Test.Integration
{
    public class IT1_LightAndOutput
    {
        private ILight light;
        private IOutput output;
        //private StringReader stringReader;
        private StringWriter stringWriter;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            light = new Light(output);
            //stringReader = new StringReader();
            //System.Console.SetOut(stringReader);
            stringWriter = new StringWriter();
            System.Console.SetOut(stringWriter);

        }

        [Test]
        public void NoMethodCall_ConsoleIsEmpty()
        {
            //Assert.That(stringWriter.ToString().IsNullOrEmpty());
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