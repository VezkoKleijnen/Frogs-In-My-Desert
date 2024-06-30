using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class GUI :EasyDraw
{
    int score;
    float reloadMax;
    Player player;
    StageController controller;
    float reloadMargin;
    float healthWidth;
    Sprite reload;
    Sprite jumps;
    Sprite speed;
    Sprite reloadSpeed;
    public GUI(StageController _controller) : base (1920, 1080, false) 
    {
        reload = new Sprite("reload.png", true, false);
        AddChild(reload);
        //reload.x = 380;

        jumps = new Sprite("jumps.png", true, false);
        AddChild(jumps);
        jumps.y = 64;

        speed = new Sprite("speed.png", true, false);
        AddChild (speed);
        speed.x = game.width - 96;

        reloadSpeed = new Sprite("reloadSpeed.png", true, false);
        AddChild(reloadSpeed);
        reloadSpeed.x = game.width - 96;
        reloadSpeed.y = 64;

        player = _controller.player;
        controller = _controller;
        TextSize(32);
    }

    void Update()
    {
        DrawBars();

    }

    void DrawBars()
    {
        ClearTransparent();
        NoStroke();
        ShapeAlign(CenterMode.Min, CenterMode.Min);
        Fill(56, 61, 45, 150);
        Rect(0, 0, game.width, 128);
        Stroke(200);
        StrokeWeight(2);

        Fill(0, 0, 0);
        TextAlign(CenterMode.Center, CenterMode.Center);
        Text("score:", game.width / 2, game.height - 70);
        Text(player.getScore().ToString(), game.width / 2, game.height - 32);
        Fill(240, 20, 20, 150);
        ShapeAlign(CenterMode.Center, CenterMode.Min);

        for (int i = 0; i < player.getHealth(); i++)
        {
            DrawHealth(i);
        }

        Fill(0, 0, 0, 0);
        for (int i = 0; i < player.getMaxHealth(); i++)
        {
            DrawHealth(i);
        }

        ShapeAlign(CenterMode.Min, CenterMode.Min);
        Fill(20, 20, 230, 150);
        reloadMargin = 320f / controller.getShotCoolDown();
        reloadMax = controller.getshotTimer() * reloadMargin;
        if (reloadMax > 320)
        {
            reloadMax = 320;
        }
        Rect(64, 20, reloadMax, 32);
        Fill(0, 0, 0, 0);
        Rect(64, 20, 320, 32);
        TextAlign(CenterMode.Min, CenterMode.Min);
        Fill(0, 0, 0, 200);
        Text("x", 56, 64);
        Text(player.getSprings().ToString(), 80, 64);

        ShapeAlign(CenterMode.Max, CenterMode.Min);


        Fill(20, 20, 230, 150);

        for (int i = 0; i < player.getSpeedUpgrades(); i++)
        {
            Rect(game.width - 128 - 40 * i, 20, 40, 32);
        }
        for (int i = 0; i < player.getReloadUpgrades(); i++)
        {
            Rect(game.width - 128 - 40 * i, 79, 40, 32);
        }

        Fill(0, 0, 0, 0);
        for (int i = 0; i < 10; i++)
        {
            Rect(game.width - 128 - 40 * i, 20, 40, 32);
        }
        for (int i = 0; i < 10; i++)
        {
            Rect(game.width - 128 - 40 * i, 79, 40, 32);
        }
    }

    void DrawHealth(int i)
    {
        Rect(((game.width / 2) - 210 + 490 / player.getMaxHealth() * i), 48, 490 / player.getMaxHealth(), 32);
    }
}

