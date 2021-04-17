using System.Collections.Generic;

namespace ImmobilierCalculator.Application.ValueObjects
{
    public sealed record Simulation(Prêt Prêt, IList<ConditionLocative> ConditionLocatives);
}