using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class Lizard : Enemy
{
    Sound walk0;
    Sound walk1;
    Sound walk2;
    Sound walk3;
    Sound walk4;
    Sound walk5;
    Sound walk6;
    Sound walking;

    float soundTimer;
    float soundTime;
    float walkVolume;

    //SoundChannel stopMySound;


    Vec2 direction;
    public Lizard(Player _player, Vec2 _position, StageController _controller, float _speed = 5, int _health = 5) : base("lizard.png", 4, 5, _player, _position, _controller, _speed, _health)
    {
        SetCycle(0, 4, 5);
        _hpMargin = 128f / health;
        shadow.y = -32;
        shadow.scaleY = 0.6f;
        soundTimer = 0;
        soundTime = 200;

        walkVolume = 0.2f;
        /*
        walk0 = new Sound("sounds/lizWalk0.wav");
        walk1 = new Sound("sounds/lizwalk1.wave");
        walk2 = new Sound("sounds/lizWalk2.wav");
        walk3 = new Sound("sounds/lizWalk3.wav");
        walk4 = new Sound("sounds/lizWalk4.wav");
        walk5 = new Sound("sounds/lizWalk5.wav");
        walk6 = new Sound("sounds/lizWalk6.wav");
        */
        //stopMySound = new SoundChannel(0);
        //walking = new Sound("sounds/lizWalking.wav", true);
        //walking.Play(false, 0, 0.2f);
    }

    void Update()
    {
        
        base.Update();
        FacePlayer();
        Movement();
    }

    void MoveTowardsPlayer()
    {
        direction = player.position - position;
        velocity = (velocity * 0.9995f) + (0.0005f * direction);
        velocity.Normalize();
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
    void Movement()
    {
        if (!player.getStun())
        {
            MoveTowardsPlayer();
        }
        else
        {
            velocity.SetXY(0, 0);
        }
    }
    void OnCollision(GameObject other)
    {
        base.OnCollision(other);
    }
    void SoundFunction() //this is unused because it didn't sound the way I wanted and had not enough time to make a better system
    {
        soundTimer += Time.deltaTime;
        if (soundTimer > soundTime)
        {
            soundTimer = 0;

            switch (Utils.Random(0, 7))
            {
                case 0: walk0.Play(false, 0, walkVolume); break;
                case 1: walk1.Play(false, 0, walkVolume); break;
                case 2: walk2.Play(false, 0, walkVolume); break;
                case 3: walk3.Play(false, 0, walkVolume); break;
                case 4: walk4.Play(false, 0, walkVolume); break;
                case 5: walk5.Play(false, 0, walkVolume); break;
                case 6: walk6.Play(false, 0, walkVolume); break;
            }


        }
    }
}

