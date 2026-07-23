using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Project3;

public class Game1 : Game
{
    private SpriteBatch _spriteBatch;
    private RenderTarget2D _renderTarget;
    
    public Game1()
    {
        _ = new GraphicsDeviceManager(this);
    }

    protected override void Initialize()
    {
        _renderTarget = new RenderTarget2D(
            graphicsDevice: GraphicsDevice,
            width: 200,
            height: 200,
            mipMap: true,
            preferredFormat: SurfaceFormat.Color,
            preferredDepthFormat: DepthFormat.None,
            preferredMultiSampleCount: 0,
            usage: RenderTargetUsage.PreserveContents
        );

        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(Color.Black);

        //var data = new Color[200 * 200];
        //s_renderTarget.GetData(data);

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
        _spriteBatch.Draw(_renderTarget, Vector2.Zero, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
