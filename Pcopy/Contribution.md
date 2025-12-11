# List of Contributions - Abel Alemayehu
## Gameplay Mechanics
I implemented all the core interactive mechanics of the game. This includes designing and coding the ladle scooping system, pouring the batter, spawning pancakes, flipping animations, and the drag and place functionality. I built the cooking progression system with multiple timed states (batter --> raw --> cooked --> burnt). 
## Cooking Logic and State
I designed and programmed the cooking lifecycle, including the cook timer, batter delay logic, flipping, fade in visuals, and cooking stop conditions when the pancake leaves the griddle. 
## Audio Integration
I integrated all audio elements: Background music, scoop sound effect, continuous sizzle sound tied to the griddle contanct, flip sound effect triggering an animation, and an order completion jingle trigggered by stacking 5 pancakes.
## UI Feedback
I implemented the CookRing UI logic, including the world to screen positioning, following active pancakes, cooking progress visualization, and color changes tied to the cooking state.
## Plate Stacking
I created the system that allows pancakes to stack on the plate in correct visual order using incrementing sprite sorting layers. I developed the automatic "last pancake goes on top" mechanic using a global counter, which improved the physical clraity of the stacking mechanic.
