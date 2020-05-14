using System;
using System.Collections.Generic;
using System.Text;

namespace PathInGraph
{
    class Path
    {
        int Lenght;
        List<Vertex> Vertices;

        public Path(Edge edge)
        {
            Vertices.Add(edge.FromVertex);
            Vertices.Add(edge.ToVertex);
            Lenght += edge.Weight;
        }

        public void AddVertexInPath(Edge edge)
        {
            var lastVertex = Vertices[Vertices.Count - 1];
            if(lastVertex == edge.FromVertex)
            {
                Lenght = edge.Weight;
            }
        }

        public int GetLenght()
        {
            return Lenght;
        }

    }
}
