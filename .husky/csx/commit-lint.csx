using System;
using System.IO;
using System.Text.RegularExpressions;

class CommitValidator
{
    private static readonly string Pattern =
        @"^(?=.{1,90}$)" + // Longueur totale 1-90 caractères
        @"(feat|feat!|chore|ci|build|docs|fix|perf|refactor|revert|style|test|wip)" + // Type de commit
        @"(\([a-z-]+\))?" + // Scope optionnel
        @":\s+" + // Deux-points + espace
        @"[A-Za-z].{3,}" + // Description 
        @"\s+\(\#\d+\)$"; // Numéro d'issue entre parenthèses

    static int Main(string[] args)
    {
        try
        {
            if (args.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Usage: commit-validator <commit_message_file_path>");
                return 2;
            }

            var commitMessage = File.ReadAllText(args[0]).Trim();
            
            if (Regex.IsMatch(commitMessage, Pattern, RegexOptions.IgnoreCase))
                return 0;

            DisplayErrorMessage();
            return 1;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            return 1;
        }
        finally
        {
            Console.ResetColor();
        }
    }

    private static void DisplayErrorMessage()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid commit message format!");
        Console.ResetColor();
        
        Console.WriteLine("\nFormat requis :");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("<type>[scope]: <description> (#<issue>)");
        Console.ResetColor();
        
        Console.WriteLine("\nExemples valides :");
        Console.WriteLine("• feat: add user authentication (#123)");
        Console.WriteLine("• fix(api): resolve timeout issue (#456)");
        Console.WriteLine("• docs: update installation guide (#789)");
        
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\nTypes autorisés : " + 
            "feat, fix, docs, chore, style, refactor, test, build, ci, perf, revert");
    }
}