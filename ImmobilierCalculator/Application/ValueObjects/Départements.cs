using System.Collections.Generic;

namespace ImmobilierCalculator.Application.ValueObjects
{
    public static class Départements
    {
        public static Département Indre { get; } = new Département("Indre", "36");
        public static Département Isère { get; } = new Département("Isère", "38");
        public static Département Morbihan { get; } = new Département("Morbihan", "56");
        public static Département Rhône { get; } = new Département("Rhône", "69");
        public static Département Mayotte { get; } = new Département("Mayotte", "976");

        public static IEnumerable<Département> All()
        {
            yield return Indre;
            yield return Isère;
            yield return Morbihan;
            yield return Rhône;
            yield return Mayotte;
        }
    }
}