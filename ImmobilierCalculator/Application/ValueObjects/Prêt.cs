namespace ImmobilierCalculator.Application.ValueObjects
{
    public sealed record Prêt(MontantPrêt Montant, MontantEchéance Echéance, TauxNetPrêt Taux, DuréePrêt Durée, TauxAssurancePrêt TauxAssurance);
}