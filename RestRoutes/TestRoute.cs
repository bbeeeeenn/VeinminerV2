using Rests;
using Terraria;

namespace Template.RestRoutes;

public class TestRoute : Models.RestRoute
{
    public override string Path { get; set; } = "/test";

    public override object Callback(RestRequestArgs args)
    {
        return new { Message = $"Greetings from {Main.worldName}!" };
    }
}
