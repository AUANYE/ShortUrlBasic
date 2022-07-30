using System;
using ContextDatabase;
using ContextDatabase.Models;
using ShortsLink.Core.Interface;
using ShortsLink.Core.Model;

namespace ShortsLink.Core.Repository
{
    public class GenerateRepository : IGenerateRepository
    {
        private readonly ShortsLinkContext _context;

        public GenerateRepository(ShortsLinkContext context)
        {
            _context = context;
        }

        public GenResultModel GenShortURL(PostURLModel postURLModel)
        {
            try
            {
                ShortsTbl s = new ShortsTbl();
                s.FullURL = postURLModel.FullURL;
                s.ShortURL = GenerateShortURL(s.FullURL);
                s.ClickCount = 0;
                s.CreateDate = DateTime.Now;
                _context.ShortsTbl.Add(s);
                _context.SaveChanges();

                return new GenResultModel { HostName = postURLModel.HostName, FullURL = s.FullURL, ShortURL = s.ShortURL };
            }
            catch (Exception ex)
            {
                return new GenResultModel { HostName = postURLModel.HostName, FullURL = "", ShortURL = "" };
            }
        }

        private static string GenerateShortURL(string fullUrl)
        {
            string shortUrl = "";
            if (fullUrl != "")
            {
                shortUrl = RandomString(9);
            }
            return shortUrl;
        }

        private static string RandomString(int charecterNum)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringResult = new string(Enumerable.Repeat(chars, charecterNum)
                .Select(w => w[random.Next(w.Length)]).ToArray());
            return stringResult;
        }

        public string GetOriginalUrl(string shortUrl)
        {
            string originalURL = "";
            var data = _context.ShortsTbl.Where(w => w.ShortURL == shortUrl).FirstOrDefault();
            if (data != null)
            {
                originalURL = data.FullURL;
                data.ClickCount++;
                _context.SaveChanges();
            }
            return originalURL;
        }

        private bool CheckDuplicate(string shortUrl)
        {
            var duplicateCount = _context.ShortsTbl.Where(w => w.ShortURL == shortUrl).Count();
            return (duplicateCount == 0 ? false : true);
        }
    }
}

