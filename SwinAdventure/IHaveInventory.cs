using System;
namespace SwinAdventure
{
	public interface IHaveInventory
	{
		public GameObject Locate(string id)
		{
			return this as GameObject;
        }

		public string Name
		{
			get
			{
				return this.Name;
			}
		}
	}
}

