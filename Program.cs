using System;
using System.IO;

namespace FantasyGameLinkedList
{
    // Define the Hero (Node)
    public class Hero
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public string Backstory { get; set; }
        public Hero Next { get; set; }

        public Hero(string name, int strength, int dexterity, int constitution, string backstory)
        {
            Name = name;
            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Backstory = backstory;
            Next = null;
        }

        // Return hero details as a string
        public override string ToString()
        {
            return $"{Name} [STR: {Strength}, DEX: {Dexterity}, CON: {Constitution}] - {Backstory}";
        }
    }

    // Define the AdventurersGuild (Linked List)
    public class AdventurersGuild
    {
        private Hero guildLeader;

        public AdventurersGuild()
        {
            guildLeader = null;
        }

        // Recruit a new hero with Pathfinder stats and backstory
        public void RecruitHero(string name, int strength, int dexterity, int constitution, string backstory)
        {
            Hero newHero = new Hero(name, strength, dexterity, constitution, backstory);
            if (guildLeader == null)
            {
                guildLeader = newHero;
            }
            else
            {
                Hero current = guildLeader;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newHero;
            }
            Console.WriteLine($"{name} has joined the Adventurers' Guild!");
        }

        // Display the guild members with stats and backstories
        public void ShowGuild()
        {
            if (guildLeader == null)
            {
                Console.WriteLine("The Adventurers' Guild is empty.");
                return;
            }

            Hero current = guildLeader;
            Console.WriteLine("Guild Members:");
            while (current != null)
            {
                Console.WriteLine(current.ToString());
                current = current.Next;
            }
        }

        // Save the guild list to a file with stats and backstories
        public void SaveGuildToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                Hero current = guildLeader;
                while (current != null)
                {
                    writer.WriteLine($"{current.Name},{current.Strength},{current.Dexterity},{current.Constitution},{current.Backstory}");
                    current = current.Next;
                }
            }
            Console.WriteLine("Guild list saved to file.");
        }

        // Load the guild list from a file
        public void LoadGuildFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                guildLeader = null; // Reset the guild list before loading
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        string name = parts[0];
                        int strength = int.Parse(parts[1]);
                        int dexterity = int.Parse(parts[2]);
                        int constitution = int.Parse(parts[3]);
                        string backstory = parts[4];
                        RecruitHero(name, strength, dexterity, constitution, backstory);
                    }
                }
                Console.WriteLine("Guild list loaded from file.");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AdventurersGuild guild = new AdventurersGuild();

            // Recruit heroes with stats and backstories
            guild.RecruitHero("Aldric the Brave", 18, 14, 16, "A veteran of countless wars, Aldric leads with courage and strength.");
            guild.RecruitHero("Luna the Swift", 12, 20, 13, "Luna is a nimble rogue, who grew up in the streets and knows how to survive.");
            guild.RecruitHero("Kael the Rogue", 14, 18, 12, "Once a thief, Kael now seeks redemption through aiding those in need.");
            guild.RecruitHero("Zara the Sorceress", 8, 16, 10, "Zara wields arcane powers and seeks lost magical knowledge in ancient ruins.");

            // Show the guild members with their stats and backstories
            guild.ShowGuild();

            // Save the guild to a file
            string filePath = "guild.txt";
            guild.SaveGuildToFile(filePath);

            // Clear the guild and show it's empty
            guild = new AdventurersGuild();
            guild.ShowGuild();  // Output: The Adventurers' Guild is empty.

            // Load the guild from the file and show it again
            guild.LoadGuildFromFile(filePath);
            guild.ShowGuild();  // Output: Restores the guild members from the file.
        }
    }
}
