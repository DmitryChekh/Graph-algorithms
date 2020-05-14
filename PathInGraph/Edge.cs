using System;
using System.Collections.Generic;
using System.Text;

namespace PathInGraph
{
    class Edge
    {
        public int Weight { get; set; }

        public Vertex FromVertex { get; set; }
        public Vertex ToVertex { get; set; }

        public Edge(Vertex from, Vertex to, int  weight = 1)
        {
            Weight = weight;
            FromVertex = from;
            ToVertex = to;
        }

        public override string ToString()
        {
            return FromVertex.ToString() + "-" + ToVertex.ToString(); 
        }
    }
}
