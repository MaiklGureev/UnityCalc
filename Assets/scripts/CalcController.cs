using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalcController : MonoBehaviour
{
    private CalculatorStateMachine stateMachine = new CalculatorStateMachine();
    public Text expression, result;
    private float startTime, endTime = 0;
    private SaveResult save;

    private void Start()
    {
        expression.text = "";
        result.text = "= 0.0";
        save = new SaveResult();
    }

    public void ClickOnButton(string symbol)
    {
        if (stateMachine.StateMachineCalculator(symbol) == true)
        {
            UpdateScreen();
        }
    }

    public void ResetCalc()
    {

        if (stateMachine.DoResetAll() == true)
        {
            UpdateScreen();
        }

    }

    public void UpdateScreen()
    {
        expression.text = stateMachine.GetFormattedExpression();
        result.text = stateMachine.GetFormattedResult();
    }

    public void LongClickForResetDown(bool isLongClick)
    {
        if (isLongClick)
        {
            startTime = Time.time;
        }

        if (!isLongClick)
        {
            endTime = Time.time;
        }

        if (endTime - startTime > 0.5f)
        {
            ResetCalc();
            startTime = 0;
            endTime = 0;
        }
    }

    public void SaveRes() {
        SaveResult.Instance.SaveData(stateMachine.GetSimpeFormattedExpression());
    }


}
