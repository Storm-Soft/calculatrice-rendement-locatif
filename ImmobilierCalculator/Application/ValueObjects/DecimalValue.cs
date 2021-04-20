namespace ImmobilierCalculator.Application.ValueObjects
{
    public record DoubleValue(double Valeur)
    {
        public static DoubleValue operator +(DoubleValue valeurA, DoubleValue valeurB)
            => new(valeurA.Valeur + valeurB.Valeur);

        public static bool operator >(DoubleValue valeurA, DoubleValue valeurB)
            => valeurA.Valeur > valeurB.Valeur;

        public static bool operator <(DoubleValue valeurA, DoubleValue valeurB)
            => valeurA.Valeur < valeurB.Valeur;

        public static DoubleValue operator *(DoubleValue valeurA, double coefficient)
            => valeurA with { Valeur = valeurA.Valeur * coefficient };
    }
}