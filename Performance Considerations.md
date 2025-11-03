# AI Use: YES for the .md file
# Performance Considerations Pancake Flipper

## First Source
My game *Pancake Flipper* and other similar casual cooking games are lightweight, but still face some performance concerns. One important source of performance cost is the repeated object to object interactions that take place in the game. There is constant collision between draggable kitchen tools (ladle, spatula) and food objects (batter, pancakes). If every object participates in a physics check each frame, the CPU load increases.  
A common strategy to deal with this could be minimizing collision layers and collider use so only essential objects are checked. I would try my best to reduce unnecessary code and only run it when things change. So, I could restrict when the ladle can collide (just the bowl and griddle), which prevents Unity from processing needless physics.

## Second Source
A second performance concern is excess frame updates. For my project, each pancake runs an `Update()` loop that increments cooking time and updates the sprite state. While manageable for a few pancakes, scaling to multiple cooking items increases CPU usage and can reduce frame rate.  
One strategy to help with this could be to have a `GameManager` update only pancakes that actually need state changes instead of every object updating every frame. This would avoid extra CPU usage and reduce jitter.

## Third Source
A third performance concern is frequent instantiation, particularly spawning pancakes every time batter is poured. Every time batter is poured, the game spawns a new pancake object, which allocates memory. When that pancake is later removed, Unity frees the memory.  
This constant loop creates performance problems that hurt the game flow of dragging and flipping objects. I could try and fix this concern by creating a reusable set of pancake objects and simply enabling/disabling them when needed rather than allocating new ones. I think this fix would reduce the performance issue of constantly adding and removing food items.

## Summary
For *Pancake Flipper*, I think the strategy that I will implement is reducing per frame processing, because the game’s core mechanic (cooking timing + flipping) depends on predictable and responsive input. Having the cooking timers inside a manager (updating only when cooking actually changes state) would minimize unnecessary logic and prevent slowdown as more pancakes appear.
