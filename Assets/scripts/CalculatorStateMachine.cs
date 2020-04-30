using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CalculatorStateMachine
{

    private enum EnumState
    {
        s,
        f,
        st1,
        st2,
        st3,
        st4,
        st5,
        st6,
        st7,
        st8,
        st9,
        st10,
        delA,
        delB,
        delSign,
    }


    private EnumState state;
    private EnumState lastState;
    private double a = 0, b = 0, res = 0;
    private string expression;
    private string operation;
    private string result;
    private StringBuilder stringA, stringB, stringResult;
    private LinkedList<string> numbers = new LinkedList<string>();
    private LinkedList<string> actions = new LinkedList<string>();

    public CalculatorStateMachine()
    {
        state = EnumState.s;
        operation = "";
        result = "";
        expression = "";

        stringA = new StringBuilder();
        stringB = new StringBuilder();
        stringResult = new StringBuilder();

        numbers.AddLast("0");
        numbers.AddLast("1");
        numbers.AddLast("2");
        numbers.AddLast("3");
        numbers.AddLast("4");
        numbers.AddLast("5");
        numbers.AddLast("6");
        numbers.AddLast("7");
        numbers.AddLast("8");
        numbers.AddLast("9");

        actions.AddLast("+");
        actions.AddLast("-");
        actions.AddLast("*");
        actions.AddLast("/");
    }

    public bool DoResetAll()
    {
        state = EnumState.s;
        stringA.Clear();
        stringB.Clear();
        stringResult.Clear();

        operation = "";
        result = "";
        expression = "";
        a = 0;
        b = 0;
        res = 0;

        return true;
    }

    public string GetFormattedExpression()
    {
        expression = "";
        expression = stringA.ToString() + "\n" + operation + " " + stringB.ToString();
        return expression;
    }

    public string GetSimpeFormattedExpression()
    {
        expression = "";
        expression = stringA.ToString() + " " + operation + " " + stringB.ToString() + GetFormattedResult();
        return expression;
    }

    public string GetFormattedResult()
    {

        result = "";
        stringResult.Clear();


        if (stringA.Length == 0)
        {
            result = "= 0.0";
        }
        else if (stringB.Length == 0 | stringB.ToString() == "-")
        {
            result = "= " + stringA.ToString();
        }
        else
        {
            res = Math.Round(res, 10);
            stringResult.Append(res);
            result = "= " + stringResult.ToString();
        }

        return result;
    }

    public bool StateMachineCalculator(string inputChar)
    {
        switch (state)
        {

            case EnumState.s:
                {
                    if (inputChar == "-")
                    {
                        stringA.Append("-");
                        state = EnumState.st1;
                    }
                    if (numbers.Contains(inputChar))
                    {
                        stringA.Append(inputChar);
                        state = EnumState.st2;
                    }
                    break;
                }

            case EnumState.st1:
                {
                    if (numbers.Contains(inputChar))
                    {
                        stringA.Append(inputChar);
                        state = EnumState.st2;
                    }
                    if (inputChar == "del" && stringA.Length == 0)
                    {
                        state = EnumState.s;
                    }
                    else if (inputChar == "del")
                    {
                        lastState = EnumState.st1;
                        //state = EnumState.delA;
                        DelCharA();
                    }
                    break;
                }

            case EnumState.st2:
                {
                    if (numbers.Contains(inputChar) && (stringA.Length < 7))
                    {
                        stringA.Append(inputChar);
                        state = EnumState.st2;
                    }
                    if (inputChar == "." && (stringA.Length < 7))
                    {
                        stringA.Append(inputChar);
                        state = EnumState.st3;
                    }
                    if (actions.Contains(inputChar))
                    {
                        operation = inputChar;
                        state = EnumState.st4;
                    }
                    if (inputChar == "del" && stringA.Length != 0)
                    {
                        lastState = EnumState.st2;
                        DelCharA();
                    }
                    break;
                }

            case EnumState.st3:
                {
                    if (numbers.Contains(inputChar) && (stringA.Length < 7))
                    {
                        stringA.Append(inputChar);
                        state = EnumState.st3;
                    }
                    if (actions.Contains(inputChar))
                    {
                        operation = inputChar;
                        state = EnumState.st4;
                    }
                    if (inputChar == "del" & stringA.Length != 0)
                    {
                        lastState = EnumState.st3;
                        DelCharA();
                    }
                    break;
                }

            case EnumState.st4:
                {

                    if (inputChar == "-" && operation != "")
                    {
                        stringB.Append("-");
                        state = EnumState.st5;
                        break;
                    }

                    if (actions.Contains(inputChar))
                    {
                        operation = inputChar;
                        state = EnumState.st4;
                        break;
                    }


                    if (numbers.Contains(inputChar))
                    {
                        stringB.Append(inputChar);
                        state = EnumState.st6;
                        CalculateExpression(stringA, stringB, operation);
                    }
                    if (inputChar == "del" && stringB.Length != 0)
                    {
                        DelCharB();
                        state = EnumState.st4;
                    }

                    if (inputChar == "del" & stringB.Length == 0)
                    {
                        lastState = EnumState.st3;
                        DelCharSign();
                    }
                    break;
                }

            case EnumState.st5:
                {
                    if (numbers.Contains(inputChar))
                    {
                        stringB.Append(inputChar);
                        state = EnumState.st6;
                        CalculateExpression(stringA, stringB, operation);
                    }
                    if (inputChar == "del")
                    {
                        lastState = EnumState.st5;
                        DelCharB();
                    }
                    break;
                }

            case EnumState.st6:
                {
                    if (numbers.Contains(inputChar) && (stringB.Length < 7))
                    {
                        stringB.Append(inputChar);
                        state = EnumState.st6;
                        CalculateExpression(stringA, stringB, operation);
                    }
                    if (inputChar == "." && (stringA.Length < 7))
                    {
                        stringB.Append(inputChar);
                        state = EnumState.st7;
                        CalculateExpression(stringA, stringB, operation);
                    }
                    if (actions.Contains(inputChar))
                    {
                        operation = inputChar;
                        stringA.Clear();
                        stringB.Clear();
                        stringA.Append(res);
                        if (stringA.Length >= 7)
                        {
                            stringA.Length = 7;
                        }
                        state = EnumState.st4;
                    }

                    if (inputChar == "del" & stringB.Length != 0)
                    {
                        lastState = EnumState.st6;
                        DelCharB();
                    }

                    break;
                }

            case EnumState.st7:
                {
                    if (numbers.Contains(inputChar) & (stringB.Length < 7))
                    {
                        stringB.Append(inputChar);
                        state = EnumState.st7;
                        CalculateExpression(stringA, stringB, operation);
                    }
                    if (actions.Contains(inputChar))
                    {
                        operation = inputChar;
                        stringA.Clear();
                        stringB.Clear();
                        stringA.Append(res);
                        if (stringA.Length >= 7)
                        {
                            stringA.Length = 7;
                        }
                        state = EnumState.st4;
                    }

                    if (inputChar == "del" & stringB.Length == 0)
                    {
                        lastState = EnumState.st3;
                        DelCharSign();
                    }
                    if (inputChar == "del" & stringB.Length != 0)
                    {
                        lastState = EnumState.st7;
                        DelCharB();
                    }
                    break;
                }
        }
        return true;

    }

    private void CalculateExpression(StringBuilder stringA, StringBuilder stringB, string opertion)
    {
        if (stringB.Length != 0 & opertion != "" & stringA.Length != 0)
        {

            a = double.Parse(stringA.ToString());
            b = double.Parse(stringB.ToString());

            switch (opertion)
            {
                case ("+"):
                    {
                        res = a + b;
                        break;
                    }
                case ("-"):
                    {
                        res = a - b;
                        break;
                    }
                case ("*"):
                    {
                        res = a * b;
                        break;
                    }
                case ("/"):
                    {
                        res = a / b;
                        break;
                    }
            }

        }


    }



    private void DelCharA()
    {

        char dot = '.';
        if (stringA.ToString().Contains(dot.ToString()))
        {
            stringA.Remove(stringA.Length - 1, 1);
            state = EnumState.st2;
        }
        else if (stringA.Length != 0)
        {
            stringA.Remove(stringA.Length - 1, 1);
            state = lastState;
        }
        if (stringA.Length == 0)
        {
            state = EnumState.s;
        }

    }

    private void DelCharB()
    {
        char dot = '.';
        if (stringB.ToString().Contains(dot.ToString()))
        {
            stringB.Remove(stringB.Length - 1, 1);
            state = EnumState.st6;
        }
        else if (stringB.Length != 0)
        {
            stringB.Remove(stringB.Length - 1, 1);
            CalculateExpression(stringA, stringB, operation);
            state = lastState;
        }

        if (stringB.Length == 0)
        {
            state = EnumState.st4;
        }
    }

    private void DelCharSign()
    {
        operation = "";
        state = lastState;
    }




}
