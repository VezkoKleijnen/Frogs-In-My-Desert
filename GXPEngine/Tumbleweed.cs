using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class Tumbleweed : Enemy
{
    float gravity;
    float bouncyness;
    Vec2 direction;
    //Sprite shadow;

    float ballVolume;
    Sound ball0;
    Sound ball1;
    Sound ball2;
    Sound ball3;
    Sound ball4;
    Sound ball5;
    Sound ball6;
    Sound ball7;
    Sound ball8;
    Sound ball9;
    public Tumbleweed(Player _player, Vec2 _position, StageController _controller, float _speed = 5, int _health = 5) : base("tumbleweed.png", 4, 5, _player, _position, _controller, _speed, _health)
    {
        SetCycle(0, 8, 8);
        _hpMargin = 128f / health;


        gravity = Utils.Random(-3, 3);
        bouncyness = 1f + 2 * scale;

        ballVolume = 0.3f;
        ball0 = new Sound("sounds/ball0.wav");
        ball1 = new Sound("sounds/ball1.wav");
        ball2 = new Sound("sounds/ball2.wav");
        ball3 = new Sound("sounds/ball3.wav");
        ball4 = new Sound("sounds/ball4.wav");
        ball5 = new Sound("sounds/ball5.wav");
        ball6 = new Sound("sounds/ball6.wav");
        ball7 = new Sound("sounds/ball7.wav");
        ball8 = new Sound("sounds/ball8.wav");
        ball9 = new Sound("sounds/ball9.wav");
    }

    void Update()
    {
        if (player.getStun())
        {
            velocity.x = 0;
            velocity.y = 0;
        }
        if (gravity > bouncyness)
        {
            gravity = -bouncyness;
            FacePlayer();
            MoveTowardsPlayer();
            shadow.y = 0;
            HushSound();
        }
        else
        {
            gravity += 0.1f;
        }
        position.y += gravity;
        shadow.scale = ((Mathf.Abs(gravity) + 2)  / 5);
        //shadow.alpha = (Mathf.Abs(gravity) / 3);
        shadow.y -= gravity;
        UpdatePos();
        base.Update();
    }

    void MoveTowardsPlayer()
    {
        direction = player.position - position;
        velocity = (velocity * 0.99f) + (0.01f * direction);
        velocity.Normalize();
    }

    void UpdatePos()
    {

        position += velocity * speed;

        x = position.x;
        y = position.y;
    }

    void FacePlayer()
    {
        if (player.x > x)
        {
            scaleX = Mathf.Abs(scaleX);
        }
        else
        {
            scaleX = -Mathf.Abs(scaleX);
        }
    }
    void HushSound()
    {
        switch (Utils.Random(0, 10))
        {
            case 0: ball0.Play(false, 0, ballVolume); break;
            case 1: ball1.Play(false, 0, ballVolume); break;
            case 2: ball2.Play(false, 0, ballVolume); break;
            case 3: ball3.Play(false, 0, ballVolume); break;
            case 4: ball4.Play(false, 0, ballVolume); break;
            case 5: ball5.Play(false, 0, ballVolume); break;
            case 6: ball6.Play(false, 0, ballVolume); break;
            case 7: ball7.Play(false, 0, ballVolume); break;
            case 8: ball8.Play(false, 0, ballVolume); break;
            case 9: ball9.Play(false, 0, ballVolume); break;
        }
    }

    void OnCollision(GameObject other)
    {
        base.OnCollision(other);
    }
}
