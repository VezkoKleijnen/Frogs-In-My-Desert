using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class ScorePart : AnimationSprite
{
    int type;
    bool bigger;
    float timer;

    public ScorePart(float _x, float _y, int type) : base ("score.png", 2, 1, -1, true, false)
    {
        bigger = true;
        SetOrigin(32, 32);
        x = _x;
        y = _y;
        SetCycle(type, 1, 5);

    }

    void Update()
    {
        timer += Time.deltaTime;
        FadingOut();
    }
    void FadingOut()
    {
        y--;
        if (bigger)
        {
            scale += 0.05f;
        }
        else
        {
            scale -= 0.05f;
        }
        if (scale < 0)
        {
            Destroy();
        }
        if (scale > 1.5f)
        {
            bigger = false;
        }
    }
}

