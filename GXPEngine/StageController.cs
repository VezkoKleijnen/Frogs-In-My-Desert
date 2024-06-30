using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
internal class StageController : GameObject
{
    Stage1 stage1;
    public Player player;
    Shadows shadows;
    GUI gui;
    MyGame game;

    float enemyTimer;
    float enemyTimerLimit;
    float enemyNumMin;
    float enemyNumMax;
    int shotCoolDown;
    int shotTimer;
    int enemyLevel;

    //code for mouse bcs GXP allows my mouse to leave a full screen windoqw with an infinite distance

    float oldMouseX;
    float oldMouseY;
    public float mouseXMove;
    public float mouseYMove;

    int dropOdds;

    float bulletVolume;
    Sound shoot0;
    Sound shoot1;
    Sound shoot2;
    Sound shoot3;
    Sound shoot4;
    Sound shoot5;
    Sound shoot6;

    float musicVolume;
    Sound bgm;
    SoundChannel stopMusic;

    int finalScore;

    bool disableSpawning;
    bool ended;
    bool disableGame;
    public StageController(MyGame _game, string stageFile, string musicFile)
    {
        game = _game;
        shotCoolDown = 550;
        shotTimer = 0;
        stage1 = new Stage1(stageFile);
        AddChild(stage1);
        shadows = new Shadows();
        AddChild(shadows);
        player = new Player(this, 5, 1f);
        AddChild(player);
        /*
        Vec2 enemyPos = new Vec2(800, 800);
        enemy = new Enemy(player, (enemyPos), 1);
        AddChild(enemy);
        */
        enemyTimerLimit = 10000f;
        enemyNumMin = 1;
        enemyNumMax = 2;
        SpawnEnemy();

        bulletVolume = 0.2f;
        shoot0 = new Sound("sounds/shoot0.wav");
        shoot1 = new Sound("sounds/shoot1.wav");
        shoot2 = new Sound("sounds/shoot2.wav");
        shoot3 = new Sound("sounds/shoot3.wav");
        shoot4 = new Sound("sounds/shoot4.wav");
        shoot5 = new Sound("sounds/shoot5.wav");
        shoot6 = new Sound("sounds/shoot6.wav");

        dropOdds = 5;

        enemyLevel = 1;


        musicVolume = 0.1f;
        bgm = new Sound(musicFile, true, true); //sorry, the music cuts out, I couldn't find a audio priority function in GXP quickly enough

        stopMusic = new SoundChannel(0);
        stopMusic = bgm.Play(false, 0, musicVolume);

        ended = false;
        disableGame = false;
        disableSpawning = false;
    }

    void Update()
    {

        if (!disableGame)
        {
            mouseXMove = oldMouseX - Input.mouseX;
            mouseYMove = oldMouseY - Input.mouseY;
            EnemyHandling();

            SpawnBullet();

            oldMouseX = Input.mouseX;
            oldMouseY = Input.mouseY;

            CheckHealth();
        }
        if (disableGame && !ended)
        {
            EndGame();
        }

    }
    
    void EndGame()
    {
        ended = true;
        EndScreen endScreen = new EndScreen(game.highScore, finalScore, game);
        game.AddChild(endScreen);
    }
    void CheckHealth()
    {
        if (player.getHealth() <= 0)
        {
            foreach (GameObject obj in GetChildren())
            {
                stopMusic.Stop();
                finalScore = player.getScore();
                obj.LateDestroy();
                disableGame = true;
                game.ShowMouse(true);
                game.cursorX = -20;
                game.cursorY = -20;
            }
        }
    }
    void SpawnBullet()
    {
        if ((Input.GetMouseButton(0) || Input.GetKey(Key.E)) && shotTimer >= shotCoolDown)
        {
            Bullet bullet = new Bullet(player.x, player.y, (game.cursorX - player.x) + player.velocity.x * player.speed, (game.cursorY - player.y) + player.velocity.y * player.speed, 8);
            AddChild(bullet);
            shotTimer = 0;

            switch (Utils.Random(0,7))
            {
                case 0: shoot0.Play(false, 0, bulletVolume); break;
                case 1: shoot1.Play(false, 0, bulletVolume); break;
                case 2: shoot2.Play(false, 0, bulletVolume); break;
                case 3: shoot3.Play(false, 0, bulletVolume); break;
                case 4: shoot4.Play(false, 0, bulletVolume); break;
                case 5: shoot5.Play(false, 0, bulletVolume); break;
                case 6: shoot6.Play(false, 0, bulletVolume); break;
            }
        }
        if (shotTimer <= shotCoolDown)
        {
            shotTimer += Time.deltaTime;
        }
    }

