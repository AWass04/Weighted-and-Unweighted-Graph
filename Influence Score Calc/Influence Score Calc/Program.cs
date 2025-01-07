using System;

namespace p4cs
{
    class Assignment
    {
        public static void Main()
        {
            // Creat adjacency lists for both graphs
            List<SinglyLinkedList> unweightedGraph = CreateAdjacencyList();
            List<SinglyLinkedList> weightedGraph = CreateWeightedAdjacencyList();

            int userChoice;

            do
            {
                Console.WriteLine("\n\n\n               . . . . . . . . . . . . . . . . . . . . .                    . . . . . . . . . . . . . . . . . . . . . . .");
                Console.WriteLine("               .    Unweighted and Undirected Graph    .                    .        Weighted and Undirected Graph      .");
                Console.WriteLine("               . . . . . . . . . . . . . . . . . . . . .                    . . . . . . . . . . . . . . . . . . . . . . .");

                Console.WriteLine("\n                                                            Select Option                                                ");
                Console.WriteLine("               . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .\n");
                Console.WriteLine("               . 1. Print Adjacency List                                     5. Print Adjacency List                     .");
                Console.WriteLine("               . 2. Find node with highest influence                         6. Find node with highest influence         .");
                Console.WriteLine("               . 3. Find node with lowest influence                          7. Find node with lowest influence          .");
                Console.WriteLine("               . 4. Find influence score of a specific node                  8. Find influence score of a specific node  .");
                Console.WriteLine("               . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .\n");

                Console.WriteLine("\n               9. Quit\n");

                // Error handling - if not a valid force error message
                if (!int.TryParse(Console.ReadLine(), out userChoice)) userChoice = 10;


                switch (userChoice)
                {

                    case 1:
                        PrintAdjacencyList(unweightedGraph); // Print unweighted graph adjacency list
                        break;
                    case 2:
                        FindInfluenceScore(unweightedGraph, true, false); // Find node with highest influence in unweighted graph
                        break;
                    case 3:
                        FindInfluenceScore(unweightedGraph, false, false); // Find node with lowest influence in unweighted graph
                        break;
                    case 4:
                        FindSpecificInfluenceScore(unweightedGraph, false); // Find specific node influence score in unweighted graph
                        break;
                    case 5:
                        PrintAdjacencyList(weightedGraph); // Print weighted graph adjacency list
                        break;
                    case 6:
                        FindInfluenceScore(weightedGraph, true, true); // Find node with highest influence in weighted graph
                        break;
                    case 7:
                        FindInfluenceScore(weightedGraph, false, true); // Find node with lowest influence in weighted graph
                        break;
                    case 8:
                        FindSpecificInfluenceScore(weightedGraph, true); // Find specific node influence score in weighted graph
                        break;
                    case 9:
                        Console.WriteLine("\nGoodbye :)\n"); // Exit the program
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please only enter whole numbers 1 - 9. \n");
                        break;
                }

            } while (userChoice != 9);
        }

        static List<SinglyLinkedList> CreateAdjacencyList()
        {
            int[][] nodes =
            [
                [2],         // Node 1
                [1, 3],      // Node 2
                [2, 4],      // Node 3
                [3, 5, 8],   // Node 4
                [4, 7, 8],   // Node 5
                [5, 7, 8],   // Node 6
                [6, 8],      // Node 7
                [4, 5, 6, 7] // Node 8
            ];

            List<SinglyLinkedList> adjacencyList = new List<SinglyLinkedList>();

            // Create unweighted adjacency list
            foreach (var neighbours in nodes) {
                
                SinglyLinkedList list = new SinglyLinkedList();
                
                foreach (var neighbour in neighbours) list.insert(neighbour,-1);

                adjacencyList.Add(list);
            }


            return adjacencyList;
        }

