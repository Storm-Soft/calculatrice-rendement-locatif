using System;
using System.Collections.Generic;
using System.Linq;

using ImmobilierCalculator.Application.ValueObjects;

namespace ImmobilierCalculator.Application.Calculatrices
{

    public sealed class CalculatriceRendement
    {
        /// <summary>
        /// Calcule le rendement à partir d'un prêt, de conditions locatives et d'une taxe foncière
        /// </summary>
        public Rendement Calculer(Prêt prêt, IList<ConditionLocative> conditions, Taxe taxeFoncière)
            => new((conditions.Sum(x => x.Loyer.Valeur + x.Charges.Valeur) * 12 - taxeFoncière.Valeur) / prêt.Montant.Valeur * 100);

        /// <summary>
        /// Calcule le loyer minimum pour atteindre un rendement en fonction d'un prêt et d'une taxe foncière
        /// </summary>
        public Loyer Calculer(MontantPrêt prêt, Rendement rendement, Taxe taxeFoncière)
         => new ((int) Math.Round((rendement.Valeur* prêt.Valeur) / (12 * 100) + taxeFoncière.Valeur/12));

        /// <summary>
        /// Calcule le montant mensuel de l'opération
        public MontantOpérationMensuel Calculer(MontantEchéance échéance, Loyer loyer)
            => échéance == loyer
                ? new OperationNeutre()
                :(échéance > loyer)
                    ? new CoûtMensuel((échéance - loyer).Valeur)
                    : new BénéficeMensuel((loyer - échéance).Valeur);

        public Simulation Simuler(Rendement rendement, BienImmobilier bienImmobilier)
        {
            var montantPrêt = new MontantPrêt(bienImmobilier.Montant.Valeur);
            var loyernécessaire =
                new CalculatriceRendement().Calculer(montantPrêt, rendement, bienImmobilier.TaxeFoncière);
            var loyerParM2 = loyernécessaire.Valeur * 1d / bienImmobilier.Lots.Sum(x => x.Surface.Valeur);

            var prêt = new CalculatricePrêt().Calculer(montantPrêt, new MontantEchéance(loyernécessaire.Valeur),
                new DuréePrêt(180));
            
            var conditionsLocatives = bienImmobilier.Lots.Select(
                lot => new ConditionLocative(new Loyer((int)Math.Round(loyerParM2 * lot.Surface.Valeur)), new ChargeMensuelle(0), lot.Surface));

            return new Simulation(prêt, conditionsLocatives.ToList());
        }
    }
}
