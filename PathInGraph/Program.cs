using System;
using System.IO;
using System.Collections.Generic;

namespace PathInGraph
{
    class Program
    {
        static void Main(string[] args)
        {

            var graph = new Graph();

            var v1 = new Vertex(1);
            var v2 = new Vertex(2);
            var v3 = new Vertex(3);
            var v4 = new Vertex(4);
            var v5 = new Vertex(5);
            var v6 = new Vertex(6);
            var v7 = new Vertex(7);
            var v8 = new Vertex(8);


            graph.AddVertex(v1);
            graph.AddVertex(v2);
            graph.AddVertex(v3);
            graph.AddVertex(v4);
            graph.AddVertex(v5);
            graph.AddVertex(v6);
            graph.AddVertex(v7);


            graph.AddEdges(v1, v2,9);
            graph.AddEdges(v1, v3,6);
            graph.AddEdges(v1, v4,7);
            graph.AddEdges(v2, v5, 5);
            graph.AddEdges(v3, v4, 25);
            graph.AddEdges(v4, v5, 10);
            graph.AddEdges(v4, v6, 9);
            graph.AddEdges(v5, v7, 15);

            List<Vertex> vlist = new List<Vertex>();
            vlist.Add(v1);
            vlist.Add(v2);
            var dct = graph.GetAdjacentVerticesWithLenght(vlist);

            foreach(var kvp in dct)
            {
                Console.WriteLine("{0}[{1}]", kvp.Key.Number,kvp.Value);
            }


            PrintMatrix(graph);

            graph.AlgorithDijkstra(v1, v7);

            PrintWeight(graph);
            
           // graph.BFS(v1, v7);
           // graph.DFS(v1, v7);

        }

        static void PrintWeight(Graph graph)
        {
            foreach(var item in graph.distanceToVertex)
            Console.Write("{0} is {1} |", item.Key, item.Value);
        }
        static void PrintMatrix(Graph graph)
        {
            var matrix = graph.GetMatrix();
            for (int i = 0; i < graph.VertexCount; i++)
            {

                Console.Write(i + 1 + "|");
                for (int j = 0; j < graph.VertexCount; j++)
                {
                    Console.Write(" " + matrix[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("-----------------------------");
            Console.Write(" ");
            for (int i = 0; i < graph.VertexCount; i++)
            {

                Console.Write("  " + (i + 1));
            }
            Console.WriteLine();
        }

    }

}
