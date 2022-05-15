using CommandService.Models;

namespace CommandService.Data;

public class CommandRepo : ICommandRepo
{
    public CommandRepo(AppDbContext context)
    {
        _context = context;
    }

    private readonly AppDbContext _context;

    public void CreateCommand(int platformId, Command command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }
        command.PlatformId = platformId;
        _context.Commands.Add(command);
    }

    public void CreatePlatform(Platform plat)
    {
        if (plat == null)
        {
            throw new ArgumentNullException(nameof(plat));
        }
        _context.Add(plat);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return _context.Platforms.ToList();
    }

    public Command GetCommand(int platformId, int commandId)
    {
        return _context.Commands.FirstOrDefault(m => m.PlatformId == platformId && m.Id == commandId);
    }

    public IEnumerable<Command> GetCommandsForPlatform(int platformId)
    {
        return _context
        .Commands
        .Where(m => m.PlatformId == platformId)
        .OrderBy(m => m.Platform.Name);
    }

    public bool PlatformExists(int platformId)
    {
        return _context.Platforms.Any(p => p.Id == platformId);
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }
}
