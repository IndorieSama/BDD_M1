using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;
using Calculator = BBD_M1.Calculator;

namespace BDD_M1.Steps
{
    [Binding]
    public class CalculatorSteps
    {
        private Calculator _calculator;
        private decimal _result;
        private Exception _caughtException;

        [Given("la calculatrice est initialisée")]
        public void GivenLaCalculatriceEstInitialisee()
        {
            _calculator = new Calculator();
            _caughtException = null;
        }

        [When(@"je calcule la somme de (.*)")]
        public void WhenJeCalculeLaSommeDe(string nombres)
        {
            var values = ParseNombres(nombres);
            _result = _calculator.Add(values);
        }

        [When(@"je calcule la soustraction de (.*)")]
        public void WhenJeCalculeLaSoustractionDe(string nombres)
        {
            var values = ParseNombres(nombres);
            _result = _calculator.Subtract(values);
        }

        [When(@"je calcule le produit de (.*)")]
        public void WhenJeCalculeLeProduitDe(string nombres)
        {
            var values = ParseNombres(nombres);
            _result = _calculator.Multiply(values);
        }

        [When(@"je calcule la division de (.*)")]
        public void WhenJeCalculeLaDivisionDe(string nombres)
        {
            try
            {
                var values = ParseNombres(nombres);
                _result = _calculator.Divide(values);
            }
            catch (Exception ex)
            {
                _caughtException = ex;
            }
        }

        [When(@"je calcule ""(.*)""")]
        public void WhenJeCalcule(string expression)
        {
            try
            {
                _result = _calculator.CalculateExpression(expression);
            }
            catch (Exception ex)
            {
                _caughtException = ex;
            }
        }

        [Then(@"le résultat doit être (.*)")]
        public void ThenLeResultatDoitEtre(double resultatAttendu)
        {
            Assert.Equal((decimal)resultatAttendu, _result);
        }

        [Then("une erreur de division par zéro est levée")]
        public void ThenUneErreurDeDivisionParZeroEstLevee()
        {
            Assert.NotNull(_caughtException);
            Assert.IsType<DivideByZeroException>(_caughtException);
        }

        // Méthode utilitaire pour parser la liste de nombres
        private List<decimal> ParseNombres(string nombres)
        {
            return nombres
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(n => decimal.Parse(n.Trim(), CultureInfo.InvariantCulture))
                .ToList();
        }
    }
}
