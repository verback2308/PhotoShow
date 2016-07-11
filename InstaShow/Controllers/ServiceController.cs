using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using InstaSharp;
using InstaSharp.Models;
using InstaSharp.Models.Responses;
using Core.Models;

namespace InstaShow.Controllers
{
    public class ServiceController : Controller
    {
        public ActionResult Login()
        {
            var config = new InstagramConfig("d6c569c43fd94fb59c0069e176dff7c5", "9752eaf10f5f42cea37210b8ade4e0b3", "http://localhost:12888/service/Index");
            var scopes = new List<OAuth.Scope>();
            scopes.Add(InstaSharp.OAuth.Scope.Basic);
            scopes.Add(InstaSharp.OAuth.Scope.Public_Content);
            var link = InstaSharp.OAuth.AuthLink(config.OAuthUri + "authorize", config.ClientId, config.RedirectUri, scopes, InstaSharp.OAuth.ResponseType.Token);
            return Redirect(link);
        }
        public async Task<ActionResult> Index()
        {
            var config = new InstagramConfig("d6c569c43fd94fb59c0069e176dff7c5", "9752eaf10f5f42cea37210b8ade4e0b3", "http://localhost:12888/service/Index");
            var oaresponse = new OAuthResponse();
            oaresponse.AccessToken = "951704014.d6c569c.2b1b3f048cef474da358cbeb7b4f9658";
            oaresponse.User = new User();
            var tagsMedia = new InstaSharp.Endpoints.Tags(config, oaresponse).Recent("testformyownproject", String.Empty, String.Empty, 3);
            var response = await tagsMedia;
            var feedList = response.Data.Select(gram => new Feed
            {
                PhotoUrl = gram.Images.StandardResolution.Url, Description = gram.Caption.Text.Replace("🏻", "") , Likes = gram.Likes.Count, Author = "@"+gram.Caption.From.Username
            }).ToList();
            return View(feedList);
        }
        public async Task<ActionResult> Refresh()
        {
            var config = new InstagramConfig("d6c569c43fd94fb59c0069e176dff7c5", "9752eaf10f5f42cea37210b8ade4e0b3", "http://localhost:12888/service/Index");
            var oaresponse = new OAuthResponse();
            oaresponse.AccessToken = "951704014.d6c569c.2b1b3f048cef474da358cbeb7b4f9658";
            oaresponse.User = new User();
            var tagsMedia = new InstaSharp.Endpoints.Tags(config, oaresponse).Recent("testformyownproject");
            var response = await tagsMedia;
            var feedList = response.Data.Select(gram => new Feed
            {
                PhotoUrl = gram.Images.StandardResolution.Url,
                Description = gram.Caption.Text,
                Likes = gram.Likes.Count
            }).ToList();
            return Json(feedList, JsonRequestBehavior.AllowGet);
        }
    }
}