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

    // Define the Quest class
    public class Quest
    {
        public string Description { get; set; }
        public List<string> Requirements { get; set; }  // E.g., "Rogue", "Mage", "Warrior"
        public string Type { get; set; }  // E.g., "combat", "stealth", "dungeon"

        public Quest(string description, string type, List<string> requirements)
        {
            Description = description;
            Type = type;
            Requirements = requirements;
        }

        // Return quest details as a string
        public override string ToString()
        {
            return $"Quest: {Description} - Type: {Type} - Requirements: {string.Join(", ", Requirements)}";
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

        // Recruit a new hero with stats and backstory
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

        // Choose a party for a quest based on quest requirements
        public List<Hero> ChooseParty(Quest quest)
        {
            List<Hero> selectedParty = new List<Hero>();
            Hero current = guildLeader;

            Console.WriteLine($"Selecting heroes for quest: {quest.Description}");
            while (selectedParty.Count < quest.Requirements.Count && current != null)
            {
                if (quest.Requirements.Contains(current.Role))
                {
                    selectedParty.Add(current);
                    Console.WriteLine($"{current.Name} the {current.Role} has been selected for the quest.");
                }
                current = current.Next;
            }

            if (selectedParty.Count < quest.Requirements.Count)
            {
                Console.WriteLine("The party does not meet all the quest requirements.");
            }

            return selectedParty;
        }

        // Evaluate whether the party is appropriate for the quest
        public bool EvaluateQuestSuccess(List<Hero> party, Quest quest)
        {
            if (party.Count < quest.Requirements.Count)
            {
                Console.WriteLine("The party does not meet the quest's requirements. The quest is likely to fail.");
                return false;
            }

            Console.WriteLine("The party meets the quest's requirements. The quest is likely to succeed!");
            return true;
        }
    }

    class Program
    {
        // Define five different quests
        static List<Quest> GenerateQuests()
        {
            List<Quest> quests = new List<Quest>
            {
                new Quest("Explore the Dark Dungeon, full of traps and enchanted foes.", "dungeon", new List<string> { "Rogue", "Mage" }),
                new Quest("Defend the village from an oncoming horde of goblins.", "battle", new List<string> { "Warrior", "Mage" }),
                new Quest("Infiltrate the enemy's fortress without being detected.", "stealth", new List<string> { "Rogue", "Rogue" }),
                new Quest("Escort a caravan across treacherous mountain passes.", "escort", new List<string> { "Warrior", "Warrior", "Rogue" }),
                new Quest("Retrieve the magical artifact from a cursed temple.", "magic", new List<string> { "Mage", "Rogue" })
            };

            return quests;
        }

        // Display available quests
        static void ShowQuests(List<Quest> quests)
        {
            Console.WriteLine("\nAvailable Quests:");
            for (int i = 0; i < quests.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {quests[i].ToString()}");
            }
        }

        // Allow the user to select a quest
        static Quest SelectQuest(List<Quest> quests)
        {
            ShowQuests(quests);
            Console.WriteLine("Choose a quest by number:");
            int choice = int.Parse(Console.ReadLine()) - 1;

            return quests[choice];
        }

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

            // Generate and display available quests
            List<Quest> quests = GenerateQuests();
            Quest selectedQuest = SelectQuest(quests);

            // Select a party for the quest
            var party = guild.ChooseParty(selectedQuest);

            // Evaluate quest success
            bool success = guild.EvaluateQuestSuccess(party, selectedQuest);

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
