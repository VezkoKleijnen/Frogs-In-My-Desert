using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Core;
internal class Frog : Enemy
{
    int jumpTimer;
    int jumpTime;
    float gravity;
    float maxGravity;
    float gravPull;
    bool startJump;

    Sound jump1;
    Sound jump2;
    Sound jump3;
    Sound jump4;


    SoundChannel jumps;
    //int frogType;
    //Sprite shadow;
    public Frog(Player _player, Vec2 _position, StageController _controller, float _speed = 5, int _health = 5) : base ("frog.png", 4, 5, _player, _position, _controller, _speed, _health)
        {
        Console.WriteLine("frog made");
        frogType = Mathf.Round(Mathf.Clamp(Utils.Random(0, 3    ), 0, 1));
        if (frogType == 0)
        {
            setFrameSit();
            jumpTime = 200 + Utils.Random(0, 300); //miliseconds between jumps
            gravity = 5;
            maxGravity = 3;
            gravPull = 0.2f;
            canvas.scale *= 1.5f;
            scale /= 1.5f;
            health = Utils.Random(2,4);
            speed = 3.5f;
        }
        else
        {
            setFrameSit();
            jumpTime = 500 + Utils.Random(0, 1000); //miliseconds between jumps
            Console.WriteLine(jumpTime);
            gravity = 5;
            maxGravity = 5;
            gravPull = 0.1f;
        }

        if (frogType == 1)
        {
            jump1 = new Sound("sounds/jump.wav");
            jump2 = new Sound("sounds/jump2.wav");
            jump3 = new Sound("sounds/jump3.wav");
            jump4 = new Sound("sounds/jump4.wav");
        }
        else
        {
            jump1 = new Sound("sounds/smallJump1.wav");
            jump2 = new Sound("sounds/smallJump2.wav");
            jump3 = new Sound("sounds/smallJump3.wav");
            jump4 = new Sound("sounds/smallJump4.wav");
        }


        jumps = new SoundChannel(0);

        _hpMargin = 128f / health;

        startJump = false;

        }

    void Update()
    {
        if (player.getStun())
        {
            velocity.x = 0;
            velocity.y = 0;
        }
        else
        {
            jumpTimer += Time.deltaTime;
        }

        if (jumpTimer > jumpTime) 
        {
            if (startJump)
            {
                startJump = false;
                MoveTowardsPlayer();
                /*
                if (x < 0)
                {
                    jump.Play(false, 0, 1, -1);
                }
                else if (x > game.width)
                {
                    jump.Play(false, 0, 1, 1);
                }
                else
                {
                    jump.Play(false, 0, 1, 0);
                }
                */
                JumpSound();
            }
            if (gravity > maxGravity)
            {
                startJump = true;
                gravity = -maxGravity;
                velocity.SetXY(0, 0);
                shadow.y = 0;
                jumpTimer = 0;
                setFrameSit();
                grounded = true;
            }
            else
            {
                gravity += gravPull;
                setFrameJump();
                grounded = false;
            }
            position.y += gravity;
            shadow.y -= gravity/2;
            shadow.scale = (Mathf.Abs(gravity)/5);
            //shadow.alpha = (Mathf.Abs(gravity) / 5);

            FacePlayer();
        }
        UpdatePos();
        base.Update();


        //base.Update();
    }

    void MoveTowardsPlayer()
    {
        velocity = player.position - position;
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

    void UpdatePos()
    {
        
        position += velocity * speed ;
        
        x = position.x;
        y = position.y;
    }

    void OnCollision(GameObject other)
    {
        base.OnCollision(other);
    }

    void setFrameJump()
    {
        if (frogType == 0)
        {
            SetCycle(1, 1, 5);
        }
        else
        {
            SetCycle(3, 1, 5);
        }
    }

    void setFrameSit()
    {
        if (frogType == 0)
        {
            SetCycle(0, 1, 5);
        }
        else
        {
            SetCycle(2, 1, 5);
        }
    }

    void JumpSound()
    {
        switch (Utils.Random(0, 4))
        {
            case 0:
                jump1.Play(false, 0, 0.2f);
                break;
            case 1:
                jump2.Play(false, 0, 0.2f);
                break;
            case 2:
                jump3.Play(false, 0, 0.2f);
                break;
            case 3:
                jump4.Play(false, 0, 0.2f);
                break;
        }
    }

    public int getFrogType() {  return frogType; }
}

