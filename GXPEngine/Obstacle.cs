using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;
internal class Obstacle : AnimationSprite
{
    Sprite mySprite;
    public Obstacle(string filename, int cols, int rows, TiledObject obj)  : base (filename, cols, rows)
    {
            
        //visible = false;
        if (Utils.Random(0, 2) > 0.5f)
        {
            mySprite = new Sprite("cactus1.png", false, false);
        }
        else
        {
            mySprite = new Sprite("cactus2.png", false, false);
        }
        AddChild (mySprite);
        mySprite.x += 5;

    }

    void Update()
    {
        SetCycle(39, 1);
        SetOrigin(0, -64);
        scaleY = 0.5f;
        mySprite.scaleY = 2f;
        RemoveChild (mySprite);
        AddChild (mySprite);
        //put code between this
        //scaleY = 1f;
    }

    void OnCollision(GameObject other)
    {
        if (other is Bullet)
        {
            other.LateDestroy();
        }
    }
}
