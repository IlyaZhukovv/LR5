using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace example2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество вершин графа: ");
            int numNodes = Convert.ToInt16(Console.ReadLine());
            //Создает двумерный массив `adjacencyMatrix` с помощью метода `GenerateAdjacencyMatrix`, который принимает `numNodes` в качестве аргумента.
            int[,] adjacencyMatrix = GenerateAdjacencyMatrix(numNodes);

            Console.WriteLine("Матрица смежности: ");
            //Вызывает метод `PrintMatrix`, который принимает `adjacencyMatrix` в качестве аргумента и печатает его содержимое в консоль.
            PrintMatrix(adjacencyMatrix);

            //Получает количество вершин графа из размера массива `adjacencyMatrix`.
            numNodes = adjacencyMatrix.GetLength(0);
            //Создает одномерный массив `isolatedNodes` с помощью метода `FindIsolatedNodes`, который принимает `adjacencyMatrix` в качестве аргумента и возвращает изолированные вершины графа
            int[] isolatedNodes = FindIsolatedNodes(adjacencyMatrix);
            //Создает одномерный массив `terminalNodes` с помощью метода `FindTerminalNodes`, который принимает `adjacencyMatrix` в качестве аргумента и возвращает концевые вершины графа
            int[] leafyNodes = FindLeafyNodes(adjacencyMatrix);
            //Создает одномерный массив `dominatingNodes` с помощью метода `FindDominatingNodes`, который принимает `adjacencyMatrix` в качестве аргумента и возвращает доминирующие вершины графа
            int[] dominatingNodes = FindDominatingNodes(adjacencyMatrix);

            Console.WriteLine("Размер графа: " + numNodes);
            Console.WriteLine("Изолированные вершины: " + string.Join(", ", isolatedNodes));
            Console.WriteLine("Концевые вершины: " + string.Join(", ", leafyNodes));
            Console.WriteLine("Доминирующие вершины: " + string.Join(", ", dominatingNodes));
        }

        //генерирует матрицу смежности для заданного количества узлов.
        //Каждый элемент матрицы может принимать значения 0 или 1, которые случайным образом генерируются с помощью объекта класса Random.
        static int[,] GenerateAdjacencyMatrix(int numNodes)
        {
            Random rand = new Random();
            int[,] matrix = new int[numNodes, numNodes];

            for (int i = 0; i < numNodes; i++)
            {
                for (int j = i + 1; j < numNodes; j++)
                {
                    int value = rand.Next(2);
                    //Значение элемента[i, j] определяет наличие или отсутствие ребра между узлами i и j
                    matrix[i, j] = value;
                    //Значение элемента [j, i] также устанавливается для обеспечения симметрии матрицы
                    matrix[j, i] = value;
                }
            }

            return matrix;
        }
        //Метод PrintMatrix печатает матрицу смежности в консоль. 
        //Он проходит по каждому элементу матрицы и выводит его значение, а также пробел.
        static void PrintMatrix(int[,] matrix)
        {
            int numNodes = matrix.GetLength(0);

            for (int i = 0; i < numNodes; i++)
            {
                for (int j = 0; j < numNodes; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        //Метод FindIsolatedNodes находит изолированные узлы в матрице смежности.
        //Метод перебирает каждый узел в матрице и проверяет, есть ли у него смежные узлы (ребра).
        //Если нет смежных узлов, то текущий узел считается изолированным и добавляется в список isolatedNodes.
        //Возвращается массив изолированных узлов.
        static int[] FindIsolatedNodes(int[,] matrix)
        {
            int numNodes = matrix.GetLength(0);
            List<int> isolatedNodes = new List<int>();

            for (int i = 0; i < numNodes; i++)
            {
                bool isolated = true;

                for (int j = 0; j < numNodes; j++)
                {
                    if (matrix[i, j] == 1 || matrix[j, i] == 1)
                    {
                        isolated = false;
                        break;
                    }
                }

                if (isolated)
                {
                    isolatedNodes.Add(i + 1);
                }
            }

            return isolatedNodes.ToArray();
        }

        //Метод FindTerminalNodes находит концевые узлы в матрице смежности.
        //Метод перебирает каждый узел в матрице и проверяет, если кол-во ребер больше 1, то ничего не выводит
        //Возвращается массив концевых узлов.
        static int[] FindLeafyNodes(int[,] matrix)
        {
            int numNodes = matrix.GetLength(0);
            List<int> leafyNodes = new List<int>();

            for (int i = 0; i < numNodes; i++)
            {
                bool terminal = true;
                int count = 0;

                for (int j = 0; j < numNodes; j++)
                { 
                    if (matrix[i, j] == 1 || matrix[j, i] == 1)
                    {
                        count++;
                    }
                    if (count == 1)
                    {
                        terminal = false;
                    }
                    else
                    {
                        terminal = true;
                    }
                }

                if (!terminal)
                {
                    leafyNodes.Add(i + 1);
                }
            }

            return leafyNodes.ToArray();
        }

        //Метод FindDominatingNodes находит доминирующие узлы в матрице смежности.
        //Метод перебирает каждый узел в матрице и проверяет, есть ли у него ребра с каждым другим узлом, кроме самого себя.
        //Если есть, то текущий узел считается доминирующим и добавляется в список dominatingNodes.
        //Возвращается массив доминирующих узлов.
        static int[] FindDominatingNodes(int[,] matrix)
        {
            int numNodes = matrix.GetLength(0);
            List<int> dominatingNodes = new List<int>();

            for (int i = 0; i < numNodes; i++)
            {
                bool dominating = true;

                for (int j = 0; j < numNodes; j++)
                {
                    if (i != j && matrix[i, j] == 0)
                    {
                        dominating = false;
                        break;
                    }
                }

                if (dominating)
                {
                    dominatingNodes.Add(i + 1);
                }
            }

            return dominatingNodes.ToArray();
        }

    }
}
