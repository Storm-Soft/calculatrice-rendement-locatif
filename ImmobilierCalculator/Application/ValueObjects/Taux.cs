namespace ImmobilierCalculator.Application.ValueObjects
{
    public record Taux(decimal Valeur) : DecimalValue(Valeur)
    {
        public double TauxPeriodiqueMensuel() => decimal.ToDouble(Valeur / 100 / 12);

        public static Taux operator +(Taux tauxA, Taux tauxB)
            => new(tauxA.Valeur + tauxB.Valeur);

        public static bool operator >(Taux tauxA, Taux tauxB)
            => tauxA.Valeur > tauxB.Valeur;

        public static bool operator <(Taux tauxA, Taux tauxB)
            => tauxA.Valeur < tauxB.Valeur;

        public static Taux operator *(Taux tauxA, decimal coefficient)
            => tauxA with { Valeur = tauxA.Valeur * coefficient };
    }

    public sealed record TauxAssurancePrêt(decimal Valeur) : Taux(Valeur);
    public sealed record TauxNetPrêt(decimal Valeur) : Taux(Valeur);
    public sealed record TauxTaeg(TauxNetPrêt TauxNet, TauxAssurancePrêt TauxAssurance) : Taux(TauxNet+TauxAssurance);
}