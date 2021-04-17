using System;

namespace ImmobilierCalculator.Application.ValueObjects
{
    public abstract record Montant(int Valeur)
    {
        public static bool operator >(Montant montantA, Montant montantB)
            => montantA.Valeur > montantB.Valeur;

        public static bool operator <(Montant montantA, Montant montantB)
            => montantA.Valeur < montantB.Valeur;

        public static Montant operator *(Montant montantA, double coefficient)
            => montantA with { Valeur = (int)Math.Round(montantA.Valeur * coefficient) };

        public static int operator +(Montant montantA, Montant montantB)
            => montantA.Valeur + montantB.Valeur;

        public static int operator -(Montant montantA, Montant montantB)
            => montantA.Valeur - montantB.Valeur;
    }


    public sealed record MontantBien(int Valeur) : Montant(Valeur);
    public sealed record MontantEchéance(int Valeur) : Montant(Valeur);
    public sealed record MontantPrêt(int Valeur) : Montant(Valeur);
    public sealed record MontantTravaux(int Valeur) : Montant(Valeur);
    public sealed record CoûtPrêt(int Valeur) : Montant(Valeur);
    public sealed record ChargeMensuelle(int Valeur) : Montant(Valeur);
    public sealed record Apport(int Valeur) : Montant(Valeur);
    public sealed record Loyer(int Valeur) : Montant(Valeur);

    public abstract record MontantOpérationMensuel(int Valeur) : Montant(Valeur);
    public sealed record OperationNeutre() : MontantOpérationMensuel(0);
    public sealed record BénéficeMensuel(int Valeur) : MontantOpérationMensuel(Valeur);
    public sealed record CoûtMensuel(int Valeur) : MontantOpérationMensuel(Valeur); 
}