using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PathInGraph
{
    class Graph
    {
        public Dictionary<Vertex, int> distanceToVertex = new Dictionary<Vertex, int>();
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

        public Dictionary<Vertex,int> GetAdjacentVerticesWithLenght(List<Vertex> vertex)
        {
            var result = new Dictionary<Vertex, int>();
            foreach(var v in vertex)
            {
                foreach (var edge in Edges)
                {
                    if (edge.FromVertex == v)
                    {
                        if(!vertex.Contains(edge.ToVertex))
                        result.Add(edge.ToVertex, edge.Weight);
                    }
                }
            }
            return result;
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

        // D[v] = min(D[v], D[w]+C[w, v]);

        public void AlgorithDijkstra(Vertex start, Vertex end)
        {
            FlagAllVertex(start);
            List<Vertex> checkedVertex = new List<Vertex>();
            checkedVertex.Add(start);

            for(int i = 0; i < Vertices.Count - 1;i++)
            {
                Dictionary<Vertex, int> adjVertices = GetAdjacentVerticesWithLenght(checkedVertex[i]);
                var closestVertex = adjVertices.Where(pair => pair.Value == adjVertices.Values.Min());


                foreach (var item in adjVertices)
                {
                    if (distanceToVertex[item.Key] == -1)
                    {
                        distanceToVertex[item.Key] = item.Value + distanceToVertex[checkedVertex[i]];
                        var test1 = distanceToVertex[item.Key];
                    }
                    else
                    {
                        var newDistance = item.Value + distanceToVertex[checkedVertex[i]];
                        if (distanceToVertex[item.Key] > newDistance || distanceToVertex[item.Key] == -1)
                        {
                            distanceToVertex[item.Key] = newDistance;
                        }
                    }
                }

                if (adjVertices.Count == 0)
                {
                    try
                    {
                        var subset = distanceToVertex.Where(item => !checkedVertex.Contains(item.Key) &&
                            item.Value > 0).OrderBy(item => distanceToVertex.Values).Select(item => item.Key).First();
                        checkedVertex.Add(subset);
                    }
                    catch
                    {
                        
                    }

                }
                else
                {
                    foreach (var item in closestVertex)
                    {
                        if (!checkedVertex.Contains(item.Key))
                        {
                            Console.WriteLine("Checking {0}. Closest {1}", checkedVertex[i], item.Key.Number);
                            checkedVertex.Add(item.Key);
                        }
                    }
                }
            }
            Console.WriteLine("{0}-{1} Short lenght - {2}", start.Number, end.Number, distanceToVertex[end]);
        }

        //public void AlgorithmDixtra(Vertex start, Vertex end)
        //{
        //    List<Vertex> checkedVertex = new List<Vertex>();
        //    checkedVertex.Add(start);
        //    FlagAllVertex(start);
        //    for (int i = 0; i < Vertices.Count; i++)
        //    {
        //        Dictionary<Vertex, int> adjVertices = GetAdjacentVerticesWithLenght(checkedVertex[i]);
        //        if (adjVertices.Count == 0)
        //        {
        //            adjVertices = GetAdjacentVerticesWithLenght(start);
        //        }

        //        var closestVertex = adjVertices.Where(pair => pair.Value == adjVertices.Values.Min());
        //        foreach (var item in adjVertices)
        //        {
        //            if (distanceToVertex[item.Key] == -1)
        //            {
        //                distanceToVertex[item.Key] = item.Value + distanceToVertex[checkedVertex[i]];
        //                var test1 = distanceToVertex[item.Key];
        //            }
        //            else
        //            {
        //                var newDistance = item.Value + distanceToVertex[checkedVertex[i]];
        //                if (distanceToVertex[item.Key] > newDistance || distanceToVertex[item.Key] == -1)
        //                {
        //                    distanceToVertex[item.Key] = newDistance;
        //                }
        //            }
        //        }
        //        foreach (var item in closestVertex)
        //        {
        //            if (!checkedVertex.Contains(item.Key))
        //            {
        //                Console.WriteLine("Checking {0}. Closest {1}", checkedVertex[i], item.Key.Number);
        //                checkedVertex.Add(item.Key);
        //            }
        //            else
        //            {
        //                checkedVertex.Add(start);
        //            }
        //        }

        //    }
        //    Console.WriteLine("{0}-{1} Short lenght - {2}", start.Number, end.Number, distanceToVertex[end]);
        //}

        public void FlagAllVertex(Vertex start)
        {
            foreach (var item in Vertices)
                distanceToVertex.Add(item, -1);
            distanceToVertex[start] = 0;
        }
        public void CheckStartNeighbours()
        {

        }

        private void UpdateDistanceToVertex(Dictionary<Vertex,int> vertices)
        {
            foreach (var item in vertices)
            {
                distanceToVertex.Add(item.Key, item.Value);
            }
        }

        //public void AlgorithmDixtra(Vertex from, Vertex to)
        //{
        //    Dictionary<Vertex, int> shortPathes = new Dictionary<Vertex, int>();
        //    List<Vertex> usedVertices = new List<Vertex>();
        //    var checkVertex = from;
        //    for (int i = 0; i < Vertices.Count; i++)
        //    {
        //        Dictionary<Vertex, int> adjVertices = GetAdjacentVerticesWithLenght(usedVertices);
        //        var closestVertex = adjVertices.Where(pair => pair.Value == adjVertices.Values.Min());

        //        foreach(var kvp in closestVertex)
        //        {
        //            if (!shortPathes.ContainsKey(kvp.Key))
        //            {
        //                shortPathes.Add(kvp.Key, kvp.Value);
        //            }
        //        }
        //    }
        //for(int i=0; i<Vertices.Count;i++)
        //{
        //    Dictionary<Vertex, int> adjVertices = GetAdjacentVerticesWithLenght(usedVertices);
        //    var closestVertex = adjVertices.Where(pair => pair.Value == adjVertices.Values.Min());

        //    foreach(var kvp in closestVertex)
        //    {
        //        if (!shortPathes.ContainsKey(kvp.Key))
        //        {
        //            shortPathes.Add(kvp.Key, kvp.Value);
        //        }
        //        else
        //        {
        //            var pathToVertex = shortPathes[kvp.Key];
        //            shortPathes[kvp.Key] = kvp.Value;
        //        }
        //    }


        //}



        //public void AlgorithmDixtra(Vertex from, Vertex to)
        //{
        //    Dictionary<Vertex, int> pathes = new Dictionary<Vertex, int>();
        //    pathes.Add(from,0);
        //    Vertex lastVertex;
        //    Queue<Vertex> vQueue = new Queue<Vertex>();
        //    vQueue = SearchQueue(from, vQueue);

        //    Dictionary<Vertex,int> adjVertices = GetAdjacentVerticesWithLenght(from);


        //    while(vQueue.Count !=0)
        //    {
        //        var subset = adjVertices.Where(pair => pair.Value == adjVertices.Values.Min()); // Получить самого близкого соседа
        //        foreach (var kvp in subset)
        //        {
        //            var vertex = vQueue.Dequeue();
        //            lastVertex = vertex;
        //            if (!pathes.ContainsKey(kvp.Key))
        //            {
        //                pathes.Add(kvp.Key, kvp.Value);
        //            }
        //            else
        //            {
        //                var pathToVertex = pathes[kvp.Key];
        //                pathes[kvp.Key] = kvp.Value;
        //                Dictionary<Vertex, int> adjVertices = GetAdjacentVerticesWithLenght(from);
        //            }
        //        }
        //    }
        //}

        //private bool isReturnToBack()
        //{
        //    if(path)
        //    return true;
        //}

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
