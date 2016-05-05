using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StemCellCodeChallenge
{
    // for section 1 - 4
    class SectionType1
    {
        public string Name { get; set; }
        public int NumberFrames { get; set; }
        public int NumberPositionsPerFrame { get; set; }
    }

    // for section 5 - 6
    class SectionType2
    {
        public string Name { get; set; }
        public int NumberRack { get; set; }
        public int NumberLevelsPerRack { get; set; }
        public int NumberBoxPerLevel { get; set; }
        public int NumberPositionPerBox { get; set; }
    }

    class Configuration
    {
        public int NumberTanks;       
        public List<SectionType1> sectionType1 = new List<SectionType1>();       
        public List<SectionType2> sectionType2 = new List<SectionType2>();

        public Configuration()
        {
            NumberTanks = 4;
            var section = new SectionType1
            {
                Name = "Section 1",
                NumberFrames = 147,
                NumberPositionsPerFrame = 7
            };
            sectionType1.Add(section);

            section = new SectionType1
            {
                Name = "Section 2",
                NumberFrames = 103,
                NumberPositionsPerFrame = 7
            };
            sectionType1.Add(section);

            section = new SectionType1
            {
                Name = "Section 2",
                NumberFrames = 44,
                NumberPositionsPerFrame = 8
            };
            sectionType1.Add(section);


            section = new SectionType1
            {
                Name = "Section 3",
                NumberFrames = 473,
                NumberPositionsPerFrame = 8
            };
            sectionType1.Add(section);


            section = new SectionType1
            {
                Name = "Section 4",
                NumberFrames = 147,
                NumberPositionsPerFrame = 8
            };
            sectionType1.Add(section);


            var section2 = new SectionType2
            {
                Name = "Section 5",
                NumberRack = 9,
                NumberLevelsPerRack = 15,
                NumberBoxPerLevel = 1,
                NumberPositionPerBox = 81,

            };
            sectionType2.Add(section2);

            section2 = new SectionType2
            {
                Name = "Section 5",
                NumberRack = 5,
                NumberLevelsPerRack = 15,
                NumberBoxPerLevel = 1,
                NumberPositionPerBox = 25,
            };
            sectionType2.Add(section2);

            section2 = new SectionType2
            {
                Name = "Section 6",
                NumberRack = 9,
                NumberLevelsPerRack = 15,
                NumberBoxPerLevel = 1,
                NumberPositionPerBox = 81,
            };
            sectionType2.Add(section2);

            section2 = new SectionType2
            {
                Name = "Section 6",
                NumberRack = 5,
                NumberLevelsPerRack = 15,
                NumberBoxPerLevel = 1,
                NumberPositionPerBox = 25
            };
            sectionType2.Add(section2);
        }
    }



    public class Tree
    {
        Node root;
        StringBuilder csv;
        string filePath = @"..\..\output.csv";

        public Tree(Node r)
        {
            csv = new StringBuilder();
            csv.Append("PublicID, PublicParentID, EntityCodeID");
            csv.AppendLine();

            root = r;
            Configuration config = new Configuration();

            // Create Tanks
            for (int t = 1; t <= config.NumberTanks; t++)
            {
                if (t < 4) continue;
                Node tank = new Node();
                tank.Name = "Tank " + t.ToString();
                tank.Parent = root;
                tank.Data = new NodeData();
                tank.Data.publicId = tank.Name;
                tank.Data.publicParentId = tank.Parent.Data.publicId;
                tank.Data.entityCodeId = "Freezer";

                // From Section 1 to 4
                foreach (var s in config.sectionType1)
                {
                    Node section = new Node();
                    section.Name = s.Name;
                    section.Parent = tank;
                    section.Data = new NodeData();
                    section.Data.publicId = section.Name;
                    section.Data.publicParentId = section.Parent.Data.publicParentId + @"\" + section.Parent.Data.publicId;
                    section.Data.entityCodeId = "Section";

                    for (int f = 1; f <= s.NumberFrames; f++)
                    {
                        Node frame = new Node();
                        frame.Name = "Frame " + f.ToString("000");
                        frame.Parent = section;
                        frame.Data = new NodeData();
                        frame.Data.publicId = frame.Name;
                        frame.Data.publicParentId = frame.Parent.Data.publicParentId + @"\" + frame.Parent.Data.publicId;
                        frame.Data.entityCodeId = "Frame";

                        for (int p = 1; p <= s.NumberPositionsPerFrame; p++)
                        {
                            Node position = new Node();
                            position.Name = "Position " + p.ToString();
                            position.Parent = frame;
                            position.Data = new NodeData();
                            position.Data.publicId = position.Name;
                            position.Data.publicParentId = position.Parent.Data.publicParentId + @"\" + position.Parent.Data.publicId;
                            position.Data.entityCodeId = "Position";

                            frame.Children.Add(position);
                        }
                        section.Children.Add(frame);
                    }
                    tank.Children.Add(section);
                }

                // From Section 5 to 6
                foreach (var s in config.sectionType2)
                {
                    Node section = new Node();
                    section.Name = s.Name;
                    section.Parent = tank;
                    section.Data = new NodeData();
                    section.Data.publicId = section.Name;
                    section.Data.publicParentId = section.Parent.Data.publicParentId + @"\" + section.Parent.Data.publicId;
                    section.Data.entityCodeId = "Section";

                    for (int f = 1; f <= s.NumberRack; f++)
                    {
                        Node rack = new Node();
                        rack.Name = "Rack " + f.ToString();
                        rack.Parent = section;
                        rack.Data = new NodeData();
                        rack.Data.publicId = rack.Name;
                        rack.Data.publicParentId = rack.Parent.Data.publicParentId + @"\" + rack.Parent.Data.publicId;
                        rack.Data.entityCodeId = "Rack";

                        for (int l = 1; l <= s.NumberLevelsPerRack; l++)
                        {
                            Node level = new Node();
                            level.Name = "Level " + l.ToString();
                            level.Parent = rack;
                            level.Data = new NodeData();
                            level.Data.publicId = level.Name;
                            level.Data.publicParentId = level.Parent.Data.publicParentId + @"\" + level.Parent.Data.publicId;
                            level.Data.entityCodeId = "Level";

                            for (int b = 1; b <= s.NumberBoxPerLevel; b++)
                            {
                                Node box = new Node();
                                box.Name = "Box " + b.ToString();
                                box.Parent = level;
                                box.Data = new NodeData();
                                box.Data.publicId = box.Name;
                                box.Data.publicParentId = box.Parent.Data.publicParentId + @"\" + box.Parent.Data.publicId;
                                box.Data.entityCodeId = "Box";

                                for (int p = 1; p <= s.NumberPositionPerBox; p++)
                                {
                                    Node position = new Node();
                                    position.Name = "Postion " + p.ToString();
                                    position.Parent = box;
                                    position.Data = new NodeData();
                                    position.Data.publicId = position.Name;
                                    position.Data.publicParentId = position.Parent.Data.publicParentId + @"\" + position.Parent.Data.publicId;
                                    position.Data.entityCodeId = "Position";

                                    box.Children.Add(position);
                                }
                                level.Children.Add(box);
                            }
                            rack.Children.Add(level);
                        }
                        section.Children.Add(rack);
                    }
                    tank.Children.Add(section);                    
                }
                root.Children.Add(tank);
            }
        }


        public void ParseTree(Node root)
        {
            var line = string.Format("{0}, {1}, {2}, {3}", root.Data.publicId, root.Data.publicParentId, root.Data.entityCodeId, Environment.NewLine);
            csv.Append(line);
            foreach (var child in root.Children)
            {
                ParseTree(child);
            }
        }

        public void ExportToCsvFile()
        {           
            File.AppendAllText(filePath, csv.ToString());
        }
    }
}
