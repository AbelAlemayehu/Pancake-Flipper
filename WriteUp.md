# Write Up

For this save system for Pancake Flipper, I considered three main storage strategies: key/value pair, object serialization, and JSON file saving.

The first option, key/value pair storage, I was going to use Unity’s built in PlayerPrefs system, which would let me store small pieces of data like integers or strings under a simple key name. I think that this way of storage would become messy when I need it for several connected values: like pancake status, total count, and quality.

The second option, object serialization, I would’ve converted my C# objects into binary data that could be written and read from a file. I was actually quite intimidated by this approach because it seemed harder to debug, and for my small prototype game, using binary serialization would have added complexity and made it difficult to verify my saved data manually.

I chose to use a JSON file as my final save method. We talked about this method in class on Thursday, so I felt much more comfortable with this method than the others. I used Unity’s built in JsonUtility, and I serialized my data (status of pancake, total pancakes, and quality of pancake) into a single .json file stored in Unity. The biggest help has been that the JSON file made testing and debugging very simple as I could just open the file and see exactly what the game saved. I need something simple and straightforward for my simple game and a JSON file is easy to understand what is being saved and loaded.
