using System;

namespace raptor
{
	/// <summary>
	/// Summary description for Clipboard_Data.
	/// </summary>
	[Serializable]
	public class Clipboard_Data
	{
		public enum kinds { symbols, comment };
		public kinds kind;
		public Component? symbols;
		public CommentBox? cb;
		public System.Guid guid;
		public logging_info log;

		public Clipboard_Data(Component c, System.Guid g, logging_info l)
		{
			this.kind = kinds.symbols;
			symbols = c;
			cb = null;
			guid = g;
			log = l;
		}

		public Clipboard_Data(CommentBox b, System.Guid g)
		{
			this.kind = kinds.comment;
			symbols = null;
			cb = b;
			guid = g;
			log = null;
		}
	}
}
