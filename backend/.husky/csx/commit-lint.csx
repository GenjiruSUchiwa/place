#!/usr/bin/env dotnet-script
using System;
using System.IO;
using System.Text.RegularExpressions;

try
{
    var pattern =
        @"^(?=.{1,90}$)"
        + @"(feat|feat!|chore|ci|build|docs|fix|perf|refactor|revert|style|test|wip)"
        + @"(\([a-z-]+\))?:\s+[A-Za-z].{3,}\s+\(\#\d+\)$";

    // Correction ici : Utiliser Count au lieu de Length
    if (Args.Count == 0)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Chemin du message de commit manquant");
        return 2;
    }

    var msg = File.ReadAllText(Args[0]).Trim();

    if (Regex.IsMatch(msg, pattern, RegexOptions.IgnoreCase))
        return 0;

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Message de commit invalide");
    Console.ResetColor();
    Console.WriteLine("Format requis : type[scope]: description (#123)");
    Console.WriteLine("Exemple : feat(auth): ajout connexion Google (#456)");
    return 1;
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"ERREUR: {ex.Message}");
    return 1;
}
finally
{
    Console.ResetColor();
}