        static List<SinglyLinkedList> CreateWeightedAdjacencyList()
        {
            int[,] edges = // From, To, Weight
            {
                {1, 3, 1}, {1, 2, 1}, {1, 5, 5},                       // Node 1
                {2, 1, 1}, {2, 3, 4}, {2, 5, 1}, {2, 7, 1}, {2, 8, 1}, // Node 2
                {3, 1, 1}, {3, 2, 4}, {3, 4, 3}, {3, 5, 1},            // Node 3
                {4, 3, 3}, {4, 5, 2}, {4, 6, 1}, {4, 7, 5},            // Node 4
                {5, 1, 5}, {5, 2, 1}, {5, 3, 1}, {5, 4, 2}, {5, 7, 2}, // Node 5
                {6, 4, 1}, {6, 8, 1},                                  // Node 6
                {7, 2, 1}, {7, 4, 5}, {7, 5, 2}, {7, 6, 2}, {7, 8, 2}, // Node 7
                {8, 2, 1}, {8, 7, 2}, {8, 9, 3},                       // Node 8
                {9, 8, 3}, {9, 10, 3},                                 // Node 9
                {10, 9, 3}                                             // Node 10
            };

            List<SinglyLinkedList> adjacencyList = new List<SinglyLinkedList>();

            // create 10 singly linked lists
            for (int i = 0; i < 10; i++)  adjacencyList.Add(new SinglyLinkedList());

            // Populate the adjacency list
            for (int i = 0; i < edges.GetLength(0); i++) {
                adjacencyList[edges[i, 0] - 1].insert(edges[i, 1], edges[i, 2]);
            }

            return adjacencyList;
        }

        static void PrintAdjacencyList(List<SinglyLinkedList> adjacencyList)
        {

            Console.WriteLine("\nAdjacency List\n");
            
            int count = 1;

            foreach (SinglyLinkedList i in adjacencyList) {

                Console.Write(count + ": ");
                i.display();

                count++;
            }
        }

        static void FindInfluenceScore(List<SinglyLinkedList> adjacencyList, bool findHighest, bool isWeighted)
        {
            int nodeWithInfluence = 0;
            double storedInfuenceScore = 1;

            // Calculate each nodes influence score
            for (int i = 0; i < adjacencyList.Count; i++) {

                double influenceScore = CalculateInfluenceScore(adjacencyList, i, isWeighted);

                // Check to see if better influence score
                if (((storedInfuenceScore == 1 || influenceScore > storedInfuenceScore) && findHighest) || ((storedInfuenceScore == 1 || influenceScore < storedInfuenceScore) && !findHighest)) {
                    
                    storedInfuenceScore = influenceScore;
                    nodeWithInfluence = i + 1;
                }
            }

            storedInfuenceScore = Math.Round(storedInfuenceScore, 2);

            if (findHighest) Console.WriteLine("\nThe node with the highest influence score in the network is: " + nodeWithInfluence + "\nInfluence score: " + storedInfuenceScore + "\n");
            else Console.WriteLine("\nThe node with the lowest influence score in the network is: " + nodeWithInfluence + "\nInfluence score: " + storedInfuenceScore + "\n");
        }

        static void FindSpecificInfluenceScore(List<SinglyLinkedList> adjacencyList, bool isWeighted)
        {
            Console.WriteLine("\nWhich node would you like to find the influence score of? ");
            bool invalid = false;
            int node = 1;

            // Error handling - if does not give node in graph profuce error
            do {
                if (!int.TryParse(Console.ReadLine(), out node) || node < 1 || node > adjacencyList.Count) {

                    Console.WriteLine("\nInvalid input. Which node would you like to find the influence score of? \n");
                    invalid = true;

                }
                else invalid = false;
            } while(invalid);

            double influenceScore = CalculateInfluenceScore(adjacencyList, node - 1, isWeighted);

            // Display adjacency list
            influenceScore = Math.Round(influenceScore, 2);
            Console.WriteLine("\nThe influence score of node " + node + " is " + influenceScore + ". \n"); 
        }

        static double CalculateInfluenceScore(List<SinglyLinkedList> adjacencyList, int startNode, bool isWeighted)
        {
            int totalDistance = 0;
            int[,] distancesWeighted;
            int[] distancesUnweighted;

            if (isWeighted) { // Use correct algorithm

                distancesWeighted = DijkstraAlgorithm(adjacencyList, startNode);

                for (int i = 0; i < adjacencyList.Count; i++) {

                    totalDistance += distancesWeighted[i, 0];
                }
            }
            else {

                distancesUnweighted = BFS(adjacencyList, startNode);
                foreach (var i in distancesUnweighted) totalDistance += i;
            }

            double influenceScore = isWeighted ? 9.0 / totalDistance : 7.0 / totalDistance; // Calculate influence score

            return influenceScore;
        }

