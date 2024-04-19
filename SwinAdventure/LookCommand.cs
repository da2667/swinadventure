using System;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace SwinAdventure
{
	public class LookCommand : Command
	{
		public LookCommand(string[] ids): base(ids)
		{
		}

		public override string Execute(Player p, string[] text)
		{
			if (text[0] != "look")
			{
				return "Error in look input";
			}
            if (text[1] != "at" )
            {
                return "What do you want to look at?";
            }
            if (text.Length == 3)
            {
                return LookAtIn(text[2], p);
            }
            if (text.Length == 5)
			{
				if(FetchContainer(p, text[4]) != null)
				{
                    return LookAtIn(text[2], FetchContainer(p, text[4]));
                }
				else
				{
					return "I can't find the " + text[4];
				}	
			}
			else
			{
				return "I don't know how to look like that";
			}
			
		}

        private IHaveInventory FetchContainer(Player p, string containerId)
        {
            return p.Locate(containerId) as IHaveInventory;
        }

        private string LookAtIn(string thingId, IHaveInventory container)
        {
			GameObject locatedItem = container.Locate(thingId);

			if(locatedItem != null && container != null)
			{
                return locatedItem.FullDescription;
            }
			else if(locatedItem == null)
			{
				return "I can't find the " + thingId;
			}
			else
			{
				return "I can't find the " + thingId + " in the " + container.Name;
			}	
        }
    }
}

