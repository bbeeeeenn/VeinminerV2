using Microsoft.Xna.Framework;
using TShockAPI;

namespace VeinminerV2.Models;

public class Vein
{
    public class State
    {
        public int tick;
        public bool canDrop;

        public State(int tick, bool canDrop)
        {
            this.tick = tick;
            this.canDrop = canDrop;
        }
    }

    public TSPlayer owner;
    public Dictionary<Point, State> tilePoints;
    public int dropNetId;
    public int dropStack;

    public Vein(TSPlayer owner, int dropNetId)
    {
        this.owner = owner;
        tilePoints = new();
        this.dropNetId = dropNetId;
        dropStack = 0;
    }
}
