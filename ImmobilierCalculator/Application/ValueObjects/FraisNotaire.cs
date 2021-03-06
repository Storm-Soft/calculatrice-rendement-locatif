using System;

namespace ImmobilierCalculator.Application.ValueObjects
{
    public record MontantTranche(double Valeur) : Montant(Valeur);
    public record AucunMontantDeTranche() : MontantTranche(0);

    public record LimiteTranche(MontantTranche Montant);
    public record PasDeLimite() : LimiteTranche(new AucunMontantDeTranche());

    public record Tranche(int Numéro, LimiteTranche LimiteBasse, LimiteTranche LimiteHaute, Taux TauxApplicable);

    public record Honoraires(double Valeur) : Montant(Valeur);

    public record FraisNotaire(MontantTranche Tranche1, MontantTranche Tranche2, MontantTranche Tranche3,
        MontantTranche Tranche4) : Montant(Tranche1 + Tranche2 + Tranche3 + Tranche4);

    public record EmolumentsFormalité(double Valeur) : Montant(Valeur);
    public record FraisDivers(double Valeur) : Montant(Valeur);
    public record DroitsMutation(double Valeur) : Montant(Valeur);
    public record ContributionSécuritéImmobilière(double Valeur) : Montant(Valeur);

    public record FraisNotariaux(FraisNotaire FraisNotaire, EmolumentsFormalité EmolumentsFormalité,
            FraisDivers FraisDivers, DroitsMutation DroitsMutation,
            ContributionSécuritéImmobilière ContributionSécuritéImmobilière)
        : Montant(FraisNotaire + EmolumentsFormalité + FraisDivers + DroitsMutation + ContributionSécuritéImmobilière);
}
