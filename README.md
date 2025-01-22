# Weighted-and-Unweighted-Graph

The project’s design focuses on various graph layouts and problems such as weightage while using appropriate data structures and algorithms to ensure scalability to real-life scenarios, like road networks. Emphasis on time and space complexity achieved this to make sure one is not prioritised at the expense of the other.

The algorithm had to traverse through for both a weighted and unweighted graph to calculate an influence score. 

For an unweighted graph, I chose **Breadth first search** to calculate the shortest path with minimum number of hops. This algorithm is  well-suited for an adjacency list and effectively calculates the sum of shortest path from a node to all other nodes. BFS uses a priority queue data structure as it ensures exploration in order by proximity to the starting node. The algorithm explores the graph by enquing the connecting nodes, updating an array as tracker which records its distance from the starting node and dequing to move on to the next node. The algorithm iterates until the queue is empty resulting in all reachable nodes being explored.

For a weighted graph, I decided to implement **Dijkstra's algorithm** to compute the shortest path minimum total edge weight. Dijkstra's algorithm specifically accounts for edge weight. It works similarly to the BFS algorithm but uses weightage between the nodes to calculate the distance by adding the weight of the edge to the current node's distance. If this new distance is smaller than the existing value in the distance array, the array is updated. Similarly, the algorithm iterates until the queue is empty, finding the shortest path to all nodes.

Regardless of the graph or algorithm used to explore it, after each iteration an influence score had to be calculated. The influence score represents the sum of distance from a given node to all other nodes in the graph. A node with a highest influence score would be comparable to central intersections in a road network. With this information, busier intersections can be prioritised for repairs or improvements and infrastructure, new plans to optimise traffic flow, planning new roads and ultimately causing safer roads.

## Graphs Used
<img width="295" alt="image" src="https://github.com/user-attachments/assets/de2eea76-f04d-4f58-9ac1-688030ed2354" /> <img width="245" alt="image" src="https://github.com/user-attachments/assets/e87ff038-28ba-4ac6-8a28-625adbeb0dfb" />

