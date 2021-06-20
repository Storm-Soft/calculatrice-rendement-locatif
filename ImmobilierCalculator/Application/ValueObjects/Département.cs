namespace ImmobilierCalculator.Application.ValueObjects
{
    public sealed record Département(string Nom, string Numéro)
    {
        public override string ToString() => $"{Numéro} - {Nom}";
    }
}