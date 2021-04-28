using System.IO;
using NUnit.Framework;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;

namespace MicroWave.Test.Integration
{
    public class Test
    {
        private ILight light;
        private IOutput output;
        private StringReader stringReader;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            light = new Light(output);
            stringReader = new StringReader();
            System.Console.SetOut(stringReader);
            
        }

        [Test]
        public void Test1()
        {
            
            Assert.Pass();
        }
    }
}