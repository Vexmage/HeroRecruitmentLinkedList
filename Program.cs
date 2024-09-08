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

        // Choose a party of 4 heroes for a quest based on quest requirements
        public List<Hero> ChooseParty()
        {
            List<Hero> selectedParty = new List<Hero>();
            Hero current = guildLeader;
            Random rand = new Random();
            int heroesSelected = 0;

            Console.WriteLine("\nSelecting 4 heroes for the quest:");

            // Randomly select heroes until we have 4, or there are no more heroes
            while (current != null && heroesSelected < 4)
            {
                selectedParty.Add(current);
                Console.WriteLine($"{current.Name} the {current.Role} has been selected for the quest.");
                current = current.Next;
                heroesSelected++;
            }

            // If there are fewer than 4 heroes in the guild, warn the player
            if (selectedParty.Count < 4)
            {
                Console.WriteLine("Warning: You don't have enough heroes in the guild for a full party. The party will be incomplete!");
            }

            return selectedParty;
        }

        // Evaluate whether the party is appropriate for the quest using a simple probability system
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

            // Calculate the chance of success based on the number of matched heroes
            Random rand = new Random();
            int successThreshold = 40 + (matches * 15); // Base success rate 40%, increases by 15% for each matched hero
            int roll = rand.Next(1, 101); // Roll between 1-100

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
        // Define new quests with a wider variety of hero types
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
            // Beginning Description
            Console.WriteLine("Welcome to *Heroes Recruited*! Your one-stop shop for all things heroic staffing!");
            Console.WriteLine("You’ve been appointed by your lord to oversee the delicate art of recruitment, tasked with hiring, organizing, and deploying a party of adventurers to tackle quests.");
            Console.WriteLine("It's not just about waving swords and casting spells—you’ll need to carefully review each hero’s resume, matching their skills to the task at hand.");
            Console.WriteLine("Be warned: Not every hero is as qualified as they claim. Some may be perfect for the job, while others... well, let’s just say, they might struggle with basic dungeon navigation.");
            Console.WriteLine("Your mission? Ensure that your recruits are well-suited to the perilous journeys ahead—or at least, that they survive long enough to bring back some loot!");

            AdventurersGuild guild = new AdventurersGuild();

            // Recruit heroes with stats and backstories
            guild.RecruitHero("Aldric the Brave", "Warrior", 18, 12, 16, "A veteran of countless wars, Aldric leads with courage and strength.");
            guild.RecruitHero("Luna the Swift", "Rogue", 12, 20, 14, "Luna is a nimble rogue, who grew up in the streets and knows how to survive.");
            guild.RecruitHero("Kael the Rogue", "Rogue", 14, 18, 12, "Once a thief, Kael now seeks redemption through aiding those in need.");
            guild.RecruitHero("Zara the Sorceress", "Mage", 8, 16, 10, "Zara wields arcane powers and seeks lost magical knowledge in ancient ruins.");
            guild.RecruitHero("Eldrin the Wise", "Cleric", 10, 14, 18, "Eldrin is a healer with deep knowledge of the divine, able to heal the gravest wounds.");
            guild.RecruitHero("Sylva the Hunter", "Ranger", 16, 18, 14, "Sylva is a master archer and survivalist, well-versed in tracking through wilderness.");
            guild.RecruitHero("Thorne the Just", "Paladin", 18, 10, 18, "Thorne combines martial prowess with divine healing, sworn to protect the innocent.");
            guild.RecruitHero("Bryn the Bard", "Bard", 12, 16, 14, "Bryn inspires the team with songs of bravery and hope, turning the tide of battle.");
            guild.RecruitHero("Milo the Silent", "Monk", 14, 20, 12, "Milo is a master of unarmed combat, relying on agility and speed to defeat his foes.");

            // Show the guild members
            guild.ShowGuild();

            // Generate and display available quests
            List<Quest> quests = GenerateQuests();
            Quest selectedQuest = SelectQuest(quests);

            // Select a party of 4 for the quest
            var party = guild.ChooseParty();

            // Evaluate quest success
            bool success = guild.EvaluateQuestSuccess(party, selectedQuest);

            if (success)
            {
                Console.WriteLine("\nYour party completes the quest with flying colors! The world now knows you're not like the other recruiters.");
            }
            else
            {
                Console.WriteLine("\nYour party barely makes it out alive. Looks like you're not so different from the rest after all...");
            }
        }
    }
}
