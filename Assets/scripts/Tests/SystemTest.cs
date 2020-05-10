using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SystemTest
    {
        
        [Test]
        public void SystemTestSimplePasses()
        {
            string pn = Application.productName;
            Assert.AreEqual(pn, "UnityCalc");
        }
    }
}
