using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.Interfaces;

namespace YoukaiKingdom.Inventory
{
    public abstract class Item: IItem
    {
        private Item _item;

        public Item item
        {
            get { return this._item; }
            set { this._item = value; }
        }

    }
}
