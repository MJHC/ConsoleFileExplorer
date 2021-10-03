using System.Collections.Generic;

namespace NiceMenu
{
    public interface IMenuItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCloseButton { get;}
        public void Select();
        
    }
}