namespace AoC22.DayLogic;

public class Day07 : BaseDay
{
    
    string _activeDirectory = string.Empty;
    private Dictionary<string, DirInfo> _directories = new();

    class FileInfo
    {
        public string Filename { get; init; }
        public uint Size { get; init; }
    }

    class DirInfo
    {

        public string FolderName { get; init; }
        public string ParentFolder { get; init; }

        public List<DirInfo> SubFolders { get; init; }

        public List<FileInfo> Files { get; init;  }

        public void AddFile(string[] info)
        {
            Files.Add(new FileInfo()
            {
                Size = uint.Parse(info[0]),
                Filename = info[1]
            });
        }

        public uint Size()
        {
            return (uint)(Files.Sum(x => x.Size) + (uint)(SubFolders.Sum(x => x.Size())));
        }
    }
    public override void PartOne()
    {
        var input = ReadInput<string>();

        // process the inputs to find out what are commands and directories.

        string path = string.Empty;

        for (int i = 0; i < input.Length; ++i)
        {
            // make it parseable
            string[] info = input[i].Split(' ');
            if (info[0] == "$") // command
            {
                // what is the command
                switch (info[1])
                {
                    case "cd":
                        // change active dir - although double check
                        switch (info[2])
                        {
                            case "/": // jump to root.
                                _activeDirectory = "/";
                                path = "/";
                                _AddDir("/", "/", true);
                                break;
                            case "..": // go up a level
                                _RemoveFromPath();

                                _activeDirectory = _directories[path].ParentFolder;
                                //remove the cur from the path
                                break;
                            default: // cd into folder name
                                _activeDirectory = info[2];
                                var parentPath = path;
                                _AddToPath(_activeDirectory);
                                _AddDir(@path, @parentPath);

                                break;
                        }
                        break;
                    case "ls":
                        // we're about to get a list of contents. Iterate until we have all info
                        do
                        {
                            ++i; // point to next line
                            
                            // Get the line info for the ls
                            info = input[i].Split(' ');

                            if (info[0].Equals("dir")) // skip, we'll add it when we cd into it
                            {
                                continue;
                            }

                            // add the file to our records.
                            _directories[path].AddFile(info);
                        } while ((i+1) < input.Length && input[i+1][0] != '$'); // keep going until next line is a command

                        // peek at the next line to see if it's a command, if it is, continue.
                        break;
                }
            }
        }

        void _AddToPath(string folder)
        {
            // append the folder to path
            path += $"{folder}/";
        }

        void _RemoveFromPath()
        {
            // rebuild the path upwards
            string[] chunks = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (chunks.Length > 1)
            {
                path = '/' + string.Join('/', chunks[..^1]);

                if (path != "/") path += "/";
            }
            else
            {
                path = "/";
            }
        }
        
        // Print Directory

        var start = _directories["/"];
        string offset = string.Empty;
        
        // iterate through all to print to console
        _PrintFolder(start);

        //part 2 shite - total storage 7xx, need 3xx
        uint spaceRequired = (30000000) - (70000000 - start.Size());
        List<DirInfo> deleteDirs = new(); // prep to cache our directories
        
        uint sum = 0;
        foreach (var d in _directories)
        {
            uint size = d.Value.Size();
            
            // Part one - sum of all <= 100k
            if (size <= 100000)
            {
                sum += size;
            }
            
            // Part Two: Add all to deleteDirs which are more than space required, but not root dir
            if (d.Key != "/" && size >= spaceRequired)
            {
                deleteDirs.Add(d.Value);
            }
        }

        Console.WriteLine($"Total Size of Folders under 100k [Part One]: {sum}");
        
        // Sort the deleteable dirs to find smallest.
        deleteDirs.Sort((info, dirInfo) => info.Size() > dirInfo.Size() ? 1 : -1);
        
        // Print
        Console.WriteLine($"Smallest Directory to delete to free-up space [Part Two] {deleteDirs[0].Size()}");
        
        

        void _PrintFolder(DirInfo cur)
        {
            Console.WriteLine($"{offset} - {cur.FolderName} (dir)");
            offset += "   ";
            foreach (var dir in cur.SubFolders)
            {
                _PrintFolder(dir);
            }

            foreach (var file in cur.Files)
            {
                Console.WriteLine($"{offset} - {file.Filename} (file, size={file.Size})");
            }

            offset = offset[..^3]; // revert our offset back one space by killing a character.
        }

        void _AddDir(string newDir, string parent, bool rootLevel=false)
        {
            if (!_directories.ContainsKey(newDir))
            {
                _directories.Add(newDir, new DirInfo()
                {
                    FolderName = newDir,
                    ParentFolder = parent,
                    SubFolders =new(),
                    Files = new()
                });
            }

            if (rootLevel) return;
            
            // try to add as a sub-dir if possible.

            var dir = _directories[newDir];

            if (_directories[parent].SubFolders.Contains(dir)) return;

            _directories[parent].SubFolders.Add(dir);

        }
    }
}