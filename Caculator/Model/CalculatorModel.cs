using System.Globalization;
using System;
using System.Collections.Generic;

namespace Calculator.Model
{
    public class CalculatorModel
    {
        public double Memory { get; private set; } = 0;
        public bool HasMemory { get; private set; } = false;

        public void SaveToMemory(double value)
        {
            Memory = value;
            HasMemory = true;
        }
        public string Evaluate(string expression)
        {
            try
            {
                if (!AreParenthesesBalanced(expression))
                    return "Error";

                var postfix = InfixToPostfix(expression);
                var result = EvaluatePostfix(postfix);
                return result.ToString(CultureInfo.InvariantCulture);
            }
            catch
            {
                return "Error";
            }
        }

        private bool AreParenthesesBalanced(string expression)
        {
            int count = 0;
            foreach (char c in expression)
            {
                if (c == '(') count++;
                else if (c == ')') count--;
                if (count < 0) return false;
            }
            return count == 0;
        }

        private List<string> InfixToPostfix(string infix)
        {
            Stack<char> ops = new Stack<char>();
            List<string> output = new List<string>();
            int i = 0;

            while (i < infix.Length)
            {
                char c = infix[i];

                if (char.IsWhiteSpace(c))
                {
                    i++;
                    continue;
                }

                
                if ((c == '-' && (i == 0 || infix[i - 1] == '(' || IsOperator(infix[i - 1]))) || char.IsDigit(c))
                {
                    string number = "";

                    
                    if (c == '-')
                    {
                        number += '-';
                        i++;
                    }

                    
                    while (i < infix.Length && (char.IsDigit(infix[i]) || infix[i] == '.'))
                    {
                        number += infix[i];
                        i++;
                    }

                    output.Add(number);
                    continue;
                }

                if (char.IsDigit(c) || c == '.')
                {
                    string number = "";
                    while (i < infix.Length && (char.IsDigit(infix[i]) || infix[i] == '.'))
                    {
                        number += infix[i];
                        i++;
                    }
                    output.Add(number);
                    continue;
                }

                if (c == '(')
                {
                    ops.Push(c);
                }
                else if (c == ')')
                {
                    while (ops.Count > 0 && ops.Peek() != '(')
                    {
                        output.Add(ops.Pop().ToString());
                    }

                    if (ops.Count == 0)
                        throw new InvalidOperationException("Mismatched parentheses");

                    ops.Pop(); // remove '('
                }
                else if (IsOperator(c))
                {
                    while (ops.Count > 0 && ops.Peek() != '(' && Precedence(ops.Peek()) >= Precedence(c))
                    {
                        output.Add(ops.Pop().ToString());
                    }
                    ops.Push(c);
                }

                i++;
            }

            while (ops.Count > 0)
            {
                char op = ops.Pop();
                if (op == '(' || op == ')')
                    throw new InvalidOperationException("Mismatched parentheses");
                output.Add(op.ToString());
            }

            return output;
        }

        private double EvaluatePostfix(List<string> postfix)
        {
            Stack<double> stack = new Stack<double>();

            foreach (string token in postfix)
            {
                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
                {
                    stack.Push(number);
                }
                else
                {
                    if (stack.Count < 2)
                        throw new InvalidOperationException("Invalid expression");

                    double b = stack.Pop();
                    double a = stack.Pop();

                    switch (token)
                    {
                        case "+": stack.Push(a + b); break;
                        case "-": stack.Push(a - b); break;
                        case "*": stack.Push(a * b); break;
                        case "/": stack.Push(a / b); break;
                        default: throw new InvalidOperationException("Unknown operator");
                    }
                }
            }

            if (stack.Count != 1)
                throw new InvalidOperationException("Invalid expression");

            return stack.Pop();
        }

        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        private int Precedence(char op)
        {
            return (op == '+' || op == '-') ? 1 : 2;
        }
    }
}
