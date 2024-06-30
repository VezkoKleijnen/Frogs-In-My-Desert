using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Cursor : Sprite
    {
        StageController controller;
        MyGame game;
        public Cursor(StageController _controller, MyGame _game) : base ("cursor.png", false, false)
        {
            SetOrigin (8, 8);
            controller = _controller;
            game = _game;
            scale = 1.5f;
        }

        void Update()
        {
            
            x = Mathf.Clamp(x - controller.mouseXMove, 0, game.width); 
            y = Mathf.Clamp(y - controller.mouseYMove, 0, game.height);
            game.cursorX = x;
            game.cursorY = y;
            
            //enable the code above and disable the code underneath if you want compatibility with fullscreen
            /*
            x = Input.mouseX;
            y = Input.mouseY;
            game.cursorX = x; 
            game.cursorY = y;
            */
        }
    }
}
