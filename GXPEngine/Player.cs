using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Core;

internal class Player : AnimationSprite
{
    StageController controller;

    public Vec2 position;
    public Vec2 velocity;
    int score;

    float accel;
    public float speed;
    bool stun;
    int stunTimer;
    int stunTime = 2500;
    int health;
    int maxHealth;
    bool falling;
    bool grounded;
    float gravity;
    float offset;
    int springs;

    int speedUpgrades;
    int reloadUpgrades;


    bool notPlaying;
    float stepVolume;
    Sound step0;
    Sound step1;
    Sound step2;
    Sound step3;
    Sound step4;

    float upgradeVolume;
    Sound upgradeSound;
    Sound pickupSound;
    float heartVolume;
    Sound heartSound;
    Sound heartSound2;

    float hurtVolume;
    Sound hurt;
    Sprite shadow;

    float jumpVolume = 0.2f;
    Sound jump0;
    Sound jump1;
    Sound jump2;
    Sound jump3;


    public Player(StageController _controller, int _speed = 5, float _accel = 1) : base("player.png", 4, 12)
    {
        springs = 3;
        offset = 0;
        grounded = true;
        falling = false;
        gravity = 0;
        score = 0;
        stun = false;
        SetCycle(0, 4, 10);
        position.SetXY(game.width/2, game.height/2);
        speed = _speed;
        accel = _accel;
        collider.isTrigger = true;
        SetOrigin(32, 64);
        health = 4;
        maxHealth = 5;

        shadow = new Sprite("shadow.png", true, false);


        AddChild(shadow);
        shadow.x = 0;
        shadow.y = 0;
        shadow.SetOrigin(32, 32);

        controller = _controller;

        speedUpgrades = 0;
        reloadUpgrades = 0;

        notPlaying = true;
        stepVolume = 0.3f;
        step0 = new Sound("sounds/step0.wav");
        step1 = new Sound("sounds/step1.wav");
        step2 = new Sound("sounds/step2.wav");
        step3 = new Sound("sounds/step3.wav");
        step4 = new Sound("sounds/step4.wav");

        upgradeVolume = 0.2f;
        upgradeSound = new Sound("sounds/upgrade.wav");
        pickupSound = new Sound("sounds/pickup.wav");
        heartVolume = 0.5f;
        heartSound = new Sound("sounds/heart.wav");
        heartSound2 = new Sound("sounds/heart2.wav");

        hurtVolume = 0.8f;
        hurt = new Sound("sounds/playerHurt.ogg");

        jump0 = new Sound("sounds/boing0.wav");
        jump1 = new Sound("sounds/boing1.wav");
        jump2 = new Sound("sounds/boing2.wav");
        jump3 = new Sound("sounds/boing3.wav");
    }

    void Update()
    {
        scaleY = 0.5f;
        Animate();
        if (notPlaying && (currentFrame == 1))
        {
            StepSound();
            notPlaying = false;
        }
        else if (currentFrame != 1)
        {
            notPlaying = true;
        }
        UpdatePos();
        PlayerController();
        scaleY = 1f;
        WrapAround();
        StunCheck();
        position.y += gravity;
        shadow.y = - offset;
        offset += gravity;
        if (grounded && Input.GetKeyDown(Key.SPACE) && springs > 0)
        {
            springs--;
            Jump();
        }
        if (!grounded)
        {
            fall();
        }
        if (Mathf.Abs(velocity.y) > 0 || Mathf.Abs(velocity.x) > 0)
        {
            SetCycle(0, 4, 10);
        }
        else
        {
            SetCycle(0, 1, 10);
        }
    }

    public void Jump()
    {
        JumpSound();

        parent.RemoveChild(this);
        grounded = false;
        offset = 0;
        gravity = -5;
        controller.AddChild(this);
    }