        static int[] BFS(List<SinglyLinkedList> adjacencyList, int startNode)
        {
            int[] distances = new int[adjacencyList.Count]; // Array of values of distance away from start
            bool[] visited = new bool[adjacencyList.Count]; // Array to check if position has been visted

            Queue<int> moves = new Queue<int>(); // queue of moves

            moves.Enqueue(startNode); // Add starting node to queue
            distances[startNode] = 0; // Distance to itself is 0
            visited[startNode] = true; // Mark as visted

            while (moves.Count > 0) {

                int position = moves.Dequeue(); // Get node from queue
                Node neighbour = adjacencyList[position].head; // Find the connecting nodes to said node

                while (neighbour != null) {

                    int neighbourPosition = neighbour.data - 1;

                    if (!visited[neighbourPosition]) {

                        visited[neighbourPosition] = true;
                        distances[neighbourPosition] = distances[position] + 1; // Update distance tracker
                        moves.Enqueue(neighbourPosition); // Add to queue
                    }

                    neighbour = neighbour.next; // Go to next neighbouring node 
                }
            }

            return distances;

        }
        static int[,] DijkstraAlgorithm(List<SinglyLinkedList> adjacencyList, int startNode)
        {
            int n = adjacencyList.Count; // Get size of graph
            const int INFINITE = int.MaxValue;
            int[,] tracker = new int[n, 2]; // Track weight and previous node

            for (int i = 0; i < n; i++) {
               
                tracker[i, 0] = INFINITE; // Weight/distance
                tracker[i, 1] = -1; // Previous node
            }

            tracker[startNode, 0] = 0; // Set distance to starting node as 0
            List<int> unvisited = new List<int>();

            for (int i = 0; i < n; i++) {

                unvisited.Add(i);
            }

            while (unvisited.Count > 0) { // Do until all nodes have been visited
            
                // Find the unvisited node with the smallest distance
                int smallestDistanceNode = -1;
                int smallestDistance = INFINITE;

                foreach (int node in unvisited) {

                    if (tracker[node, 0] < smallestDistance) {

                        smallestDistance = tracker[node, 0];
                        smallestDistanceNode = node;
                    }
                }

                int position = smallestDistanceNode;
                unvisited.Remove(position); // Remove from unvisited list

                Node neighbour = adjacencyList[position].head; // Find all connecting nodes via adjancency list

                while (neighbour != null) {

                    int neighbourPosition = neighbour.data - 1; // Get connecting node in 0 based index

                    int newDistance = tracker[position, 0] + neighbour.weight;

                    if (newDistance < tracker[neighbourPosition, 0]) { // If a shorter path is found
                    
                        tracker[neighbourPosition, 0] = newDistance; // Set new distance
                        tracker[neighbourPosition, 1] = position; // Set previous node
                    }

                    neighbour = neighbour.next; // Move to the next node
                }
            }

            return tracker;
        }
        class Node
        {
            public int data;
            public Node next;
            public int weight;
            public Node(int item, int weightage)
            {
                data = item;
                weight = weightage;
                next = null;
            }
        }
        class SinglyLinkedList
        {
            public Node head;
            public SinglyLinkedList()
            {
                head = null;
            }
            public void insert(int item, int weightage)
            {
                Node newNode = new Node(item, weightage);
                Node pointer = head;
               
                if (head == null) {

                    head = newNode;
                }
                else {
                    while (pointer.next != null) {

                        pointer = pointer.next;
                    }
                   
                    pointer.next = newNode;
                }
            }

            public void display()
            {
                Node pointer = head;

                while (pointer != null) {

                    Console.Write(pointer.data + " --> ");
                    pointer = pointer.next;
                }

                Console.WriteLine("Null");
            }
        }
    }
}