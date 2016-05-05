using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StemCellCodeChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Node root = new Node();
            root.Name = "Acme Lab";
            root.Parent = null;
            root.Data = new NodeData()
            {
                publicId = root.Name,
                publicParentId = " ",
                entityCodeId = "Building"
            };           

            Tree tree = new Tree(root);
            tree.ParseTree(root);
            tree.ExportToCsvFile();

        }
    }
}
