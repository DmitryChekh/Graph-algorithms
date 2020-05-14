using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PathInGraph
{
    class Graph
    {
        public Dictionary<Vertex, int> distanceToVertex = new Dictionary<Vertex, int>();
        List<Vertex> checkedVertex = new List<Vertex>();
        List<Edge> Edges = new List<Edge>();
        List<Vertex> Vertices = new List<Vertex>();
        public int VertexCount => Vertices.Count;
        public int EdgeCount => Edges.Count;

        public void AddVertex(Vertex vertex)
        {
            Vertices.Add(vertex);
        }

        public void AddEdges(Vertex from, Vertex to, int weight = 1)
        {
            Edge edge = new Edge(from, to, weight);
            Edges.Add(edge);
        }

        public int[,] GetMatrix()
        {
            int[,] matrix = new int[Vertices.Count, Vertices.Count];
            foreach (var edge in Edges)
            {
                int row = edge.FromVertex.Number - 1;
                int column = edge.ToVertex.Number - 1;

                matrix[row, column] = edge.Weight;
            }
            return matrix;
        }


        public List<Vertex> GetAdjacentVertices(Vertex vertex)
        {
            List<Vertex> adjacentVerctices = new List<Vertex>();
            foreach(var edge in Edges)
            {
                if(edge.FromVertex == vertex)
                {
                    adjacentVerctices.Add(edge.ToVertex);
                }
            }
            return adjacentVerctices;
        }

        public Dictionary<Vertex, int> GetAdjacentVerticesWithLenght(Vertex vertex)
        {
            var result = new Dictionary<Vertex, int>();

            foreach (var edge in Edges)
            {
                if (edge.FromVertex == vertex)
                {
                        result.Add(edge.ToVertex, edge.Weight);
                }
            }

            return result;
        }


        public void AlgorithDijkstra(Vertex start, Vertex end)
        {
            FlagAllVertex(start);
            checkedVertex.Add(start);

            for(int i = 0; i < Vertices.Count; i++)
            {
                Vertex checkingVertex = checkedVertex[checkedVertex.Count - 1];
                Dictionary<Vertex, int> adjVertices = GetAdjacentVerticesWithLenght(checkingVertex);
                CheckingNeightbours(adjVertices, checkingVertex);
                ChoosingNextVertex(adjVertices);
            }
            checkedVertex.Clear();
        }

        private void ChoosingNextVertex(Dictionary<Vertex, int> adjVertices)
        {

            if (adjVertices.Count == 0) 
            {
                GoToNonCheckedVertexWithShortestLenght();
            }
            else
            {
                GoToClosestVertex(adjVertices);
            }
        }

        private void GoToNonCheckedVertexWithShortestLenght()
        {
            var subset = distanceToVertex.Where(item => !checkedVertex.Contains(item.Key) &&
                item.Value > 0).OrderBy(item => distanceToVertex.Values).Select(item => item.Key).First();
            checkedVertex.Add(subset);
        }

        private void GoToClosestVertex(Dictionary<Vertex, int> adjVertices)
        {
            var closestVertex = adjVertices.Where(pair => pair.Value == adjVertices.Values.Min());
            foreach (var item in closestVertex)
            {
                if (!checkedVertex.Contains(item.Key))
                {
                    checkedVertex.Add(item.Key);
                }
            }
        }

        private void CheckingNeightbours(Dictionary<Vertex, int> adjVertices, Vertex checkingVertex)
        {
            foreach (var item in adjVertices)
            {
                if (distanceToVertex[item.Key] == -1) 
                {
                    distanceToVertex[item.Key] = item.Value + distanceToVertex[checkingVertex];
                }
                else
                {
                    var newDistance = item.Value + distanceToVertex[checkingVertex];
                    if (distanceToVertex[item.Key] > newDistance)
                    {
                        distanceToVertex[item.Key] = newDistance;
                    }
                }
            }
        }


        public void FlagAllVertex(Vertex start)
        {
            foreach (var item in Vertices)
                distanceToVertex.Add(item, -1);
            distanceToVertex[start] = 0;
        }
    

        public void DFS(Vertex from, Vertex to)
        {
            
            Stack<Vertex> vStack = new Stack<Vertex>();
            List<Vertex> checkedVertex = new List<Vertex>();
            vStack = SearchStack(from, vStack);

            while (vStack.Count != 0)
            {
                var vertex = vStack.Pop();
                Console.Write("{0} - ", vertex.ToString()); ;
                if (!checkedVertex.Contains(vertex))
                {
                    if (vertex == to)
                    {
                        Console.WriteLine("Путь к {0} найден", vertex.Number);
                        return;
                    }
                    else
                    {
                        vStack = SearchStack(vertex, vStack);
                        checkedVertex.Add(vertex);
                    }
                }
            }
            Console.WriteLine("Не найдено");
        }

        public void BFS(Vertex from, Vertex to)
        {
            Queue<Vertex> vQueue = new Queue<Vertex>();
            List<Vertex> checkedVertex = new List<Vertex>();
            vQueue = SearchQueue(from, vQueue);

            while (vQueue.Count != 0)
            {
                var vertex = vQueue.Dequeue();
                Console.Write("{0} - ",vertex.ToString()); ;
                if (!checkedVertex.Contains(vertex))
                {
                    if (vertex == to)
                    {
                        Console.WriteLine("Путь к {0} найден", vertex.Number);
                        return;
                    }
                    else
                    {
                        vQueue = SearchQueue(vertex, vQueue);
                        checkedVertex.Add(vertex);
                    }
                }
            }
            Console.WriteLine("Не найдено");
        }


        private Stack<Vertex> SearchStack(Vertex from, Stack<Vertex> vStack)
        {
            var list = GetAdjacentVertices(from);
            foreach(var v in list)
            {
                vStack.Push(v);
            }

            return vStack;
        }
        private Queue<Vertex> SearchQueue(Vertex from, Queue<Vertex> vQueue)
        {
            var list = GetAdjacentVertices(from);
            foreach (var v in list)
            {
                vQueue.Enqueue(v);
            }

            return vQueue;
        }

    }
}
