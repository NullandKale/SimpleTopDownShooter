# SimpleTopDownShooter
This is a game using the "Engine" from NullandKale/OpenTKTests

I started this as a interesting project for my CS162 final, of course I decided to do something far too complicated and as such I am implementing my own game engine and creating a Simple Top Down Shooter with said engine. I am attempting to use no external libraries besides OpenTK, with exposes OpenGL in C#. The following is a todo list based on the requirements of this final. When the item is crossed off the todo list it will be moved to the done section below and include a link to the best example of that requirement.

Before I continue I'd like to explain one thing I do in comments that can be confusing for some people. If I am going to continue a comment in a few lines or so, above the corresponding code I will end the comment with a double tilde "~~", and then pickup the next comment with another double tilde "~~".

## TODO
  1. Implement a health system for the player
  2. Implement a game over screen
  3. Implement more weapons
  4. Implement melee attacks
    
## DONE
  1. An understanding of the use of variables, conditionals, loops, and arrays | [Link](https://github.com/NullandKale/SimpleTopDownShooter/blob/faf0e8a1c6bc2fa0356bfb86e66829f0e14e0381/CS162Final/StateMachines/GameState.cs)
  2. An understanding of code organization, decomposition (methods, classes, and source files) | [Link](https://github.com/NullandKale/SimpleTopDownShooter/tree/faf0e8a1c6bc2fa0356bfb86e66829f0e14e0381)
  3. An understanding of various design (hierarchy) and testing (test methods and maybe a separate driver that runs tests on your other classes!) techniques | [Link](https://github.com/NullandKale/SimpleTopDownShooter/tree/faf0e8a1c6bc2fa0356bfb86e66829f0e14e0381/CS162Final/StateMachines)
  4. User IO, File IO, and Input validation | [User IO Link](https://github.com/NullandKale/SimpleTopDownShooter/blob/faf0e8a1c6bc2fa0356bfb86e66829f0e14e0381/CS162Final/Managers/InputManager.cs) [File IO Link](https://github.com/NullandKale/SimpleTopDownShooter/blob/faf0e8a1c6bc2fa0356bfb86e66829f0e14e0381/CS162Final/Managers/TextureManager.cs)
  5. ~~Java~~ C# interfaces | [Interface Link](https://github.com/NullandKale/SimpleTopDownShooter/blob/faf0e8a1c6bc2fa0356bfb86e66829f0e14e0381/CS162Final/Entity%20-%20Component/iRenderable.cs) [Implementation Link](https://github.com/NullandKale/SimpleTopDownShooter/blob/faf0e8a1c6bc2fa0356bfb86e66829f0e14e0381/CS162Final/Entity%20-%20Component/quad.cs)
  6. Recursion | [Link](https://github.com/NullandKale/SimpleTopDownShooter/blob/faf0e8a1c6bc2fa0356bfb86e66829f0e14e0381/CS162Final/Managers/EnemyManager.cs)
  7. Nested classes | [Link](https://github.com/NullandKale/SimpleTopDownShooter/blob/faf0e8a1c6bc2fa0356bfb86e66829f0e14e0381/CS162Final/StateMachines/GameState.cs)
  8. GUI components, Event listeners, and their various uses | [Link](https://github.com/NullandKale/SimpleTopDownShooter/blob/faf0e8a1c6bc2fa0356bfb86e66829f0e14e0381/CS162Final/StateMachines/MenuState.cs)
  9. Exceptions | [Link](https://github.com/NullandKale/SimpleTopDownShooter/blob/faf0e8a1c6bc2fa0356bfb86e66829f0e14e0381/CS162Final/Managers/SingletonException.cs)
  10.Inheritance and Polymorphism | [Link](https://github.com/NullandKale/SimpleTopDownShooter/blob/faf0e8a1c6bc2fa0356bfb86e66829f0e14e0381/CS162Final/Entity%20-%20Component/Button.cs)
  11. Comment EVERYTHING