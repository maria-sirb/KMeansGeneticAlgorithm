# KMeansGeneticAlgorithm

A genetic algorithm based on the k-means principles, implemented in Windows Forms.

The algorithm groups a dataset into 4 clusters (K = 4). Each cluster is represented by a centroid. A point is considered to be part of a cluster if it is closer to that cluster's centroid than any other centroids.

The algorithm follows these steps:
1. Initialize a random population of solutions
2. Calculate the fitness of each solution
3. Solutions with highest fitness value are chosen as parents for the next generation of solutions
4. The next generation is created by crossover and mutation operations
5. Steps 2-4 are repeated for a number of generations
6. The best solution is chosen from the last generation

![Screenshot (4)](https://github.com/maria-sirb/KMeansGeneticAlgorithm/assets/91878977/c887aa7c-e48d-4d6b-9a18-10155858a931)

