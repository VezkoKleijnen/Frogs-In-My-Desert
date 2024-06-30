using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.IO;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {
    Cursor cursor;
	public float cursorX;
	public float cursorY;
	LevelSelect mainMenu;
	public int highScore;

    public MyGame() : base(1920, 1017, false)     
	{
		mainMenu = new LevelSelect(this);
		AddChild(mainMenu);
		highScore = 0;
        /*

		*/

        // create reader & open file
        TextReader tr = new StreamReader("SaveScore.txt");

        string finalScore = tr.ReadLine();

        highScore = Convert.ToInt32(finalScore);

        tr.Close();
    }

	void Update() {

	}

	public void StartStage(string levelFile, string musicFile)
	{
        foreach (GameObject obj in GetChildren())
        {
            obj.LateDestroy();
        }
        ShowMouse(false);
        StageController stageController = new StageController(this, levelFile, musicFile);
        AddChild(stageController);
        GUI gui = new GUI(stageController);
        AddChild(gui);
        cursor = new Cursor(stageController, this);
        AddChild(cursor);
    }

	public void ClearGame()
	{
        foreach (GameObject obj in GetChildren())
        {
            obj.LateDestroy();
        }
        mainMenu = new LevelSelect(this);
        AddChild(mainMenu);
    }

    public void UpdateHighScore(int _highScore)
    {
        highScore = _highScore;
    }

	static void Main()                          
	{
		new MyGame().Start();
	}
}