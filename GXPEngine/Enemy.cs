using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Core;

internal class Enemy : AnimationSprite
{
    public Player player;
    public Vec2 velocity;
    public Vec2 position;
    public float speed;
    public bool grounded;
    public int health;
    public float _hpMargin;
    public EasyDraw canvas;
    Shadows shadows;
    public int frogType;

    float hitVolume;
    Sound hit0;
    Sound hit1;
    Sound hit2;
    Sound hit3;
    Sound hit4;
    Sound hit5;
    Sound hit6;
    Sound hit7;
    Sound hit8;
    Sound hit9;
    Sound hit10;
    Sound hit11;

    Sound dead0;
    Sound dead1;
    Sound dead2;
    Sound dead3;
    Sound dead4;

    float dropVolume;
    Sound dropSound;

    StageController controller;


    public Sprite shadow;
    public Enemy(string filename, int cols, int rows, Player _player, Vec2 _position, StageController _controller, float _speed = 4, int _health = 5) : base (filename, cols, rows, -1, true)
    {
        grounded = true;
        scale = 0.01f * Utils.Random(80, 110);
        if (Utils.Random(0, 200 ) == 0)
        {
            scale *= 0.5f;
        }
        player = _player;
        position = _position;
        UpdatePos();
        speed = _speed;
        collider.isTrigger = true;
        SetOrigin(32, 64);
        health = _health;
        canvas = new EasyDraw(128, 128, false);
        AddChild(canvas);
        canvas.y = -32;
        canvas.scale /= scale;
        canvas.scale *= 0.5f;
        canvas.ShapeAlign(CenterMode.Min, CenterMode.Min);
        canvas.SetOrigin(64, 128);
        shadow = new Sprite("shadow.png", true, false);


        AddChild(shadow);
        shadow.x = 0;
        shadow.y = 0;
        shadow.SetOrigin(32, 32);

        frogType = 0;
        controller = _controller;

        hitVolume = 0.3f;
        hit0 = new Sound("sounds/enHit0.wav");
        hit1 = new Sound("sounds/enHit1.wav");
        hit2 = new Sound("sounds/enHit2.wav");
        hit3 = new Sound("sounds/enHit3.wav");
        hit4 = new Sound("sounds/enHit4.wav");
        hit5 = new Sound("sounds/enHit5.wav");
        hit6 = new Sound("sounds/enHit6.wav");
        hit7 = new Sound("sounds/enHit7.wav");
        hit8 = new Sound("sounds/enHit8.wav");
        hit9 = new Sound("sounds/enHit9.wav");
        hit10 = new Sound("sounds/enHit10.wav");
        hit11 = new Sound("sounds/enHit11.wav");

        dead0 = new Sound("sounds/5Points.wav");
        dead1 = new Sound("sounds/5Points1.wav");
        dead2 = new Sound("sounds/5Points2.wav");
        dead3 = new Sound("sounds/5Points3.wav");
        dead4 = new Sound("sounds/5Points4.wav");

        dropVolume = 0.1f;
        dropSound = new Sound("sounds/drop.mp3");
    }

    public void Update()
    {
        CheckDeath();
        Animate();
        if (scale < 0)
        {
            canvas.scaleX = Mathf.Abs(canvas.scaleX) * -1;
        }
        else
        {
            canvas.scaleX = Mathf.Abs(canvas.scale);
        }

        UpdateHealthbar();
        if (!(this is Frog) && !(this is Tumbleweed))
        {
            UpdatePos();
        }
    }


    void UpdatePos()
    {
        Collision collision = MoveUntilCollision(velocity.x * speed, 0);
        if (collision != null)
        {
            velocity.x = 0;
        }
        else
        {
            position.x += velocity.x * speed;
        }

        collision = MoveUntilCollision(0, velocity.y * speed);
        if (collision != null)
        {
            velocity.y = 0;
        }
        else
        {
            position.y += velocity.y * speed;
        }
        x = position.x;
        y = position.y;
    }

    void UpdateHealthbar()
    {
        canvas.ClearTransparent();
        canvas.Fill(240, 10, 10);
        canvas.Rect(0, 0, health * _hpMargin, 16);
        canvas.Fill(155, 155, 155, 100);
        canvas.Rect(0, 0, 128, 16);
    }

    public void OnCollision (GameObject other)
    {
        if (other is Bullet)
        {
            other.LateDestroy();
            health--;
            if (health > 0)
            {
                HitSound();
                player.increaseScore(1);
                ScorePart part = new ScorePart(x, y, 0);
                parent.LateAddChild(part);
            }
        }
    }

    public void CheckDeath()
    {
        scaleY *= 0.5f;
        if (health <= 0)
        {
            player.increaseScore(5);
            LateDestroy();

            DeadSound();

            if (Utils.Random(0, controller.getDropOdds()) == 0)
            { //just pretend there's a functional switch function for this situation in GXP okay :D
                controller.ResetDrop();
                if (!(this is Lizard) && !(this is Tumbleweed))
                {
                    dropSound.Play(false, 0, dropVolume);
                }
                if (this is Lizard)
                {
                    if (player.getSpeedUpgrades() < 10)
                    {
                        Item item = new Item(0);
                        item.SetXY(x, y);
                        parent.LateAddChild(item);
                        dropSound.Play(false, 0, dropVolume);
                    }
                }
                if (this is RedStar)
                {
                    Item item = new Item(1);
                    item.SetXY(x, y);
                    parent.LateAddChild(item);
                }
                if (this is Frog)
                {
                    if (frogType == 0 && player.getMaxHealth() < 8)
                    {
                        Item item = new Item(4);
                        item.SetXY(x, y);
                        parent.LateAddChild(item);
                    }
                    else
                    {
                        Item item = new Item(2);
                        item.SetXY(x, y);
                        parent.LateAddChild(item);
                    }

                }
                if (this is Tumbleweed)
                {
                    if (player.getReloadUpgrades() < 10)
                    {
                        Item item = new Item(3);
                        item.SetXY(x, y);
                        parent.LateAddChild(item);
                        dropSound.Play(false, 0, dropVolume);
                    }
                }
            }
            else
            {
                controller.IncreaseDrop();
            }
            ScorePart part = new ScorePart(x, y, 1);
            parent.LateAddChild(part);
        }
        scaleY *= 2;
    }

    void HitSound()
    {
        switch (Utils.Random(0, 12))
        {
            case 0: hit0.Play(false, 0, hitVolume); break;
            case 1: hit1.Play(false, 0, hitVolume); break;
            case 2: hit2.Play(false, 0, hitVolume); break;
            case 3: hit3.Play(false, 0, hitVolume); break;
            case 4: hit4.Play(false, 0, hitVolume); break;
            case 5: hit5.Play(false, 0, hitVolume); break;
            case 6: hit6.Play(false, 0, hitVolume); break;
            case 7: hit7.Play(false, 0, hitVolume); break;
            case 8: hit8.Play(false, 0, hitVolume); break;
            case 9: hit9.Play(false, 0, hitVolume); break;
            case 10: hit10.Play(false, 0, hitVolume); break;
            case 11: hit11.Play(false, 0, hitVolume); break;
        }
    }
    void DeadSound()
    {
        switch (Utils.Random(0, 5))
        {
            case 0: dead0.Play(false, 0, hitVolume); break;
            case 1: dead1.Play(false, 0, hitVolume); break;
            case 2: dead2.Play(false, 0, hitVolume); break;
            case 3: dead3.Play(false, 0, hitVolume); break;
            case 4: dead4.Play(false, 0, hitVolume); break;
        }
    }
    public bool getGrounded() { return grounded; }
}


