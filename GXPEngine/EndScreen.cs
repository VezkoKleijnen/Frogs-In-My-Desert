using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;
internal class EndScreen : EasyDraw
{
    int finalScore;
    AnimationSprite back;
    MyGame game;
    public EndScreen(int _highScore, int _finalScore, MyGame _game) : base(1920, 1017, false)
    {
        finalScore = _finalScore;
        game = _game;
        TextSize(32);
        Fill(0,0,0);

        Clear(255, 201, 127);
        TextAlign(CenterMode.Center, CenterMode.Center);
        Text("Your Score was:", game.width / 2, game.height / 2 - 256);
        Text(finalScore.ToString(), game.width / 2, game.height / 2 - 192);

        if (finalScore > _highScore)
        {
            _highScore = finalScore;
            game.UpdateHighScore(finalScore);
            Text("NEW HIGH SCORE!", game.width / 2, game.height / 2);

            TextWriter tw = new StreamWriter("SaveScore.txt");
            tw.WriteLine(finalScore);

            tw.Close();


        }

        Text("HighScore:", game.width / 2, game.height / 2 + 192);
        Text(_highScore.ToString(), game.width / 2, game.height / 2 + 256);

        back = new AnimationSprite("back.png", 2, 1, -1, false, false);
        AddChild(back);
        //back.SetOrigin(128, 128);
        back.x = game.width / 2 - 128;
        back.y = game.height - 128- 32;
    }

    void Update()
    {
        
        if (Input.mouseY > back.y && Input.mouseY < back.y + back.height && Input.mouseX > back.x && Input.mouseX < back.x + back.width)
        {
            back.SetCycle(1, 1, 5);
            if (Input.GetMouseButtonDown(0))
            {
                game.ClearGame();
            }
        }
        else
        {
            back.SetCycle(0, 1, 5);
        }
        
        back.Animate();
    }
}
