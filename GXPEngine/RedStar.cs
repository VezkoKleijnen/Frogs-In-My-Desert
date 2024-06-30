using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

internal class RedStar : Enemy
{
    Vec2 direction;

    bool notPlaying;
    float stepVolume;
    Sound step0;
    Sound step1;
    Sound step2;
    Sound step3;
    Sound step4;


    public RedStar(Player _player, Vec2 _position, StageController _controller, float _speed = 5, int _health = 5) : base ("enemy.png", 4, 5, _player, _position, _controller, _speed, _health)
    {
        SetCycle(4, 4, 10);
        _hpMargin = 128f / health;

        notPlaying = true;
        stepVolume = 0.2f;
        step0 = new Sound("sounds/step0.wav");
        step1 = new Sound("sounds/step1.wav");
        step2 = new Sound("sounds/step2.wav");
        step3 = new Sound("sounds/step3.wav");
        step4 = new Sound("sounds/step4.wav");
    }

    void Update()
    {
        if (notPlaying && (currentFrame == 4 || currentFrame == 8))
        {
            StepSound();
            notPlaying = false;
        }
        else if (currentFrame != 4 &&  currentFrame != 8)
        {
            notPlaying = true;
        }

        base.Update();
        FacePlayer();
        if (!player.getStun())
        {
            MoveTowardsPlayer();
        }
        else
        {
            velocity.SetXY(0, 0);
        }
    }

    void MoveTowardsPlayer()
    {
        direction = player.position - position;
        velocity = (velocity * 0.9f) + (0.1f * direction);
        velocity.Normalize();
    }
    void StepSound()
    {
        switch (Utils.Random(0, 5))
        {
            case 0: step0.Play(false, 0, stepVolume); break;
            case 1: step1.Play(false, 0, stepVolume); break;
            case 2: step2.Play(false, 0, stepVolume); break;
            case 3: step3.Play(false, 0, stepVolume); break;
            case 4: step4.Play(false, 0, stepVolume); break;
        }
    }
    void FacePlayer()
    {
        if (player.x > x)
        {
            SetCycle(4,4,10);
        }
        else
        {
            SetCycle (8,4,10);
        }
    }
    void OnCollision(GameObject other)
    {
        base.OnCollision(other);
    }
}

