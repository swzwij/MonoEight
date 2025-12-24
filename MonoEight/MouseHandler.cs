using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class MouseHandler
{
    private MouseState _mouse;
    private MouseState _lastMouse;

    public Point Position
    {
        get
        {
            Rectangle displayRect = GraphicsHelper.CalculateDisplayRect(MEWindow.Graphics.GraphicsDevice);

            int mouseX = _mouse.X;
            int mouseY = _mouse.Y;

            if (!displayRect.Contains(mouseX, mouseY))
            {
                return new Point(-1, -1);
            }

            float relativeX = (mouseX - displayRect.X) / (float)displayRect.Width;
            float relativeY = (mouseY - displayRect.Y) / (float)displayRect.Height;

            int gameX = (int)(relativeX * MEWindow.Resolution.X);
            int gameY = (int)(relativeY * MEWindow.Resolution.Y);

            return new Point(gameX, gameY);
        }
    }

    public Vector2 _worldPosition;

    public Vector2 WorldPosition
    {
        get
        {
            Point screenPos = Position;

            if (screenPos.X < 0 || screenPos.Y < 0)
                return _worldPosition;

            if (SceneManager.ActiveScene != null)
            {
                Vector2 resolutionOffset = new Vector2(MEWindow.Resolution.X / 2, MEWindow.Resolution.Y / 2);
                Vector2 cameraOffset = SceneManager.ActiveScene.Camera.Position - resolutionOffset;

                return new Vector2(screenPos.X, screenPos.Y) + cameraOffset;
            }

            _worldPosition = new Vector2(screenPos.X, screenPos.Y);
            return _worldPosition;
        }
    }

    public bool LeftDown => _mouse.LeftButton == ButtonState.Pressed;
    public bool LeftUp => _mouse.LeftButton == ButtonState.Released;
    public bool LeftPressed => _mouse.LeftButton == ButtonState.Pressed && _lastMouse.LeftButton == ButtonState.Released;
    public bool LeftReleased => _mouse.LeftButton == ButtonState.Released && _lastMouse.LeftButton == ButtonState.Pressed;

    public bool RightDown => _mouse.RightButton == ButtonState.Pressed;
    public bool RightUp => _mouse.RightButton == ButtonState.Released;
    public bool RightPressed => _mouse.RightButton == ButtonState.Pressed && _lastMouse.RightButton == ButtonState.Released;
    public bool RightReleased => _mouse.RightButton == ButtonState.Released && _lastMouse.RightButton == ButtonState.Pressed;

    public void Update()
    {
        _lastMouse = _mouse;
        _mouse = Mouse.GetState();
    }
}