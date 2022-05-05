# DLL-Snake
This project is meant to demonstrate dll importing. Snake behaviour is defined in C++ and imported for use in Unity.

To play in editor, run the "SnakeScene" scene. The snake is stationary to start. Gameplay begins when player inputs a direction on the arrow keys, and ends when the snake's head collides with its body. Snake CAN move reverse directions if its body is only one cell. After eating the first apple, this is no longer possible.

- C++ files are in the SnakeVSProj folder. 
- SnakeAPI (C++) and SnakeGlue (C#) are used to communicate between the two languages.

Build provided is most recent Windows build
