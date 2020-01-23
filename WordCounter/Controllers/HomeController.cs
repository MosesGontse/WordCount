using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WordCounter.Models;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace WordCounter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           var words = GetWordFrequency("wwwroot/txt/war-and-peace.txt");
            return View(words);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private Dictionary<string, int> GetWordFrequency(string file)
        {
            var dictionary = System.IO.File.ReadLines(file) //Selected all the words from file
                       .SelectMany(x => x.Split()) //Split string into a list of words
                       .Where(x => x != string.Empty) //Exclude empty strings
                       .GroupBy(x => RemoveSpecialCharacters(x.Trim().ToLower())) //Group all the words together and remove special chars                        
                       .ToDictionary(x => x.Key, x => x.Count()); //Add each word and its number of Occurrences

            return dictionary;
        }
        public string RemoveSpecialCharacters(string str)
        {
            return  Regex.Replace(str, @"[^0-9a-zA-Z]+", "");
        }
    }
}
