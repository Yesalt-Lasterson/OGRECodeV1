using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
namespace OGRECodeV1Gen
{
    static internal class OGRECodeV1
    {
        static internal Dictionary<string, string> Substitutions = new Dictionary<string, string>()
        {
            {"A", "Superable"},
            {"a", "Time"},
            {"B","Moon"},
            {"b","Ages"},
            {"C","Delta"},
            {"c","Millenia"},
            {"D","Alpha"},
            {"d","Although"},
            {"E","Tuesday"},
            {"e","Something"},
            {"F","Respect"},
            {"f","Arguments"},
            {"G","Substitute"},
            {"g","Lesser"},
            {"H","Ogre"},
            {"h","Lord"},
            {"I","Am"},
            {"i","Cars"},
            {"J","Carver"},
            {"j","Scenery"},
            {"K","Professional"},
            {"k","Change"},
            {"L","Burger"},
            {"l","Effect"},
            {"M","Grease"},
            {"m","Insuperable"},
            {"N","Spectacle"},
            {"n","Not"},
            {"O","Astral"},
            {"o","Agree"},
            {"P","Spiro globe"},
            {"p","Terms"},
            {"Q","Gallium"},
            {"q","On"},
            {"R","Polonium"},
            {"r","Your"},
            {"S","Boss"},
            {"s","Conditions"},
            {"T","Pinto"},
            {"t","Conditioned"},
            {"U","Petroleum"},
            {"u","Pass"},
            {"V","Militant"},
            {"v","Phrase"},
            {"W","Supremacy"},
            {"w","Word"},
            {"X","Boron"},
            {"x","Letter"},
            {"Y","Operator"},
            {"y","Potent"},
            {"Z","Mass"},
            {"z","Character"}
        };
        static internal string? key_name { get; set; }
        static internal string? user_name { get; set; }
        static internal string? content { get; set; }
        static internal string Title { get; set; } = $"OGRECode V1 key_";
        static internal string Author { get; set; } = $"{DateTime.Now:MM/dd/yyyy}_";
        static internal string ID { get; set; } = "Astral Substitute Polonium Tuesday Petroleum Conditions Something Your";
        static internal string? Key { get; set; }
    }
    internal class OGRECodeV1Syntax
    {
        internal static string OGREKeyV1 { get; set; } = $"Title: {OGRECodeV1.Title}{OGRECodeV1.key_name}\nAuthor: {OGRECodeV1.Author}{OGRECodeV1.user_name}\nOGRECode V1 -- Context --\n\n\n{OGRECodeV1.content}\n\n\n\n--- End of Context ---\n\nIdentifier: {OGRECodeV1.ID}\n\n| {OGRECodeV1.Key} |\n------------------------";
        internal static bool Parser(string OGREKeyV1)
        {
            if (string.IsNullOrEmpty(OGREKeyV1))
                return false;
            string pattern = @"Title: OGRECode V1 key_[a-zA-Z]*\nAuthor: [0-9]{1,2}/[0-9]{1,2}/[0-9]+_[a-zA-Z ]*\nOGRECode V1 -- Context --\n\n\n[a-zA-Z ]*\n\n\n\n--- End of Context ---\n\nIdentifier: [a-zA-Z ]*\n\n| [\s\S]* |\n------------------------";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(OGREKeyV1);
            if (!match.Success)
                return false;
            OGRECodeV1.Title = match.Groups[1].Value;
            OGRECodeV1.Author = match.Groups[2].Value + "/" + match.Groups[3].Value + "/" + match.Groups[4].Value + "/" + match.Groups[5].Value;
            OGRECodeV1.content = match.Groups[6].Value;
            OGRECodeV1.ID = match.Groups[7].Value;
            return true;
        }
    }
    internal class OGRECodeV1Gen
    {
        internal static string GenerateKey(string? userName, string? keyName, string PATH)
        {
            if (userName == null || keyName == null)
            {
                return "-1";
            }
            for (var i = 0; i < userName.Length; i++)
            {
                if (!OGRECodeV1.Substitutions.ContainsKey(userName[i].ToString()))
                    return "-100";
            }
            for (var i = 0; i < keyName.Length; i++)
            {
                if (!OGRECodeV1.Substitutions.ContainsKey(keyName[i].ToString()))
                    return "-100";
            }
            Random random = new();
            OGRECodeV1.user_name = userName;
            OGRECodeV1.key_name = keyName;
            string completedID = "";
            string content = "";

            for (var i = 0; i < userName.Length; i++)
            {
                string character = userName[i].ToString();
                if (!OGRECodeV1.Substitutions.ContainsKey(character))
                {
                    completedID += character;
                }
                completedID += OGRECodeV1.Substitutions[character];
            }
            for (var i = 0; i < 64; i++)
            {
                int characterIndex = random.Next(OGRECodeV1.Substitutions.Keys.Count);
                content += OGRECodeV1.Substitutions[OGRECodeV1.Substitutions.Keys.ElementAt(characterIndex)] + " ";
            }
            OGRECodeV1.ID = completedID;
            RSACryptoServiceProvider key = new();
            string Key = key.ToXmlString(true);
            OGRECodeV1.Key = Key;
            OGRECodeV1.content = content;
            string OGRECodeV1Key = $"Title: {OGRECodeV1.Title}{OGRECodeV1.key_name}\nAuthor: {OGRECodeV1.Author}{OGRECodeV1.user_name}\nOGRECode V1 -- Context --\n\n\n{content}\n\n\n\n--- End of Context ---\n\nIdentifier: {OGRECodeV1.ID}\n\n| {Key} |\n------------------------";
            File.WriteAllText($@"{PATH}\{OGRECodeV1.Title}{OGRECodeV1.key_name}.txt", OGRECodeV1Key);
            Console.WriteLine(OGRECodeV1Key);
            return OGRECodeV1Key;
        }
    }
    class OGRECodeV1Encoder
    {
        internal static string OGREEncodeV1(string OGREKeyV1, string Text)
        {
            if (OGRECodeV1Syntax.Parser(OGREKeyV1) == false)
                return "-8";
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(OGREKeyV1));
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(Text);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }


    }
    internal class OGRECodev1Decoder
    {
        internal static string OGREDecodeV1(string OGREKeyV1, string EncodedText)
        {
            if (OGRECodeV1Syntax.Parser(OGREKeyV1) == false)
                return "-8";
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(EncodedText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(OGREKeyV1));
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
    internal class UI
    {
        internal static void RunInterface(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                InterfaceGen();
                return;
            }
            if (args[0] == "Gen")
            {
                InterfaceGen();
                return;
            }
            if (args[0] == "Encode")
            {
                InterfaceEncode();
                return;
            }
            if (args[0] == "Decode")
            {
                InterfaceDecode();
                return;
            }
        }
        internal static void InterfaceGen()
        {
            Console.WriteLine("OGRECode V1 Generator!!!\nCopyright (C) 2024 Yesalt Lasterson\nEnter Key's name: ");
            string? keyName = Console.ReadLine();
            Console.WriteLine("Enter your name: ");
            string? userName = Console.ReadLine();
            Console.WriteLine("Enter Directory To Save: ");
        Invalid1:
            string? path = Console.ReadLine();
            if (path == null)
            {
                Console.WriteLine("Directory cannot be null");
                goto Invalid1;
            }
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Directory does not exist, creating new one...");
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex + "ERROR: Directory cannot be created, this can happen if you don't have write permission or if the path is invalid");
                    goto Invalid1;
                }
            }
            string statusCode = OGRECodeV1Gen.GenerateKey(userName, keyName, path);
            if (statusCode == "-1" || statusCode == "-100")
            {
                Console.WriteLine("Error: Invalid user name or key name");
            }
            Console.ReadLine();
        }
        internal static void InterfaceEncode()
        {
            Console.WriteLine("OGRECode V1 Encoder!!!\nCopyright (C) 2024 Yesalt Lasterson\nEnter Key's Path: ");
        Invalid1:
            string? path = Console.ReadLine();
            if (path == null)
            {
                Console.WriteLine("Path cannot be null");
                goto Invalid1;
            }
            if (File.Exists(path) == false)
            {
                Console.WriteLine("File does not exist");
                goto Invalid1;
            }
            string? key = File.ReadAllText(path);
            bool repeat = true;
            while (repeat)
            {
            invalid1:
                Console.WriteLine("Enter text to encode: ");
                string? text = Console.ReadLine();
                if (string.IsNullOrEmpty(text))
                {
                    Console.WriteLine("Text cannot be null");
                    goto invalid1;
                }
                string encodedText = OGRECodeV1Encoder.OGREEncodeV1(key, text);
                Console.WriteLine(encodedText);
                Console.WriteLine("Do you want to encode again? (y/n)");
                char? answer = Console.ReadLine()[0];
                if (answer != null && answer == 'n')
                {
                    repeat = false;
                }
            }
            Console.ReadLine();
        }
        internal static void InterfaceDecode()
        {
            Console.WriteLine("OGRECode V1 Decoder!!!\nCopyright (C) 2024 Yesalt Lasterson\nEnter Key's Path: ");
        Invalid1:
            string? path = Console.ReadLine();
            if (path == null)
            {
                Console.WriteLine("Path cannot be null");
                goto Invalid1;
            }
            if (File.Exists(path) == false)
            {
                Console.WriteLine("File does not exist");
                goto Invalid1;
            }
            string? key = File.ReadAllText(path);
            bool repeat = true;
            while (repeat)
            {
            invalid1:
                Console.WriteLine("Enter text to decode: ");
                string? encodedText = Console.ReadLine();
                if (string.IsNullOrEmpty(encodedText))
                {
                    Console.WriteLine("Text cannot be null");
                    goto invalid1;
                }
                string text = OGRECodev1Decoder.OGREDecodeV1(key, encodedText);
                Console.WriteLine(text);
                Console.WriteLine("Do you want to decode again? (y/n)");
                char? answer = Console.ReadLine()[0];
                if (answer != null && answer == 'n')
                {
                    repeat = false;
                }
            }
            Console.ReadLine();
        }
    }
    class Program : UI
    {
        static void Main(string[] args)
        {
            RunInterface(args);
            Console.ReadLine();
        }
    }
}