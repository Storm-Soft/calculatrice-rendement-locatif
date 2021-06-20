using System;
using System.Runtime.InteropServices;

namespace ImmobilierCalculator.Application.ValueObjects
{
    public record Montant(double Valeur)
    {
        public static bool operator >(Montant montantA, Montant montantB)
            => montantA.Valeur > montantB.Valeur; 
        public static bool operator >=(Montant montantA, Montant montantB)
            => montantA.Valeur >= montantB.Valeur;

        public static bool operator <(Montant montantA, Montant montantB)
            => montantA.Valeur < montantB.Valeur;
        public static bool operator <=(Montant montantA, Montant montantB)
            => montantA.Valeur <= montantB.Valeur;

        //public static Montant operator *(Montant montantA, double coefficient)
        //    => montantA with { Valeur = (int)Math.Round(montantA.Valeur * coefficient) }; 

        public static Montant operator *(Montant montantA, Taux taux)
            => new(montantA.Valeur * taux.Valeur);

        public static Montant operator *(Montant montantA, double coefficient)
            => new(montantA.Valeur * coefficient);

        public static Montant operator +(Montant montantA, Montant montantB)
            => new(montantA.Valeur + montantB.Valeur);

        public static Montant operator -(Montant montantA, Montant montantB)
            => new(montantA.Valeur - montantB.Valeur);
        
        public int Round() => (int) Math.Round(Valeur, 0);
    }

    public sealed record AucunMontant() : Montant(0);
    public sealed record MontantBien(double Valeur) : Montant(Valeur);
    public sealed record MontantEchéance(double Valeur) : Montant(Valeur);
    public sealed record MontantPrêt(double Valeur) : Montant(Valeur);
    public sealed record MontantTravaux(double Valeur) : Montant(Valeur);
    public sealed record CoûtPrêt(double Valeur) : Montant(Valeur);
    public sealed record ChargeMensuelle(double Valeur) : Montant(Valeur);
    public sealed record Apport(double Valeur) : Montant(Valeur);
    public sealed record Loyer(double Valeur) : Montant(Valeur);

    public abstract record MontantOpérationMensuel(double Valeur) : Montant(Valeur);
    public sealed record OperationNeutre() : MontantOpérationMensuel(0);
    public sealed record BénéficeMensuel(double Valeur) : MontantOpérationMensuel(Valeur);
    public sealed record CoûtMensuel(double Valeur) : MontantOpérationMensuel(Valeur); 
}