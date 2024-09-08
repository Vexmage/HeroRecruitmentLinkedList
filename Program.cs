using System;
using System.IO;  // For file operations

namespace FantasyGameLinkedList
{
    // Define the Hero (Node)
    public class Hero
    {
        public string Name { get; set; }
        public Hero Next { get; set; }

        public Hero(string name)
        {
            Name = name;
            Next = null;
        }
    }

    // Define the AdventurersGuild (Linked List)
    public class AdventurersGuild
    {
        private Hero guildLeader;  // The first hero in the list (Head)

        public AdventurersGuild()
        {
            guildLeader = null;  // The guild starts with no members
        }

        // Recruit a new hero (Add at the end)
        public void RecruitHero(string name)
        {
            Hero newHero = new Hero(name);
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

        // Display the guild members (Print)
        public void ShowGuild()
        {
            if (guildLeader == null)
            {
                Console.WriteLine("The Adventurers' Guild is empty.");
                return;
            }

            Hero current = guildLeader;
            Console.Write("Guild Members: ");
            while (current != null)
            {
                Console.Write(current.Name + " -> ");
                current = current.Next;
            }
            Console.WriteLine("End of Guild.");
        }

        // Search for a hero (Find)
        public bool SearchHero(string name)
        {
            Hero current = guildLeader;
            while (current != null)
            {
                if (current.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"{name} is part of the Adventurers' Guild!");
                    return true;
                }
                current = current.Next;
            }
            Console.WriteLine($"{name} is not in the guild.");
            return false;
        }

        // Remove a hero (Remove)
        public void RemoveHero(string name)
        {
            if (guildLeader == null) return;

            if (guildLeader.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"{name} has left the guild!");
                guildLeader = guildLeader.Next;
                return;
            }

            Hero current = guildLeader;
            while (current.Next != null && !current.Next.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                current = current.Next;
            }

            if (current.Next != null)
            {
                Console.WriteLine($"{name} has left the guild!");
                current.Next = current.Next.Next;
            }
        }

        // Save the guild list to a file
        public void SaveGuildToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                Hero current = guildLeader;
                while (current != null)
                {
                    writer.WriteLine(current.Name);
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
                        RecruitHero(line);  // Add each hero from the file
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

            // Recruit heroes into the guild
            guild.RecruitHero("Aldric the Brave");
            guild.RecruitHero("Luna the Swift");
            guild.RecruitHero("Kael the Rogue");
            guild.RecruitHero("Zara the Sorceress");

            // Show the guild
            guild.ShowGuild();

            // Save the guild to a file
            string filePath = "guild.txt";
            guild.SaveGuildToFile(filePath);

            // Clear the guild and show it's empty
            guild = new AdventurersGuild(); // New empty guild
            guild.ShowGuild();

            // Load the guild from a file
            guild.LoadGuildFromFile(filePath);
            guild.ShowGuild();  // Should now show the guild from the file
        }
    }
}
