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
    public class CalculatricePrêtTests
    {
        [Fact(DisplayName = "Calculer du montant total Prêt")]
        public void Calcul_MontantTotal()
        {
            var mensualité = new MontantEchéance(1011);
            var durée = new DuréePrêt(180);
            var taux = new TauxNetPrêt(0.85);
            var assurance = new TauxAssurancePrêt(0.15);

            var prêt = new CalculatricePrêt().Calculer(mensualité, durée, taux, assurance);
            var montantAttendu = new MontantPrêt(168924);
            prêt.Montant.Should().Be(montantAttendu);
        }

        [Fact(DisplayName = "Calculer de l'échéance")]
        public void Calcul_Mensualité()
        {
            var montant = new MontantPrêt(169_000);
            var durée = new DuréePrêt(180);
            var taux = new TauxNetPrêt(0.85);
            var assurance = new TauxAssurancePrêt(0.15);

            var prêt = new CalculatricePrêt().Calculer(montant, durée, taux, assurance);

            var échéanceAttendue = 1011;
            prêt.Echéance.Round().Should().Be(échéanceAttendue);
        }

        [Fact(DisplayName = "Calculer du taux")]
        public void Calcul_Taux()
        {
            var montant = new MontantPrêt(169_000);
            var mensualité = new MontantEchéance(1011);
            var durée = new DuréePrêt(180);

            var prêt = new CalculatricePrêt().Calculer(montant, mensualité, durée);
            var tauxAttendu = new TauxNetPrêt(0.85);

            var acceptable = prêt.Taux < tauxAttendu * 1.01 && prêt.Taux > tauxAttendu * 0.99;
            acceptable.Should().BeTrue($"taux attendu {tauxAttendu} - taux trouvé {prêt.Taux}");
        }

    }
}
