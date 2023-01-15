using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MemesFinderGreeter.Extensions
{
	public static class TgUpdateExtensions
	{
		public static Chat GetChat(this Update tgUpdate) => tgUpdate?.Type switch
		{
			UpdateType.Message => tgUpdate.Message.Chat,
			UpdateType.EditedMessage => tgUpdate.EditedMessage.Chat,
			_ => null
		};
	}
}

