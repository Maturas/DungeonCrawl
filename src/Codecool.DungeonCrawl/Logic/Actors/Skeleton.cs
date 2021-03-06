using System;
using Codecool.DungeonCrawl.Logic.Interfaces;
using Codecool.DungeonCrawl.Logic.Map;

namespace Codecool.DungeonCrawl.Logic.Actors
{
    /// <summary>
    ///     Sample enemy
    /// </summary>
    public class Skeleton : Actor, IUpdatable
    {
        private static readonly Random _random = new Random();
        private float _timeLastMove;

        public Skeleton(Cell cell) : base(cell, TileSet.GetTile(TileType.Skeleton))
        {
            Program.AllUpdatables.Add(this);
        }

        ~Skeleton()
        {
            Program.AllUpdatables.Remove(this);
        }

        public void Update(float deltaTime)
        {
            _timeLastMove += deltaTime;

            if (_timeLastMove <= 0.25f)
                return;

            _timeLastMove = 0.0f;

            var moveX = _random.Next(-1, 2);
            var moveY = _random.Next(-1, 2);

            if (moveX == 0 && moveY == 0)
                return;

            var dir = (moveX, moveY).ToDirection();

            TryMove(dir);
        }

        private void TryMove(Direction dir)
        {
            var targetCell = Cell.GetNeighbour(dir);
            var canPass = targetCell?.OnCollision(this) ?? false;

            if (canPass)
                AssignCell(targetCell);
        }

        public override bool OnCollision(Actor other)
        {
            // TODO receive damage logic
            return false;
        }
    }
}