using System;
using System.Collections.Generic;
using System.Linq;

namespace BBD_M1
{
    public class Calculator
    {
        public decimal Add(List<decimal> numbers)
        {
            return numbers.Sum();
        }

        public decimal Subtract(List<decimal> numbers)
        {
            if (numbers == null || numbers.Count == 0)
                throw new ArgumentException("La liste des nombres ne peut pas être vide.");

            decimal result = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                result -= numbers[i];
            }
            return result;
        }

        public decimal Multiply(List<decimal> numbers)
        {
            if (numbers == null || numbers.Count == 0)
                throw new ArgumentException("La liste des nombres ne peut pas être vide.");

            decimal result = 1;
            foreach (var number in numbers)
            {
                result *= number;
            }
            return result;
        }

        public decimal Divide(List<decimal> numbers)
        {
            if (numbers == null || numbers.Count == 0)
                throw new ArgumentException("La liste des nombres ne peut pas être vide.");

            decimal result = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] == 0)
                    throw new DivideByZeroException("Division par zéro détectée.");
                result /= numbers[i];
            }
            return result;
        }

        // BONUS : Calcul séquentiel sans priorité opératoire
        public decimal CalculateExpression(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentException("Expression vide");

            var tokens = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 3)
                throw new ArgumentException("Expression invalide");

            decimal result = decimal.Parse(tokens[0]);

            for (int i = 1; i < tokens.Length; i += 2)
            {
                string op = tokens[i];
                decimal value = decimal.Parse(tokens[i + 1]);

                result = op switch
                {
                    "+" => result + value,
                    "-" => result - value,
                    "*" => result * value,
                    "/" => value == 0 ? throw new DivideByZeroException() : result / value,
                    _ => throw new InvalidOperationException($"Opérateur inconnu: {op}")
                };
            }

            return result;
        }
    }
}
