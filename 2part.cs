using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.Write("Введите количество вершин графа: ");
        int numNodes = Convert.ToInt16(Console.ReadLine());
        Console.Write("Введите количество ребер графа: ");
        int numEdges = Convert.ToInt16(Console.ReadLine());
        int[,] incidenceMatrix = GenerateIncidenceMatrix(numNodes, numEdges);

        //Вывод матрицы инцидентности
        PrintIncidenceMatrix(incidenceMatrix);

        int graphSize = GetGraphSize(incidenceMatrix);
        Console.WriteLine("Размер графа G: " + graphSize);

        //Вызывается метод FindIsolatedNodes, который находит изолированные вершины графа, то есть вершины, не связанные ни с одним ребром.
        //Результат сохраняется в списке isolatedNodes, который выводится на консоль
        List<int> isolatedNodes = FindIsolatedNodes(incidenceMatrix);
        Console.WriteLine("Изолированные вершины: " + string.Join(", ", isolatedNodes));

        //Вызывается метод FindLeafNodes, который находит концевые вершины графа, то есть вершины, связанные только с одним ребром.
        //Результат сохраняется в списке leafNodes, который выводится на консоль.
        List<int> leafNodes = FindLeafNodes(incidenceMatrix);
        Console.WriteLine("Концевые вершины: " + string.Join(", ", leafNodes));

        //Вызывается метод FindDominatingNodes, который находит доминирующие вершины графа, то есть вершины, которые связаны со всеми ребрами.
        //Результат сохраняется в списке dominatingNodes, который выводится на консоль.
        List<int> dominatingNodes = FindDominatingNodes(incidenceMatrix);
        Console.WriteLine("Доминирующие вершины: " + string.Join(", ", dominatingNodes));
    }

    //Метод GenerateIncidenceMatrix генерирует случайную матрицу инцидентности для заданного количества вершин и ребер.
    static int[,] GenerateIncidenceMatrix(int numNodes, int numEdges)
    {
        //Он создает двумерный массив размером numNodes на numEdges и заполняет его случайными значениями 0 и 1. 
        Random rand = new Random();
        int[,] matrix = new int[numNodes, numEdges];

        for (int i = 0; i < numEdges; i++)
        {
            int node1 = rand.Next(numNodes);
            int node2 = rand.Next(numNodes);

            //Каждая строка матрицы соответствует вершине, а каждый столбец соответствует ребру.
            //Если в ячейке матрицы стоит 1, это означает, что вершина связана с соответствующим ребром.
            matrix[node1, i] = 1;
            matrix[node2, i] = 1;
        }

        return matrix;
    }

    //Метод PrintIncidenceMatrix выводит матрицу инцидентности на консоль.
    //Он получает размеры матрицы из ее параметров и использует два вложенных цикла для печати значений каждой ячейки.
    static void PrintIncidenceMatrix(int[,] matrix)
    {
        int numNodes = matrix.GetLength(0);
        int numEdges = matrix.GetLength(1);

        Console.WriteLine("Матрица инцидентности:");

        for (int i = 0; i < numNodes; i++)
        {
            for (int j = 0; j < numEdges; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }

            Console.WriteLine();
        }
    }

    //Метод GetGraphSize вычисляет количество ребер в графе, представленном матрицей инцидентности.
    //Он проходит по всем ячейкам матрицы и увеличивает счетчик, если в ячейке стоит 1.
    static int GetGraphSize(int[,] matrix)
    {
        int numNodes = matrix.GetLength(0);
        int numEdges = matrix.GetLength(1);

        int graphSize = 0;

        for (int i = 0; i < numNodes; i++)
        {
            for (int j = 0; j < numEdges; j++)
            {
                if (matrix[i, j] == 1)
                {
                    graphSize++;
                    break;
                }
            }
        }

        return graphSize;
    }
    //Метод FindIsolatedNodes находит изолированные вершины в графе. Он проходит по всем вершинам и проверяет, есть ли связанные с ними ребра.
    //Если ни одно ребро не связано с вершиной, она считается изолированной и добавляется в список изолированных вершин.
    static List<int> FindIsolatedNodes(int[,] matrix)
    {
        List<int> isolatedNodes = new List<int>();
        int numNodes = matrix.GetLength(0);
        int numEdges = matrix.GetLength(1);

        for (int i = 0; i < numNodes; i++)
        {
            bool isolated = true;

            for (int j = 0; j < numEdges; j++)
            {
                if (matrix[i, j] == 1)
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

        return isolatedNodes;
    }
    //Метод FindLeafNodes находит концевые вершины в графе. Он проходит по всем вершинам и подсчитывает количество связанных с ними ребер.
    //Если количество ребер равно 1, вершина считается концевой и добавляется в список концевых вершин.
    static List<int> FindLeafNodes(int[,] matrix)
    {
        List<int> leafNodes = new List<int>();
        int numNodes = matrix.GetLength(0);
        int numEdges = matrix.GetLength(1);

        for (int i = 0; i < numNodes; i++)
        {
            bool isLeafNode = true;
            int count = 0;

            for (int j = 0; j < numEdges; j++)
            {
                if (matrix[i, j] == 1)
                {
                    count++;
                }
                if (count == 1)
                {
                    isLeafNode = false;
                }
                else
                {
                    isLeafNode = true;
                }
            }

            if (!isLeafNode)
            {
                leafNodes.Add(i + 1);
            }
        }

        return leafNodes;
    }
    //Метод FindDominatingNodes находит доминирующие вершины в графе. Он проходит по всем вершинам и проверяет, связаны ли все ребра с данной вершиной.
    //Если все ребра связаны с вершиной, она считается доминирующей и добавляется в список доминирующих вершин.
    static List<int> FindDominatingNodes(int[,] matrix)
    {
        List<int> dominatingNodes = new List<int>();
        int numNodes = matrix.GetLength(0);
        int numEdges = matrix.GetLength(1);

        for (int i = 0; i < numNodes; i++)
        {
            bool isDominatingNode = true;

            for (int j = 0; j < numEdges; j++)
            {
                if (matrix[i, j] == 0)
                {
                    isDominatingNode = false;
                    break;
                }
            }
            if (isDominatingNode)
            {
                dominatingNodes.Add(i+1);
            }
        }

        return dominatingNodes;
    }
}