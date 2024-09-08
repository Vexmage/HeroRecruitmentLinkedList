using System;
using System.IO;
using System.Collections.Generic;

namespace FantasyGameLinkedList
{
    // Define the Hero (Node)
    public class Hero
    {
        public string Name { get; set; }
        public string Role { get; set; }  // Warrior, Rogue, Mage, etc.
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public string Backstory { get; set; }
        public Hero Next { get; set; }

        public Hero(string name, string role, int strength, int dexterity, int constitution, string backstory)
        {
            Name = name;
            Role = role;
            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Backstory = backstory;
            Next = null;
        }

        // Return hero details as a string
        public override string ToString()
        {
            return $"{Name} [{Role}] - STR: {Strength}, DEX: {Dexterity}, CON: {Constitution} - {Backstory}";
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
        public void RecruitHero(string name, string role, int strength, int dexterity, int constitution, string backstory)
        {
            Hero newHero = new Hero(name, role, strength, dexterity, constitution, backstory);
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
                    writer.WriteLine($"{current.Name},{current.Role},{current.Strength},{current.Dexterity},{current.Constitution},{current.Backstory}");
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
                        string role = parts[1];
                        int strength = int.Parse(parts[2]);
                        int dexterity = int.Parse(parts[3]);
                        int constitution = int.Parse(parts[4]);
                        string backstory = parts[5];
                        RecruitHero(name, role, strength, dexterity, constitution, backstory);
                    }
                }
                Console.WriteLine("Guild list loaded from file.");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }

        // Choose a party for a quest
        public List<Hero> ChooseParty(int numHeroes)
        {
            List<Hero> selectedParty = new List<Hero>();
            Hero current = guildLeader;
            Random rand = new Random();
            while (selectedParty.Count < numHeroes && current != null)
            {
                if (rand.NextDouble() > 0.5) // Randomly select heroes for now
                {
                    selectedParty.Add(current);
                }
                current = current.Next;
            }

            Console.WriteLine($"Selected {selectedParty.Count} heroes for the quest:");
            foreach (var hero in selectedParty)
            {
                Console.WriteLine(hero.ToString());
            }
            return selectedParty;
        }

        // Evaluate whether the party is appropriate for the quest
        public bool EvaluateQuestSuccess(List<Hero> party, string questType)
        {
            int totalStrength = 0;
            int totalDexterity = 0;
            foreach (var hero in party)
            {
                totalStrength += hero.Strength;
                totalDexterity += hero.Dexterity;
            }

            // Example: For a combat-heavy quest, strength is more important
            if (questType == "combat")
            {
                Console.WriteLine($"Total party strength: {totalStrength}");
                return totalStrength > 30; // Arbitrary success threshold
            }
            else if (questType == "stealth")
            {
                Console.WriteLine($"Total party dexterity: {totalDexterity}");
                return totalDexterity > 30;
            }

            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AdventurersGuild guild = new AdventurersGuild();

            // Recruit heroes with stats and backstories
            guild.RecruitHero("Aldric the Brave", "Warrior", 18, 12, 16, "A veteran of countless wars, Aldric leads with courage and strength.");
            guild.RecruitHero("Luna the Swift", "Rogue", 12, 20, 14, "Luna is a nimble rogue, who grew up in the streets and knows how to survive.");
            guild.RecruitHero("Kael the Rogue", "Rogue", 14, 18, 12, "Once a thief, Kael now seeks redemption through aiding those in need.");
            guild.RecruitHero("Zara the Sorceress", "Mage", 8, 16, 10, "Zara wields arcane powers and seeks lost magical knowledge in ancient ruins.");

            // Show the guild members
            guild.ShowGuild();

            // Select a party for a combat quest
            var party = guild.ChooseParty(3);
            bool success = guild.EvaluateQuestSuccess(party, "combat");

            if (success)
            {
                Console.WriteLine("The quest is likely to succeed!");
            }
            else
            {
                Console.WriteLine("The quest is likely to fail...");
            }
        }
    }
}
