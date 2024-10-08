﻿using Sandbox_Simulator_2024.src.scripting;

namespace Sandbox_Simulator_2024.src;

internal static class Program
{
    private static async Task Main()
    {
        Print.Clear();
        WriteLine(new string('#', WindowWidth));
        WriteLine(new string('#', WindowWidth));
        WriteLine(new string('#', WindowWidth));
        WriteLine(new string('#', WindowWidth));
        WriteLine(new string('#', WindowWidth));
        WriteLine(new string('#', WindowWidth));
        WriteLine(new string('#', WindowWidth));
        WriteLine(new string('#', WindowWidth));
        WriteLine(new string('#', WindowWidth));
        WriteLine(new string('#', WindowWidth));
        Print.Immediate("Sandbox Simulator 2024");
        Print.ConsoleResetColor();

        /*for (int i = 0; i < 100; i++)
        {
            Stats stats = new Stats();
            Print.Line($"Health: {stats.RollHealth()}");
            Print.Line($"Intelligence: {stats.RollIntelligence()}");
            Print.Line($"Luck: {stats.RollLuck()}");
            Print.Line($"Addiction Propensity: {stats.RollAddictionPropensity()}");
            Print.Line($"Empathy: {stats.RollEmpathy()}");
            Print.Line($"Survival Odds: {stats.RollSurvivalOdds()}");
            Print.Line($"Charisma: {stats.RollCharisma()}");
            Print.Line($"Strength: {stats.RollStrength()}");
            Print.Line($"Mental Health: {stats.RollMentalHealth()}");
            Print.Line($"Energy: {stats.RollEnergy()}");
            Print.Line($"Happiness: {stats.RollHappiness()}");
            Print.Line($"Criminality: {stats.RollCriminality()}");
            Print.Line();
            Print.Line();
            Print.Line();
        }*/

        Print.Line("Welcome to Sandbox Simulator 2024");

        ScriptInterpreter interpreter = new(FireStationExampleScript.draft);
        //await Network.Start("Test Net");

        await Task.Delay(-1);
        while (true)
        {
            Print.Clear();
            Print.Pause("play again");
        }
    }
}