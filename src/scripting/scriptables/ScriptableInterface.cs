namespace Sandbox_Simulator_2024.src.scripting.scriptable;

using Sandbox_Simulator_2024.src.backend;
using System;
using Identifier = string;

public class ScriptableInterface(Identifier identifier) : IScriptable
{
    public interface IProperty { PropertyType PropertyType { get; } }

    public enum PropertyType
    {
        Bool,
        Int,
        String,
        Random,
        Action,
    }

    public Identifier identifier { get; } = identifier;
    readonly Dictionary<Identifier, object> properties = new();

    public bool AddProperty(string propertyType, Identifier propertyName)
    {
        if (properties.ContainsKey(propertyName))
        {
            ForegroundColor = ConsoleColor.Red;
            Write("(duplicate) ");
            return false;
        }

        switch (propertyType)
        {
            case "bool":
                properties.Add(propertyName, false);
                break;

            case "int":
                properties.Add(propertyName, 0);
                break;

            case "string":
                properties.Add(propertyName, "");
                break;

            case "random":
                properties.Add(propertyName, new Chance());
                break;

            case "action":
                properties.Add(propertyName, new Action(() => { }));
                break;

            default:
                ForegroundColor = ConsoleColor.Red;
                Write("(invalid) ");
                return false;
        }

        ForegroundColor = ConsoleColor.White;
        Write($"(property {propertyType} added) ");
        return true;
    }
}