    public void UpReload()
    {
        shotCoolDown -= 40;
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < Utils.Random(enemyNumMin, enemyNumMax); i++)
        {
            int _x = 0;
            int _y = 0;
            
            switch (Utils.Random(0, 4))
            {
                case 0:
                    _x = Utils.Random(-64, game.width + 64);
                    _y = Utils.Random(game.height + 64, game.height + 128);

                    break;
                case 1:
                    _x = Utils.Random(-64, game.width + 64);
                    _y = Utils.Random(-192, -128);
                    break;
                case 2:
                    _x = Utils.Random(-128, -64);
                    _y = Utils.Random(-64, game.height + 64);
                    break;
                case 3:
                    _x = Utils.Random(game.width + 64, game.width + 128);
                    _y = Utils.Random(-64, game.height + 64);
                    break;
            }
            if (Mathf.Abs(player.x -  _x) > 300 && Mathf.Abs(player.y - _y) > 300)
            {
                Vec2 enemyPos = new Vec2(_x, _y);
                int spawnEnemy = Utils.Random(0, enemyLevel);
                switch (spawnEnemy) //enemy spawn handling, the last 2 digits are the speed and health, second one being health first one being speed
                {
                    case 0: //red alien spawn
                        RedStar redStar = new RedStar(player, enemyPos, this, Utils.Random(2f, 3.5f), Utils.Random(3, 6));
                        AddChild(redStar);
                        break;
                    case 1:
                        Tumbleweed tumbleweed = new Tumbleweed(player, enemyPos, this, Utils.Random(2f, 4f), Utils.Random(2, 5));
                        AddChild(tumbleweed);
                        break;
                    case 2:
                        Frog frog = new Frog(player, enemyPos, this, 3f, Utils.Random(3, 6));
                        AddChild(frog);
                        break;
                    case 3:
                        Lizard lizard = new Lizard(player, enemyPos, this, Utils.Random(3f, 7f), Utils.Random(2, 4));
                        AddChild(lizard);
                        break;
                }
            }
        }
    }
    void EnemyHandling()
    {
        enemyTimer += Time.deltaTime;
        EnemyLevelManager();
        int enemiesLeft = 0;
        foreach (GameObject obj in GetChildren())
        {
            if (obj is Enemy)
            {
                enemiesLeft++;
            }
        }
        if (enemiesLeft < 2)
        {
            enemyTimer = 0;
            SpawnEnemy();
            enemyTimerLimit -= 0.01f * enemyTimerLimit;
            enemyNumMin += 0.1f;
            enemyNumMax += 0.2f;
        }
        if (enemiesLeft > 65)
        {
            disableSpawning = true;
        }
        else
        {
            disableSpawning = false;
        }

        if (enemyTimer > enemyTimerLimit && !disableSpawning)
        {
            enemyTimer = 0;
            SpawnEnemy();
            enemyTimerLimit -= 0.01f * enemyTimerLimit;
            enemyNumMin += 0.1f;
            enemyNumMax += 0.2f;
        }
    }
    void EnemyLevelManager()
    {
        if (enemyLevel != 4)
        {
            if (player.getScore() > 400)
            {
                enemyLevel = 4;
            }
            else if (player.getScore() > 150)
            {
                enemyLevel = 3;
            }
            else if (player.getScore() > 50)
            {
                enemyLevel = 2;
            }
        }
    }

    public void IncreaseDrop()
    {
        if (dropOdds != 0)
        {
            dropOdds--;
        }
    }
    public void ResetDrop()
    {
        dropOdds = 12;
    }
    public int getDropOdds() { return dropOdds; }
    public float getShotCoolDown() { return shotCoolDown; }
    public float getshotTimer() { return shotTimer; }
}
