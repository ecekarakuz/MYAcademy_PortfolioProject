using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;


namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolioContext _context;

        public MessageController(PortfolioContext context)
        {
            _context = context;
        }

        // Liste: en yeni üstte, gönderen veya konuya göre arama
        public IActionResult MessageList(string? search, int? openId)
        {
            ViewBag.OpenId = openId;
            var query = _context.Messages.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(m => m.SenderName.Contains(search) || m.Subject.Contains(search));
            }

            var values = query.OrderByDescending(m => m.SendDate).ToList();

            ViewBag.Search = search;
            ViewBag.TotalCount = _context.Messages.Count();
            ViewBag.UnreadCount = _context.Messages.Count(m => !m.IsRead);
            return View(values);
        }

        // Modal için detay: sayfa yenilenmeden JSON döner ve mesajı okundu yapar
        [HttpPost]
        public IActionResult GetDetail(int id)
        {
            var message = _context.Messages.Find(id);
            if (message == null) return NotFound();

            bool wasUnread = !message.IsRead;
            if (wasUnread)
            {
                message.IsRead = true;
                _context.SaveChanges();
            }

            return Json(new
            {
                id = message.MessageId,
                senderName = message.SenderName,
                senderEmail = message.SenderEmail,
                subject = message.Subject,
                messageDetail = message.MessageDetail,
                sendDate = message.SendDate.ToString("dd.MM.yyyy HH:mm"),
                wasUnread
            });
        }

        // Okundu <-> Okunmadı değiştir
        [HttpPost]
        public IActionResult ToggleRead(int id)
        {
            var message = _context.Messages.Find(id);
            if (message == null) return NotFound();

            message.IsRead = !message.IsRead;
            _context.SaveChanges();

            TempData["Success"] = message.IsRead ? "Mesaj okundu olarak işaretlendi." : "Mesaj okunmadı olarak işaretlendi.";
            return RedirectToAction("MessageList");
        }

        [HttpPost]
        public IActionResult MarkAllRead()
        {
            var unread = _context.Messages.Where(m => !m.IsRead).ToList();
            foreach (var message in unread)
            {
                message.IsRead = true;
            }
            _context.SaveChanges();

            TempData["Success"] = "Tüm mesajlar okundu olarak işaretlendi.";
            return RedirectToAction("MessageList");
        }

        [HttpPost]
        public IActionResult DeleteMessage(int id)
        {
            var message = _context.Messages.Find(id);
            if (message == null) return NotFound();

            _context.Messages.Remove(message);
            _context.SaveChanges();

            TempData["Success"] = "Mesaj silindi.";
            return RedirectToAction("MessageList");
        }
    }
}