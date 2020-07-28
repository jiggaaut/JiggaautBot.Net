using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace JiggaautBotv3
{
    class Program
    {
        private static ITelegramBotClient botClient;
        static void Main()
        {
            botClient = new TelegramBotClient("896718256:AAEgsxXzo09I6nQB-7On8FuEBlCTO8ZyHCI");
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Bot id: {me.Id}, Bot name: {me.FirstName}");
            botClient.OnMessage += BotOnMessage;
            botClient.StartReceiving();
            Console.ReadLine();
            botClient.StopReceiving();
        }

        private static async void BotOnMessage(object sender, MessageEventArgs e)
        {           
            var text = e?.Message?.Text;
            if (text == null || text == "/start")
                return;
            if (text == "/hello")
            {
                Message message = await botClient.SendTextMessageAsync(chatId: e.Message.Chat, 
                    text: "Приветики из ASP.NET!");
            }
            else
            {
                Message message = await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Использованы все параметры сообщений",
                    parseMode: ParseMode.Markdown,
                    disableNotification: true,
                    replyToMessageId: e.Message.MessageId,
                    replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(
                        "Работаем..",
                        "https://github.com/jiggaaut/"
                    ))
                );
            }
            
            Console.WriteLine($"Text: {text} in chat {e.Message.Chat.Id}");

            //await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: $"Echo: {text}").ConfigureAwait(false);
        }
    }
}
