using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

internal class Stage1 : GameObject
{
    public Stage1(string file) 
    {
        TiledLoader loader = new TiledLoader(file);
        loader.autoInstance = true;
        loader.rootObject = this;
        loader.addColliders = false;
        loader.LoadImageLayers(0);
        loader.LoadTileLayers(0);
        loader.LoadTileLayers(1);
        loader.LoadObjectGroups(0);
    }
}

