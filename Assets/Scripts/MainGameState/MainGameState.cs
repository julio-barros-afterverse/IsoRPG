using UnityEngine;

namespace MainGameState
{
    public abstract class MainGameState
    {
        public virtual void OnEnter() {}
        public virtual void OnExit() {}
        public virtual void OnHoverTile(TileSystem tile)
        {
        }
        
        public virtual void OnUnhoverTile(TileSystem tile)
        {
        }
        
        public virtual void OnSelectTile(TileSystem tile)
        {
        }
    }
}