using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите количество вершин графа: ");
        int numNodes = Convert.ToInt32(Console.ReadLine());

        int[,] adjacencyMatrix = GenerateAdjacencyMatrix(numNodes);
        int[,] incidenceMatrix = GenerateIncidenceMatrix(adjacencyMatrix);

        Console.WriteLine("Матрица смежности:");
        PrintMatrix(adjacencyMatrix);

        Console.WriteLine("Матрица инцидентности:");
        PrintMatrix(incidenceMatrix);
    }

    static int[,] GenerateAdjacencyMatrix(int numNodes)
    {
        Random rand = new Random();
        int[,] matrix = new int[numNodes, numNodes];

        //происходит заполнение матрицы смежности случайными значениями 0 и 1, где value случайное значение,
        //полученное с помощью метода Next
        for (int i = 0; i < numNodes; i++)
        {
            for (int j = i + 1; j < numNodes; j++)
            {
                //Значения элементов матрицы matrix[i, j] и matrix[j, i] устанавливаются равными value, чтобы обеспечить симметрию матрицы.
                int value = rand.Next(2);
                matrix[i, j] = value;
                matrix[j, i] = value;
            }
        }

        return matrix;
    }
    //Вывод матрицы на экран
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
    //GenerateIncidenceMatrix принимает матрицу смежности adjacencyMatrix в качестве параметра и возвращает матрицу инцидентности.
    static int[,] GenerateIncidenceMatrix(int[,] adjacencyMatrix)
    {
        //Получаем количество вершин в графе, которое будет равно размерности матрицы смежности по первому измерению.
        int numNodes = adjacencyMatrix.GetLength(0);
        //Вызываем метод GetNumEdges, который получает количество ребер в графе, основываясь на матрице смежности.
        int numEdges = GetNumEdges(adjacencyMatrix);

        //Создаем двумерный массив matrix с размерностью numNodes x numEdges, который будет представлять матрицу инцидентности.
        int[,] matrix = new int[numNodes, numEdges];
        int edgeIndex = 0;

        //Мы проходим по каждой вершине графа.
        for (int i = 0; i < numNodes; i++)
        {
            //Мы проходим по оставшимся вершинам, начиная с вершины следующей за i, чтобы избежать повторных проверок ребер.
            for (int j = i + 1; j < numNodes; j++)
            {
                if (adjacencyMatrix[i, j] == 1)
                {
                    //Если ребро между вершинами i и j существует, мы устанавливаем значение 1 в соответствующие строки i и j в текущем столбце edgeIndex в матрице инцидентности.
                    matrix[i, edgeIndex] = 1;
                    //Аналогично, устанавливаем значение 1 в соответствующие строки i и j в текущем столбце edgeIndex в матрице инцидентности.
                    matrix[j, edgeIndex] = 1;
                    edgeIndex++;
                }
            }
        }

        return matrix;
    }

    //метод GetNumEdges, который получает количество ребер в графе, основываясь на матрице смежности
    static int GetNumEdges(int[,] matrix)
    {
        //получение размерности матрицы matrix по нулевому индексу (количество строк).
        int numNodes = matrix.GetLength(0);
        int count = 0;

        for (int i = 0; i < numNodes; i++)
        {
            for (int j = i + 1; j < numNodes; j++)
            {
                if (matrix[i, j] == 1)
                {
                    count++;
                }
            }
        }

        return count;
    }
}
