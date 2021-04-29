using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = Microwave.Classes.Boundary.Timer;

namespace MicroWave.Test.Integration
{
    public class IT6_CookControllerAndTimer
    {
        private ICookController cookController;
        private ITimer timer;
        private IDisplay display;
        private IUserInterface userInterface;
        private IPowerTube powerTube;
        private StringWriter stringWriter;
        private IOutput output;

        [SetUp]
        public void SetUp()
        {
            display = Substitute.For<IDisplay>();
            userInterface = Substitute.For<IUserInterface>();
            powerTube = Substitute.For<IPowerTube>();

            timer = new Timer();
            cookController = new CookController(timer, display, powerTube, userInterface);
            stringWriter = new StringWriter();
            output = new Output();

            Console.SetOut(stringWriter);
        }

        [Test]
        public void Start_TimerTick_WaitForTick_DisplayShowsTime()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            timer.TimerTick += (sender, args) => pause.Set();
            cookController.StartCooking(100, 60);

            Assert.That(pause.WaitOne(1050));
            display.Received(1).ShowTime(Arg.Any<int>(), Arg.Any<int>());
        }

        [Test]
        public void Start_TimerTick_NoTick_DisplayEmpty()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            timer.TimerTick += (sender, args) => pause.Set();
            cookController.StartCooking(100, 60);

            Assert.That(!pause.WaitOne(950));
            display.DidNotReceive().ShowTime(Arg.Any<int>(), Arg.Any<int>());
        }

        [Test]
        public void Start_TimerExpired_WaitForTick_CookingDone()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            timer.Expired += (sender, args) => pause.Set();
            cookController.StartCooking(100, 2);

            Assert.That(pause.WaitOne(2050));
            userInterface.Received(1).CookingIsDone();
        }

        [Test]
        public void Start_TimerExpired_NoTick_CookingNotDone()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            timer.Expired += (sender, args) => pause.Set();
            cookController.StartCooking(100, 2);

            Assert.That(!pause.WaitOne(1950));
            userInterface.DidNotReceive().CookingIsDone();
        }

        [Test]
        public void Stop_TimerTick_NoTick_CookingNotDone()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            timer.TimerTick += (sender, args) => pause.Set();
            cookController.StartCooking(50, 2);
            cookController.Stop();

            Assert.That(!pause.WaitOne(1100));
        }

    }
}
