using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DataModelTest
    {

        // A Test behaves as an ordinary method
        [Test]
        public void DataModelTestSimplePasses()
        {
            // Use the Assert class to test conditions
            CalculatorStateMachine calculatorState = new CalculatorStateMachine();
            calculatorState.StateMachineCalculator("4");
            calculatorState.StateMachineCalculator("+");
            calculatorState.StateMachineCalculator("4");
           Assert.True(Object.Equals(calculatorState.GetFormattedResult(), "= 8"));
        }

    }
}
