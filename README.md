FlappyBird Using Genetic Algorithm and Neural Networks:

In this project, I have implemented an AI for the Flappy Bird
which makes the computer play the game much more efficient as compared to what a
normal human being can do. I have achieved these goals using Genetic Algorithms
with an underlying Neural Network Architecture.

You can see this project in youtube: https://youtu.be/IWwaMLeiYp4 .

Implmentation:
1) Neural Network: I have neural network with five nodes, one for Bird Position and one for Velocity and one for Distance between next pipe
and one for Upper pipe height and finally for Lower pipe height. There is one output means perform flap and else do nothing and without any hidden layer.
Each node uses the sigmoid function as its activation function.
2) Genetic Algorithm: I sort the individuals of the current generation in decreasing order of their fitness values,
then we get top 25% directly to the next generation. Then I do cross-over and add them to next generation.

References:

https://www.cse.iitb.ac.in/~aviral/reportFlappy.pdf

https://medium.com/towards-data-science/neural-network-plays-flappy-bird-e585b1e49d97
