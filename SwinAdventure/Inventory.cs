using System;
using System.Text;

namespace SwinAdventure
{
	public class Inventory
	{
		private List<Item> _items;

		public Inventory()
		{
			_items = new();
		}

		public bool HasItem(string id)
		{
			foreach (Item item in _items)
			{
				return (item.AreYou(id));
			}

			return false;
		}

		public void Put(Item itm)
		{
			_items.Add(itm);
		}

		public Item Take(string id)
		{
			foreach (Item item in _items)
			{
				if(item.AreYou(id))
				{
                    _items.Remove(item);
                    return item;
				}
			}

			return null;
		}

		public Item Fetch(string id)
		{
			foreach (Item item in _items)
			{
				if(item.AreYou(id))
				{
					return item;
				}
			}

			return null;
		}

		public string ItemList
		{
			get
			{
				StringBuilder itemList = new StringBuilder();

				foreach (Item item in _items)
				{
					itemList.AppendLine("\t" + item.ShortDescription);
				}

				return itemList.ToString();
			}
		}
	}
}

