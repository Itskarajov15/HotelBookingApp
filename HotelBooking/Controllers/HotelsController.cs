using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Hotels;
using HotelBooking.Core.Models.Rooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class HotelsController : BaseController
    {
        private readonly IHotelService hotelService;
        private readonly IRoomService roomService;

        public HotelsController(IHotelService service, IRoomService roomService)
        {
            this.hotelService = service;
            this.roomService = roomService;
        }

        public IActionResult All(string searchString)
        {
            IEnumerable<HotelCardViewModel> hotels;

            if (String.IsNullOrWhiteSpace(searchString))
            {
                hotels = hotelService.GetHotels();
            }
            else
            {
                hotels = hotelService.GetHotels(searchString);
            }

            return this.View(hotels);
        }

        public IActionResult Details(int id)
        {
            var hotel = hotelService.GetHotel(id);

            if (hotel == null)
            {
                return RedirectToAction("All", "Hotels");
            }

            FilterRoomsViewModel filterModel = new FilterRoomsViewModel();

            if (TempData["CityId"] != null)
            {
                filterModel.StartDate = DateTime.Parse(TempData["StartDate"].ToString());
                filterModel.EndDate = DateTime.Parse(TempData["EndDate"].ToString());
                filterModel.CountOfPeople = (int)TempData["CountOfPeople"];
                filterModel.CityId = (int)TempData["CityId"];
            }

            this.ViewBag.Rooms = this.roomService.GetAllRoomsByHotel(id, filterModel);

            return this.View(hotel);
        }

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var hotels = this.hotelService.GetHotels()
                             .Where(h => h.Name.ToLower().StartsWith(prefix.ToLower()))
                             .Select(h => new
                             {
                                 label = h.Name,
                                 val = h.Name
                             })
                             .ToList();

            return Json(hotels);
        }
    }
}