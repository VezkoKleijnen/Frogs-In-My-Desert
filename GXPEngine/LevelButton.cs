using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class LevelButton : AnimationSprite
{
    string levelFile;
    string musicFile;
    MyGame myGame;
    public LevelButton(string _levelFile, MyGame _myGame, string _musicFile, string myPicture) : base (myPicture, 2, 1)
    {
        levelFile = _levelFile;
        myGame = _myGame;
        musicFile = _musicFile;
    }

    void Update()
    {
        ClickHandling();
    }

    void ClickHandling()
    {
        if (Input.mouseY > y && Input.mouseY < y + height && Input.mouseX > x && Input.mouseX < x + width)
        {
            SetCycle(0, 1, 5);
            if (Input.GetMouseButtonDown(0))
            {
                myGame.StartStage(levelFile, musicFile);
            }
        }
        else
        {
            SetCycle(1, 1, 5);
        }
    }

}
