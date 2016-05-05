using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StemCellCodeChallenge
{   
    public class NodeData
    {
        public string publicId { get; set; }
        public string publicParentId { get; set; }
        public string entityCodeId { get; set; }
    }


    public class Node
    {
        public NodeData Data;
        public string Name { get; set; }
        public Node Parent { get; set; }
        public List<Node> Children = new List<Node>();
    }


}
