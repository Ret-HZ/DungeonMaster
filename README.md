# ![banner](https://github.com/Ret-HZ/DungeonMaster/assets/20912888/229b490e-66ec-45cf-a392-6834a4933c0e)
A dungeon layout editor for **_Like a Dragon: Infinite Wealth_**.
<br/>
<br/>

## How dungeons work
Dungeon floors are saved as **DGRF** files in the `battle/dungeon` folder. The game will choose one of the presets at random before loading a floor.<br/>
Each floor is comprised of a 31x31 grid, totalling 961 _possible_ rooms.<br/>
<br/>

### Limits
Despite the high number of possible rooms, the content generation for these rooms is far more limited.<br/>
Going above the limit may result in some rooms being completely empty, or the game crashing when attempting to load.<br/>
The player starting position is stored in the database and, by default, will always be the center tile, regardless of where the Start room is positioned. It is recommended not to move this room to avoid falling out of bounds.<br/>
<br/>

### Room content
The contents of a room will be randomized every time the layout is loaded.<br/>
A room of type _Random_, as the name implies, will randomly turn into any other room type except _Start_, _End_ and _Hallways_. The _Random_ room is the one used by default in most vanilla presets.<br/>
Despite there being room types to force a specific type of content to be generated inside a room, the actual contents will be randomly chosen from a table in the database. This falls out of scope for this tool and will not be covered.<br/>
<br/>

## Screenshots
![imagen](https://github.com/Ret-HZ/DungeonMaster/assets/20912888/53ac1a86-a24f-4334-b860-288a3a94534b)
![imagen](https://github.com/Ret-HZ/DungeonMaster/assets/20912888/c6b87ead-c0d6-463f-8b7d-857a29821ece)
<br/>
<br/>

## Credits
Many thanks to **Kent** for making the graphics used in this tool!
