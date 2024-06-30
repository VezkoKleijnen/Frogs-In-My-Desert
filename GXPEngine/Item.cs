using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class Item : AnimationSprite
{
    int type;
    float gravity;
    bool up;
    float shadowSize;

    Sprite shadow;

    public Item(int _type) : base ("items.png", 3, 3)
    {
        scale = 0;
        SetOrigin(32, 64);
        type = _type;
        SetCycle(type, 1, 5);
        collider.isTrigger = true;
        up = false;
        shadow = new Sprite("shadow.png", true, false);
        AddChild(shadow);
        shadow.x = 0;
        shadow.y = 32;
        shadow.SetOrigin(32, 32);
        shadow.scale = 0;
    }

    void Update()
    {
        FadeIn();
        Floating();
    }
    void FadeIn()
    {
        if (scale < 0.9f)
        {
            scale += 0.1f;
        }
    }
    void Floating()
    {
        if (up)
        {
            gravity -= 0.1f;
        }
        else if (!up)
        {
            gravity += 0.1f;
        }
        if (gravity > 2)
        {
            up = true;
        }
        if (gravity < -2)
        {
            up = false;
        }
        if (gravity > 0)
        {
            shadow.scale += 0.025f;
        }
        if (gravity < 0)
        {
            shadow.scale -= 0.025f;
        }
        y += gravity;
        shadow.y -= gravity;
        if (gravity > 0) ; // als gravity groter is dan 0 maak groter, anders maak kleiner met zelfde snelheid als de gravity veranderd, dus 0.1f
    }
    public int getItemType () { return type; }
}
