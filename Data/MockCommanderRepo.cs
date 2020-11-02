using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{ Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Ketle & Pan" },
                new Command{ Id = 1, HowTo = "Cut Bread", Line = "Get a knife", Platform = "Knife & Chopping board" },
                new Command{ Id = 2, HowTo = "Make cup of tea", Line = "Place teabag in cup", Platform = "Ketle & Cup" }
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command{ Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Ketle & Pan" }; 
        }
    }
}