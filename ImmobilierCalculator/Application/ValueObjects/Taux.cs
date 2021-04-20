namespace ImmobilierCalculator.Application.ValueObjects
{
    public record Taux(double Valeur) : DoubleValue(Valeur)
    {
        public Taux TauxPeriodiqueMensuel() => new Taux( Valeur / 100 / 12);

        public static Taux operator +(Taux tauxA, Taux tauxB)
            => new(tauxA.Valeur + tauxB.Valeur);

        public static bool operator >(Taux tauxA, Taux tauxB)
            => tauxA.Valeur > tauxB.Valeur;

        public static bool operator <(Taux tauxA, Taux tauxB)
            => tauxA.Valeur < tauxB.Valeur;

        public static Taux operator *(Taux tauxA, double coefficient)
            => tauxA with { Valeur = tauxA.Valeur * coefficient };
    }

    public sealed record TauxAssurancePrêt(double Valeur) : Taux(Valeur);
    public sealed record TauxNetPrêt(double Valeur) : Taux(Valeur);
    public sealed record TauxTaeg(TauxNetPrêt TauxNet, TauxAssurancePrêt TauxAssurance) : Taux(TauxNet+TauxAssurance);
}