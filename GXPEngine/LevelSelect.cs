using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class LevelSelect : GameObject
{
    LevelButton level1;
    LevelButton level2;
    LevelButton level3;
    Sprite explain;
    public LevelSelect(MyGame _myGame) {
        level1 = new LevelButton("tiledFiles/Stage1.tmx", _myGame, "sounds/project_1.wav", "screenshots2.png");
        AddChild(level1);
        
        level2 = new LevelButton("tiledFiles/Stage2.tmx", _myGame, "sounds/project_1.wav", "screenshots.png");
        AddChild(level2);
        level2.y = 339;

        level3 = new LevelButton("tiledFiles/Stage3.tmx", _myGame, "sounds/project_2.wav", "screenshots3.png");
        AddChild(level3);
        level3.y = 678;

        explain = new Sprite("explanation.png");
        AddChild(explain);
        explain.x = 540;
    }
}
