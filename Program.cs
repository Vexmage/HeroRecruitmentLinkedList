using System;
using System.IO;
using System.Collections.Generic;

namespace FantasyGameLinkedList
{
    // Define the Hero (Node)
    public class Hero
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public string Backstory { get; set; }
        public string SpecialAbility { get; set; }  // New Special Ability
        public Hero Next { get; set; }

        public Hero(string name, string role, int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma, string specialAbility, string backstory)
        {
            Name = name;
            Role = role;
            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Charisma = charisma;
            SpecialAbility = specialAbility;
            Backstory = backstory;
            Next = null;
        }

        // Return hero details as a string
        public override string ToString()
        {
            return $"{Name} [{Role}] - STR: {Strength}, DEX: {Dexterity}, CON: {Constitution}, INT: {Intelligence}, WIS: {Wisdom}, CHA: {Charisma} - Ability: {SpecialAbility} - {Backstory}";
        }
    }

    // Define the Quest class
    public class Quest
    {
        public string Description { get; set; }
        public List<string> Requirements { get; set; }
        public string Type { get; set; }

        public Quest(string description, string type, List<string> requirements)
        {
            Description = description;
            Type = type;
            Requirements = requirements;
        }

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

        // Recruit a new hero
        public void RecruitHero(string name, string role, int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma, string specialAbility, string backstory)
        {
            Hero newHero = new Hero(name, role, strength, dexterity, constitution, intelligence, wisdom, charisma, specialAbility, backstory);
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

        // Display the guild members
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

        // Choose a party of 4 heroes for a quest
        public List<Hero> ChooseParty()
        {
            List<Hero> selectedParty = new List<Hero>();
            Hero current = guildLeader;
            Random rand = new Random();
            int heroesSelected = 0;

            Console.WriteLine("\nSelecting 4 heroes for the quest:");

            // Randomly select heroes until we have 4
            while (current != null && heroesSelected < 4)
            {
                selectedParty.Add(current);
                Console.WriteLine($"{current.Name} the {current.Role} has been selected for the quest.");
                current = current.Next;
                heroesSelected++;
            }

            // Warn the player if there are fewer than 4 heroes
            if (selectedParty.Count < 4)
            {
                Console.WriteLine("Warning: You don't have enough heroes in the guild for a full party.");
            }

            return selectedParty;
        }

        // Handle random events during a quest
        public void HandleRandomEvent()
        {
            Random rand = new Random();
            int eventRoll = rand.Next(1, 101);

            if (eventRoll <= 20)
            {
                Console.WriteLine("A bandit ambush occurred! The party takes damage but escapes.");
            }
            else if (eventRoll <= 40)
            {
                Console.WriteLine("A magical storm disrupts the party's journey, delaying their progress.");
            }
            else if (eventRoll <= 60)
            {
                Console.WriteLine("The party stumbles upon hidden treasure, boosting their morale.");
            }
            else
            {
                Console.WriteLine("The journey is uneventful.");
            }
        }

        // Evaluate whether the party is appropriate for the quest
        public bool EvaluateQuestSuccess(List<Hero> party, Quest quest)
        {
            int matches = 0;

            // Check how many party members match the quest's requirements
            foreach (var hero in party)
            {
                if (quest.Requirements.Contains(hero.Role))
                {
                    matches++;
                }
            }

            // Check for hero special abilities that impact the quest
            foreach (var hero in party)
            {
                if (hero.SpecialAbility == "Battle Cry" && quest.Type == "battle")
                {
                    matches++;
                    Console.WriteLine($"{hero.Name} uses Battle Cry! The party's chances of success increase.");
                }
                else if (hero.SpecialAbility == "Trap Disarm" && quest.Type == "dungeon")
                {
                    matches++;
                    Console.WriteLine($"{hero.Name} uses Trap Disarm! The party avoids traps in the dungeon.");
                }
                // Add more abilities as needed
            }

            // Calculate the chance of success based on matched heroes and abilities
            Random rand = new Random();
            int successThreshold = 40 + (matches * 15);  // Base success rate 40%, increases by 15% for each match
            int roll = rand.Next(1, 101);

            Console.WriteLine($"\nQuest Success Roll: {roll}. Needed: {successThreshold} or below to succeed.");
            if (roll <= successThreshold)
            {
                Console.WriteLine("\nSuccess! The quest was completed successfully!");
                return true;
            }
            else
            {
                Console.WriteLine("\nFailure! The quest did not succeed.");
                return false;
            }
        }
    }

    class Program
    {
        // Define quests with a variety of hero types
        static List<Quest> GenerateQuests()
        {
            List<Quest> quests = new List<Quest>
            {
                new Quest("Explore the Dark Dungeon, full of traps and enchanted foes.", "dungeon", new List<string> { "Rogue", "Mage" }),
                new Quest("Defend the village from an oncoming horde of goblins.", "battle", new List<string> { "Warrior", "Mage" }),
                new Quest("Infiltrate the enemy's fortress without being detected.", "stealth", new List<string> { "Rogue", "Rogue" }),
                new Quest("Escort a caravan across treacherous mountain passes.", "escort", new List<string> { "Warrior", "Warrior", "Rogue" }),
                new Quest("Retrieve the magical artifact from a cursed temple.", "magic", new List<string> { "Mage", "Rogue" }),
                new Quest("Healing Waters", "healing", new List<string> { "Cleric", "Ranger" }),
                new Quest("The Siege of Dragonspire", "battle", new List<string> { "Warrior", "Paladin", "Bard" }),
                new Quest("The Poisoned Crypt", "dungeon", new List<string> { "Rogue", "Cleric" }),
                new Quest("The Hunt for the Shadow Beast", "hunt", new List<string> { "Ranger", "Rogue", "Monk" }),
                new Quest("The Broken Artifact", "magic", new List<string> { "Mage", "Paladin" })
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
            Console.WriteLine("\nChoose a quest by number:");
            int choice = int.Parse(Console.ReadLine()) - 1;

            return quests[choice];
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to *Heroes Recruited*! Your one-stop shop for all things heroic staffing!");

            AdventurersGuild guild = new AdventurersGuild();

            // Recruit heroes with stats, backstories, and special abilities
            guild.RecruitHero("Aldric the Brave", "Warrior", 18, 12, 16, 10, 8, 12, "Battle Cry", "A veteran of countless wars, Aldric leads with courage and strength.");
            guild.RecruitHero("Luna the Swift", "Rogue", 12, 20, 14, 12, 10, 10, "Trap Disarm", "Luna is a nimble rogue, who grew up in the streets and knows how to survive.");
            guild.RecruitHero("Zara the Sorceress", "Mage", 8, 16, 10, 18, 12, 16, "Arcane Shield", "Zara wields arcane powers and seeks lost magical knowledge.");
            guild.RecruitHero("Eldrin the Wise", "Cleric", 10, 14, 18, 14, 18, 12, "Divine Healing", "Eldrin is a healer with deep knowledge of the divine.");

            // Show the guild members
            guild.ShowGuild();

            // Generate and display available quests
            List<Quest> quests = GenerateQuests();
            Quest selectedQuest = SelectQuest(quests);

            // Select a party of 4 for the quest
            var party = guild.ChooseParty();

            // Handle a random event during the quest
            guild.HandleRandomEvent();

            // Evaluate quest success
            bool success = guild.EvaluateQuestSuccess(party, selectedQuest);

            if (success)
            {
                Console.WriteLine("\nYour party completes the quest with flying colors!");
            }
            else
            {
                Console.WriteLine("\nYour party barely makes it out alive...");
            }
        }
    }
}
