using System;
using System.Linq;

namespace SwinAdventure
{
	public class IdentifiableObject
	{
		private List<string> _identifiers = new();

		public IdentifiableObject(string[] idents)
		{
            foreach(string id in idents)
			{
                AddIdentifier(id);
            }
		}

		public bool AreYou(string id)
		{
			return _identifiers.Contains(id.ToLower());
			
		}

		public string FirstId
		{
			get
			{
				if(_identifiers.Count > 0)
				{
                    return _identifiers[0];
                }
				else
				{
					return "";
				}
				
			}
		}

		public void AddIdentifier(string id)
		{
            _identifiers.Add(id.ToLower());
		}
	}
}

