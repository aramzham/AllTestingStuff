﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TelegramBotMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "My telega bot";
        }
    }
}