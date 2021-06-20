using System;
using FluentAssertions;
using ImmobilierCalculator.Application.Calculatrices;
using ImmobilierCalculator.Application.ValueObjects;
using Xunit;

namespace ImmobilierCalculator.Tests
{
    public class CalculRendementTests
    {
        [Fact(DisplayName = "Calcul du rendement")]
        public void Calculer()
        {
            var bienImmobilier = new BienImmobilier(
                new(120_000),
                new MontantTravaux(30_000),
                default, 
                default,
                new Taxe(1500),
                Localisation:Départements.Isère);

            var conditions = new ConditionLocative[]
            {
                new(new(500), new(0), new(50)),
                new(new(540), new(0), new(54)),
                new(new(400), new(0), new(40)),
            };

            var rendement = new CalculatriceRendement().Calculer(bienImmobilier, conditions);

            rendement.Valeur.Should().Be(10.52);
        }

        [Fact(DisplayName = "Simulation")]
        public void Simuler()
        {
            var rendement = new Rendement(7);
            var bien = new BienImmobilier(
                new MontantBien(150_000),
                new MontantTravaux(19_000),
                new Surface(150),
                new[]
                {
                    new Lot(new(50), 1),
                    new Lot(new(42), 2),
                    new Lot(new(40), 3),
                },
                new Taxe(1000),
                Départements.Isère);

            var simulation = new CalculatriceRendement().Simuler(rendement, bien);

            var rendementCalculé =
                new CalculatriceRendement().Calculer(bien, simulation.ConditionLocatives);

            var acceptable = rendementCalculé > rendement * 0.99 && rendementCalculé < rendement * 1.01;
            acceptable.Should().BeTrue();
        }
    }
}
