using ImmobilierCalculator.Application.ValueObjects;

namespace ImmobilierCalculator.Application.Calculatrices
{
    public sealed class CalculatriceFraisNotariaux
    {
        public FraisNotariaux Calculer(BienImmobilier bienImmobilier)
        {
            var fraisNotaires = new CalculatriceFraisNotaire().Calculer(bienImmobilier);
            var contributionSécuritéImmobilière = CalculerContributionSécuritéImmobilière(bienImmobilier);
            var droitsMutation = CalculerDroitsMutation(bienImmobilier);
            var fraisDivers = CalculerFraisDivers(bienImmobilier);
            var fraisFormalité = CalculerFraisFormalités(bienImmobilier);

            return new FraisNotariaux(fraisNotaires, fraisFormalité, fraisDivers, droitsMutation,
                contributionSécuritéImmobilière);
        }

        private EmolumentsFormalité CalculerFraisFormalités(BienImmobilier bienImmobilier)
            => new EmolumentsFormalité(800);

        private FraisDivers CalculerFraisDivers(BienImmobilier bienImmobilier)
            => new FraisDivers(400);

        private DroitsMutation CalculerDroitsMutation(BienImmobilier bienImmobilier)
            => new DroitsMutation((bienImmobilier.Montant * GetTauxDroitsMutation(bienImmobilier.Localisation)).Valeur);

        private static double GetTauxDroitsMutation(Département département)
            => département switch
            {
                var dpt when
                    dpt == Départements.Isère ||
                    dpt == Départements.Indre ||
                    dpt == Départements.Morbihan ||
                    dpt == Départements.Mayotte => 0.0509006,
                _ => 0.0580665
            };


        private ContributionSécuritéImmobilière CalculerContributionSécuritéImmobilière(BienImmobilier bienImmobilier)
            => new ContributionSécuritéImmobilière((bienImmobilier.Montant * 0.001).Valeur);
    }
}