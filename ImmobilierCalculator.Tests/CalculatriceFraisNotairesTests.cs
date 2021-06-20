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
            var bienImmobilier = new BienImmobilier(montantPrêt, default, default, default, default, Départements.Isère);
            var fraisAttendus = new FraisNotaire(new(251.55), new(167.58), new(457.52), new(1118.6));

            var fraisNotaire = new CalculatriceFraisNotaire().Calculer(bienImmobilier);

            fraisNotaire.Valeur.Should().Be(fraisAttendus.Valeur);

        }
    }
    public class CalculatriceFraisNotariauxTests
    {
        [Fact(DisplayName = "Calcul des frais notariaux (Majorité des départements)")]
        public void Calcul_Frais_Notariaux_Majorité()
        {
            var montantPrêt = new MontantBien(200_000);
            var bienImmobilier = new BienImmobilier(montantPrêt, default, default, default, default, Départements.Isère);
            var fraisNotairesAttendus = new FraisNotaire(new(251.55), new(167.58), new(457.52), new(1118.6));
            var droitsMutationAttendus = new DroitsMutation(11_613.3);
            var fraisDiversAttendus = new FraisDivers(400);
            var fraisFormalitésAttendus = new EmolumentsFormalité(800);
            var contributionAttendue = new ContributionSécuritéImmobilière(200);
            var fraisAttendus = new FraisNotariaux(fraisNotairesAttendus, fraisFormalitésAttendus,
                fraisDiversAttendus, droitsMutationAttendus, contributionAttendue);
           
            var fraisNotariaux = new CalculatriceFraisNotariaux().Calculer(bienImmobilier);

            var acceptable = fraisNotariaux.Valeur < fraisAttendus.Valeur * 1.001 && fraisNotariaux.Valeur > fraisAttendus.Valeur * 0.099;
            acceptable.Should().BeTrue($"fraisAttendus {fraisAttendus} - frais trouvés {fraisNotariaux}");

        }
        [Fact(DisplayName = "Calcul des frais notariaux (Isère, Indre, Morbihan et Mayotte)")]
        public void Calcul_Frais_Notariaux()
        {
            var montantPrêt = new MontantBien(200_000);
            var bienImmobilier = new BienImmobilier(montantPrêt, default, default, default, default, Départements.Isère);
            var fraisNotairesAttendus = new FraisNotaire(new(251.55), new(167.58), new(457.52), new(1118.6));
            var droitsMutationAttendus = new DroitsMutation(10_181.2); var fraisDiversAttendus = new FraisDivers(400);
            var fraisFormalitésAttendus = new EmolumentsFormalité(800);
            var contributionAttendue = new ContributionSécuritéImmobilière(200);

            var fraisAttendus = new FraisNotariaux(fraisNotairesAttendus, fraisFormalitésAttendus,
                fraisDiversAttendus, droitsMutationAttendus, contributionAttendue);
            var fraisNotariaux = new CalculatriceFraisNotariaux().Calculer(bienImmobilier);
           
            var acceptable = fraisNotariaux.Valeur < fraisAttendus.Valeur * 1.001 && fraisNotariaux.Valeur > fraisAttendus.Valeur * 0.099;
            acceptable.Should().BeTrue($"fraisAttendus {fraisAttendus} - frais trouvés {fraisNotariaux}");

        }
    }
}
