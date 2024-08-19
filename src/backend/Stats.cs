namespace Sandbox_Simulator_2024;

// Applicable to any creature entity in the game
public class Stats
{
    private const float PersonVariabilitySigma = 0.1f;

    public static float GaussianBetween01(float sigma)
    {
        const float mean = 0.5f; // Centered at 0.5 to keep the values within [0, 1]
        float u1 = Random.Shared.NextSingle();
        float u2 = Random.Shared.NextSingle();
        float z0 = MathF.Sqrt(-2.0f * MathF.Log(u1)) * MathF.Cos(2.0f * MathF.PI * u2);
        float value = mean + (sigma * z0);

        // Clamp the result between 0 and 1 to keep within bounds
        value = MathF.Max(0, MathF.Min(1, value));

        return value;
    }

    // Add a
    public class TraitOperators
    {
        // Get all float from the derived class and apple the correct operator to them, either +, or /, or recombine (for reproduction later down the line)
        public static TraitOperators operator +(TraitOperators a, TraitOperators b)
        {
            TraitOperators result = new();
            foreach (var property in a.GetType().GetProperties())
            {
                if (property.PropertyType == typeof(float))
                {
                    property.SetValue(result, (float?)property.GetValue(a) + (float?)property.GetValue(b));
                }
            }
            return result;
        }

        public static TraitOperators operator /(TraitOperators a, TraitOperators b)
        {
            TraitOperators result = new();
            foreach (var property in a.GetType().GetProperties())
            {
                if (property.PropertyType == typeof(float))
                {
                    property.SetValue(result, (float?)property.GetValue(a) / (float?)property.GetValue(b));
                }
            }
            return result;
        }
    }

    public class Traits
    {
        public class Immutable : TraitOperators
        {
            public float Luck { get; set; } = GaussianBetween01(PersonVariabilitySigma);
            public float AddictionPropensity { get; set; } = GaussianBetween01(PersonVariabilitySigma);
            public float Empathy { get; set; } = GaussianBetween01(PersonVariabilitySigma);
            public float Happiness { get; set; } = GaussianBetween01(PersonVariabilitySigma);
            public float Criminality { get; set; } = GaussianBetween01(PersonVariabilitySigma);
            public float Aggression { get; set; } = GaussianBetween01(PersonVariabilitySigma);
        }

        public class Variable : TraitOperators
        {
            public float Health { get; set; } = GaussianBetween01(PersonVariabilitySigma);
            public float Intelligence { get; set; } = GaussianBetween01(PersonVariabilitySigma);
            public float Addiction { get; set; } = GaussianBetween01(PersonVariabilitySigma);
            public float Energy { get; set; } = GaussianBetween01(PersonVariabilitySigma);
            public float Hunger { get; set; } = GaussianBetween01(PersonVariabilitySigma);
            public float SocialFulfillment { get; set; } = GaussianBetween01(PersonVariabilitySigma);
        }

        public Immutable immutable = new();
        public Variable variable = new();
    }

    public Traits traits = new();

    public bool RollHealth() => Random.Shared.NextSingle() < DetermineHealth();

    public float DetermineHealth() => traits.variable.Health * traits.immutable.Happiness;

    public bool RollIntelligence() => Random.Shared.NextSingle() < DetermineIntelligence();

    public float DetermineIntelligence() => traits.variable.Health * Math.Min(traits.variable.Intelligence, traits.immutable.Criminality);

    public bool RollLuck() => Random.Shared.NextSingle() < traits.immutable.Luck;

    public bool RollAddictionPropensity() => Random.Shared.NextSingle() < DetermineAddictionPropensity();

    public float DetermineAddictionPropensity() => (1f - traits.variable.Health) * Math.Min(Math.Min(traits.immutable.AddictionPropensity, traits.immutable.Luck), traits.variable.Intelligence);

    public bool RollEmpathy() => Random.Shared.NextSingle() < DetermineEmpathy();

    public float DetermineEmpathy() => (1f - traits.variable.Addiction) * traits.immutable.Empathy;

    public bool RollSurvivalOdds() => Random.Shared.NextSingle() < DetermineSurvivalOdds();

    public float DetermineSurvivalOdds() => ((traits.variable.Health + traits.variable.Intelligence + traits.immutable.Luck) / 3f);

    public bool RollCharisma() => Random.Shared.NextSingle() < DetermineCharisma();

    public float DetermineCharisma() => traits.variable.Health * Math.Max(1f, ((traits.variable.Intelligence + traits.immutable.Luck) / 2f) + traits.variable.SocialFulfillment);

    public bool RollStrength() => Random.Shared.NextSingle() < DetermineStrength();

    public float DetermineStrength() => traits.variable.Health * ((traits.immutable.Aggression + traits.variable.Energy) / 2f);

    public bool RollMentalHealth() => Random.Shared.NextSingle() < DetermineMentalHealth();

    public float DetermineMentalHealth() => Math.Min(1f - traits.immutable.Aggression, Math.Min((1f - traits.immutable.Criminality), (1f - traits.variable.Addiction))) * Math.Max(((traits.variable.Intelligence + traits.immutable.Luck) / 2f), traits.variable.SocialFulfillment);

    public bool RollEnergy() => Random.Shared.NextSingle() < DetermineEnergy();

    public float DetermineEnergy() => (1f - traits.variable.Hunger) * Math.Max(0, ((traits.variable.Energy + traits.immutable.Happiness) / 2f) - traits.variable.Addiction);

    public bool RollHappiness() => Random.Shared.NextSingle() < DetermineHappiness();

    public float DetermineHappiness() => traits.variable.Health * (1f - traits.variable.Addiction) * ((traits.immutable.Happiness + traits.variable.Energy + traits.variable.SocialFulfillment) / 3f);

    public bool RollCriminality() => Random.Shared.NextSingle() < DetermineCriminality();

    public float DetermineCriminality() => (1f - ((traits.immutable.Empathy + traits.variable.Intelligence) / 2f)) * Math.Max(traits.immutable.Criminality, traits.immutable.Luck);

    public bool RollAggression() => Random.Shared.NextSingle() < DetermineAggression();

    public float DetermineAggression() => (1f - traits.immutable.Empathy) * traits.immutable.Aggression;
}