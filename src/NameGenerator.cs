namespace Sandbox_Simulator_2024;

public static class NameGenerator
{
    private const string consonants = "bcdfghjklmnprstvwxz";
    private const string vowels = "aeiouaeiouaeioueeeeeaay";

    private static readonly string[] NameEnders = [
        "a", "ia", "en", "on",
        "ar", "or", "ur", "ir",
        "el", "il", "ol", "ul",
        "es", "is", "os", "us",
        "an", "in", "on", "un",
    ];

    private static readonly string[] Awks = [
        "pf", "bv", "lp", "rp",
        "tk", "dk", "gk",
        "mr", "lr", "nr",
        "zm", "zn", "zl",
        "vl", "vr", "vn",
        "js", "jz", "jm",
        "kp", "kd", "kt",
        "xb", "xd", "xg",
        "uu", "ii", "oo",
        "aou", "eiu", "oie",
        "uie", "eao", "aei",
        "bt", "ct", "ft", "pt",
        "md", "mt", "ng",
        "wl", "wn", "wt",
        "rb", "rg", "rk",
        "sv", "sr", "sl",
        "tl", "tn", "gn",
        "ngl", "mbl", "ntl",
        "rts", "lts", "nts",
        "str", "spr", "scr",
        "hg", "hk", "hb",
        "vh", "jh", "lh",
        "qk", "qg", "qd",
        "cx", "cv", "cb",
        "fv", "fw", "fz",
        "shs", "shz", "zhz",
        "sts", "szs", "zsh",
        "edo", "otu", "aki",
    ];

    private static HashSet<string> usedNames = new();

    private static Random r = Random.Shared;

    public static string GenerateName()
    {
        
        try
        {
            string name = "";

            // Start with a partial name
            name += PartialName();

            // Add a random number of partial names
            int numParts = r.Next(0, 2);
            for (int i = 0; i < numParts; i++)
            {
                name += PartialName();
            }

            // Chop off the end bits maybe
            if (name.Length > 9)
            {
                int startIndex = r.Next(0, 3);
                int endIndex = r.Next(0, 3);

                // Chop off the end bits
                name = name.Substring(0, name.Length - endIndex);

                // Chop off the start bits
                name = name.Substring(startIndex);
            }

            if (r.NextSingle() > 0.5f)
            {
                // If the last letter is a vowel, add a consonant
                if (r.NextSingle() > 0.5f && vowels.Contains(name[^1])) name += GenerateConsonant();
                name += NameEnders[r.Next(0, NameEnders.Length)];
            }

            // Remove any awkward letter combinations
            foreach (var awk in Awks)
            {
                string candidateName = name.Replace(awk, "");
                if (candidateName != name)
                {
                    // Only remove 1 awkward letter combination
                    name = candidateName;
                    break;
                }
            }

            // Remove triplicate letters
            for (int i = 0; i < name.Length - 2; i++)
            {
                if (name[i] == name[i + 1] && name[i] == name[i + 2])
                {
                    name = name.Remove(i + 2, 1);
                }
            }

            // Remove triplicate vowels
            for (int i = 0; i < name.Length - 2; i++)
            {
                if (vowels.Contains(name[i]) && vowels.Contains(name[i + 1]) && vowels.Contains(name[i + 2]))
                {
                    name = name.Remove(i + 2, 1);
                }
            }

            // Remove triplicate consonants
            for (int i = 0; i < name.Length - 2; i++)
            {
                if (consonants.Contains(name[i]) && consonants.Contains(name[i + 1]) && consonants.Contains(name[i + 2]))
                {
                    name = name.Remove(i + 2, 1);
                }
            }

            // Remove double-consonant endings
            if (consonants.Contains(name[name.Length - 1]) && consonants.Contains(name[name.Length - 2]))
            {
                name = name.Remove(name.Length - 1);
            }

            // Remove double-consonants beginnings
            if (consonants.Contains(name[0]) && consonants.Contains(name[1]))
            {
                name = name.Remove(1);
            }

            // Remove double-vowels that aren't 'oo' or 'ee'
            for (int i = 0; i < name.Length - 1; i++)
            {
                if (vowels.Contains(name[i]) && vowels.Contains(name[i + 1]) && name[i] != 'o' && name[i] != 'e')
                {
                    name = name.Remove(i + 1);
                }
            }

            // If the name is too short, try again
            if (name.Length <= 1) return GenerateName();

            // Capitalize the first letter and return
            string finalCandidateName = string.Concat(name[0].ToString().ToUpper(), name.AsSpan(1));
            if (usedNames.Contains(finalCandidateName)) return GenerateName();
            usedNames.Add(finalCandidateName);
            return finalCandidateName;
        }
        catch (Exception)
        {
            return GenerateName();
        }
    }

    private static string PartialName()
    {

        if (r.NextSingle() > 0.5f)
        {
            return GenerateConsonant() + OneOrTwoVowels() + GenerateConsonant();
        }
        else
        {
            return GenerateVowel() + OneOrTwoConsonants() + GenerateVowel();
        }
    }

    private static string OneOrTwoConsonants()
    {
        if (r.NextSingle() > 0.5f) return GenerateConsonant();
        else return GenerateConsonant() + GenerateConsonant();
    }

    private static string OneOrTwoVowels()
    {
        if (r.NextSingle() > 0.5f) return GenerateVowel();
        else return GenerateVowel() + GenerateVowel();
    }

    private static string GenerateConsonant() => consonants[r.Next(0, consonants.Length)].ToString();

    private static string GenerateVowel() => vowels[r.Next(0, vowels.Length)].ToString();
}