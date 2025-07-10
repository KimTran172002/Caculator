Algorithm for Caculator

    CLASS CalculatorModel
    
    VAR Memory = 0
    VAR HasMemory = false

    METHOD SaveToMemory(value)
        Memory = value
        HasMemory = true

    METHOD Evaluate(expression)
        IF parentheses are unbalanced THEN RETURN "Error"
        postfix = convert infix expression to postfix
        result = evaluate postfix expression
        RETURN result as string

    METHOD InfixToPostfix(infix)
        FOR each character in infix:
            - IF digit → append to output
            - IF '(' → push to operator stack
            - IF ')' → pop until '('
            - IF operator → pop higher/equal precedence from stack
        RETURN output as postfix list

    METHOD EvaluatePostfix(postfix)
        FOR each token in postfix:
            - IF number → push to stack
            - IF operator → pop two numbers, apply operation, push result
        RETURN final result from stack

    METHOD AreParenthesesBalanced(expression)
        Track number of '(' and ')' to ensure balance

    METHOD IsOperator(c)
        RETURN true if c is +, -, *, or /

    METHOD Precedence(op)
        RETURN 1 for +, -
        RETURN 2 for *, /
<img width="428" height="676" alt="image" src="https://github.com/user-attachments/assets/f088b718-3047-4bb6-9ebe-9052a0ca3166" />
