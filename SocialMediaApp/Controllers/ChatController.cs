using SocialMediaApp.Helpers;
using SocialMediaApp.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SocialMediaApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index(int id = -1)
        {
            ViewBag.Chats = ConnectionHelper.Context.UserChats.Where(x => x.UserId == (Guid)Session["ID"]).ToList();


            ViewBag.Chat = ConnectionHelper.Context.Messages.Where(x => x.ChatId == id).ToList();
            ViewBag.Chatter = ConnectionHelper.Context.UserChats.FirstOrDefault(x => x.ChatId == id);

            return View();
        }


        [HttpPost]
        public ActionResult Send(int id, string message)
        {
            Message msg = new Message();
            msg.SenderId = (Guid)Session["ID"];
            msg.MessageText = message;
            msg.ChatId = id;
            msg.DateSend = DateTime.Now;
            ConnectionHelper.Context.Messages.InsertOnSubmit(msg);
            ConnectionHelper.Context.SubmitChanges();
            return RedirectToAction("Index", new { id = msg.ChatId });
        }

        public ActionResult CreateChat(Guid id)
        {
            Chat chat = new Chat();
            ConnectionHelper.Context.Chats.InsertOnSubmit(chat);
            ConnectionHelper.Context.SubmitChanges();
            UserChat userchat = new UserChat();
            userchat.ChatId = chat.id;
            userchat.UserId = (Guid)Session["ID"];
            userchat.ReceiverId = id;
            UserChat recieverchat = new UserChat();
            recieverchat.ChatId = chat.id;
            recieverchat.ReceiverId = (Guid)Session["ID"];
            recieverchat.UserId = id;
            ConnectionHelper.Context.UserChats.InsertOnSubmit(userchat);
            ConnectionHelper.Context.UserChats.InsertOnSubmit(recieverchat);
            ConnectionHelper.Context.SubmitChanges();

            return RedirectToAction("Index");
        }
    }
}