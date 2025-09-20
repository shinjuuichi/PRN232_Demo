using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class UsersController(IUserApiClient _userApiClient) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var list = await _userApiClient.GetAll();
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userApiClient.GetById(id.Value);
            if (user == null) return NotFound();

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                await _userApiClient.Create(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userApiClient.GetById(id.Value);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email")] User user)
        {
            if (id != user.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _userApiClient.Update(id, user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userApiClient.GetById(id.Value);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userApiClient.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
