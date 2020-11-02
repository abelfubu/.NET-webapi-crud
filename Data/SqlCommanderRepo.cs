using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;
        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;            
        }
        public  IEnumerable<Command> GetAllCommands()
        {
            return  _context.Commands.ToList();
        }

        public  Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(command => command.Id == id);
        }

        public void CreateCommand(Command command)
        {
            if(command == null) throw new ArgumentNullException(nameof(command));
            
            _context.Commands.Add(command);
        }

        public void UpdateCommand(Command command)
        {
            if(command == null) throw new ArgumentNullException(nameof(command));

            _context.Commands.Update(command);
        }

        public  bool SaveChanges() =>  _context.SaveChanges() >= 0;

        public void DeleteCommand(Command command)
        {
            if(command == null) throw new ArgumentNullException(nameof(command));

            _context.Commands.Remove(command);
        }
    }
}