using HotelBooking.Core.Models;
using HotelBooking.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class HotelsController : Controller
    {
        private readonly ApplicationDbContext data;

        public HotelsController(ApplicationDbContext data)
        {
            this.data = data;
        }

        //public IActionResult All()
        //{
        //    var result = this.data.
        //                     .Select(h => new HotelCardViewModel
        //                     {
        //                         Id = h.Id,
        //                         HotelName = h.HotelName,
        //                         PrimaryImageUrl = h.PrimaryImageUrl
        //                     })
        //                     .ToList();

        //    return this.View(result);
        //}
    }
}