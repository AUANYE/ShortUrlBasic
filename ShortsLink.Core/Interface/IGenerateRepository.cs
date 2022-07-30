using System;
using ShortsLink.Core.Model;

namespace ShortsLink.Core.Interface
{
    public interface IGenerateRepository
    {
        GenResultModel GenShortURL(PostURLModel postURLModel);
        string GetOriginalUrl(string shortUrl);
    }
}