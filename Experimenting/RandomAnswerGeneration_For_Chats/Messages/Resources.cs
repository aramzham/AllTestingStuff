using System;
using System.Collections.Generic;
using System.Linq;
using MersenneTwister;

namespace RandomAnswerGeneration_For_Chats.Messages
{
    public class Resources
    {
        private static Random _random = MTRandom.Create();

        public static string GetRandomMessage()
        {
            var sectionName = Messages.Keys.ElementAt(_random.Next(0, Messages.Keys.Count));
            var randomSection = Messages[sectionName];
            var randomMessage = randomSection[_random.Next(0, randomSection.Length)];

            return $"{randomMessage} ({sectionName})";
        }

        public static Dictionary<string, string[]> Messages = new()
        {
            {"интрига", new []
            {
                "я кое-что в тебе заметил",
                "опять этот взгляд",
                "давно хотел тебе это сказать",
                "это пиздец :)",
                "в тебе есть черта, которая не нравится мужчинам, но мне кажется милым",
                "не делай так больше",
                "сейчас кое-что будет",
                "тебе это понравится",
                "про тебя слили инфу",
                "я знаю твой секрет",
                "это правда?",
                "сыграем в игру",
                "ты известная в узких кругах"
            }},
            {"испуг", new []
            {
                "тебе пиздец",
                "сдай анализы",
                "не выходи из дома",
                "не выходи в подъезд",
                "звони в морг",
                "за тобой следят",
                "спасай свою жизнь",
                "за тобой идут",
                "тебя ищут 2 чеченца",
                "тебя сегодня выебут",
                "беги",
                "уезжай из города",
                "затаись",
                "смени имя",
                "на тебе кредит(ипотека)",
                "ты ходишь по ахуенно тонкому льду",
                "ты на счетчике",
                "ее убили, ты следующая"
            }}
        };
    }
}