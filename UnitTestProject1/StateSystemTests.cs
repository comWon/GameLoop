using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameLoop;

namespace UnitTestProject1
{
    [TestClass]
    public class StateSystemTests
    {
        [TestMethod]
        public void AddState()
        {

            StateSystem SS = new GameLoop.StateSystem();
            SplashScreen splash = new GameLoop.SplashScreen();

            //Act
            SS.AddState("Splash", splash);

            //Assert
            Assert.IsTrue(SS.Exists("Splash"));

        }

        [TestMethod]
        [ExpectedException (typeof(Exception), "State Already Exists")]
        public void AddStateTwice()
        {
            StateSystem SS = new GameLoop.StateSystem();
            SplashScreen splash = new GameLoop.SplashScreen();

            //Act
            SS.AddState("Splash", splash);
  
            SS.AddState("Splash", splash);
        }

        [TestMethod]
        public void SetSplashScreenFromNew()
        {
            StateSystem SS = new GameLoop.StateSystem();
            SplashScreen splash = new GameLoop.SplashScreen();
            SS.AddState("Splash", splash);

            //act
            SS.ChangeState("Splash");

            //Assert
            Assert.AreEqual(SS.CurrentState(), splash);
        } 
    }
}
