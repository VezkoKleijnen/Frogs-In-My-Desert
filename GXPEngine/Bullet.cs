using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class Bullet : AnimationSprite
{
    Vec2 position;
    Vec2 velocity;
    float speed;
    float angle;

    public Bullet(float _x, float _y, float velX, float velY, float _speed = 5) : base("Bullet.png", 4, 1)
    {
        x = _x;
        y = _y - 32;
        SetCycle(0, 4, 5);
        SetOrigin(width / 2, height / 2);   
        scale = 0.5f;
        speed = _speed;
        position.SetXY(_x, _y - 32);
        velocity.SetXY(velX, velY + 32);
        velocity.Normalize();
        collider.isTrigger = true;
        speed = _speed;
    }

    void Update()
    {
        Animate();
        rotation = angle;
        angle+= 10;
        CheckScreen();
        UpdatePosition();
    }

    void CheckScreen()
    {
        if (x > game.width + 32 || y > game.height + 32 || x < -32 || y < -32)
        {
            LateDestroy();
        }
    }

    void UpdatePosition()
    {
        y = position.y;
        position += velocity * speed;
        x = position.x;
    }


}

