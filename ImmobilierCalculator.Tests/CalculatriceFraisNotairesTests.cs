using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using ImmobilierCalculator.Application.Calculatrices;
using ImmobilierCalculator.Application.ValueObjects;
using Xunit;

namespace ImmobilierCalculator.Tests
{
    public class CalculatriceFraisNotairesTests
    {
        [Fact(DisplayName = "Calcul des frais de notaire")]
        public void Calcul_Frais_Notaire()
        {
            var montantPrêt = new MontantBien(200_000);
            var bienImmobilier = new BienImmobilier(montantPrêt, default, default, default, default);
            var fraisAttendus = new FraisNotaire(new(251.55), new(167.58), new(457.52), new(1118.6));

            var fraisNotaire = new CalculatriceFraisNotaire().Calculer(bienImmobilier);

            fraisNotaire.Valeur.Should().Be(fraisAttendus.Valeur);

        }
    }
}
