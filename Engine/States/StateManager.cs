using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Manages the states of the game.
/// </summary>
public static class StateManager
{
    private static readonly Dictionary<string, State> _states = [];
    private static State _currentState;
    private static string _currentStateName;

    public static State CurrentState => _currentState;
    public static string CurrentStateName => _currentStateName;

    /// <summary>
    /// Adds a new state to the manager.
    /// </summary>
    /// <param name="stateName">The name of the state.</param>
    /// <param name="state">The state instance.</param>
    public static void AddState(string stateName, State state)
    {
        if (_states.TryAdd(stateName, state))
            return;

        throw new ArgumentException($"State '{stateName}' already exists in the manager");
    }

    /// <summary>
    /// Changes the current state to the specified state.
    /// </summary>
    /// <param name="stateName">The name of the state to change to.</param>
    public  static void ChangeState(string stateName)
    {
        if (!_states.TryGetValue(stateName, out State value))
            throw new ArgumentException($"State '{stateName}' does not exist in the manager");

        _currentState?.UnloadContent();

        _currentStateName = stateName;
        _currentState = value;

        _currentState.Initialize();
        _currentState.LoadContent();
    }

    public static void Update(GameTime gameTime)
    {
        _currentState?.Update(gameTime);
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        _currentState?.Draw(spriteBatch);
    }
}