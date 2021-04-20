using System;
using ImmobilierCalculator.Application.ValueObjects;

namespace ImmobilierCalculator.Application.Calculatrices
{
    public sealed class CalculatricePrêt
    {
        public Prêt Calculer(MontantEchéance echéance, DuréePrêt durée,
            TauxNetPrêt taux, TauxAssurancePrêt assurance)
        {
            var taegMensuel = (taux + assurance).TauxPeriodiqueMensuel();

            var montant = Math.Round(echéance.Valeur*(1-Math.Pow(1+ taegMensuel.Valeur, -durée.NombreMois))/ taegMensuel.Valeur);

            return new Prêt(new(montant), echéance, taux, durée, assurance);
        }

        public Prêt Calculer(MontantPrêt montantTotal, DuréePrêt durée,
            TauxNetPrêt taux, TauxAssurancePrêt assurance)
        {
            var taegMensuel = (taux + assurance).TauxPeriodiqueMensuel();

            var échéance =(montantTotal * taegMensuel).Valeur / (1 - Math.Pow(
                1 + taegMensuel.Valeur, -durée.NombreMois));

            return new Prêt(montantTotal, new(échéance), taux, durée, assurance);

        }

        public Prêt Calculer(MontantPrêt montantTotal, MontantEchéance échéance, DuréePrêt durée)
        {
            var taux = 0.5;

            var tauxPrêt = new TauxNetPrêt(0.5);
            var tauxAssurance = new TauxAssurancePrêt(0.15);

            var prêt = Calculer(montantTotal, durée, tauxPrêt, tauxAssurance);

            if (prêt.Echéance == échéance)
                return prêt;
            if (prêt.Echéance > échéance)
            {
                do
                {
                    taux = taux - 0.01;
                    tauxPrêt = new(taux);
                    prêt = Calculer(montantTotal, durée, tauxPrêt, tauxAssurance);
                } while (prêt.Echéance > échéance);
            }
            else
            {
                do
                {
                    taux = taux + 0.01;
                    tauxPrêt = new(taux);
                    prêt = Calculer(montantTotal, durée, tauxPrêt, tauxAssurance);
                } while (prêt.Echéance < échéance);
            }

            return prêt ;
        }
    }
}