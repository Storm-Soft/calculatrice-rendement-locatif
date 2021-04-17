namespace ImmobilierCalculator.Application.ValueObjects
{
    public record DecimalValue(decimal Valeur)
    {
        public static DecimalValue operator +(DecimalValue valeurA, DecimalValue valeurB)
            => new(valeurA.Valeur + valeurB.Valeur);

        public static bool operator >(DecimalValue valeurA, DecimalValue valeurB)
            => valeurA.Valeur > valeurB.Valeur;

        public static bool operator <(DecimalValue valeurA, DecimalValue valeurB)
            => valeurA.Valeur < valeurB.Valeur;

        public static DecimalValue operator *(DecimalValue valeurA, decimal coefficient)
            => valeurA with { Valeur = valeurA.Valeur * coefficient };
    }
}