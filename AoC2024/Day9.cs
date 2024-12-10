using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class Day9 : Day
    {
        public class File
        {
            public int id;
            public int length;
            public int freeSpace;
            public int startingAt;
            public override string ToString()
            {
                return $"File {id} size: {length} then {freeSpace} free";
            }
        }

        public class FileSystem
        {
            public List<File> files = new List<File>();
            public int capacity = 0;
            public int[] fat;

            public void init(string description)
            {
                for (int i = 0; i < description.Length; i += 2)
                {
                    File f = new File()
                    {
                        id = i / 2,
                        length = description[i] - '0'
                    };
                    f.freeSpace = i<description.Length-1?description[i + 1] - '0':0;
                    capacity += f.length + f.freeSpace;
                    files.Add(f);
                    l.Log(f.ToString());
                }
                fat = new int[capacity];
                Format();
                Update();
            }

            public void Defrag()
            {
                int iFreeSpace = 0;
                int iEnd = capacity-1;
                while(iFreeSpace < iEnd-1)
                {
                    // find free space
                    while (fat[iFreeSpace] != -1)
                    {
                        iFreeSpace++;
                    }

                    if (fat[iEnd] != -1)
                    {
                        fat[iFreeSpace] = fat[iEnd];
                        fat[iEnd] = -1;
                    }
                    iEnd--;

                    if (l.logLevel <= 1)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < capacity; i++)
                        {
                            if (i == iFreeSpace)
                                sb.Append("F");
                            else if (i == iEnd)
                                sb.Append("E");
                            else
                                sb.Append(" ");
                        }
                        l.Log(sb.ToString());
                        l.Log(this.ToString());
                    }
                }
            }

            public void DefragB()
            {
                // try each file once, startin with the last one
                for(int i = files.Count-1; i >= 1; i--)
                {
                    l.Log($"Trying file {files[i].id} length {files[i].length}");

                    // find free space
                    int space = 0;
                    int iFreeSpace = 0;
                    for(int j = 0; j < files[i].startingAt; j++)
                    {
                        if (fat[j] == -1)
                        {
                            iFreeSpace = j;
                            space++;
                        }
                        else
                            space = 0;
                        if(space >= files[i].length)
                        {
                            //l.Log($"Found space ending at {iFreeSpace}");
                            while(space > 0)
                            {
                                fat[iFreeSpace - space + 1] = files[i].id;
                                fat[files[i].startingAt + files[i].length - space] = -1;
                                space--;
                            }
                            //l.Log($"{this.ToString()}");
                            break;
                        }
                    }
                }
            }

            public void Format()
            {
                for (int i = 0; i < capacity; i++)
                {
                    fat[i] = -1;
                }
            }

            public long CheckSum()
            {
                long total = 0;
                for(int i = 0; i < capacity; i++)
                {
                    if (fat[i] != -1)
                    {
                        total += fat[i] * i;
                    }
                }
                return total;
            }

            public void Update()
            {
                Format();
                int i = 0;
                foreach(File f in files)
                {
                    f.startingAt = i;
                    for(int j = 0; j < f.length; j++)
                    {
                        fat[i++] = f.id;
                    }
                    for (int j = 0; j < f.freeSpace; j++)
                    {
                        fat[i++] = -1;
                    }
                }
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i < capacity; i++)
                {
                    switch(fat[i])
                    {
                        case -1:
                            sb.Append(".");
                            break;
                        default:
                            sb.Append(fat[i]);
                            break;
                    }
                }
                return sb.ToString();
            }

            public FileSystem(string description, Day logger)
            {
                l = logger;
                init(description);
            }

            Day l;
        }

        public override string PartA()
        {
            Log("Loading...", 3);
            FileSystem fs = new FileSystem(data[0], this);
            Log(fs.ToString());
            Log("Defragging", 3);
            fs.Defrag();
            Log("Calculating Checksum");
            return $"{fs.CheckSum()}";
        }

        public override string PartB()
        {
            Log("Loading...", 3);
            FileSystem fs = new FileSystem(data[0], this);
            Log(fs.ToString());
            Log("Defragging", 3);
            fs.DefragB();
            Log("Calculating Checksum");
            return $"{fs.CheckSum()}";
        }
    }
}