    public void fall()
    {
        if (!falling)
        {
            gravity -= 0.1f;
        }
        if (falling)
        {
            gravity += 0.1f;
        }
        if (gravity < -5)
        {
            falling = true;
        }
        if (gravity > 5)
        {

        }
        if (offset > 0)
        {
            gravity = 0;
            falling = false;
            grounded = true;
            offset = 0;
        }
    }
    void JumpSound()
    {
        switch (Utils.Random(0, 4))
        {
            case 0: jump0.Play(false, 0, jumpVolume); break;
            case 1: jump1.Play(false, 0, jumpVolume); break;
            case 2: jump2.Play(false, 0, jumpVolume); break;
            case 3: jump3.Play(false, 0, jumpVolume); break;
        }
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
    void UpdatePos()
    {
        if (grounded)
        {
            Collision collision = MoveUntilCollision(velocity.x * speed, 0);
            if (collision != null)
            {
                OnCollision(collision.other);
            }
            else
            {
                position.x += velocity.x * speed;
            }

            collision = MoveUntilCollision(0, velocity.y * speed);
            if (collision != null)
            {
                OnCollision(collision.other);
            }
            else
            {
                position.y += velocity.y * speed;
            }
        }
        else
        {
            position += velocity * speed;
        }

        x = position.x;
        y = position.y;
    }

    void PlayerController()
    {
        if (grounded)
        {

            if (Input.GetKey(Key.D) && velocity.x < 5)
            {
                velocity.x += accel;
                scaleX = 1;
            }
            else if (Input.GetKey(Key.A) && velocity.x > -5)
            {
                velocity.x -= accel;
                scaleX = -1;
            }
            else
            {
                if (velocity.x < -0.9f)
                {
                    velocity.x += accel;
                }
                else if (velocity.x > 0.9f)
                {
                    velocity.x -= accel;
                }
                else
                {
                    velocity.x = 0;
                }
            }

            if (Input.GetKey(Key.S) && velocity.y < 5)
            {
                velocity.y += accel;
            }
            else if (Input.GetKey(Key.W) && velocity.y > -5)
            {
                velocity.y -= accel;
            }
            else
            {
                if (velocity.y < -0.5f)
                {
                    velocity.y += accel;
                }
                else if (velocity.y > 0.5f)
                {
                    velocity.y -= accel;
                }
                else
                {
                    velocity.y = 0;
                }
            }
        }
        velocity.Normalize();
    }

    void WrapAround()
    {

        if (x > game.width + 65)
        {
            position.x = -64;
        }
        if (x < -65)
        {
            position.x = game.width + 64;
        }
        if (y > game.height + 65)
        {
            position.y = -64;
        }
        if (y < -65)
        {
            position.y = game.height + 64;
        }
    }

    void StunCheck()
    {
        if (stun)
        {
            visible = !visible;
            stunTimer += Time.deltaTime;
            if (stunTimer > stunTime)
            {
                stun = false;
                stunTimer = 0;
                visible = true;
            }
        }
    }
    
    void OnCollision(GameObject other)
    {
        if (other is Enemy)
        {
            Enemy enemy = other as Enemy;
            if (enemy.getGrounded() && !stun && grounded)
            {
                stun = true;
                hurt.Play(false, 0, hurtVolume);
                health--;
            }
        }
        if (other is Item)
        {
            Item item = other as Item;
            switch (item.getItemType())
            {
                case 0:
                    if (speedUpgrades < 10)
                    {
                        speed += 0.2f;
                        speedUpgrades++;
                    }
                    upgradeSound.Play(false, 0, upgradeVolume);
                    break;
                case 1:
                    if (health < maxHealth)
                    health++;
                    heartSound.Play(false, 0, heartVolume);
                    break;
                case 2:
                    springs++;
                    pickupSound.Play(false, 0, upgradeVolume);
                    break;
                case 3:
                    if (reloadUpgrades < 10)
                    {
                        controller.UpReload();
                        reloadUpgrades++;
                    }
                    upgradeSound.Play(false, 0, upgradeVolume);
                    break;
                case 4:
                    maxHealth++;
                    health++;
                    heartSound2.Play(false, 0, upgradeVolume);
                    break;
            }

            other.LateDestroy();
        }
    }

    public void increaseScore(int amount)
    {
        score += amount;
    }

    public int getHealth() {  return health; }
    public int getMaxHealth() { return maxHealth; }
    public int getScore() {  return score; }
    public bool getStun() { return stun; }
    public int getSprings() { return springs;}
    public int getSpeedUpgrades() { return speedUpgrades;}
    public int getReloadUpgrades() { return reloadUpgrades;}
}
