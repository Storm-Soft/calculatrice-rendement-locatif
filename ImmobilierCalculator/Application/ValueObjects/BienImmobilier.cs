using System.Collections.Generic;

namespace ImmobilierCalculator.Application.ValueObjects
{
    public sealed record BienImmobilier(MontantBien Montant, MontantTravaux Travaux, Surface SurfaceTotale,
        IList<Lot> Lots, Taxe TaxeFoncière)
    {
        public Montant MontantTotal => Montant + Travaux;
    }
}