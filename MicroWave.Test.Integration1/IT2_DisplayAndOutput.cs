using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NUnit.Framework;

namespace MicroWave.Test.Integration
{
    public class IT2_DisplayAndOutput
    {
        private IDisplay display;
        private IOutput output;
        //private StringReader stringReader;
        private StringWriter stringWriter;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            display = new Display(output);
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

        [TestCase(45,30)]
        [TestCase(2,23)]
        [TestCase(0,40)]
        public void DisplayShowsTime(int minutes, int seconds)
        {
            display.ShowTime(minutes,seconds);
            Assert.That(stringWriter.ToString().Contains($"{minutes}") && stringWriter.ToString().Contains($"{seconds}"));
        }

        [TestCase(300)]
        [TestCase(600)]
        [TestCase(900)]
        public void DisplayShowsPower(int power)
        {
            display.ShowPower(power);
            Assert.That(stringWriter.ToString().Contains($"{power}"));
        }

        [Test]
        public void DisplayCleared()
        {
            display.Clear();
            Assert.That(stringWriter.ToString().Contains("cleared"));
        }
    
}
}
