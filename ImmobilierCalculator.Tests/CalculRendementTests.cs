using System;
using FluentAssertions;
using ImmobilierCalculator.Application.Rendement;
using ImmobilierCalculator.Application.ValueObjects;
using Xunit;

namespace ImmobilierCalculator.Tests
{
    public class CalculRendementTests
    {
        [Fact(DisplayName = "Calcul du rendement")]
        public void Calculer()
        {
            var pr�t = new Pr�t(
                new(150000),
                new(1000),
                new(0.85m),
                new(150), 
                new(0.15m));

            var conditions = new ConditionLocative[]
            {
                new(new(500), new(0), new(50)),
                new(new(540), new(0), new(54)),
                new(new(400), new(0), new(40)),
            };

            var rendement = new CalculatriceRendement().Calculer(pr�t, conditions, new(1500));

            rendement.Valeur.Should().Be(10.52m);
        }

        [Fact(DisplayName = "Simulation")]
        public void Simuler()
        {
            var rendement = new Rendement(7);
            var bien = new BienImmobilier(
                new MontantBien(169000),
                new MontantTravaux(0),
                new Surface(150),
                new[]
                {
                    new Lot(new(50), 1),
                    new Lot(new(42), 2),
                    new Lot(new(40), 3),
                },
                new Taxe(1000));

            var simulation = new CalculatriceRendement().Simuler(rendement, bien);

            var rendementCalcul� =
                new CalculatriceRendement().Calculer(simulation.Pr�t, simulation.ConditionLocatives, bien.TaxeFonci�re);

            var acceptable = rendementCalcul� > rendement * 0.99m && rendementCalcul� < rendement * 1.01m;
            acceptable.Should().BeTrue();
        }

    }
}
