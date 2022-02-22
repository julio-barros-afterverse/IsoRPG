using System.Collections;

namespace MainGameState
{
    public abstract class MainGameState
    {
        public virtual IEnumerator OnEnter()
        {
            yield break;
        }
        public virtual IEnumerator OnExit()
        {
            yield break;
        }
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