using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmobilierCalculator.Application.ValueObjects;

namespace ImmobilierCalculator.Application.Calculatrices
{
    public sealed class CalculatriceFraisNotaire
    {
        private const double TauxTranche1 = 3.870/100;
        private const double TauxTranche2 = 1.596/100;
        private const double TauxTranche3 = 1.064/100;
        private const double TauxTranche4 = 0.799/100;

        private static readonly Tranche Tranche1 = new (1, new LimiteTranche(new(0)), new LimiteTranche(new(6500)), new Taux(TauxTranche1));
        private static readonly Tranche Tranche2 = new (2, new LimiteTranche(new(6500)), new LimiteTranche(new(17000)), new Taux(TauxTranche2));
        private static readonly Tranche Tranche3 = new (3, new LimiteTranche(new(17000)), new LimiteTranche(new(60000)), new Taux(TauxTranche3));
        private static readonly Tranche Tranche4 = new (4, new LimiteTranche(new(60000)), new PasDeLimite(), new Taux(TauxTranche4));

        private FraisNotaire Create(MontantTranche tranche1 = default, MontantTranche tranche2 = default, MontantTranche tranche3 = default,
            MontantTranche tranche4 = default)
            => new (
                tranche1 ?? new AucunMontantDeTranche(),
                tranche2 ?? new AucunMontantDeTranche(), 
                tranche3 ?? new AucunMontantDeTranche(), 
                tranche4 ?? new AucunMontantDeTranche());


        public FraisNotaire Calculer(BienImmobilier bienImmobilier)
        {
            var fraisNotaire = new FraisNotaire(new AucunMontantDeTranche(), new AucunMontantDeTranche(), new AucunMontantDeTranche(), new AucunMontantDeTranche());
            if (bienImmobilier.Montant < Tranche1.LimiteHaute.Montant)
                return Create(tranche1:new((bienImmobilier.Montant * Tranche1.TauxApplicable).Valeur));

            fraisNotaire = Create(tranche1: new((Tranche1.LimiteHaute.Montant * Tranche1.TauxApplicable).Valeur));

            if (bienImmobilier.Montant < Tranche2.LimiteHaute.Montant)
                return Create(tranche1: fraisNotaire.Tranche1,
                              tranche2: new(((bienImmobilier.Montant - Tranche2.LimiteBasse.Montant) * Tranche2.TauxApplicable).Valeur));

            fraisNotaire = Create(tranche1: fraisNotaire.Tranche1,
                                  tranche2: new(((Tranche2.LimiteHaute.Montant - Tranche2.LimiteBasse.Montant) * Tranche2.TauxApplicable).Valeur));

            if (bienImmobilier.Montant < Tranche3.LimiteHaute.Montant)
                return Create(tranche1: fraisNotaire.Tranche1,
                              tranche2: fraisNotaire.Tranche2,
                              tranche3: new (((bienImmobilier.Montant - Tranche3.LimiteBasse.Montant) * Tranche3.TauxApplicable).Valeur));

            return Create(
                tranche1: fraisNotaire.Tranche1,
                tranche2: fraisNotaire.Tranche2,
                tranche3: new(((Tranche3.LimiteHaute.Montant - Tranche3.LimiteBasse.Montant) * Tranche3.TauxApplicable).Valeur),
                tranche4: new(((bienImmobilier.Montant - Tranche4.LimiteBasse.Montant) * Tranche4.TauxApplicable).Valeur));
        }
    }
}
