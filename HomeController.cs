using Microsoft.AspNetCore.Mvc;
using WebsiteQLHDTN.Models;
using WebsiteQLHDTN.ViewModels;

namespace WebsiteQLHDTN.Controllers
{
    public class HomeController : Controller
    {

        QlhdtnContext db= new QlhdtnContext();
        private readonly QlhdtnContext _context;
        public IActionResult Trangchu()
        {
            List<ViewModels.thongtinHoatdong> listHoatdong = (from hoatdong in db.Hoatdongs
                                join mota in db.MotaHds on hoatdong.Idhoatdong equals mota.Idhoatdong
                                where hoatdong.Trangthai==0
                                select new ViewModels.thongtinHoatdong
                                {
                                    Idhoatdong1 = hoatdong.Idhoatdong,
                                    Tenhoatdong1 = mota.Tenhoatdong,
                                    MuctieuHd1 = mota.MuctieuHd,
                                }).ToList();
            return View(listHoatdong);
        }
        public IActionResult Gioithieu()
        {
            return View();
        }
        public IActionResult thongtinToChuc()
        {
            return View();
        }
        public IActionResult chitiet(int id)
        {
            var tenban = db.YeucauTnvs.Where(n=>n.Idhoatdong==id)
                .Select(n => n.Tenban).ToList();
            var ketqua = (from hoatdong in db.Hoatdongs
                          join mota in db.MotaHds on hoatdong.Idhoatdong equals mota.Idhoatdong
                          join linhvuc in db.Linhvucs on mota.Linhvuc equals linhvuc.Idlinhvuc
                          where hoatdong.Idhoatdong == id
                          select new ViewModels.thongtinHoatdong
                          {
                              Idhoatdong1 = hoatdong.Idhoatdong,
                              Tenhoatdong1 = mota.Tenhoatdong,
                              Thoigianbatdau1 = mota.Thoigianbatdau,
                              Thoigiaketthuc1 = mota.Thoigiaketthuc,
                              DiaDiem1 = mota.DiaDiem,
                              MuctieuHd1 = mota.MuctieuHd,
                              Linhvuc11 = linhvuc.Linhvuc1,
                              Sltnvcan1 = hoatdong.Sltnvcan,
                              Tenban1 = tenban
                          }).FirstOrDefault();
            return View(ketqua);
        }
    }
}