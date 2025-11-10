# Pancake Flipper – Prototype Documentation

## Updated Game Pitch
Pancake Flipper is a compact, timing based cooking game where the player prepares pancakes as accurately as possible. You scoop batter from the bowl, pour it onto the griddle, flip at the right time, and drag the pancake to the plate before it burns. The core pleasure is immediate and tactile simple actions with a clear rhythm that rewards attention and timing. Instead of managing a busy kitchen, the game emphasizes precision and flow around a single item, giving the process a tight, reactive feel that is easy to learn and satisfying to repeat.

## Project Synopsis
This prototype demonstrates the essential interactions needed to form a full cooking loop. The player uses a draggable ladle to collect batter and deposit it on a griddle. When poured, a pancake prefab is spawned at the griddle’s center and begins cooking immediately. From there, the pancake progresses through several time based phases (batter, raw, golden, burnt). The player can flip once with the spacebar, drag the pancake off the griddle to stop cooking, and finally serve it by placing it on a plate. Served pancakes visually stack, with each new pancake appearing on top.

It handles cooking state transitions, visual sprite swaps, collider resizing for accuracy, flipping animation, drag logic, and serving behavior. The current build supports a focused, single-pancake flow that lays a foundation for expansion into scoring, order pressure, and UI driven progression.

## Objective
The objective is to serve pancakes at their ideal golden stage. To achieve this, the player must pour batter, wait through early cook time, flip at a reasonable moment, and then remove and serve before burning. Mistiming leads to raw or burnt results. Although the prototype has no scoring system yet, success is naturally reflected by visual state and plate placement. The intended challenge is balancing patience and speed waiting long enough to hit the golden window, but reacting quickly enough to avoid burning.

## Core Mechanics
Players begin by dragging the ladle to the bowl to collect batter. Once filled, placing it over the griddle spawns a new pancake at a consistent central position. From that moment, the pancake cooks automatically while on the griddle. Cooking progresses through batter → raw → golden → burnt, by internal timers. The player may flip the pancake once by pressing space, giving a small bit of physical and timing significance. The pancake can be grabbed and dragged at any time; removing it from the griddle stops further cooking. When dragged to the plate, the pancake is considered served and visually stacked. The newest pancake always renders above previous ones, giving physical feedback for progress.

Behind the scenes, each state also updates the pancake’s collider size to match its sprite proportions, consistent interaction regardless of graphic changes. The combination of time based state change, player flipping, and drag to serve completes a full gameplay cycle that can be repeatedly performed.

## Notes on UI and Feedback
Although a cooking progress ring system was explored, it was not finalized. The game currently communicates through sprite changes and animation rather than UI bars or meters. The flip animation provides feedback for action timing, and plate stacking serves as a minimal visual indicator of success. Future development could improve feedback via UI countdowns, order display cards, or animation cues for golden vs. burnt states.


