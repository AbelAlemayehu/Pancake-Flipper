# AI Use: Yes for the .md format

# Elevator Pitch
My game is called **Pancake Flipper** and it’s this little cooking game that’s all about the chaos and satisfaction of making the perfect pancake. So you’re actually scooping batter and you’re pouring it on the griddle and you’re flipping it yourself (physcially using your mouse to do it). So it feels like you’re actually cooking and it’s simplistic but also a fun way to interact with cooking and making a pancake. It has interesting visual cues like the batter cooking and the pancake sizzling and when you nail the timing of a golden perfect flip, it's going to be satisfying like actual cooking it.

And if you mess up and it burns because you go too early or it’s raw, it kind of becomes this fast, chaotic cooking game. You as a player are actually cooking, dealing with the motions and feeling captuered of being in the kitchen and getting our hands messy and having fun with the chaos.

---

# Game Synopsis
**Pancake Flipper** is a chaotic little cooking game where you get to live out the dream of making the perfect pancake. From scooping the batter to flipping it in mid air: it;s all hands on because you actually drag the ladle, pour the batterm, and flip the pancake. The game is real and ridiculous, batter spalts everywhere, pancakes stack up, and timing becomes everything. You’ll burn some, undercook others, and maybe pull off that golden, flawless flip that makes you feel like a real chef. Enjoy the messy joy of learning to make the perfect pancake!

---

# Objective
The objective of **Pancake Flipper** is to successfully cook and flip pancakes to perfection by mastering timing and movement. The player begins with a ladle and bowl of batter, then must pour it carefully on the griddle to form pancakes. One on the grill, the pancake cooks for a short duration: too short and it remains raw, too long and it burns.

The player's task is to watch, listen, and respond to visual and timing cues that signal the perfect flipping moment. Using the spatula, the player must drag under the pancake and flip it smoothly to cook the other side. Each correct action brings satisfaction, while eros like burning or undercooking a pancake, bring frustration.

The ultimate goal is to find the perfect rhythm of cooking: steady hands, good timing, and attention to sensory details.

---

# Decription of Mechanics
**Pancake Flipper** is built around the **Direct Manipualtion interaction model**, meaning that the player interacts with the world by physically acting on it: dragging, clicking, and releasing objects in real time.

### Ladle Mechanics
- The player can click and drag the ladle using the mouse.  
- When the ladle touches the bowl (detected via colliders), it scoops batter, indicated visually and with a sound cue.  
- Moving the ladle over the griddle and releasing it pours the batter, spawning a pancake at the cursor's location.

### Pancake Mechanics
- Each pancake brings a cook timer as soon as it’s placed on the griddle.  
- After some time, it enters an optimal range where it can be flipped for perfect results.  
- If flipped too early, the pancake appears pale and soft; if slipped too late, it becomes darker or burnt.  

### Spatula Mechanics
- The spatula is also draggable using the mouse, allowing the player to slide it beneath the pancake’s collider.  
- A click input triggers the flip animation (a simple rotation), changing the pancake's state.  
- Proper flipping within the right cook window rewards the player with visual and auditory feedback (a crisp sound, golden pancake


# Initial Wireframe
![JPEG image-497F-AC58-1D-0](https://github.com/user-attachments/assets/ea1f8a57-ebc1-439c-85d5-af863872d5a6)

# Final Wireframe
<img width="1079" height="653" alt="Screenshot 2025-10-19 at 4 36 11 PM" src="https://github.com/user-attachments/assets/a9b3af08-874a-49e3-a4ef-71cc2ad83558" />

# Mockups
## Mockup 1
<img width="787" height="385" alt="Screenshot 2025-10-19 at 4 50 36 PM" src="https://github.com/user-attachments/assets/55c93291-8a55-42fe-9910-2c3476638a10" />


## Mockup 2
<img width="787" height="385" alt="Screenshot 2025-10-19 at 4 50 47 PM" src="https://github.com/user-attachments/assets/e5faaf33-c062-491f-9a9b-c273ffb4ab15" />


## Mockup 3
<img width="793" height="381" alt="Screenshot 2025-10-19 at 4 33 57 PM" src="https://github.com/user-attachments/assets/26a8ce2f-096e-4e84-9799-854bd7e64531" />
