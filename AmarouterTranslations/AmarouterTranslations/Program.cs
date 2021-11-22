using System;
using Mono.TextTemplating;

namespace Translations
{
    public class Program
    {
        private static readonly string AMAROUTER_PROJECT_PATH = @"D:\repos\myamarouter\";

        private static readonly string SHARED_PROJECT_PATH = @"D:\repos\shared-ui\";

        private static readonly string[] LOCALES = { "en", "da", "de", "et", "fi", "fr", "lv", "nb", "nl", "pl", "sv" };

        static void Main(string[] args)
        {
            Console.WriteLine("---AMAROUTER---");
            AmarouterCoreProjectTranslations();
            AmarouterIOSProjectTranslations();

            //Console.WriteLine("\n---SHARED---");
            //SharedCoreProjectTranslations();
            //SharedIOSProjectTranslations();

            Console.WriteLine("\n---ALL DONE---");
            Console.ReadKey();
        }

        private static void AmarouterCoreProjectTranslations()
        {
            CoreProjectTranslations(AMAROUTER_PROJECT_PATH + "/src/Amazone.IoM.Agrirouter.Core/Localization", "Strings");
        }

        private static void AmarouterIOSProjectTranslations()
        {
            IOSProjectTranslations(AMAROUTER_PROJECT_PATH + "/src/Amazone.IoM.Agrirouter.iOS/Resources", "Localizable");
        }

        private static void SharedCoreProjectTranslations()
        {
            CoreProjectTranslations(SHARED_PROJECT_PATH + "src/Amazone.IoM.SharedUI.Core/Localization", "SharedStrings");
        }

        private static void SharedIOSProjectTranslations()
        {
            IOSProjectTranslations(SHARED_PROJECT_PATH + "src/Amazone.IoM.SharedUI.IOS/Resources", "SharedLocalizable");
        }

        private static void CoreProjectTranslations(string path, string fileName)
        {
            Console.WriteLine("Generating of core project translations started");

            var generator = new TemplateGenerator();
            generator.ProcessTemplate(path + "/" + fileName + ".tt", path + "/" + fileName + ".cs");

            if (generator.Errors.HasErrors)
            {
                var consoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;

                foreach (var error in generator.Errors)
                {
                    Console.WriteLine(error);
                }

                Console.ForegroundColor = consoleColor;
            }
            else
            {
                Console.WriteLine("SUCCESS");
            }
        }

        private static void IOSProjectTranslations(string path, string fileName)
        {
            Console.WriteLine("Generating of iOS project translations started");

            var generator = new TemplateGenerator();

            foreach (var locale in LOCALES)
            {
                var folder = path + "/" + locale + ".lproj";
                generator.ProcessTemplate(folder + "/" + fileName + ".tt", folder + "/" + fileName + ".strings");
            }

            if (generator.Errors.HasErrors)
            {
                var consoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;

                foreach (var error in generator.Errors)
                {
                    Console.WriteLine(error);
                }

                Console.ForegroundColor = consoleColor;
            }
            else
            {
                Console.WriteLine("SUCCESS");
            }
        }
    }
}
