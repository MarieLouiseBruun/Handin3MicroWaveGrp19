using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NUnit.Framework;
using Castle.Core.Internal;

namespace MicroWave.Test.Integration
{
    public class IT3_PowerTubeAndOutput
    {
        private IPowerTube powerTube;
        private IOutput output;
        //private StringReader stringReader;
        private StringWriter stringWriter;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            powerTube = new PowerTube(output);
            //stringReader = new StringReader();
            //System.Console.SetOut(stringReader);
            stringWriter = new StringWriter();
            System.Console.SetOut(stringWriter);
        }

        [Test]
        public void NoMethodCall_ConsoleIsEmpty()
        {
            Assert.That(stringWriter.ToString().IsNullOrEmpty());
        }

        [TestCase(50)]
        [TestCase(124)]
        [TestCase(700)]
        public void PowerTubeTurnOn(int power)
        {
            powerTube.TurnOn(power);
            Assert.That(stringWriter.ToString().Contains($"{power}"));
        }

        [TestCase(300)]
        [TestCase(600)]
        [TestCase(900)]
        public void PowerTubeTurnOff(int power)
        {
            powerTube.TurnOff();
            Assert.That(stringWriter.ToString().Contains($"{power}"));
        }
    }
}
