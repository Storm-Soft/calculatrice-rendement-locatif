using System;
using System.Collections.Generic;
using System.Linq;
using ImmobilierCalculator.Application.ValueObjects;

namespace ImmobilierCalculator.Application.Rendement
{
   
    public sealed class CalculatriceRendement
    {
        public ValueObjects.Rendement Calculer(Prêt prêt, IList<ConditionLocative> conditions, Taxe taxeFoncière)
            => new((conditions.Sum(x => x.Loyer.Valeur + x.Charges.Valeur) * 12m - taxeFoncière.Valeur) / prêt.Montant.Valeur * 100);

        public Simulation Simuler(ValueObjects.Rendement rendement, BienImmobilier bienImmobilier)
        {
            var montantPrêt = new MontantPrêt(bienImmobilier.Montant.Valeur);
            var loyernécessaire = new Loyer((int)Math.Round((rendement.Valeur * montantPrêt.Valeur) / (12 * 100) + bienImmobilier.TaxeFoncière.Valeur/12));

            var loyerParM2 = loyernécessaire.Valeur * 1d / bienImmobilier.Lots.Sum(x => x.Surface.Valeur);

            var prêt = new CalculatricePrêt().Calculer(montantPrêt, new MontantEchéance(loyernécessaire.Valeur),
                new DuréePrêt(180));
            
            var conditionsLocatives = bienImmobilier.Lots.Select(
                lot => new ConditionLocative(new Loyer((int)Math.Round(loyerParM2 * lot.Surface.Valeur)), new ChargeMensuelle(0), lot.Surface));

            return new Simulation(prêt, conditionsLocatives.ToList());
        }
    }

    public sealed class CalculatricePrêt
    {
        public Prêt Calculer(MontantEchéance echéance, DuréePrêt durée,
            TauxNetPrêt taux, TauxAssurancePrêt assurance)
        {
            var taegMensuel = (taux + assurance).TauxPeriodiqueMensuel();

            var montant = (int)Math.Round(echéance.Valeur*(1-Math.Pow(1+ taegMensuel, -durée.NombreMois))/ taegMensuel);

            return new Prêt(new(montant), echéance, taux, durée, assurance);
        }

        public Prêt Calculer(MontantPrêt montantTotal, DuréePrêt durée,
            TauxNetPrêt taux, TauxAssurancePrêt assurance)
        {
            var taegMensuel = (taux + assurance).TauxPeriodiqueMensuel();

            var échéance = (int)Math.Round(montantTotal.Valeur * taegMensuel / (1 - Math.Pow(
                1 + taegMensuel, -durée.NombreMois)));

            return new Prêt(montantTotal, new(échéance), taux, durée, assurance);

        }

        public Prêt Calculer(MontantPrêt montantTotal, MontantEchéance échéance, DuréePrêt durée)
        {
            var taux = 0.5m;

            var tauxPrêt = new TauxNetPrêt(0.5m);
            var tauxAssurance = new TauxAssurancePrêt(0.15m);

            var prêt = Calculer(montantTotal, durée, tauxPrêt, tauxAssurance);

            if (prêt.Echéance == échéance)
                return prêt;
            if (prêt.Echéance > échéance)
            {
                do
                {
                    taux = taux - 0.01m;
                    tauxPrêt = new(taux);
                    prêt = Calculer(montantTotal, durée, tauxPrêt, tauxAssurance);
                } while (prêt.Echéance > échéance);
            }
            else
            {
                do
                {
                    taux = taux + 0.01m;
                    tauxPrêt = new(taux);
                    prêt = Calculer(montantTotal, durée, tauxPrêt, tauxAssurance);
                } while (prêt.Echéance < échéance);
            }

            return prêt ;
        }
    }

}
